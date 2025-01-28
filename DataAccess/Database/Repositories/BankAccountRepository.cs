using Abstractions.Repositories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Models.Accounts;
using Npgsql;

namespace Database.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public BankAccountRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task CreateAccount(long id, string pinCode)
    {
        const string sql = """
                           insert into accounts
                           values(:id, :pinCode, 0)
                           """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        await using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id).AddParameter("pinCode", pinCode);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
    }

    public async Task UpdateBalance(long id, decimal newBalance)
    {
        const string sql = """
                           update accounts
                           set balance = :balance
                           where id = :id
                           """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        await using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id).AddParameter("balance", newBalance);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
    }

    public async Task<FindBankAccountResult> FindAccountById(long id, string pinCode)
    {
        const string sql = """
                           select id, pinCode
                           from accounts
                           where id = :id;
                           """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        await using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return new FindBankAccountResult.AccountNotFound();
        }

        string correctPinCode = reader.GetString(1);
        if (correctPinCode != pinCode)
        {
            return new FindBankAccountResult.IncorrectPinCode();
        }

        return new FindBankAccountResult.Success(new BankAccount(
            reader.GetInt64(0),
            reader.GetDecimal(2)));
    }
}