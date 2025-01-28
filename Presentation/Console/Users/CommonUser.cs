using Console.Scenarios;
using Spectre.Console;

namespace Console.Users;

public class CommonUser : IUser
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

    public string Name => "Common user";

    public CommonUser(ICollection<IScenario> scenarios)
    {
        _scenarios = scenarios;
    }

    public void Login()
    {
        SelectAction();
    }
}