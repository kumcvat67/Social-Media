namespace List;
public enum Istatus
{
    ok,
    bad
}
public class DataBase
{
    public static List<Post> Posts = new List<Post>();
}

public class Post
{
    public string username{get; set;}
    public string text{get; set;}

    public Post(string userName, string PostText)
    {
        username = userName;
        text = PostText;
    }
}

public class IBaseManager
{
    Istatus addPost(string username, string text)
    {
        DataBase.Posts.Add(new Post(username, text));
        return Istatus.ok;
    }
}