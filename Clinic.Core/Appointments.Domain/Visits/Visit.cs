using Clinic.Shared.Model;
using Clinic.Core.Appointments.Domain.Patients;

namespace Clinic.Core.Appointments.Domain.Visits
{
    public class Visit : Entity<Guid>, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset Date { get; private set; }
        public void SetDate(DateTimeOffset date)
        {
            this.Date = date;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid PatientId { get; private set; }
        public void SetPatientId(Guid patientId)
        {
            this.PatientId = patientId;
        }
        public Patient? Patient { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public String Physician { get; private set; }
        public void SetPhysician(String physician)
        {
            this.Physician = physician;
        }

        /// <summary>
        /// 
        /// </summary>
        public String PresentIllness { get; private set; }
        public void SetPresentIllness(String presentIllness)
        {
            this.PresentIllness = presentIllness;
        }

        private readonly List<Procedure> _procedures;
        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyCollection<Procedure> Procedures => _procedures;

        private readonly List<Investigation> _iInvestigation;
        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyCollection<Investigation> Investigations => _iInvestigation;

        protected Visit() : base(Guid.NewGuid())
        {
            _procedures = new List<Procedure>();
            _iInvestigation = new List<Investigation>();
        }

        public Visit(Guid patientId, String physician, String presentIllness) : this()
        {
            this.SetDate(DateTimeOffset.Now);
			this.SetPatientId(patientId);
			this.SetPhysician(physician);
			this.SetPresentIllness(presentIllness);
			

            // TODO: Add the StartedDomainEvent to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            // AddOrderStartedDomainEvent(userId, cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);
        }

        public void AddProcedure(Procedure newItem)
        {
            _procedures.Add(newItem);
            // TODO: Add domain logic, e.g change discount rate if item counts exceed some value
        }

        public Procedure UpdateProcedure(int id, Procedure item) 
        {
            var existing = _procedures.Where(o => o.Id == id).SingleOrDefault();
            if (existing != null)
            {
                //TODO: update values
                existing.Name = item.Name;
				existing.Description = item.Description;
				
                // ...
            }
            return existing;
        }

        public void RemoveProcedure(int id)
        {
            var existing = _procedures.Where(o => o.Id == id).SingleOrDefault();
            if (existing != null)
            {
                _procedures.Remove(existing);
            }
        }

        /// <summary>
        /// Add Procedures
        /// </summary>
        public void AddProcedures(Procedure[] items)
        {
            _procedures.Clear();
            _procedures.AddRange(items);
        }

        /// <summary>
        /// Remove Procedures
        /// </summary>
        public void RemoveAllProcedures()
        {
            _procedures.Clear();
        }

        public void AddInvestigation(Investigation newItem)
        {
            _iInvestigation.Add(newItem);
            // TODO: Add domain logic, e.g change discount rate if item counts exceed some value
        }

        public Investigation UpdateInvestigation(int id, Investigation item) 
        {
            var existing = _iInvestigation.Where(o => o.Id == id).SingleOrDefault();
            if (existing != null)
            {
                //TODO: update values
                existing.Name = item.Name;
				existing.Description = item.Description;
				
                // ...
            }
            return existing;
        }

        public void RemoveInvestigation(int id)
        {
            var existing = _iInvestigation.Where(o => o.Id == id).SingleOrDefault();
            if (existing != null)
            {
                _iInvestigation.Remove(existing);
            }
        }

        /// <summary>
        /// Add Investigations
        /// </summary>
        public void AddInvestigations(Investigation[] items)
        {
            _iInvestigation.Clear();
            _iInvestigation.AddRange(items);
        }

        /// <summary>
        /// Remove Investigations
        /// </summary>
        public void RemoveAllInvestigations()
        {
            _iInvestigation.Clear();
        }


    }
}
