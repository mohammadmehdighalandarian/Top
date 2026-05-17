using TopinLite.Domain.Commons;

namespace TopinLite.Services.Commons;

public interface ICommonValidation
{
    //Task<ExecResult> ValidateCallSalePackageAsync(PackageRequestModel request);
    Task<ExecResult> ValidateCallSaleAnarestanAsync(PackageRequestModel request, CancellationToken cancellationToken);
    Task<ExecResult> ValidateBusinessAsync(PackageRequestModel context, CancellationToken cancellationToken);
    Task<ExecResult> ValidateExecBusinessAsync(PackageConfirmOrderValidationRequest request, CancellationToken cancellationToken);

    //Task<ExecResult> ReserveLoyaltyAsync(PackageRequestModel context, decimal providerId);
}
