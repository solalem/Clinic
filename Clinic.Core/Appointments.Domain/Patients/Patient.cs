using Clinic.SharedKernel.Domain.Abstractions.Model;

namespace Clinic.Core.Appointments.Domain.Patients
{
    public class Patient : Entity<Guid>, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public String CardNumber { get; private set; }
        public void SetCardNumber(String cardNumber)
        {
            this.CardNumber = cardNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        public String FullName { get; private set; }
        public void SetFullName(String fullName)
        {
            this.FullName = fullName;
        }

        /// <summary>
        /// 
        /// </summary>
        public String Gender { get; private set; }
        public void SetGender(String gender)
        {
            this.Gender = gender;
        }

        /// <summary>
        /// 
        /// </summary>
        public String PhoneNumber { get; private set; }
        public void SetPhoneNumber(String phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DateOfBirth { get; private set; }
        public void SetDateOfBirth(DateTime? dateOfBirth)
        {
            this.DateOfBirth = dateOfBirth;
        }

        /// <summary>
        /// 
        /// </summary>
        public String Email { get; private set; }
        public void SetEmail(String email)
        {
            this.Email = email;
        }


        protected Patient() : base(Guid.NewGuid())
        {
        }

        public Patient(String cardNumber, String fullName, String gender, String phoneNumber, DateTime? dateOfBirth, String email) : this()
        {
            this.SetCardNumber(cardNumber);
			this.SetFullName(fullName);
			this.SetGender(gender);
			this.SetPhoneNumber(phoneNumber);
			this.SetDateOfBirth(dateOfBirth);
			this.SetEmail(email);
			

            // TODO: Add the StartedDomainEvent to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            // AddOrderStartedDomainEvent(userId, cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);
        }


    }
}
