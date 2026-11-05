namespace Clinic.Web.Security;

public static class ClinicRoles
{
    public const string Physician = "Physician";
    public const string FrontDeskOfficer = "FrontDeskOfficer";
    public const string PatientRecords = Physician + "," + FrontDeskOfficer;
}
