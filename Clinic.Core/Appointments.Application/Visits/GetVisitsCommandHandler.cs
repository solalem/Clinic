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
            var summaries = _dbContext.Visits.FromSql(@$"
                select v.*, p.fullname as patientname from visits v
                left join patients p on v.patientid = p.id
                where 
                    {pagination.SearchString} = '' or
                    ({pagination.SearchString} <> '' and
                        (v.physician like '%{pagination.SearchString}%' or
                        p.fullname like '%{pagination.SearchString}%' or
                        p.cardnumber like '%{pagination.SearchString}%' ))
                ");

            var models = GetVisitsCommand.ToResponse(message.Request, summaries);// new VisitList
            //{
            //    Items = await summaries.Select(x => new VisitSummary
            //    {
                    
            //    }).ToListAsync(),
            //    PaginationInfo = pagination
            //};

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
                   PatientName = model.PatientName,//.ToString(),// TODO
                   Physician = model.Physician,
               }).ToList();

            list.PaginationInfo = request.PaginationInfo;
            return list;
        }

    }

}

