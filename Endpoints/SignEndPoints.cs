using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using List;

public static class SignEndpoints
{
    public static void sign(this WebApplication app)
    {
        var group = app.MapGroup("api/user");
        group.MapPost("signUp", SignUp);
        group.MapPost("signIn", SignIn);
    }
    
    public static IResult SignIn(SignupReq req)
    {
        var user = DataBase.Users.FirstOrDefault(u=>u.username==req.username);

        if (user is null || user.password != req.password)
        {
            return Results.Unauthorized();
        }
        return Results.Ok(new{token=tokenBase.generateToken(req.username)});
    }
    public static IResult SignUp(SignupReq req, tokenBase tokenBase)
    {
        if(DataBase.Users.Any(u=>u.username==req.username))return Results.Conflict();
        if(string.IsNullOrWhiteSpace(req.password)||req.password.Length<8)return Results.BadRequest();

        DataBase.Users.Add(new User(req.username,req.password));

        return Results.Ok(new {token = tokenBase.generateToken(req.username)});
    }
}

public record SignupReq(string username, string password);