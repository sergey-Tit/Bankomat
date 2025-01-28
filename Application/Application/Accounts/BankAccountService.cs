using Abstractions.Repositories;
using Contracts.Accounts;
using Contracts.ResultTypes;
using Models.Accounts;
using Models.Operations;

namespace Application.Accounts;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _bankAccountRepository;

    private readonly IBankAccountOperationRepository _bankAccountOperationRepository;

    private BankAccount? _currentBankAccount = null;

    private long _accountId = 0;

    public BankAccountService(
        IBankAccountRepository bankAccountRepository,
        IBankAccountOperationRepository bankAccountOperationRepository)
    {
        _bankAccountRepository = bankAccountRepository;
        _bankAccountOperationRepository = bankAccountOperationRepository;
    }

    public LoginResult Login(long id, string pinCode)
    {
        FindBankAccountResult result = _bankAccountRepository.FindAccountById(id, pinCode).Result;
        switch (result)
        {
            case FindBankAccountResult.AccountNotFound:
                return new LoginResult.AccountNotFound();
            case FindBankAccountResult.IncorrectPinCode:
                return new LoginResult.IncorrectPinCode();
            default:
            {
                var success = (FindBankAccountResult.Success)result;
                _currentBankAccount = success.BankAccount;

                return new LoginResult.Success();
            }
        }
    }

    public long Create(string pinCode)
    {
        _bankAccountRepository.CreateAccount(_accountId++, pinCode);
        _bankAccountOperationRepository.AddOperation(new BankAccountOperation(
            _accountId,
            DateTime.Now,
            null,
            BankAccountOperationType.Create));

        return _accountId;
    }

    public WithdrawResult Withdraw(decimal amount)
    {
        ArgumentNullException.ThrowIfNull(_currentBankAccount);

        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0.");
        }

        if (_currentBankAccount.Balance < amount)
        {
            return new WithdrawResult.LowBalanceError();
        }

        _currentBankAccount = new BankAccount(_currentBankAccount.Id, _currentBankAccount.Balance - amount);

        _bankAccountRepository.UpdateBalance(
            _currentBankAccount.Id,
            _currentBankAccount.Balance);
        _bankAccountOperationRepository.AddOperation(new BankAccountOperation(
            _currentBankAccount.Id,
            DateTime.Now,
            amount,
            BankAccountOperationType.Withdraw));

        return new WithdrawResult.Success();
    }

    public void Deposit(decimal amount)
    {
        ArgumentNullException.ThrowIfNull(_currentBankAccount);

        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0.");
        }

        _currentBankAccount = new BankAccount(_currentBankAccount.Id, _currentBankAccount.Balance + amount);

        _bankAccountRepository.UpdateBalance(
            _currentBankAccount.Id,
            _currentBankAccount.Balance);
        _bankAccountOperationRepository.AddOperation(new BankAccountOperation(
            _currentBankAccount.Id,
            DateTime.Now,
            amount,
            BankAccountOperationType.Deposit));
    }

    public decimal CheckBalance()
    {
        ArgumentNullException.ThrowIfNull(_currentBankAccount);
        return _currentBankAccount.Balance;
    }

    public ICollection<BankAccountOperation> GetOperations()
    {
        ArgumentNullException.ThrowIfNull(_currentBankAccount);
        return _bankAccountOperationRepository.GetOperations(_currentBankAccount.Id).Result;
    }
}