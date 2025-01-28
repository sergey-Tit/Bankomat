using Contracts.ResultTypes;
using Models.Operations;

namespace Contracts.Accounts;

public interface IBankAccountService
{
    LoginResult Login(long id, string pinCode);

    long Create(string pinCode);

    WithdrawResult Withdraw(decimal amount);

    void Deposit(decimal amount);

    decimal CheckBalance();

    ICollection<BankAccountOperation> GetOperations();
}