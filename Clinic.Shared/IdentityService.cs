using Microsoft.AspNetCore.Http;

namespace Clinic.Shared
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserIdentity()
        {
            var sub = _context.HttpContext.User.FindFirst("name");
            return sub == null ? null : sub.Value;
        }

        public bool IsInRole(string role)
        {
            return _context.HttpContext.User.IsInRole(role);
        }
    }
}
