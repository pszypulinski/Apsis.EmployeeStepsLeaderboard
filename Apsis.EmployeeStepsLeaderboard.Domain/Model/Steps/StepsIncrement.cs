namespace Apsis.EmployeeStepsLeaderboard.Domain.Model.Steps
{
    public class StepsIncrement
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int MemberId { get; set; }
        public int Value { get; set; }
    }
}
