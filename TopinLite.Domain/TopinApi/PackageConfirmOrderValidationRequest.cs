using TopinLite.Domain.Commons;
using TopinLite.Domain.Messaging;

namespace TopinLite.Domain.TopinApi;

public sealed class PackageConfirmOrderValidationRequest
{
    public decimal ProviderId { get; set; }
    public PackageRequestModel PackageRequest { get; set; }
    public PackageConfirmOrderRequest PackageConfirm { get; set; }
}
