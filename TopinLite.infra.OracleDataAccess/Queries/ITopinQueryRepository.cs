using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.infra.OracleDataAccess.Queries
{
    public interface ITopinQueryRepository
    {
        Task<ExecResult<IEnumerable<BrokersModel>>> GetAllBrokers();
        Task<ExecResult<IEnumerable<BrokersAccessModel>>> GetAllBrokersAccess();
        Task<ExecResult<IEnumerable<DynamicConditionModel>>> GetAllDynamicCondition();
        Task<ExecResult<IEnumerable<OffersModel>>> GetAllOffers();
    }
}
