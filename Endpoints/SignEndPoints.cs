using System.Text.RegularExpressions;
using List;

public static class SignEndpoints
{
    public static void sign(this WebApplication app)
    {
        var group = app.MapGroup("api/user");
        group.MapPost("signUp", SignUp);
    }
    
    public static IResult SignUp(SignupReq req)
    {
        if(DataBase.Users.Any(u=>u.username==req.username))return Results.BadRequest();
        if(string.IsNullOrWhiteSpace(req.password)||req.password.Length<8)return Results.BadRequest();

        DataBase.Users.Add(new User(req.username,req.password));
        return Results.Ok();
    }
}

public record SignupReq(string username, string password);