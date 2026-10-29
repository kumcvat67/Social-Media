using System.Text.Json.Serialization;
using List;
public static class PostEndPoint
{
    public static void Post(this WebApplication app)
    {
        var group = app.MapGroup("api/post");
        group.MapPost("make", (PostReq req) =>
        {
            DataBase.Posts.Add(new Post(req.username, req.text));
            return Results.Ok();
        });
        group.MapGet("get", () =>
        {
            List<Post> AllPosts = DataBase.Posts;
            if (AllPosts == null) { return Results.BadRequest(); }
            return Results.Ok(AllPosts);
        });
    }
}

public record PostReq(string username, string text);