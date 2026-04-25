using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopinLite.Domain.HuaweiApiModel.CM
{
    public class DeleteSubscriberOfferingRequestModel
    {
        public string PrimaryIdentity { get; set; }

        public string OfferId { get; set; }
        public string MessageSeq { get; set; }
    }
}
