using Contracts.Accounts;
using Contracts.ResultTypes;
using Models.Operations;
using Spectre.Console;

namespace Console.Scenarios;

public class ShowHistoryScenario : IScenario
{
    private readonly IBankAccountService _bankAccountService;

    public ShowHistoryScenario(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    public string Name => "Show operations history";

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

        ICollection<BankAccountOperation> history = _bankAccountService.GetOperations();
        foreach (BankAccountOperation operation in history)
        {
            AnsiConsole.Write(operation.Info());
        }
    }
}