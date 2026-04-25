using System;
using System.Collections.Generic;
using System.Text;

using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging
{
    public class BrokersAccessResponseModel : BrokersAccessModel
    {
    }

    public class BrokersAccessRequestModel
    {
        public string MethodName { get; set; }
        public Int64 SapId { get; set; }
    }

}
