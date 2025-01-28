using Abstractions.Repositories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Models.Operations;
using Npgsql;

namespace Database.Repositories;

public class BankAccountOperationRepository : IBankAccountOperationRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public BankAccountOperationRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task AddOperation(BankAccountOperation operation)
    {
        const string sql = """
                           insert into operations
                           values(:id, :dateTime, :operationType, :amount)
                           """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        await using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", operation.BankAccountId)
            .AddParameter("dateTime", operation.DateTime)
            .AddParameter("operationType", operation.Type)
            .AddParameter("amount", operation.Amount);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
    }

    public async Task<ICollection<BankAccountOperation>> GetOperations(long bankAccountId)
    {
        const string sql = """
                           select *
                           from operations
                           where account_id = :id;
                           """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        await using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", bankAccountId);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        ICollection<BankAccountOperation> operations = [];
        while (await reader.ReadAsync())
        {
            operations.Add(new BankAccountOperation(
                reader.GetInt64(0),
                reader.GetDateTime(1),
                reader.GetDecimal(2),
                reader.GetFieldValue<BankAccountOperationType>(3)));
        }

        return operations;
    }
}