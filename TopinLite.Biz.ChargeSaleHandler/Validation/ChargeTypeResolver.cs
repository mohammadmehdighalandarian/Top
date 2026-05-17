using Microsoft.Extensions.Options;

namespace TopinLite.Biz.ChargeSaleHandler.Validation;

public sealed class ChargeTypeRules
{
    public const string SectionName = "ChargeRules";
    public Dictionary<string, string> PayloadMap { get; set; } = new();
    public Dictionary<string, string> ValidateSubjects { get; set; } = new();
}

public interface IChargeTypeResolver
{
    (string ChargeType, string ValidateSubject)? Resolve(string? payloadId);
}

public sealed class ChargeTypeResolver : IChargeTypeResolver
{
    private readonly Dictionary<string, string> _payloadMap;
    private readonly Dictionary<string, string> _subjects;

    public ChargeTypeResolver(IOptions<ChargeTypeRules> options)
    {
        ChargeTypeRules rules = options.Value;

        _payloadMap = new Dictionary<string, string>(rules.PayloadMap, StringComparer.OrdinalIgnoreCase);
        _subjects   = new Dictionary<string, string>(rules.ValidateSubjects, StringComparer.OrdinalIgnoreCase);
    }

    public (string ChargeType, string ValidateSubject)? Resolve(string? payloadId)
    {
        if (string.IsNullOrWhiteSpace(payloadId))
            return null;

        if (!_payloadMap.TryGetValue(payloadId, out string? chargeType))
            return null;

        if (!_subjects.TryGetValue(chargeType, out string? subject))
            return null;

        return (chargeType, subject);
    }
}