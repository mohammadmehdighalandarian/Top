using System;
using System.Collections.Generic;
using System.Text;

namespace TopinLite.Domain.TopinDatabaseModels
{
    public class SelectedBrokerModel
    {
        public decimal SelectedBrokerId { get; set; }
        public decimal BrokerId { get; set; }
        public decimal OfferId { get; set; }
        public decimal GiftPercent { get; set; }
        public bool Status { get; set; }
    }
}
