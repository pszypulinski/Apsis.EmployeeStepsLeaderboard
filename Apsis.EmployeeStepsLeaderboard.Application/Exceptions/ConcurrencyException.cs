namespace Apsis.EmployeeStepsLeaderboard.Application.Exceptions
{
    /// <summary>
	/// This exception is raised to indicate a concurrency conflict.
	/// </summary>
    public class ConcurrencyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
        /// </summary>
        /// <param name="message">A message that describes an error.</param>
        public ConcurrencyException(string message)
            : base(message)
        {
        }
    }
}
