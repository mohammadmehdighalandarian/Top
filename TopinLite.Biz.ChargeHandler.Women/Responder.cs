using NATS.Client.Core;

using TopinLite.Biz.ChargeHandler.Women.Validation;
using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.ChargeHandler.Women
{
    public class Responder : BackgroundService
    {
        private readonly NatsConnection _nats;
        private readonly IWomenChargeValidationService _validationService;
        private readonly IWomenConfirmOrderService _confirmOrderService;
        private readonly ILogger<Responder> _logger;

        public Responder(
            NatsConnection nats,
            IWomenChargeValidationService validationService,
            IWomenConfirmOrderService confirmOrderService,
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
                               subject: "TopinLite.Biz.ChargeHandler.Women.RequestOrder",
                               queueGroup: "TopinLite.Biz.ChargeHandler.Women.RequestOrder",
                               cancellationToken: stoppingToken))
            {
                RequestOrderRequestModel request = msg.Data ?? new RequestOrderRequestModel();
                ExecResult result = await _validationService.ValidateAsync(request, stoppingToken).ConfigureAwait(false);

                _logger.LogInformation(
                    "Women requestorder processed broker={BrokerId}, result={ResultCode}",
                    request.BrokerId,
                    result.ResultCode);

                await msg.ReplyAsync(result, cancellationToken: stoppingToken).ConfigureAwait(false);
            }
        }

        private async Task HandleConfirmOrderMessagesAsync(CancellationToken stoppingToken)
        {
            await foreach (var msg in _nats.SubscribeAsync<WomenConfirmOrderRequestModel>(
                               subject: "TopinLite.Biz.ChargeHandler.Women.ConfirmOrder",
                               queueGroup: "TopinLite.Biz.ChargeHandler.Women.ConfirmOrder",
                               cancellationToken: stoppingToken))
            {
                WomenConfirmOrderRequestModel request = msg.Data ?? new WomenConfirmOrderRequestModel();
                ExecResult<WomenConfirmOrderResponseModel> response = await _confirmOrderService.ConfirmAsync(request, stoppingToken).ConfigureAwait(false);

                _logger.LogInformation(
                    "Women confirmorder processed orderId={OrderId}, result={ResultCode}",
                    request.OrderId,
                    response.ResultCode);

                await msg.ReplyAsync(response, cancellationToken: stoppingToken).ConfigureAwait(false);
            }
        }
    }
}
