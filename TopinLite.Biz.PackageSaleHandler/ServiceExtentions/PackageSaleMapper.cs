using TopinLite.Biz.PackageSaleHandler.PackageOrder;
using TopinLite.infra.PostgreSQL.Models;

namespace TopinLite.Biz.PackageSaleHandler.ServiceExtentions;

public static class PackageSaleMapper
{

    public static PackageSale ToLockSale(PackageOrderContext sale,
                                         decimal providerId,
                                         string saleDate,
                                         string saleTime)
    {
        return new PackageSale
        {
            PkSeqPackageSales = providerId,
            PkPackageSales = providerId,
            FkTelNum = sale.TelNum,
            FkTelGift = sale.TelGift,
            PackageAmount = sale.Amount,
            FkPackageType = sale.OfferId,
            FkBrokerId = sale.BrokerId,
            SapId = sale.SapId,
            FkAccounts = sale.AccountId,
            ChannelId = sale.ChannelId,
            CampOrder = sale.CampOrder,
            ConfirmStatus = 1,

            // Lock state — PAY_STATUS = 2 (in progress)
            PayStatus = 2,
            ReserveStatus = 2,
            ResponseType = 2,

            UpdDate = saleDate,
            UpdTime = saleTime,
            InsTimestamp = DateTime.UtcNow,
        };
    }

    /// <summary>
    /// Success update — mirrors Oracle:
    /// UPDATE TBL_PACKAGE_SALES SET PAY_STATUS = 1 ... WHERE PK_SEQ_PACKAGE_SALES = V_PROVIDER_ID
    /// </summary>
    public static PackageSale ToSuccessSale(PackageOrderContext sale,
                                            decimal providerId,
                                            decimal bankId,
                                            string encCardNo,
                                            decimal CardType,
                                            decimal rrn,
                                            string orderId,
                                            string saleDate,
                                            string saleTime,
                                            decimal accountId,
                                            decimal crmMessageSeq,
                                            string responseDesc)
    {
        return new PackageSale
        {
            PkSeqPackageSales = providerId,
            PkPackageSales = providerId,
            FkTelNum = sale.TelNum,
            FkTelGift = sale.TelGift,
            PackageAmount = sale.Amount,
            FkPackageType = sale.OfferId,
            FkBrokerId = sale.BrokerId,
            SapId = sale.SapId,
            CampOrder = sale.CampOrder,
            ChannelId = sale.ChannelId,

            // Success state — PAY_STATUS = 1
            PayStatus = 1,
            ReserveStatus = 1,
            ResponseType = 0,
            ResponseDesc = responseDesc,

            // Payment details
            FkBank = bankId,
            IdCardNo = encCardNo,
            IdCardType = CardType,
            Rrn = rrn.ToString(CultureInfo.InvariantCulture),
            OrderId = orderId,

            // Timestamps
            PayDate = saleDate,
            PayTime = saleTime,
            PayTimestamp = DateTime.UtcNow,
            InsTimestamp = DateTime.UtcNow,

            // CRM
            CrmMessageSequence = crmMessageSeq,
            FkAccounts = accountId,

        };
    }

    public static PackageSale ToFailedSale(PackageOrderContext sale,
                                           decimal providerId,
                                           decimal responseType,
                                           string responseDesc,
                                           string saleDate,
                                           string saleTime)
    {
        return new PackageSale
        {
            PkSeqPackageSales = providerId,
            PkPackageSales = providerId,
            FkTelNum = sale.TelNum,
            FkTelGift = sale.TelGift,
            PackageAmount = sale.Amount,
            FkPackageType = sale.OfferId,
            FkBrokerId = sale.BrokerId,
            SapId = sale.SapId,
            CampOrder = sale.CampOrder,
            ChannelId = sale.ChannelId,
            FkAccounts = sale.AccountId,

            // Failure state — PAY_STATUS = -1
            PayStatus = -1,
            ReserveStatus = -1,
            ResponseType = responseType,
            ResponseDesc = responseDesc,

            UpdDate = saleDate,
            UpdTime = saleTime,
            InsTimestamp = DateTime.UtcNow,
        };
    }
}