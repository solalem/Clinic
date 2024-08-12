using Clinic.Core.Appointments.Domain.Visits;
using Clinic.Core.Appointments.Persistence;
using Clinic.ViewModels.Appointments.Patients;
using Clinic.ViewModels.Appointments.Visits;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Core.Appointments.Application.Visits
{
    public class GetVisitsCommandHandler
        : IRequestHandler<GetVisitsCommand, VisitList>
    {
        private readonly AppointmentsDbContext _dbContext;
        
        public GetVisitsCommandHandler(AppointmentsDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<VisitList> Handle(GetVisitsCommand message, CancellationToken cancellationToken)
        {
            var pagination = message.Request.PaginationInfo;
            var summaries = _dbContext.Visits.Include(x => x.Patient).
                Where(x =>
                    string.IsNullOrEmpty(pagination.SearchString) ||
                    (!string.IsNullOrEmpty(pagination.SearchString) &&
                        (x.Physician.Contains(pagination.SearchString) ||
                        x.Patient.FullName.Contains(pagination.SearchString) ||
                        x.Patient.CardNumber.Contains(pagination.SearchString))
                    ));

            var models = GetVisitsCommand.ToResponse(message.Request, summaries);// new VisitList

            return models;// GetVisitsCommand.ToResponse(message.Request, models);
        }
    }

    /// <summary>
    /// Get Visit Command Model
    /// </summary>
    public class GetVisitsCommand : IRequest<VisitList>
    {
        public GetVisits Request { get; set; }

        public GetVisitsCommand(GetVisits request)
        {
            Request = request;
        }

        public static VisitList ToResponse(GetVisits request, IEnumerable<Visit> models)
        {
            var list = new VisitList();
            list.Items = models.Select(model =>
               new VisitSummary
               {
                   Id = model.Id,
                   Date = model.Date,
                   PresentIllness = model.PresentIllness,
                   PatientId = model.PatientId,
                   PatientName = model.Patient.FullName,
                   Physician = model.Physician,
               }).ToList();

            list.PaginationInfo = request.PaginationInfo;
            return list;
        }

    }

}

