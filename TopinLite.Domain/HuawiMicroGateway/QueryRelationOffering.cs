using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopinLite.Domain.HuawiMicroGateway
{
    public class QueryRelationOfferingRequestModel : MassTransitRequestBaseModel
    {
    }

    public class QueryRelationOfferingResponseModel : MassTransitResponseBaseModel
    {
    }

    public interface IQueryRelationOfferingRequestModel : IMassTransitRequestBaseModel
    {
    }

    public class QueryRelationOfferingTcpRequest
    {
        public string MessageSeq { get; set; }
        public string MSISDN { get; set; }
        public string RelationType { get; set; }
        public string OfferId { get; set; }
    }
}
