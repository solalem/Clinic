namespace Clinic.ViewModels.Appointments.Visits
{
    public class CreateVisit
    {
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
        public Guid PatientId { get; set; }
        public String Physician { get; set; }
        public String Description { get; set; }
    }

    public class UpdateVisit
    {
        public Guid Id { get; set; }
        // public DateTimeOffset Date { get; set; }
        public Guid PatientId { get; set; }
        public String Physician { get; set; }
        public String Description { get; set; }
        public List<ProcedureSummary> Procedures { get; set; } = new List<ProcedureSummary>();
    }

    public class DeleteVisit
    {
        public Guid Id { get; set; }
    }

    public class GetVisit
    {
        public Guid Id { get; set; }
    }

    public class GetVisits
    {
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }

}
