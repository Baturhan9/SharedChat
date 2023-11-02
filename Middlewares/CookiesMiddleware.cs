namespace SharedChatWebSite.Middlewares;

public class CookiesMiddleware
{
    private readonly RequestDelegate _next;

    public CookiesMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if(context.Request.Cookies.TryGetValue("userId", out var number) && context.Request.Path == "/")
        {
            context.Response.Redirect("/Home/Chat");
        }
        await this._next(context);
    }
}