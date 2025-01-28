using Contracts.Accounts;
using Spectre.Console;

namespace Console.Scenarios;

public class CreateAccountScenario : IScenario
{
    private readonly IBankAccountService _bankAccountService;

    public CreateAccountScenario(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    public string Name => "Create new account";

    public void Run()
    {
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter password for new account: "));
        long createdId = _bankAccountService.Create(password);
        AnsiConsole.Write($"Account {createdId} has been created");
    }
}