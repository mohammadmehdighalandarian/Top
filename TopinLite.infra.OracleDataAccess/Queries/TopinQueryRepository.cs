using TopinLite.Domain.Configuration;
using TopinLite.Domain.TopinApi;
using TopinLite.Domain.TopinDatabaseModels;

namespace TopinLite.infra.OracleDataAccess.Queries
{
    public class TopinQueryRepository: ITopinQueryRepository
    {
        private readonly OracleDatabaseConfigModel oracleDatabaseConfig;
        private readonly string OraSchema;
        public TopinQueryRepository(IOptions<OracleDatabaseConfigModel> oracleDatabaseConfigModel)
        {
            oracleDatabaseConfig= oracleDatabaseConfigModel.Value;
            OraSchema= oracleDatabaseConfig.Schema;
        }


        public async Task<ExecResult<IEnumerable<OffersModel>>> GetAllOffers()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT * FROM {OraSchema}.VW_OFFERS";
                    IEnumerable<OffersModel> ResultData = await dbConn.QueryAsync<OffersModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<OffersModel>>
                    {
                        ExecStatus = true,
                        ResultCode = 0,
                        ResultMessage = "Success Execution",
                        Data = ResultData
                    };
                }
            }
            catch (System.Exception Ex)
            {
                return new ExecResult<IEnumerable<OffersModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }

        public async Task<ExecResult<IEnumerable<BrokersModel>>> GetAllBrokers()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT * FROM {OraSchema}.VW_CACHE_BROKERS";
                    IEnumerable<BrokersModel> ResultData = await dbConn.QueryAsync<BrokersModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<BrokersModel>>
                    {
                        ExecStatus = true,
                        ResultCode = 0,
                        ResultMessage = "Success Execution",
                        Data = ResultData
                    };
                }
            }
            catch (System.Exception Ex)
            {
                return new ExecResult<IEnumerable<BrokersModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }


        public async Task<ExecResult<IEnumerable<BrokersAccessModel>>> GetAllBrokersAccess()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT T.EN_NAME AS MethodName,T.FK_SAP_ID AS SapId FROM {OraSchema}.VW_ACCESS_LIST_BROKER T";
                    IEnumerable<BrokersAccessModel> ResultData = await dbConn.QueryAsync<BrokersAccessModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<BrokersAccessModel>>
                    {
                        ExecStatus = true,
                        ResultCode = 0,
                        ResultMessage = "Success Execution",
                        Data = ResultData
                    };
                }
            }
            catch (System.Exception Ex)
            {
                return new ExecResult<IEnumerable<BrokersAccessModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }

        //GetAllDynamicCondition
        public async Task<ExecResult<IEnumerable<DynamicConditionModel>>> GetAllDynamicCondition()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"select * from {OraSchema}.VW_CACHE_DYNAMIC_CONDITION";
                    IEnumerable<DynamicConditionModel> ResultData = await dbConn.QueryAsync<DynamicConditionModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<DynamicConditionModel>>
                    {
                        ExecStatus = true,
                        ResultCode = 0,
                        ResultMessage = "Success Execution",
                        Data = ResultData
                    };
                }
            }
            catch (System.Exception Ex)
            {
                return new ExecResult<IEnumerable<DynamicConditionModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }
    }
}
