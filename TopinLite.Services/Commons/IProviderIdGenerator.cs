namespace TopinLite.Services.Commons
{
    public interface IProviderIdGenerator
    {
        decimal Generate();
        decimal GeneratePackageProviderId();
    }
}
