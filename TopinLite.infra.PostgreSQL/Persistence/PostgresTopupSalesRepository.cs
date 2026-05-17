using Npgsql;

using NpgsqlTypes;

using TopinLite.infra.PostgreSQL.Abstractions;
using TopinLite.infra.PostgreSQL.Models;

namespace TopinLite.infra.PostgreSQL.Persistence;

public sealed class PostgresTopupSalesRepository : ITopupSalesRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public PostgresTopupSalesRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public Task InsertPackageSaleAsync(PackageSale item, CancellationToken cancellationToken = default)
    {
        return BulkInsertPackageSalesAsync([item], cancellationToken);
    }

    public Task InsertPinlessChargeAsync(PinlessCharge item, CancellationToken cancellationToken = default)
    {
        return BulkInsertPinlessChargesAsync([item], cancellationToken);
    }

    public async Task BulkInsertPackageSalesAsync(IReadOnlyCollection<PackageSale> items, CancellationToken cancellationToken = default)
    {
        if (items.Count == 0)
        {
            return;
        }

        const string copySql = """
        COPY topup.tbl_package_sales
        (
            pk_seq_package_sales, fk_tel_num, package_amount,
            confirm_status, confirm_date, confirm_time,
            pay_status, pay_date, pay_time,
            response_type, response_desc, product_code,
            ins_date, ins_time, upd_date, upd_time,
            fk_tel_gift, fk_package_type, fk_bank, reserve_status,
            rrn, id_card_type, id_card_no, retry,
            channel_id, order_id, fk_broker_id,
            ins_timestamp, pay_timestamp,
            sap_id, offer_code, crm_message_sequence, fk_accounts,
            pk_package_sales, camp_order
        )
        FROM STDIN (FORMAT BINARY)
        """;

        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var writer = await conn.BeginBinaryImportAsync(copySql, cancellationToken).ConfigureAwait(false);

        foreach (var item in items)
        {
            await writer.StartRowAsync(cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.PkSeqPackageSales, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkTelNum, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.PackageAmount, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.ConfirmStatus, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.ConfirmDate, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.ConfirmTime, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.PayStatus, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.PayDate, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.PayTime, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.ResponseType, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.ResponseDesc, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.ProductCode, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.InsDate, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.InsTime, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.UpdDate, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.UpdTime, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkTelGift, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkPackageType, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkBank, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.ReserveStatus, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.Rrn, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.IdCardType, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.IdCardNo, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.Retry, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.ChannelId, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.OrderId, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkBrokerId, cancellationToken).ConfigureAwait(false);
            await WriteTimestampAsync(writer, item.InsTimestamp, cancellationToken).ConfigureAwait(false);
            await WriteTimestampAsync(writer, item.PayTimestamp, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.SapId, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.OfferCode, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.CrmMessageSequence, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkAccounts, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.PkPackageSales, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.CampOrder, cancellationToken).ConfigureAwait(false);
        }

        await writer.CompleteAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task BulkInsertPinlessChargesAsync(IReadOnlyCollection<PinlessCharge> items, CancellationToken cancellationToken = default)
    {
        if (items.Count == 0)
        {
            return;
        }

        const string copySql = """
        COPY topup.tbl_pinless_charge
        (
            pk_seq_pinless_charge, fk_tel_num, charge_amount,
            confirm_status, confirm_date, confirm_time,
            charge_status, charge_date, charge_time,
            response_type, response_desc,
            ins_date, ins_time, upd_date, upd_time,
            fk_tel_charger, fk_charge_type, fk_bank, fk_crm_seq,
            retry, rrn, id_card_type, id_card_no, recharge_serial_no,
            channel_id, fk_broker_id,
            ins_timestamp, charge_timestamp,
            sap_id, fk_accounts, offer_code,
            pk_pinless_charge, bank_uniq_id
        )
        FROM STDIN (FORMAT BINARY)
        """;

        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var writer = await conn.BeginBinaryImportAsync(copySql, cancellationToken).ConfigureAwait(false);

        foreach (var item in items)
        {
            await writer.StartRowAsync(cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.PkSeqPinlessCharge, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkTelNum, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.ChargeAmount, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.ConfirmStatus, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.ConfirmDate, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.ConfirmTime, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.ChargeStatus, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.ChargeDate, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.ChargeTime, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.ResponseType, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.ResponseDesc, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.InsDate, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.InsTime, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.UpdDate, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.UpdTime, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkTelCharger, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkChargeType, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkBank, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkCrmSeq, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.Retry, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.Rrn, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.IdCardType, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.IdCardNo, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.RechargeSerialNo, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.ChannelId, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkBrokerId, cancellationToken).ConfigureAwait(false);
            await WriteTimestampAsync(writer, item.InsTimestamp, cancellationToken).ConfigureAwait(false);
            await WriteTimestampAsync(writer, item.ChargeTimestamp, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.SapId, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.FkAccounts, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.OfferCode, cancellationToken).ConfigureAwait(false);
            await WriteNumericAsync(writer, item.PkPinlessCharge, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.BankUniqId, cancellationToken).ConfigureAwait(false);
        }

        await writer.CompleteAsync(cancellationToken).ConfigureAwait(false);
    }

    private static Task WriteNumericAsync(NpgsqlBinaryImporter writer, decimal value, CancellationToken cancellationToken)
    {
        return writer.WriteAsync(value, NpgsqlDbType.Numeric, cancellationToken);
    }

    private static Task WriteNumericAsync(NpgsqlBinaryImporter writer, decimal? value, CancellationToken cancellationToken)
    {
        return writer.WriteAsync((object?)value ?? DBNull.Value, NpgsqlDbType.Numeric, cancellationToken);
    }

    private static Task WriteVarcharAsync(NpgsqlBinaryImporter writer, string? value, CancellationToken cancellationToken)
    {
        return writer.WriteAsync((object?)value ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken);
    }

    private static Task WriteTimestampAsync(NpgsqlBinaryImporter writer, DateTime value, CancellationToken cancellationToken)
    {
        return writer.WriteAsync(value, NpgsqlDbType.Timestamp, cancellationToken);
    }

    private static Task WriteTimestampAsync(NpgsqlBinaryImporter writer, DateTime? value, CancellationToken cancellationToken)
    {
        return writer.WriteAsync((object?)value ?? DBNull.Value, NpgsqlDbType.Timestamp, cancellationToken);
    }
}