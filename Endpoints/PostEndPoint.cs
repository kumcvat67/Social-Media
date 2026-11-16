using System.Text.Json.Serialization;
using List;
public static class PostEndPoint
{
    public static void Post(this WebApplication app)
    {
        var group = app.MapGroup("api/post");
        group.MapPost("make", (PostReq req) =>
        {
            string? username = tokenBase.checkToken(req.token);
            if(username==null)
            {
                return Results.Unauthorized();
            }
            if (string.IsNullOrWhiteSpace(req.text))
            {
                return Results.BadRequest(new{error="Empty post"});
            }
            DataBase.Posts.Add(new Post(username, req.text));
            return Results.Ok();
        });
        group.MapGet("get", () =>
        {
            List<Post> AllPosts = DataBase.Posts;
            if (AllPosts == null) { return Results.BadRequest(); }
            return Results.Ok(AllPosts);
        });
        group.MapPost("like", (string token, string postId) =>
        {
            var user = tokenBase.checkToken(token);
            if (user==null){return Results.Unauthorized();}
            var post = DataBase.Posts.FirstOrDefault(p=>p.PostID==postId);
            if (post==null){return Results.NotFound();}
            bool isLiked = post.setLike(user);
            return Results.Ok(new{like=isLiked});
        });
    }
}

public record PostReq(string token, string text);