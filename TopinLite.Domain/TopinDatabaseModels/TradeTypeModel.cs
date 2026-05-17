using System;
using System.Collections.Generic;
using System.Text;

namespace TopinLite.Domain.TopinDatabaseModels
{
    public class TradeTypeModel
    {
        public decimal RechargeType { get; set; }
        public string RechargeDesc { get; set; }
        public decimal OperatorId { get; set; }
        public string OperatorName { get; set; }
    }
}
