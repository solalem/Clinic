using Microsoft.EntityFrameworkCore;
using Clinic.Shared.Model;
using Clinic.Core.Appointments.Domain.Visits;
using Clinic.ViewModels;
using Clinic.ViewModels.Appointments.Visits;
using System.Linq;

namespace Clinic.Core.Appointments.Persistence
{
    public class VisitRepository : IVisitRepository
    {
        private readonly AppointmentsDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public VisitRepository(AppointmentsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Visit Add(Visit visit)
        {
            return  _context.Visits.Add(visit).Entity;               
        }

        public async Task<Visit> GetAsync(Guid id)
        {
            var visit = await _context.Visits.FindAsync(id);

            return visit;
        }

        public void Update(Visit visit)
        {
            _context.Entry(visit).State = EntityState.Modified;
        }
        
        public async void Delete(Guid id)
        {
            _context.Remove(_context.Visits.FirstOrDefault(x => x.Id == id));
        }

    }
}
