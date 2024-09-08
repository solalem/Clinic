namespace Clinic.Shared
{
    /// <summary>
    /// Common Exception for domain level exceptions.
    /// </summary>
    public class DomainException : Exception
    {
        protected class ErrorCodes
        {
            public const string NullOrEmpty = "NULL_OR_EMPTY";
            public const string OutOfRange = "OUT_OF_RANGE";
            public const string Duplicate = "DUPLICATE";
            public const string UnAuthorized = "UNAUTHORIZED";
            public const string Validation = "VALIDATION";
            public const string InvalidState = "INVALID_STATE";
        }
        /// <summary>
        /// used as a key to get the actual error message. 
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// The module where the exception has originated. Full name is recommended 
        /// </summary>
        public string SourceModule { get; }

        /// <summary>
        /// The specific method where the exception is raised.
        /// </summary>
        public string SourceMethod { get; }

        /// <summary>
        /// Parameters to be inserted in the error message. 
        /// </summary>
        public object[] Parameters { get; }


        public DomainException(
            string code,
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base("Domain Exception")
        {
            Code = code;
            SourceMethod = sourceMethod;
            SourceModule = sourceModule;
            Parameters = values;

        }

        public DomainException(
            Exception innerException,
            string code,
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base("Domain Exception", innerException)
        {
            Code = code;
            SourceMethod = sourceMethod;
            SourceModule = sourceModule;
            Parameters = values;
        }

    }
}
