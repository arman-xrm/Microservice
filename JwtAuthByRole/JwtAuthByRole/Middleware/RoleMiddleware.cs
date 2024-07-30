namespace JwtAuthByRole.Middleware
{
    public class RoleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _roles;

        public RoleMiddleware(RequestDelegate next, params string[] roles)
        {
            _next = next;
            _roles = roles;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated || !_roles.Any(role => context.User.IsInRole(role)))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            await _next(context);
        }
    }
}
