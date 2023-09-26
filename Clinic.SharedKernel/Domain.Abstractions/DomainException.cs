using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Clinic.SharedKernel.Domain.Abstractions.Exceptions
{
    public interface IExceptionSource
    {
        string Code { get; }
        string SourceModule { get; }
        string SourceMethod { get; }
        object[] Parameters { get; }
    }
    /// <summary>
    /// Common Exception for domain level exceptions.
    /// </summary>
    public class DomainException : Exception, IExceptionSource
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
        /// The module where the exception has originated. Full name is recommended <example>SharedKernel.Registration.Application.Organization.Commands.RegistrationCommand</example>  handler
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
            this.Code = code;
            this.SourceMethod = sourceMethod;
            this.SourceModule = sourceModule;
            this.Parameters = values;

        }

        public DomainException(
            Exception innerException,
            string code,
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base("Domain Exception", innerException)
        {
            this.Code = code;
            this.SourceMethod = sourceMethod;
            this.SourceModule = sourceModule;
            this.Parameters = values;
        }


    }

    /// <summary>
    /// Exception for duplicates, usually when renaming , or creating a new one if the name is already taken.
    /// HTTP Status Code = 409
    /// </summary>
    public class DuplicateException : DomainException
    {

        public DuplicateException(
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base(ErrorCodes.Duplicate, sourceModule, sourceMethod, values)
        { }

        public DuplicateException(
            Exception innerException,
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base(innerException, ErrorCodes.Duplicate, sourceModule, sourceMethod, values)
        { }


    }

    /// <summary>
    /// Exception when GetById is returning null.
    /// HTTP Status Code = 404
    /// </summary>
    public class NotFoundException : DomainException
    {

        public NotFoundException(
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base(ErrorCodes.NullOrEmpty, sourceModule, sourceMethod, values)
        { }

        public NotFoundException(
            Exception innerException,
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base(innerException, ErrorCodes.NullOrEmpty, sourceModule, sourceMethod, values)
        { }
    }

    public class OutOfRangeException : DomainException
    {

        public OutOfRangeException(
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base(ErrorCodes.OutOfRange, sourceModule, sourceMethod, values)
        { }

        public OutOfRangeException(
            Exception innerException,
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base(innerException, ErrorCodes.OutOfRange, sourceModule, sourceMethod, values)
        { }
    }

    /// <summary>
    /// When 
    /// </summary>
    public class AuthorizationException : DomainException
    {

        public AuthorizationException(
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base(ErrorCodes.UnAuthorized, sourceModule, sourceMethod, values)
        { }

        public AuthorizationException(
            Exception innerException,
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base(innerException, ErrorCodes.UnAuthorized, sourceModule, sourceMethod, values)
        { }
    }
    public class ValidationException : DomainException
    {

        public ValidationException(
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base(ErrorCodes.Validation, sourceModule, sourceMethod, values)
        { }

        public ValidationException(
            Exception innerException,
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base(innerException, ErrorCodes.Validation, sourceModule, sourceMethod, values)
        { }
    }

    public class InvalidException : DomainException
    {

        public InvalidException(
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base(ErrorCodes.InvalidState, sourceModule, sourceMethod, values)
        { }

        public InvalidException(
            Exception innerException,
            string sourceModule,
            string sourceMethod,
            params object[] values)
            : base(innerException, ErrorCodes.InvalidState, sourceModule, sourceMethod, values)
        { }
    }
}
