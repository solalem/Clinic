using Clinic.Core.Appointments.Persistence;
using Clinic.ViewModels.Appointments.Patients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Core.Appointments.Application.Patients
{
    public class GetPatientsCommandHandler
        : IRequestHandler<GetPatientsCommand, GetPatientsResponse>
    {
        private readonly AppointmentsDbContext _dbContext;

        public GetPatientsCommandHandler(AppointmentsDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetPatientsResponse> Handle(GetPatientsCommand message, CancellationToken cancellationToken)
        {
            var pagination = message.Request.PaginationInfo;
            var searchString = pagination.SearchString ?? string.Empty;
            var searchPattern = $"%{searchString}%";
            
            var summaries = _dbContext.Database.SqlQuery<PatientSummary>(@$"
                select p.*, vd.lastvisit from patients p
                left join (
                  select patientid, max(date) AS lastvisit
                  from visits v
                  group by v.patientid
                ) vd on vd.patientid = p.id
                where 
                    {searchString} = '' or
                    ({searchString} <> '' and
                        (p.fullname like {searchPattern} or
                        p.phonenumber like {searchPattern} or
                        p.cardnumber like {searchPattern}))
                ");

            return new GetPatientsResponse
            {
                Succeed = true,
                Data = new PatientList
                {
                    Items = await summaries.Skip(pagination.Index).Take(pagination.PageSize).ToListAsync(),
                    PaginationInfo = pagination
                }
            };
        }
    }

    /// <summary>
    /// Get Patient Command Model
    /// </summary>
    public class GetPatientsCommand : IRequest<GetPatientsResponse>
    {
        public GetPatientsRequest Request { get; set; }

        public GetPatientsCommand(GetPatientsRequest request)
        {
            Request = request;
        }
    }
}
