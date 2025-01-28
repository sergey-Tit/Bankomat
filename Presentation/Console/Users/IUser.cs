namespace Console.Users;

public interface IUser
{
    string Name { get; }

    void Login();
}