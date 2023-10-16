using System;
using System.Linq;
using System.Collections.Generic;
using Clinic.ViewModels;

namespace Clinic.ViewModels.Appointments.Visits
{
    public class VisitSummary : CachableEntity
    {
        // TODO: Trim unnecessary properties that you dont want end users see
        public DateTimeOffset Date { get; set; }

        public Guid PatientId { get; set; }

        public string PatientName { get; set; }

        public String Physician { get; set; }

        public String Description { get; set; }

        public List<ProcedureSummary> Procedures { get; set; } = new List<ProcedureSummary>();


        public string DisplayName => this.Physician;
        public string DisplayDescription => "N/A";
        public string More => "";
    }

    public class ProcedureSummary
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class VisitDetail: VisitSummary
    {
  
    }

    public class VisitList
    {
        public int Total { get => PaginationInfo.TotalItems; set => PaginationInfo.TotalItems = value; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo("", "Billing", "Visits/Index", "");
        public List<VisitSummary> Items { get; set; }

        public VisitList()
        {
            Items = new List<VisitSummary>();
        }

        public VisitList(List<VisitSummary> items, PaginationInfo paginationInfo = null)
        {
            Items = items;
            if(paginationInfo != null)
                PaginationInfo = paginationInfo;
        }

        public static VisitList Empty()
        {
            return new VisitList(new List<VisitSummary>());
        }
        
    }

}
