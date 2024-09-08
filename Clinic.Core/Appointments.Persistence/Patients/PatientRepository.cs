using Microsoft.EntityFrameworkCore;
using Clinic.ViewModels;
using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Shared.Model;

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

        public void Update(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
        }
        
        public async void Delete(Guid id)
        {
            _context.Remove(_context.Patients.FirstOrDefault(x => x.Id == id));
        }

        public async Task<Patient> GetAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

            return patient;
        }

        public async Task<int> GetCountAsync(PaginationInfo paginationInfo)
        {
            var query = GetQuery(paginationInfo);

            return await query.CountAsync();
        }

        public async Task<IEnumerable<Patient>> GetManyAsync(PaginationInfo pagination)
        {
            var query = GetQuery(pagination);

            return await query.ToListAsync();
        }
  
        private IQueryable<Patient> GetQuery(PaginationInfo pagination)
        {
            var query = _context.Patients
                .Skip(pagination.Index)
                .Take(pagination.PageSize).AsQueryable();
            if (!string.IsNullOrEmpty(pagination.SearchString))
                query = query.Where(x => x.CardNumber.Contains(pagination.SearchString) ||
                    x.FullName.Contains(pagination.SearchString));

            return query.Skip(pagination.Index).Take(pagination.PageSize);
        }

    }
}
