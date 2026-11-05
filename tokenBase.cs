namespace List;

public class tokenBase
{
    public static Dictionary<string, string> tokens = new Dictionary<string, string>();
    public static string generateToken(string username)
    {
        string token = Guid.NewGuid().ToString();
        tokens.Add(token, username);
        return token;
    }
    public static string? checkToken(string token)
    {
        tokens.TryGetValue(token, out string? username);
        return username;
    }
}