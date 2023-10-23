using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Core.Appointments.Persistence;
using Clinic.Core.Appointments.Persistence.Patients;
using Clinic.ViewModels.Appointments.Patients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Core.Appointments.Application.Patients
{
    public class GetPatientsCommandHandler
        : IRequestHandler<GetPatientsCommand, PatientList>
    {
        private readonly AppointmentsQueryDbContext _dbContext;

        public GetPatientsCommandHandler(AppointmentsQueryDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PatientList> Handle(GetPatientsCommand message, CancellationToken cancellationToken)
        {
            var pagination = message.Request.PaginationInfo;
            var summaries = _dbContext.PatientSummaries.FromSqlInterpolated(@$"
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
                        p.fullname like '%{pagination.SearchString}%' or
                        p.phonenumber like '%{pagination.SearchString}%' or
                        p.cardnumber like '%{pagination.SearchString}%' ))
                ");

            var models =  new PatientList
            {
                Items = await summaries.Skip(pagination.Index).Take(pagination.PageSize).ToListAsync(),
                PaginationInfo = pagination
            };

            return models;// GetPatientsCommand.ToResponse(message.Request, models);
        }
    }

    /// <summary>
    /// Get Patient Command Model
    /// </summary>
    public class GetPatientsCommand : IRequest<PatientList>
    {
        public GetPatients Request { get; set; }

        public GetPatientsCommand(GetPatients request)
        {
            Request = request;
        }

        public static PatientList ToResponse(GetPatients request, IEnumerable<Patient> models)
        {
            var list = new PatientList();
            list.Items = models.Select(model =>
               new PatientSummary
               {
                   Id = model.Id,
                   CardNumber = model.CardNumber,
                   FullName = model.FullName,
                   Gender = model.Gender,
                   PhoneNumber = model.PhoneNumber,
                   DateOfBirth = model.DateOfBirth,
                   Email = model.Email
               }).ToList();

            list.PaginationInfo = request.PaginationInfo;
            return list;
        }

    }

}
