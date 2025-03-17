namespace Apsis.EmployeeStepsLeaderboard.Domain.Model.Members
{
    public class Member
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public required string Name { get; set; }
    }
}
