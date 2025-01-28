using Contracts.Accounts;
using Contracts.ResultTypes;
using Spectre.Console;

namespace Console.Scenarios;

public class LoginScenario : IScenario
{
    private readonly IBankAccountService _bankAccountService;

    private readonly ICollection<IScenario> _scenarios;

    private void SelectAction()
    {
        IScenario selectedScenario = AnsiConsole.Prompt(
            new SelectionPrompt<IScenario>()
                .Title("Select action")
                .AddChoices(_scenarios)
                .UseConverter(scenario => scenario.Name));

        selectedScenario.Run();
    }

    public LoginScenario(IBankAccountService bankAccountService, ICollection<IScenario> scenarios)
    {
        _bankAccountService = bankAccountService;
        _scenarios = scenarios;
    }

    public string Name => "Login into existing account";

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

        SelectAction();
    }
}