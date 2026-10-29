namespace List;
public enum Istatus
{
    ok,
    bad
}
public class DataBase
{
    public static List<Post> Posts = new List<Post>();
    public static List<User> Users = new List<User>();
}

public class Post
{
    private static int _counter;
    public int PostID{get;set;}
    public string username{get;set;}
    public string text{get; set;}

    public Post(string name, string PostText)
    {
        username = name;
        text = PostText;
        PostID=_counter++;
    }
}

public class User
{
    private static int _counter = 1;
    public string username{get;set;}
    public string password{get;set;}
    public User(string name, string Password)
    {
        username=name;
        password=Password;
    }
}
public class IBaseManager
{
    Istatus addPost(string username, string text)
    {
        DataBase.Posts.Add(new Post(username, text));
        return Istatus.ok;
    }
    bool isThereUser(string username)
    {
        return DataBase.Users.Any(u => u.username==username);
    } 
}