using Models.Accounts;

namespace Abstractions.Repositories;

public abstract record FindBankAccountResult
{
    private FindBankAccountResult() { }

    public sealed record Success(BankAccount BankAccount) : FindBankAccountResult;

    public sealed record AccountNotFound : FindBankAccountResult;

    public sealed record IncorrectPinCode : FindBankAccountResult;
}