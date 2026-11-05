namespace Clinic.Core.Appointments.Domain.ClinicAccounts
{
    public sealed class ClinicAccount
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string NormalizedUsername { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }

}
