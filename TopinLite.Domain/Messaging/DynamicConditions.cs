using System;
using System.Collections.Generic;
using System.Text;

using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.Domain.Messaging
{
    public class DynamicConditionsResponseModel : DynamicConditionModel
    {

    }


    public class DynamicConditionsRequestModel
    {
        public string Biztype { get; set; }
        public string KeyStr { get; set; }
    }
}
