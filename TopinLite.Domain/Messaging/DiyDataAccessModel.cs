using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging;

public class DiyDataAccessResponseModel : DiyDataAccessModel
{
}
public class DiyDataAccessRequestModel
{
    public long BrokerId { get; set; }
    public long OfferId { get; set; }
    public int AttributeType { get; set; }
    public decimal AttributeVal { get; set; }
}
