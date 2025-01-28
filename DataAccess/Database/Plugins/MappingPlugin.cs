using Itmo.Dev.Platform.Postgres.Plugins;
using Models.Operations;
using Npgsql;

namespace Database.Plugins;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<BankAccountOperationType>();
    }
}