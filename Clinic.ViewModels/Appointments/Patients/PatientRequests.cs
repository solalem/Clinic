using System;

namespace Clinic.ViewModels.Appointments.Patients
{
    public record GetPatientRequest (Guid Id);
    public class GetPatientResponse: ApiResponse<PatientDetail>
    {
    }

    public record GetPatientsRequest(PaginationInfo PaginationInfo);
    public class GetPatientsResponse: ApiListResponse<PatientList>
    {
    }

    public record ArchivePatientRequest (Guid Id);
    public class ArchivePatientResponse: ApiResponse<PatientDetail>
    {
    } 

    
}
