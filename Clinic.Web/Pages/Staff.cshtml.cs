using System.ComponentModel.DataAnnotations;
using Clinic.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clinic.Web.Pages;

[Authorize(Roles = ClinicRoles.Physician)]
public sealed class StaffModel : PageModel
{
    private readonly IClinicAccountService _accounts;

    public StaffModel(IClinicAccountService accounts)
    {
        _accounts = accounts;
    }

    [BindProperty]
    public StaffInput Input { get; set; } = new();

    public string? SuccessMessage { get; private set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var created = await _accounts.CreateAccountAsync(
            new NewClinicAccount(Input.Username, Input.DisplayName, Input.Role, Input.Password));
        if (!created)
        {
            ModelState.AddModelError(string.Empty, "That username already exists or the account details are invalid.");
            return Page();
        }

        Input = new StaffInput();
        SuccessMessage = "The account was created.";
        return Page();
    }

    public sealed class StaffInput
    {
        [Required, StringLength(100)]
        public string DisplayName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = ClinicRoles.FrontDeskOfficer;

        [Required, StringLength(256, MinimumLength = 12), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
