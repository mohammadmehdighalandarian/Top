using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopinLite.Domain.Commons
{ 
    // Changed underlying type from bool to int to fix CS1008
    public enum ExecStatus : int
    {
        Success = 1,
        Failed = 0
    }

    ///===========================================================
    ///===========================================================
    ///===========================================================
    
    public static class MemoryDataKeys
    {
        public const string Offers = "Offers:";
        public const string Brokers = "Brokers:";
        public const string BrokersAccess = "BrokersAccess:";
        public const string DynamicCondition = "DynamicCondition:";

    }

}
