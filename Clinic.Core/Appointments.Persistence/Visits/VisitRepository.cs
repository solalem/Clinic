using Microsoft.EntityFrameworkCore;
using Clinic.ViewModels;
using Clinic.Core.Appointments.Domain.Visits;
using Clinic.Shared.Model;

namespace Clinic.Core.Appointments.Persistence.Visits
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

        public void Update(Visit visit)
        {
            _context.Entry(visit).State = EntityState.Modified;
        }
        
        public async void Delete(Guid id)
        {
            _context.Remove(_context.Visits.FirstOrDefault(x => x.Id == id));
        }

        public async Task<Visit> GetAsync(Guid id)
        {
            var visit = await _context.Visits.FindAsync(id);
            if (visit != null)
            {
                await _context.Entry(visit)
                    .Collection(i => i.Procedures).LoadAsync();
            }

            return visit;
        }

        public async Task<int> GetCountAsync(PaginationInfo paginationInfo)
        {
            var query = GetQuery(paginationInfo);

            return await query.CountAsync();
        }

        public async Task<IEnumerable<Visit>> GetManyAsync(PaginationInfo pagination)
        {
            var query = GetQuery(pagination);

            return await query.ToListAsync();
        }
  
        private IQueryable<Visit> GetQuery(PaginationInfo pagination)
        {
            var query = _context.Visits
                .Skip(pagination.Index)
                .Take(pagination.PageSize).AsQueryable();
            if (!string.IsNullOrEmpty(pagination.SearchString))
                query = query.Where(x => x.Physician.Contains(pagination.SearchString));

            return query.Skip(pagination.Index).Take(pagination.PageSize);
        }

    }
}
