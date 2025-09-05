using TinyArcade.API.DatabaseModels;
using TinyArcade.API.Services.Interfaces;

namespace TinyArcade.API.Middleware
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDatabaseService _databaseService;
        private readonly IUserContext _userContext;

        public UserContextMiddleware(RequestDelegate next, IDatabaseService databaseService, IUserContext userContext)
        {
            _next = next;
            _databaseService = databaseService;
            _userContext = userContext;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.User.Identity.IsAuthenticated)
            {
                DBUser user = _databaseService.GetUser(context.User.Identity.Name);
                _userContext.UserName = user.Name;
                _userContext.Role = user.Role;
                _userContext.Id = user.Id;
            }

            await _next(context);
        }

    }
}