using NATS.Client.Core;
using TopinLite.Domain.TopinApi;
using TopinLite.Biz.ChargeHandler.Direct.Validation;

namespace TopinLite.Biz.ChargeHandler.Direct
{
    public class Responder : BackgroundService
    {
        private readonly NatsConnection _nats;
        private readonly IDirectChargeValidationService _validationService;
        private readonly IDirectConfirmOrderService _confirmOrderService;
        private readonly ILogger<Responder> _logger;

        public Responder(
            NatsConnection nats,
            IDirectChargeValidationService validationService,
            IDirectConfirmOrderService confirmOrderService,
            ILogger<Responder> logger)
        {
            _nats = nats;
            _validationService = validationService;
            _confirmOrderService = confirmOrderService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task requestOrderTask = HandleRequestOrderMessagesAsync(stoppingToken);
            Task confirmOrderTask = HandleConfirmOrderMessagesAsync(stoppingToken);

            await Task.WhenAll(requestOrderTask, confirmOrderTask).ConfigureAwait(false);
        }

        private async Task HandleRequestOrderMessagesAsync(CancellationToken stoppingToken)
        {
            await foreach (var msg in _nats.SubscribeAsync<RequestOrderRequestModel>(
                               subject: "TopinLite.Biz.ChargeHandler.Direct.RequestOrder",
                               queueGroup: "TopinLite.Biz.ChargeHandler.Direct.RequestOrder",
                               cancellationToken: stoppingToken))
            {
                RequestOrderRequestModel request = msg.Data ?? new RequestOrderRequestModel();
                ExecResult response = await _validationService.ValidateAsync(request, stoppingToken).ConfigureAwait(false);

                _logger.LogInformation(
                    "Direct requestorder processed broker={BrokerId}, result={ResultCode}",
                    request.BrokerId,
                    response.ResultCode);

                await msg.ReplyAsync(response, cancellationToken: stoppingToken).ConfigureAwait(false);
            }
        }

        private async Task HandleConfirmOrderMessagesAsync(CancellationToken stoppingToken)
        {
            await foreach (var msg in _nats.SubscribeAsync<DirectConfirmOrderRequestModel>(
                               subject: "TopinLite.Biz.ChargeHandler.Direct.ConfirmOrder",
                               queueGroup: "TopinLite.Biz.ChargeHandler.Direct.ConfirmOrder",
                               cancellationToken: stoppingToken))
            {
                DirectConfirmOrderRequestModel request = msg.Data ?? new DirectConfirmOrderRequestModel();
                ExecResult<DirectConfirmOrderResponseModel> response = await _confirmOrderService.ConfirmAsync(request, stoppingToken).ConfigureAwait(false);

                _logger.LogInformation(
                    "Direct confirmorder processed orderId={OrderId}, result={ResultCode}",
                    request.OrderId,
                    response.ResultCode);

                await msg.ReplyAsync(response, cancellationToken: stoppingToken).ConfigureAwait(false);
            }
        }
    }
}
