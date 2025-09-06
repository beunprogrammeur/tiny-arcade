using TinyArcade.API.DatabaseModels;
using TinyArcade.API.Services.Interfaces;

namespace TinyArcade.API.Middleware
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.User.Identity.IsAuthenticated)
            {
                IDatabaseService databaseService = context.RequestServices.GetRequiredService<IDatabaseService>();
                IUserContext userContext = context.RequestServices.GetRequiredService<IUserContext>();


                DBUser user = databaseService.GetUser(context.User.Identity.Name);
                userContext.UserName = user.Name;
                userContext.Role = user.Role;
                userContext.Id = user.Id;
            }

            await _next(context);
        }

    }
}