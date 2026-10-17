using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Clinic.ViewModels.Appointments.Visits
{
    public class CreateVisitRequest
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        [Required(ErrorMessage = "Select a clinician.")]
        public String Physician { get; set; } = string.Empty;
        public String PresentIllness { get; set; } = "";
    }
    
    public class CreateVisitResponse : ApiResponse<VisitDetail>
    {
    }
}
