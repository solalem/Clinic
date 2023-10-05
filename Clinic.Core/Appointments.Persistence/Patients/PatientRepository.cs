using Microsoft.EntityFrameworkCore;
using Clinic.Core.Appointments.Domain.Patients;
using Clinic.SharedKernel.Domain.Abstractions.Model;

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

        public async Task<IEnumerable<Patient>> GetManyAsync(int skip, int take, string? searchString = null)
        {
            var query = _context.Patients.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
                query = query.Where(x => x.CardNumber.Contains(searchString) ||
                    x.FullName.Contains(searchString) ||
                    x.PhoneNumber.Contains(searchString));

            return await query.Skip(skip).Take(take).ToListAsync();
        }
    }
}
