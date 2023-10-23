using Microsoft.EntityFrameworkCore;
using Clinic.SharedKernel.Domain.Abstractions.Model;
using Clinic.ViewModels.Appointments.Patients;
using Clinic.ViewModels;
using System.Linq;
using Clinic.Core.Appointments.Persistence;
using Clinic.Core.Appointments.Persistence.Patients;

namespace Clinic.Core.Appointments.Domain.Patients
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
            return _context.Patients.Add(patient).Entity;
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
    }
}
