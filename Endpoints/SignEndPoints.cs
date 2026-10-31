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

        if (user is null)
        {
            return Results.BadRequest(new {message="В базі такого логіна нема"});
        }
        if (user.password != req.password)
        {
            return Results.BadRequest(new {message="Невірний пароль"});
        }
        return Results.Ok();
    }
    public static IResult SignUp(SignupReq req)
    {
        if(DataBase.Users.Any(u=>u.username==req.username))return Results.BadRequest(new {message="Такий користувач уже зареєстрований"});
        if(string.IsNullOrWhiteSpace(req.password)||req.password.Length<8)return Results.BadRequest(new {message="Довжина пароля має бути 8 та більше символів"});

        DataBase.Users.Add(new User(req.username,req.password));
        return Results.Ok();
    }
}

public record SignupReq(string username, string password);