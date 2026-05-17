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
                    string strQuery = $"SELECT * FROM {OraSchema}.VW_BROKER_ACCESS_LIST T";
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

        public async Task<ExecResult<IEnumerable<DiyPriceModel>>> GetAllDiyPrices()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT * FROM {OraSchema}.VW_DIY_PRICE";
                    IEnumerable<DiyPriceModel> ResultData = await dbConn.QueryAsync<DiyPriceModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<DiyPriceModel>>
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
                return new ExecResult<IEnumerable<DiyPriceModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }

        public async Task<ExecResult<IEnumerable<BrokerOfferAccessModel>>> GetAllBrokerOfferAccess()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT T.BrokerId AS BrokerId, T.OfferId AS OfferId, T.STATUS AS Status FROM {OraSchema}.VW_BROKER_ACCESS T WHERE T.STATUS = 1";
                    IEnumerable<BrokerOfferAccessModel> ResultData = await dbConn.QueryAsync<BrokerOfferAccessModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<BrokerOfferAccessModel>>
                    {
                        ExecStatus = true,
                        ResultCode = 0,
                        ResultMessage = "Success Execution",
                        Data = ResultData
                    };
                }
            }
            catch (Exception Ex)
            {
                return new ExecResult<IEnumerable<BrokerOfferAccessModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }

        public async Task<ExecResult<IEnumerable<DiyDataAccessModel>>> GetAllDiyDataAccess()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT T.BrokerId AS BrokerId, T.OfferId AS OfferId, T.AttributeType AS AttributeType, T.AttributeMin AS AttributeMin, T.AttributeMax AS AttributeMax FROM VW_DIY_ACCESS T WHERE T.AttributeType = 1";
                    IEnumerable<DiyDataAccessModel> ResultData = await dbConn.QueryAsync<DiyDataAccessModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<DiyDataAccessModel>>
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
                return new ExecResult<IEnumerable<DiyDataAccessModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }

        public async Task<ExecResult<IEnumerable<RuleAccountModel>>> GetAllRuleAccounts()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT * FROM {OraSchema}.VW_RULE_ACCOUNTS";
                    IEnumerable<RuleAccountModel> ResultData = await dbConn.QueryAsync<RuleAccountModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<RuleAccountModel>>
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
                return new ExecResult<IEnumerable<RuleAccountModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }

        public async Task<ExecResult<IEnumerable<PrimaryOffersModel>>> GetAllPrimaryOffers()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT * FROM {OraSchema}.VW_PRIMARY_WHITE_LIST";
                    IEnumerable<PrimaryOffersModel> ResultData = await dbConn.QueryAsync<PrimaryOffersModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<PrimaryOffersModel>>
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
                return new ExecResult<IEnumerable<PrimaryOffersModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }

        public async Task<ExecResult<IEnumerable<BrokerSaleLimitModel>>> GetBrokerSaleLimits()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT * FROM {OraSchema}.VW_BROKER_SALE_LIMIT";
                    IEnumerable<BrokerSaleLimitModel> ResultData = await dbConn.QueryAsync<BrokerSaleLimitModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<BrokerSaleLimitModel>>
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
                return new ExecResult<IEnumerable<BrokerSaleLimitModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }

        public async Task<ExecResult<IEnumerable<TradeTypeModel>>> GetTradeTypes()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT * FROM {OraSchema}.VW_TRADE_TYPES";
                    IEnumerable<TradeTypeModel> ResultData = await dbConn.QueryAsync<TradeTypeModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<TradeTypeModel>>
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
                return new ExecResult<IEnumerable<TradeTypeModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }

        public async Task<ExecResult<IEnumerable<SelectedBrokerModel>>> GetSelectedBrokers()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT * FROM {OraSchema}.VW_SELECTED_BROKERS";
                    IEnumerable<SelectedBrokerModel> ResultData = await dbConn.QueryAsync<SelectedBrokerModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<SelectedBrokerModel>>
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
                return new ExecResult<IEnumerable<SelectedBrokerModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }

        public async Task<ExecResult<IEnumerable<BrokerSmsModel>>> GetBrokerSmsTexts()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT * FROM {OraSchema}.VW_SMS_TEXT";
                    IEnumerable<BrokerSmsModel> ResultData = await dbConn.QueryAsync<BrokerSmsModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<BrokerSmsModel>>
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
                return new ExecResult<IEnumerable<BrokerSmsModel>>
                {
                    ExecStatus = false,
                    ResultCode = -9009,
                    ResultMessage = $"Message:{Ex.Message} & StackTrace:{Ex.StackTrace}",
                    Data = null
                };
            }
        }

        public async Task<ExecResult<IEnumerable<MessagesModel>>> GetMessages()
        {
            try
            {
                using (OracleConnection dbConn = new OracleConnection(oracleDatabaseConfig.GetConnectionString()))
                {
                    await dbConn.OpenAsync();
                    string strQuery = $"SELECT * FROM {OraSchema}.VW_MESSAGES";
                    IEnumerable<MessagesModel> ResultData = await dbConn.QueryAsync<MessagesModel>(strQuery).ConfigureAwait(false);
                    await dbConn.CloseAsync();
                    return new ExecResult<IEnumerable<MessagesModel>>
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
                return new ExecResult<IEnumerable<MessagesModel>>
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
