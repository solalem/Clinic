namespace Clinic.ViewModels.Appointments.Investigations
{
    public class InvestigationSummary : CachableEntity
    {
        // TODO: Trim unnecessary properties that you dont want end users see
        public String Name { get; set; }

        public String Description { get; set; }

    }

    public class InvestigationDetail: InvestigationSummary
    {
  
    }

    public class InvestigationList
    {
        public int Total { get => PaginationInfo.TotalItems; set => PaginationInfo.TotalItems = value; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo("", "Billing", "Investigations/Index", "");
        public List<InvestigationSummary> Items { get; set; }

        public InvestigationList()
        {
            Items = new List<InvestigationSummary>();
        }

        public InvestigationList(List<InvestigationSummary> items, PaginationInfo paginationInfo = null)
        {
            Items = items;
            if(paginationInfo != null)
                PaginationInfo = paginationInfo;
        }

        public static InvestigationList Empty()
        {
            return new InvestigationList(new List<InvestigationSummary>());
        }
        
    }

}
