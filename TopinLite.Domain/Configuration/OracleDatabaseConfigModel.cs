using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopinLite.Domain.Configuration
{
    public class OracleDatabaseConfigModel
    {
        public string HostAddress { get; set; }
        public string Port { get; set; }
        public string ServiceName { get; set; }
        public string Schema { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string GetConnectionString()
        {
            return $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={HostAddress})(PORT={Port}))(CONNECT_DATA=(SERVICE_NAME={ServiceName})));User Id={Username};Password={Password};";
        }

    }
}
