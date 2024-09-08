using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Clinic.ViewModels.Appointments.Visits
{
    public class UpdateVisitRequest
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public String Physician { get; set; }
        public String PresentIllness { get; set; }
        public List<ProcedureSummary> Procedures { get; set; } = new List<ProcedureSummary>();
    }

    public class UpdateVisitResponse : ApiResponse<VisitDetail>
    {
    }

}
