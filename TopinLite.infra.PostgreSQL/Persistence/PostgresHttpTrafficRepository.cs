using Npgsql;

using NpgsqlTypes;

using TopinLite.infra.PostgreSQL.Abstractions;
using TopinLite.infra.PostgreSQL.Models;

namespace TopinLite.infra.PostgreSQL.Persistence;

public sealed class PostgresHttpTrafficRepository : IHttpTrafficRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public PostgresHttpTrafficRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task InsertRequestAsync(HttpRequestLog request, CancellationToken cancellationToken = default)
    {
        await BulkInsertRequestsAsync([request], cancellationToken).ConfigureAwait(false);
    }

    public async Task InsertResponseAsync(HttpResponseLog response, CancellationToken cancellationToken = default)
    {
        await BulkInsertResponsesAsync([response], cancellationToken).ConfigureAwait(false);
    }

    public async Task BulkInsertTrafficAsync(IReadOnlyCollection<HttpTrafficLogItem> items, CancellationToken cancellationToken = default)
    {
        if (items.Count == 0)
        {
            return;
        }

        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var tx = await conn.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

        await BulkInsertRequestsCoreAsync(conn, items.Select(x => x.Request), cancellationToken).ConfigureAwait(false);
        await BulkInsertResponsesCoreAsync(conn, items.Select(x => x.Response), cancellationToken).ConfigureAwait(false);

        await tx.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task BulkInsertRequestsAsync(IReadOnlyCollection<HttpRequestLog> requests, CancellationToken cancellationToken = default)
    {
        if (requests.Count == 0)
        {
            return;
        }

        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await BulkInsertRequestsCoreAsync(conn, requests, cancellationToken).ConfigureAwait(false);
    }

    public async Task BulkInsertResponsesAsync(IReadOnlyCollection<HttpResponseLog> responses, CancellationToken cancellationToken = default)
    {
        if (responses.Count == 0)
        {
            return;
        }

        await using var conn = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await BulkInsertResponsesCoreAsync(conn, responses, cancellationToken).ConfigureAwait(false);
    }

    private static async Task BulkInsertRequestsCoreAsync(
        NpgsqlConnection conn,
        IEnumerable<HttpRequestLog> requests,
        CancellationToken cancellationToken)
    {
        const string copySql = """
        COPY traffic.http_requests
        (
            id, request_time, trace_id, correlation_id, server_name, environment_name,
            method, scheme, host, port, path, query_string,
            client_ip, x_forwarded_for, user_agent, referer,
            content_type, content_length, request_headers, request_body,
            app_user, tenant_id
        )
        FROM STDIN (FORMAT BINARY)
        """;

        await using var writer = await conn.BeginBinaryImportAsync(copySql, cancellationToken).ConfigureAwait(false);

        foreach (var item in requests)
        {
            await writer.StartRowAsync(cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.Id, NpgsqlDbType.Uuid, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.RequestTime.UtcDateTime, NpgsqlDbType.TimestampTz, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.TraceId ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.CorrelationId ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.ServerName ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.EnvironmentName ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.Method, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.Scheme ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.Host ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.Port ?? DBNull.Value, NpgsqlDbType.Integer, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.Path, NpgsqlDbType.Text, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.QueryString ?? DBNull.Value, NpgsqlDbType.Text, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.ClientIp ?? DBNull.Value, NpgsqlDbType.Inet, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.XForwardedFor ?? DBNull.Value, NpgsqlDbType.Text, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.UserAgent ?? DBNull.Value, NpgsqlDbType.Text, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.Referer ?? DBNull.Value, NpgsqlDbType.Text, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.ContentType ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.ContentLength ?? DBNull.Value, NpgsqlDbType.Bigint, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.RequestHeadersJson ?? DBNull.Value, NpgsqlDbType.Jsonb, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.RequestBody ?? DBNull.Value, NpgsqlDbType.Text, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.AppUser ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.TenantId ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
        }

        await writer.CompleteAsync(cancellationToken).ConfigureAwait(false);
    }

    private static async Task BulkInsertResponsesCoreAsync(
        NpgsqlConnection conn,
        IEnumerable<HttpResponseLog> responses,
        CancellationToken cancellationToken)
    {
        const string copySql = """
        COPY traffic.http_responses
        (
            id, request_time, response_time, request_id,
            trace_id, correlation_id, server_name, environment_name,
            status_code, duration_ms, content_type, content_length,
            response_headers, response_body, error_message, exception_type, is_success
        )
        FROM STDIN (FORMAT BINARY)
        """;

        await using var writer = await conn.BeginBinaryImportAsync(copySql, cancellationToken).ConfigureAwait(false);

        foreach (var item in responses)
        {
            await writer.StartRowAsync(cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.Id, NpgsqlDbType.Uuid, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.RequestTime.UtcDateTime, NpgsqlDbType.TimestampTz, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.ResponseTime.UtcDateTime, NpgsqlDbType.TimestampTz, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.RequestId ?? DBNull.Value, NpgsqlDbType.Uuid, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.TraceId ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.CorrelationId ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.ServerName ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.EnvironmentName ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.StatusCode, NpgsqlDbType.Integer, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.DurationMs, NpgsqlDbType.Bigint, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.ContentType ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.ContentLength ?? DBNull.Value, NpgsqlDbType.Bigint, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.ResponseHeadersJson ?? DBNull.Value, NpgsqlDbType.Jsonb, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.ResponseBody ?? DBNull.Value, NpgsqlDbType.Text, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.ErrorMessage ?? DBNull.Value, NpgsqlDbType.Text, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync((object?)item.ExceptionType ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.IsSuccess, NpgsqlDbType.Boolean, cancellationToken).ConfigureAwait(false);
        }

        await writer.CompleteAsync(cancellationToken).ConfigureAwait(false);
    }
}
