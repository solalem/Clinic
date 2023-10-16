using Microsoft.EntityFrameworkCore;
using Clinic.SharedKernel.Domain.Abstractions.Model;
using Clinic.Core.Appointments.Domain.Visits;

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

        public async Task<IEnumerable<Visit>> GetManyAsync(int skip, int take, string? searchString = null)
        {
            var query = _context.Visits.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
                query = query.Where(x => x.Physician.Contains(searchString) ||
                    //x.Patient.FullName.Contains(searchString) ||
                    x.Description.Contains(searchString));

            return await query.Skip(skip).Take(take).ToListAsync();
        }

    }
}
