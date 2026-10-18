using System.Text.RegularExpressions;

public static class AuthorizationEndPoint
{
    public static void authorization(this WebApplication app)
    {
        var group = app.MapGroup("app");

        group.MapPost("/signup", (SignUpReq req) =>
        {
            
        });
    }
}

public record SignUpReq(string Login, string password);