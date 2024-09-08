using System;

namespace Clinic.ViewModels.Appointments.Visits
{
    public record GetVisitRequest (Guid Id);
    public class GetVisitResponse: ApiResponse<VisitDetail>
    {
    }

    public record GetVisitsRequest(PaginationInfo PaginationInfo);
    public class GetVisitsResponse: ApiListResponse<VisitList>
    {
    }

    public record ArchiveVisitRequest (Guid Id);
    public class ArchiveVisitResponse: ApiResponse<VisitDetail>
    {
    } 

    public record AddProcedureRequest (string Name);
    public class AddProcedureResponse : ApiResponse<VisitDetail> { }
    public record UpdateProcedureRequest (Guid Id, string Name);
    public class UpdateProcedureResponse : ApiResponse<VisitDetail> { }
    public record RemoveProcedureRequest (Guid Id, Guid VisitId);
    public class RemoveProcedureResponse : ApiResponse<VisitDetail> { }

    
}
