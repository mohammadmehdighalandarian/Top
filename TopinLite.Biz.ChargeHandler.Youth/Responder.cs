using NATS.Client.Core;
using TopinLite.Biz.ChargeHandler.Youth.Validation;
using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.ChargeHandler.Youth
{
    public class Responder : BackgroundService
    {
        private readonly NatsConnection _nats;
        private readonly IYouthChargeValidationService _validationService;
        private readonly IYouthConfirmOrderService _confirmOrderService;
        private readonly ILogger<Responder> _logger;

        public Responder(NatsConnection nats, IYouthChargeValidationService validationService, IYouthConfirmOrderService confirmOrderService, ILogger<Responder> logger)
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
                               subject: "TopinLite.Biz.ChargeHandler.Youth.RequestOrder",
                               queueGroup: "TopinLite.Biz.ChargeHandler.Youth.RequestOrder",
                               cancellationToken: stoppingToken))
            {
                RequestOrderRequestModel request = msg.Data ?? new RequestOrderRequestModel();
                ExecResult result = await _validationService.ValidateAsync(request, stoppingToken).ConfigureAwait(false);

                _logger.LogInformation(
                    "Youth requestorder processed broker={BrokerId}, result={ResultCode}",
                    request.BrokerId,
                    result.ResultCode);

                await msg.ReplyAsync(result, cancellationToken: stoppingToken).ConfigureAwait(false);
            }
        }

        private async Task HandleConfirmOrderMessagesAsync(CancellationToken stoppingToken)
        {
            await foreach (var msg in _nats.SubscribeAsync<YouthConfirmOrderRequestModel>(
                               subject: "TopinLite.Biz.ChargeHandler.Youth.ConfirmOrder",
                               queueGroup: "TopinLite.Biz.ChargeHandler.Youth.ConfirmOrder",
                               cancellationToken: stoppingToken))
            {
                YouthConfirmOrderRequestModel request = msg.Data ?? new YouthConfirmOrderRequestModel();
                ExecResult<YouthConfirmOrderResponseModel> response = await _confirmOrderService.ConfirmAsync(request, stoppingToken).ConfigureAwait(false);

                _logger.LogInformation(
                    "Youth confirmorder processed orderId={OrderId}, result={ResultCode}",
                    request.OrderId,
                    response.ResultCode);

                await msg.ReplyAsync(response, cancellationToken: stoppingToken).ConfigureAwait(false);
            }
        }
    }
}
