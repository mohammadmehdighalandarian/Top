using NATS.Client.Core;

using TopinLite.Biz.ChargeHandler.Loyalty.Validation;
using TopinLite.Domain.TopinApi;

namespace TopinLite.Biz.ChargeHandler.Loyalty
{
    public class Responder : BackgroundService
    {
        private readonly NatsConnection _nats;
        private readonly ILoyaltyChargeValidationService _validationService;
        private readonly ILoyaltyConfirmOrderService _confirmOrderService;
        private readonly ILogger<Responder> _logger;

        public Responder(
            NatsConnection nats,
            ILoyaltyChargeValidationService validationService,
            ILoyaltyConfirmOrderService confirmOrderService,
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
                               subject: "TopinLite.Biz.ChargeHandler.Loyalty.RequestOrder",
                               queueGroup: "TopinLite.Biz.ChargeHandler.Loyalty.RequestOrder",
                               cancellationToken: stoppingToken))
            {
                RequestOrderRequestModel request = msg.Data ?? new RequestOrderRequestModel();
                ExecResult result = await _validationService.ValidateAsync(request, stoppingToken).ConfigureAwait(false);

                _logger.LogInformation(
                    "Loyalty requestorder processed broker={BrokerId}, result={ResultCode}",
                    request.BrokerId,
                    result.ResultCode);

                await msg.ReplyAsync(result, cancellationToken: stoppingToken).ConfigureAwait(false);
            }
        }

        private async Task HandleConfirmOrderMessagesAsync(CancellationToken stoppingToken)
        {
            await foreach (var msg in _nats.SubscribeAsync<LoyaltyConfirmOrderRequestModel>(
                               subject: "TopinLite.Biz.ChargeHandler.Loyalty.ConfirmOrder",
                               queueGroup: "TopinLite.Biz.ChargeHandler.Loyalty.ConfirmOrder",
                               cancellationToken: stoppingToken))
            {
                LoyaltyConfirmOrderRequestModel request = msg.Data ?? new LoyaltyConfirmOrderRequestModel();
                ExecResult<LoyaltyConfirmOrderResponseModel> response = await _confirmOrderService.ConfirmAsync(request, stoppingToken).ConfigureAwait(false);

                _logger.LogInformation(
                    "Loyalty confirmorder processed orderId={OrderId}, result={ResultCode}",
                    request.OrderId,
                    response.ResultCode);

                await msg.ReplyAsync(response, cancellationToken: stoppingToken).ConfigureAwait(false);
            }
        }
    }
}
