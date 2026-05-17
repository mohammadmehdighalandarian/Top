using Npgsql;
using NpgsqlTypes;

namespace TopinLite.Workers.LogWorker.Persistence;

public sealed class PostgresLogRepository : ILogRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public PostgresLogRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task BulkInsertUserActivityAsync(
        IReadOnlyCollection<UserActivityLogRow> rows,
        CancellationToken cancellationToken = default)
    {
        if (rows.Count == 0)
        {
            return;
        }

        const string copySql = """
        COPY traffic.user_activity_log
        (
            event_at, server_name, environment_name, trace_identifier,
            direction, verb, path, status_code,
            body, headers, connection_info, consumer_key, origin
        )
        FROM STDIN (FORMAT BINARY)
        """;

        await using NpgsqlConnection conn = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using NpgsqlBinaryImporter writer = await conn.BeginBinaryImportAsync(copySql, cancellationToken).ConfigureAwait(false);

        foreach (UserActivityLogRow item in rows)
        {
            await writer.StartRowAsync(cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.EventAt.UtcDateTime, NpgsqlDbType.TimestampTz, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.ServerName, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.EnvironmentName, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.TraceIdentifier, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.Direction, NpgsqlDbType.Smallint, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.Verb, cancellationToken).ConfigureAwait(false);
            await WriteTextAsync(writer, item.Path, cancellationToken).ConfigureAwait(false);
            await writer.WriteAsync(item.StatusCode, NpgsqlDbType.Integer, cancellationToken).ConfigureAwait(false);
            await WriteTextAsync(writer, item.Body, cancellationToken).ConfigureAwait(false);
            await WriteJsonbAsync(writer, item.HeadersJson, cancellationToken).ConfigureAwait(false);
            await WriteTextAsync(writer, item.ConnectionInfo, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.ConsumerKey, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.Origin, cancellationToken).ConfigureAwait(false);
        }

        await writer.CompleteAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task BulkInsertErrorsAsync(
        IReadOnlyCollection<ErrorLogRow> rows,
        CancellationToken cancellationToken = default)
    {
        if (rows.Count == 0)
        {
            return;
        }

        const string copySql = """
        COPY traffic.error_log
        (
            server_name, environment_name, request_path, query_param,
            request_headers, exception_message, stack_trace, exception_json
        )
        FROM STDIN (FORMAT BINARY)
        """;

        await using NpgsqlConnection conn = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using NpgsqlBinaryImporter writer = await conn.BeginBinaryImportAsync(copySql, cancellationToken).ConfigureAwait(false);

        foreach (ErrorLogRow item in rows)
        {
            await writer.StartRowAsync(cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.ServerName, cancellationToken).ConfigureAwait(false);
            await WriteVarcharAsync(writer, item.EnvironmentName, cancellationToken).ConfigureAwait(false);
            await WriteTextAsync(writer, item.RequestPath, cancellationToken).ConfigureAwait(false);
            await WriteTextAsync(writer, item.QueryParam, cancellationToken).ConfigureAwait(false);
            await WriteJsonbAsync(writer, item.RequestHeadersJson, cancellationToken).ConfigureAwait(false);
            await WriteTextAsync(writer, item.ExceptionMessage, cancellationToken).ConfigureAwait(false);
            await WriteTextAsync(writer, item.StackTrace, cancellationToken).ConfigureAwait(false);
            await WriteJsonbAsync(writer, item.ExceptionJson, cancellationToken).ConfigureAwait(false);
        }

        await writer.CompleteAsync(cancellationToken).ConfigureAwait(false);
    }

    private static Task WriteVarcharAsync(NpgsqlBinaryImporter writer, string? value, CancellationToken cancellationToken)
        => writer.WriteAsync((object?)value ?? DBNull.Value, NpgsqlDbType.Varchar, cancellationToken);

    private static Task WriteTextAsync(NpgsqlBinaryImporter writer, string? value, CancellationToken cancellationToken)
        => writer.WriteAsync((object?)value ?? DBNull.Value, NpgsqlDbType.Text, cancellationToken);

    private static Task WriteJsonbAsync(NpgsqlBinaryImporter writer, string? value, CancellationToken cancellationToken)
        => writer.WriteAsync((object?)value ?? DBNull.Value, NpgsqlDbType.Jsonb, cancellationToken);
}