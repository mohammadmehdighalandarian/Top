namespace TopinLite.Domain.TopinApi;

public sealed class DirectConfirmOrderRequestModel : ConfirmOrderRequestModel
{
    public string TelCharger { get; set; }
    public decimal FaceAmount { get; set; }
    public decimal ExtraCharge { get; set; }
    public decimal SeqCrm { get; set; }
    public decimal ProviderId { get; set; }
    public string BrokerId { get; set; }
    public decimal ChannelId { get; set; }
    public decimal AccountId { get; set; }
    public decimal SapId { get; set; }
    public decimal TelSponser { get; set; }
}
