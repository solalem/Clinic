using Clinic.Core.Appointments.Domain.Patients;
using Clinic.Shared.Domain.Abstractions.Model;

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

        public virtual Patient Patient { get; set; }

        private readonly List<Procedure> _procedures;
        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyCollection<Procedure> Procedures => _procedures;

        protected Visit() : base(Guid.NewGuid())
        {
            _procedures = new List<Procedure>();
        }

        public Visit(DateTimeOffset date, Guid patientId, String physician, String presentIllness) : this()
        {
            this.SetDate(date);
			this.SetPatientId(patientId);
			this.SetPhysician(physician);
			this.SetPresentIllness(presentIllness);
        }

        public void AddProcedure(Procedure newItem)
        {
            var existingId = _procedures.FirstOrDefault(x => x.Id == newItem.Id);
            if (existingId != null)
            {
                existingId.Description = newItem.Description;
                existingId.Name = newItem.Name;
            }
            else
                _procedures.Add(newItem);
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
