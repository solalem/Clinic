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
        public DateTimeOffset? DateOfBirth { get; private set; }
        public void SetDateOfBirth(DateTimeOffset? dateOfBirth)
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

        /// <summary>
        /// 
        /// </summary>
        public String? City { get; private set; }
        public void SetCity(String city)
        {
            this.City = city;
        }

        /// <summary>
        /// 
        /// </summary>
        public String? MedicalHistory { get; private set; }
        public void SetMedicalHistory(String medicalHistory)
        {
            this.MedicalHistory = medicalHistory;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? RegisterationDate { get; private set; }
        public void SetRegisterationDate(DateTimeOffset? registerationDate)
        {
            this.RegisterationDate = registerationDate;
        }

        protected Patient() : base(Guid.NewGuid())
        {
        }

        public Patient(String cardNumber, String fullName, String gender, String phoneNumber, DateTimeOffset? dateOfBirth, String email, String city, String medicalHistory) : this()
        {
            this.SetCardNumber(cardNumber);
			this.SetFullName(fullName);
			this.SetGender(gender);
			this.SetPhoneNumber(phoneNumber);
			this.SetDateOfBirth(dateOfBirth);
			this.SetEmail(email);
			this.SetCity(city);
            this.SetMedicalHistory(medicalHistory);

            SetRegisterationDate(DateTimeOffset.UtcNow);
        }


    }
}
