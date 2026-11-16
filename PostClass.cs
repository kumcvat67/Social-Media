namespace List;

public class Post
{
    private static int _counter;
    public string PostID{get;set;}
    public string username{get;set;}
    public string text{get; set;}
    private HashSet<string> LikesList = new HashSet<string>();

    public Post(string name, string PostText)
    {
        username = name;
        text = PostText;
        PostID=username+"#"+_counter++;
    }
    public bool setLike(string username)
    {
        bool isAdded = LikesList.Add(username);
        if (isAdded)
        {
            return true;
        }
        else
        {
            LikesList.Remove(username);
            return false;
        }
    }
}
