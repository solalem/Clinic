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
            var summaries = _dbContext.Database.SqlQuery<PatientSummary>(@$"
                select p.*, vd.lastvisit from patients p
                left join (
                  select patientid, max(date) AS lastvisit
                  from visits v
                  group by v.patientid
                ) vd on vd.patientid = p.id
                where 
                    {pagination.SearchString} = '' or
                    ({pagination.SearchString} <> '' and
                        (p.fullname like '%{pagination.SearchString}%' or
                        p.phonenumber like '%{pagination.SearchString}%' or
                        p.cardnumber like '%{pagination.SearchString}%' ))
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
