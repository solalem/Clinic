using System;
using System.Linq;
using System.Collections.Generic;
using Clinic.ViewModels;
using System.Text.Json;

namespace Clinic.ViewModels.Appointments.Visits
{
    public class VisitSummary : CachableEntity
    {
        private List<ProcedureSummary>? _procedureList;

        // TODO: Trim unnecessary properties that you dont want end users see
        public DateTimeOffset Date { get; set; }

        public Guid PatientId { get; set; }

        public string PatientName { get; set; }

        public String Physician { get; set; }

        public String PresentIllness { get; set; }

        public List<ProcedureSummary> ProcedureList 
        {
            get => _procedureList ??= JsonSerializer.Deserialize<List<ProcedureSummary>>(Procedures ?? "[]") ?? new List<ProcedureSummary>();
            set
            {
                _procedureList = value ?? new List<ProcedureSummary>();
                Procedures = JsonSerializer.Serialize(_procedureList);
            }
        }
        
        public string? Procedures { get; set; }

        public string DisplayName => PatientName;
        public string DisplayDescription => Physician;
        public string More => Date.ToString("MMM dd, yy");
    }

    public class ProcedureSummary
    {
        public int Id { get; set; }
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
