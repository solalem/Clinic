namespace Clinic.ViewModels.Appointments.Procedures
{
    public class ProcedureSummary : CachableEntity
    {
        // TODO: Trim unnecessary properties that you dont want end users see
        public String Name { get; set; }

        public String Description { get; set; }

    }

    public class ProcedureDetail: ProcedureSummary
    {
  
    }

    public class ProcedureList
    {
        public int Total { get => PaginationInfo.TotalItems; set => PaginationInfo.TotalItems = value; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo("", "Billing", "Procedures/Index", "");
        public List<ProcedureSummary> Items { get; set; }

        public ProcedureList()
        {
            Items = new List<ProcedureSummary>();
        }

        public ProcedureList(List<ProcedureSummary> items, PaginationInfo paginationInfo = null)
        {
            Items = items;
            if(paginationInfo != null)
                PaginationInfo = paginationInfo;
        }

        public static ProcedureList Empty()
        {
            return new ProcedureList(new List<ProcedureSummary>());
        }
        
    }

}
