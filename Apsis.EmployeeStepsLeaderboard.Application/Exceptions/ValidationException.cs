using FluentValidation.Results;

namespace Apsis.EmployeeStepsLeaderboard.Application.Exceptions
{
    /// <summary>
    /// This exception is raised to indicate that a request has failed validation rules.
    /// </summary>
    public class ValidationException : Exception
    {
        public List<string> ValidationErrors { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="validationResult">A result of a validation.</param>
        public ValidationException(ValidationResult validationResult)
        {
            ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        }
    }
}
