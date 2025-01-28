using Contracts.Accounts;
using Contracts.ResultTypes;
using Spectre.Console;

namespace Console.Scenarios;

public class WithdrawScenario : IScenario
{
    private readonly IBankAccountService _bankAccountService;

    public WithdrawScenario(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    public string Name => "Withdraw";

    public void Run()
    {
        long accountNumber = AnsiConsole.Prompt(new TextPrompt<long>("Enter account number: "));
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter account password: "));

        LoginResult result = _bankAccountService.Login(accountNumber, password);
        if (result is LoginResult.AccountNotFound)
        {
            AnsiConsole.Write("Account not found");
            return;
        }

        if (result is LoginResult.IncorrectPinCode)
        {
            AnsiConsole.Write("Incorrect pin code");
            return;
        }

        decimal amount = AnsiConsole.Prompt(new TextPrompt<decimal>("Enter amount: "));

        WithdrawResult operationResult = _bankAccountService.Withdraw(amount);
        if (operationResult is WithdrawResult.LowBalanceError)
        {
            AnsiConsole.Write("Current balance too low");
            return;
        }

        AnsiConsole.Write("Operation successful");
    }
}