using System;
using System.Linq;
using System.Collections.Generic;

namespace Clinic.ViewModels.Appointments.Patients
{
    public class PatientSummary : CachableEntity
    {
        // TODO: Trim unnecessary properties that you dont want end users see
        public String CardNumber { get; set; }

        public String FullName { get; set; }

        public String Gender { get; set; }

        public String PhoneNumber { get; set; }

        public DateTimeOffset? DateOfBirth { get; set; }

        public String Email { get; set; }


        public string DisplayName => this.FullName;
        public string DisplayDescription => "N/A";
        public string More => "";
    }

    public class PatientDetail: PatientSummary
    {
  
    }

    public class PatientList
    {
        public int Total { get => PaginationInfo.TotalItems; set => PaginationInfo.TotalItems = value; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo("", "Billing", "Patients/Index", "");
        public List<PatientSummary> Items { get; set; }

        public PatientList()
        {
            Items = new List<PatientSummary>();
        }

        public PatientList(List<PatientSummary> items, PaginationInfo paginationInfo = null)
        {
            Items = items;
            if(paginationInfo != null)
                PaginationInfo = paginationInfo;
        }

        public static PatientList Empty()
        {
            return new PatientList(new List<PatientSummary>());
        }
        
    }

}
