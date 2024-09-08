using Clinic.Shared.Model;

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

        protected Visit() : base(Guid.NewGuid())
        {
            _procedures = new List<Procedure>();
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


    }
}
