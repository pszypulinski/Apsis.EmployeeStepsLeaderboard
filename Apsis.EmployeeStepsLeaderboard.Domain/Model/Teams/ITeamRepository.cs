using System.Collections.Immutable;

namespace Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams
{
    public interface ITeamRepository
    {
        Task<int> AddTeam(Team team);
        Task DeleteTeam(int teamId);
        Task<bool> DoesTeamExist(int teamId);
        Task<IImmutableList<Team>> GetTeams();
    }
}
