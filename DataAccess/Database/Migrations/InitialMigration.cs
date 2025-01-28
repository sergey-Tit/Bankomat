using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Database.Migrations;

[Migration(1, "Initial")]
public class InitialMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
    """
    create type operation_type as enum
    (
        'withdraw',
        'create',
        'deposit'
    );
    
    create table accounts
    (
        id bigint primary key,
        pin_code string not null,
        balance decimal not null
    );
    
    create table operations
    (
        account_id bigint not null references accounts(id),
        date_time timestamp not null,
        type operation_type not null,
        amount decimal
    );
    """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
    """
    drop table operations
    drop table accounts
    drop type operation_type
    """;
}