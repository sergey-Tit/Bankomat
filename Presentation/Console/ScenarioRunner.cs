using Console.Users;
using Spectre.Console;

namespace Console;

public class ScenarioRunner
{
    private readonly ICollection<IUser> _list;

    public ScenarioRunner(ICollection<IUser> list)
    {
        _list = list;
    }

    public void Run()
    {
        IUser selectedUser = AnsiConsole.Prompt(
            new SelectionPrompt<IUser>()
                .Title("Select user mode")
                .AddChoices(_list)
                .UseConverter(user => user.Name));

        selectedUser.Login();
    }
}