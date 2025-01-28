using Contracts.Accounts;
using Contracts.ResultTypes;
using Spectre.Console;

namespace Console.Scenarios;

public class DepositScenario : IScenario
{
    private readonly IBankAccountService _bankAccountService;

    public DepositScenario(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    public string Name => "Deposit";

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

        _bankAccountService.Deposit(amount);
        AnsiConsole.Write("Operation successful");
    }
}