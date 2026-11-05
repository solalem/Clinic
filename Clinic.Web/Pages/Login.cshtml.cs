using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Clinic.Web.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clinic.Web.Pages;

[AllowAnonymous]
public sealed class LoginModel : PageModel
{
    private readonly IClinicAccountService _accounts;

    public LoginModel(IClinicAccountService accounts)
    {
        _accounts = accounts;
    }

    [BindProperty]
    public LoginInput Input { get; set; } = new();

    [BindProperty]
    public string? ReturnUrl { get; set; }

    public bool NeedsSetup { get; private set; }

    public async Task OnGetAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
        NeedsSetup = !await _accounts.HasAccountsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        NeedsSetup = !await _accounts.HasAccountsAsync();
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var account = await _accounts.AuthenticateAsync(Input.Username, Input.Password);
        if (account is null)
        {
            ModelState.AddModelError(string.Empty, "The username or password is incorrect.");
            return Page();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, account.Id),
            new Claim(ClaimTypes.Name, account.DisplayName),
            new Claim("name", account.DisplayName),
            new Claim(ClaimTypes.Role, account.Role)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = Input.RememberMe });

        if (!string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
        {
            return LocalRedirect(ReturnUrl);
        }

        return Redirect("/");
    }

    public sealed class LoginInput
    {
        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, StringLength(256)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
