using StackExchange.Redis.Extensions.Core.Abstractions;



namespace TopinLite.Services.BusinessMiniApiCommands
{
    public class OffersCommand : IRequest<ExecResult<List<OffersModel>>>
    {
        public OffersCommand(ProductsRequestModel model)
        {
            Model = model;
        }

        public ProductsRequestModel Model { get; set; }
    }

    public class OffersCommandHandler : IRequestHandler<OffersCommand, ExecResult<List<OffersModel>>>
    {
        #region Construction

        private readonly IRedisClientFactory redisDatabase;

        public OffersCommandHandler(IRedisClientFactory redisDatabase)
        {
            this.redisDatabase = redisDatabase;
        }

        #endregion Construction

        public async Task<ExecResult<List<OffersModel>>> Handle(OffersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //// Get Single Product Offer by ProductId
                if (request.Model.ProductCategoryId == 0 && request.Model.ProductId != 0)
                {
                    var RedisInstance = redisDatabase.GetRedisDatabase();
                    var offer = await RedisInstance.SetMembersAsync<OffersModel>($"offer:{request.Model.ProductId}");
                    return new ExecResult<List<OffersModel>>
                    {
                        ExecStatus = true,
                        Data = new List<OffersModel> { offer[0] },
                        ResultCode = 0,
                        ResultMessage = "Success Execution"
                    };
                }

                //// Get All Product Offers by ProductCategoryId
                if (request.Model.ProductCategoryId != 0 && request.Model.ProductId == 0)
                {
                    IRedisDatabase RedisInstance = redisDatabase.GetRedisDatabase();
                    var allOffers = await RedisInstance.SetMembersAsync<OffersModel[]>("listOffers");

                    List<OffersModel> filteredOffers = allOffers[0].Where(o => o.Category == request.Model.ProductCategoryId).ToList();
                    return new ExecResult<List<OffersModel>>
                    {
                        ExecStatus = true,
                        Data = filteredOffers,
                        ResultCode = 0,
                        ResultMessage = "Success Execution"
                    };
                }

                return new ExecResult<List<OffersModel>>
                {

                };
            }
            catch (Exception)
            {
                return new ExecResult<List<OffersModel>>
                {
                    ExecStatus = false,
                    ResultCode = 500,
                    ResultMessage = "An error occurred while processing the request."
                };
            }
        }
    }
}