using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopinLite.Domain.HuawiMicroGateway
{


    public class DeleteOfferingTcpRequest
    {
        public string PrimaryIdentity { get; set; }
        public string OfferingId { get; set; }
        public string Mss { get; set; }
    }

}
