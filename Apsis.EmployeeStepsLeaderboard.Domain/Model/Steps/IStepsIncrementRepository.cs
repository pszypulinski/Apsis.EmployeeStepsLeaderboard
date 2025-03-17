using System.Collections.Immutable;

namespace Apsis.EmployeeStepsLeaderboard.Domain.Model.Steps
{
    public interface IStepsIncrementRepository
    {
        Task<int> AddStepsIncrement(StepsIncrement stepsIncrement);
        Task DeleteStepsIncrementsByMember(int memberId);
        Task DeleteStepsIncrementsByTeam(int teamId);
        Task<IImmutableList<StepsIncrement>> GetMemberStepsIncrements(int memberId);
    }
}
