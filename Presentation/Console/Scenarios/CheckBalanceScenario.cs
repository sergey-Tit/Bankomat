using Contracts.Accounts;
using Contracts.ResultTypes;
using Spectre.Console;

namespace Console.Scenarios;

public class CheckBalanceScenario : IScenario
{
    private readonly IBankAccountService _bankAccountService;

    public CheckBalanceScenario(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    public string Name => "Check balance";

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

        AnsiConsole.Write($"Current balance: {_bankAccountService.CheckBalance()}");
    }
}