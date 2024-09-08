using Clinic.Core.Appointments.Domain.Visits;
using Clinic.ViewModels.Appointments.Visits;
using System.Text.Json;

namespace Clinic.Core.Appointments.Application.Visits
{
    public static class VisitMapper
    {
        public static IEnumerable<VisitSummary> FromModel(IEnumerable<Visit> models)
        {
            return models.Select(x => FromModel(x)).ToList();
        }

        public static VisitDetail FromModel(Visit model)
        {
            return new VisitDetail
            {
                Id = model.Id,
                Date = model.Date,
                PatientId = model.PatientId,
                Physician = model.Physician,
                PresentIllness = model.PresentIllness,
                Procedures = JsonSerializer.Serialize(model.Procedures)
            };
        }

    }
}
