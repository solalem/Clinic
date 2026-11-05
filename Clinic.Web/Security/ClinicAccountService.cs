using Clinic.Core.Appointments.Domain.ClinicAccounts;
using Clinic.Core.Appointments.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Web.Security;

public sealed record NewClinicAccount(string Username, string DisplayName, string Role, string Password);
public sealed record AuthenticatedClinicAccount(string Id, string Username, string DisplayName, string Role);

public interface IClinicAccountService
{
    Task<bool> HasAccountsAsync(CancellationToken cancellationToken = default);
    Task<AuthenticatedClinicAccount?> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default);
    Task<bool> CreateInitialAccountsAsync(NewClinicAccount physician, NewClinicAccount frontDeskOfficer, CancellationToken cancellationToken = default);
    Task<bool> CreateAccountAsync(NewClinicAccount account, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> GetPhysicianNamesAsync(CancellationToken cancellationToken = default);
}

public sealed class ClinicAccountService : IClinicAccountService
{
    private readonly AppointmentsDbContext _dbContext;
    private readonly PasswordHasher<ClinicAccount> _passwordHasher = new();

    public ClinicAccountService(AppointmentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> HasAccountsAsync(CancellationToken cancellationToken = default) =>
        _dbContext.Accounts.AnyAsync(cancellationToken);

    public async Task<AuthenticatedClinicAccount?> AuthenticateAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        var normalizedUsername = NormalizeUsername(username);
        var account = await _dbContext.Accounts.SingleOrDefaultAsync(
            item => item.NormalizedUsername == normalizedUsername,
            cancellationToken);

        if (account is null ||
            _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, password) == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return ToAuthenticatedAccount(account);
    }

    public async Task<bool> CreateInitialAccountsAsync(
        NewClinicAccount physician,
        NewClinicAccount frontDeskOfficer,
        CancellationToken cancellationToken = default)
    {
        if (!IsValidAccount(physician, ClinicRoles.Physician) ||
            !IsValidAccount(frontDeskOfficer, ClinicRoles.FrontDeskOfficer) ||
            NormalizeUsername(physician.Username) == NormalizeUsername(frontDeskOfficer.Username) ||
            await HasAccountsAsync(cancellationToken))
        {
            return false;
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        if (await HasAccountsAsync(cancellationToken))
        {
            return false;
        }

        _dbContext.Accounts.Add(CreateAccount(physician));
        _dbContext.Accounts.Add(CreateAccount(frontDeskOfficer));
        await _dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return true;
    }

    public async Task<bool> CreateAccountAsync(NewClinicAccount account, CancellationToken cancellationToken = default)
    {
        if (!IsValidAccount(account, account.Role))
        {
            return false;
        }

        var normalizedUsername = NormalizeUsername(account.Username);
        if (await _dbContext.Accounts.AnyAsync(
                item => item.NormalizedUsername == normalizedUsername,
                cancellationToken))
        {
            return false;
        }

        var entity = CreateAccount(account);
        _dbContext.Accounts.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<string>> GetPhysicianNamesAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Accounts
            .AsNoTracking()
            .Where(account => account.Role == ClinicRoles.Physician)
            .Select(account => account.DisplayName)
            .Distinct()
            .OrderBy(name => name)
            .ToListAsync(cancellationToken);

    private ClinicAccount CreateAccount(NewClinicAccount account)
    {
        var entity = new ClinicAccount
        {
            Username = account.Username.Trim(),
            NormalizedUsername = NormalizeUsername(account.Username),
            DisplayName = account.DisplayName.Trim(),
            Role = account.Role
        };
        entity.PasswordHash = _passwordHasher.HashPassword(entity, account.Password);
        return entity;
    }

    private static bool IsValidAccount(NewClinicAccount account, string expectedRole) =>
        account.Role == expectedRole &&
        !string.IsNullOrWhiteSpace(account.Username) &&
        account.Username.Trim().Length <= 50 &&
        !string.IsNullOrWhiteSpace(account.DisplayName) &&
        account.DisplayName.Trim().Length <= 100 &&
        account.Password.Length is >= 12 and <= 256 &&
        (expectedRole == ClinicRoles.Physician || expectedRole == ClinicRoles.FrontDeskOfficer);

    private static string NormalizeUsername(string username) => username.Trim().ToUpperInvariant();

    private static AuthenticatedClinicAccount ToAuthenticatedAccount(ClinicAccount account) =>
        new(account.Id.ToString(), account.Username, account.DisplayName, account.Role);
}
