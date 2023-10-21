using Microsoft.EntityFrameworkCore;
using Clinic.Core.Appointments.Domain.Patients;
using Clinic.SharedKernel.Domain.Abstractions.Model;
using Clinic.ViewModels.Appointments.Patients;
using Clinic.ViewModels;
using System.Linq;

namespace Clinic.Core.Appointments.Persistence.Patients
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppointmentsDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public PatientRepository(AppointmentsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Patient Add(Patient patient)
        {
            return  _context.Patients.Add(patient).Entity;               
        }

        public async Task<Patient> GetAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

            return patient;
        }

        public void Update(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
        }
        
        public async void Delete(Guid id)
        {
            _context.Remove(_context.Patients.FirstOrDefault(x => x.Id == id));
        }

        public async Task<PatientList> GetManyAsync(PaginationInfo pagination)
        {
            var summaries = _context.PatientSummaries.FromSqlInterpolated(@$"
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

            return new PatientList
            {
                Items = await summaries.Skip(pagination.Index).Take(pagination.PageSize).ToListAsync(),
                PaginationInfo = pagination
            };
        }
    }
}
