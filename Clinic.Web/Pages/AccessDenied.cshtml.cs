using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clinic.Web.Pages;

[AllowAnonymous]
public sealed class AccessDeniedModel : PageModel
{
}
