using System.ComponentModel.DataAnnotations;
using Clinic.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clinic.Web.Pages;

[AllowAnonymous]
public sealed class SetupModel : PageModel
{
    private readonly IClinicAccountService _accounts;

    public SetupModel(IClinicAccountService accounts)
    {
        _accounts = accounts;
    }

    [BindProperty]
    public SetupInput Input { get; set; } = new();

    public bool IsAlreadyConfigured { get; private set; }

    public async Task OnGetAsync()
    {
        IsAlreadyConfigured = await _accounts.HasAccountsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        IsAlreadyConfigured = await _accounts.HasAccountsAsync();
        if (IsAlreadyConfigured)
        {
            return Page();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var created = await _accounts.CreateInitialAccountsAsync(
            new NewClinicAccount(Input.PhysicianUsername, Input.PhysicianDisplayName, ClinicRoles.Physician, Input.PhysicianPassword),
            new NewClinicAccount(Input.FrontDeskUsername, Input.FrontDeskDisplayName, ClinicRoles.FrontDeskOfficer, Input.FrontDeskPassword));

        if (!created)
        {
            ModelState.AddModelError(string.Empty, "Accounts could not be created. Check that the usernames differ and try again.");
            return Page();
        }

        return RedirectToPage("/Login");
    }

    public sealed class SetupInput
    {
        [Required, StringLength(100)]
        public string PhysicianDisplayName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string PhysicianUsername { get; set; } = string.Empty;

        [Required, StringLength(256, MinimumLength = 12), DataType(DataType.Password)]
        public string PhysicianPassword { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string FrontDeskDisplayName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string FrontDeskUsername { get; set; } = string.Empty;

        [Required, StringLength(256, MinimumLength = 12), DataType(DataType.Password)]
        public string FrontDeskPassword { get; set; } = string.Empty;
    }
}
