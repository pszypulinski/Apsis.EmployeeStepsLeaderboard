using Apsis.EmployeeStepsLeaderboard.Application.Exceptions;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams;
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace Apsis.EmployeeStepsLeaderboard.Persistence.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ConcurrentDictionary<int, Team> _teams;
        private int _teamIdSequence;

        public TeamRepository()
        {
            _teams = new ConcurrentDictionary<int, Team>();
        }

        public Task<int> AddTeam(Team team)
        {
            team.Id = GenerateId();

            if (!_teams.TryAdd(team.Id, team))
            {
                throw new ConcurrencyException($"Team with id {team.Id} already exists.");
            }

            return Task.FromResult(team.Id);
        }

        public Task DeleteTeam(int teamId)
        {
            if (!_teams.TryRemove(teamId, out _))
            {
                throw new NotFoundException($"Team with id: {teamId} has not be found.");
            }

            return Task.CompletedTask;
        }

        public Task<bool> DoesTeamExist(int teamId)
        {
            return Task.FromResult(_teams.ContainsKey(teamId));
        }

        public Task<IImmutableList<Team>> GetTeams()
        {
            IImmutableList<Team> teams = _teams.Values.ToImmutableList();

            return Task.FromResult(teams);
        }

        private int GenerateId()
        {
            return Interlocked.Increment(ref _teamIdSequence);
        }
    }
}
