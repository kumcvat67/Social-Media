namespace List;

public class User
{
    public string username{get;set;}
    public string password{get;set;}
    public User(string name, string Password)
    {
        username=name;
        password=Password;
    }
}
