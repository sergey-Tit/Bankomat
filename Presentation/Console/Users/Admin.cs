using Console.Scenarios;
using Spectre.Console;

namespace Console.Users;

public class Admin : IUser
{
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

    public string Name => "Administrator";

    public Admin(ICollection<IScenario> scenarios)
    {
        _scenarios = scenarios;
    }

    public void Login()
    {
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter system password: "));
        if (password != SystemPassword.Password)
        {
            AnsiConsole.Write("Incorrect system password");
            return;
        }

        SelectAction();
    }
}