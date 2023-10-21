using Clinic.Core.Appointments.Domain.Visits;
using Clinic.Core.Appointments.Persistence.Visits;
using Clinic.ViewModels.Appointments.Visits;
using MediatR;

namespace Clinic.Core.Appointments.Application.Visits
{
    public class GetVisitsCommandHandler
        : IRequestHandler<GetVisitsCommand, VisitList>
    {
        private readonly IVisitRepository _visitRepository;

        public GetVisitsCommandHandler(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository ?? throw new ArgumentNullException(nameof(visitRepository));
        }

        public async Task<VisitList> Handle(GetVisitsCommand message, CancellationToken cancellationToken)
        {
            // TODO: Add Integration events to notify others
            var models = await _visitRepository.GetManyAsync(message.Request.PaginationInfo);

            return GetVisitsCommand.ToResponse(message.Request, models);
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
                   Description = model.Description,
                   PatientId = model.PatientId,
                   PatientName = model.PatientId.ToString(),// TODO
                   Physician = model.Physician,
               }).ToList();

            list.PaginationInfo = request.PaginationInfo;
            return list;
        }

    }

}

