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
        group.MapDelete("{id}", (string id, string token) =>
        {
            var user = tokenBase.checkToken(token);
            if (user == null) { return Results.Unauthorized(); }

            var post = DataBase.Posts.FirstOrDefault(p => p.PostID == id);
            if (post != null)
            {
                if (post.username != user) { return Results.Forbid(); }

                DataBase.Posts.Remove(post);
                DataBase.Comments.RemoveAll(c => c.ParentPostId == id); 
                return Results.Ok(new { deleted = "post" });
            }

            var comment = DataBase.Comments.FirstOrDefault(c => c.PostID == id);
            if (comment != null)
            {
                if (comment.username != user) { return Results.Forbid(); }

                DataBase.Comments.Remove(comment);
                return Results.Ok(new { deleted = "comment" });
            }

            return Results.NotFound();
        });
        group.MapPost("comment/{postId}", (string postId, PostReq req) =>
        {
            var user = tokenBase.checkToken(req.token);
            if (user==null){return Results.Unauthorized();}
            var mother_post = DataBase.Posts.FirstOrDefault(p=>p.PostID==postId);
            if (mother_post==null){return Results.NotFound();}
            if (string.IsNullOrWhiteSpace(req.text))
            {
                return Results.BadRequest(new{error="Empty post"});
            }
            DataBase.Comments.Add(new CommentPost(user,mother_post.PostID, req.text));
            return Results.Ok();
        });
        group.MapGet("comments/{postId}", (string postId) =>
        {
            List<CommentPost> Allpost = DataBase.Comments.Where(c=>c.ParentPostId==postId).ToList();
            return Results.Ok(Allpost);
        });
    }
}

public record PostReq(string token, string text);