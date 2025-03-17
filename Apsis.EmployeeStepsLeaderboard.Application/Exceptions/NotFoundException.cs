namespace Apsis.EmployeeStepsLeaderboard.Application.Exceptions
{
    /// <summary>
	/// This exception is raised to indicate that a requested resource has not been found.
	/// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">A message that describes an error.</param>
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
