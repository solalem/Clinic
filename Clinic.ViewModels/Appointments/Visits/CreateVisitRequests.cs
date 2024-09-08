using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Clinic.ViewModels.Appointments.Visits
{
    public class CreateVisitRequest
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public String Physician { get; set; }
        public String PresentIllness { get; set; }
    }
    
    public class CreateVisitResponse : ApiResponse<VisitDetail>
    {
    }
}
