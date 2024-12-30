namespace Clinic.Shared
{
    public interface IIdentityService
    {
        string GetUserIdentity();
        bool IsInRole(string role);
    }
}