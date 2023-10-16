using Clinic.Core.Appointments.Domain.Visits;
using Clinic.ViewModels.Appointments.Visits;
using FluentValidation;
using MediatR;

namespace Clinic.Core.Appointments.Application.Visits
{
    public class GetVisitCommandHandler
        : IRequestHandler<GetVisitCommand, VisitDetail>
    {
        private readonly IVisitRepository _patientRepository;

        public GetVisitCommandHandler(IVisitRepository patientRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        public async Task<VisitDetail> Handle(GetVisitCommand message, CancellationToken cancellationToken)
        {
            // TODO: Add Integration events to notify others
            var model = await _patientRepository.GetAsync(message.Request.Id) ??
                throw new ArgumentException($"No patient found {message.Request.Id}");

            return GetVisitCommand.ToResponse(model);
        }
    }

    /// <summary>
    /// Get Visit Command Model
    /// </summary>
    public class GetVisitCommand : IRequest<VisitDetail>
    {
        public GetVisit Request { get; set; }

        public GetVisitCommand(GetVisit request)
        {
            Request = request;
        }

        public static VisitDetail ToResponse(Visit model)
        {
            return new VisitDetail
            {
                Id = model.Id,
                Date = model.Date,
                Description = model.Description,
                PatientId = model.PatientId,
                PatientName = model.PatientId.ToString(),// TODO
                Physician = model.Physician,
                Procedures = model.Procedures.Select(x => new ProcedureSummary 
                { 
                    Name = x.Name, 
                    Description = x.Description 
                }).ToList()
            };
        }

    }

}
