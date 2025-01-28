namespace Abstractions.Repositories;

public interface IBankAccountRepository
{
    Task<FindBankAccountResult> FindAccountById(long id, string pinCode);

    Task CreateAccount(long id, string pinCode);

    Task UpdateBalance(long id, decimal newBalance);
}