using Apsis.EmployeeStepsLeaderboard.Application.Exceptions;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Steps;
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace Apsis.EmployeeStepsLeaderboard.Persistence.Repositories
{
    public class StepsIncrementRepository : IStepsIncrementRepository
    {
        private readonly ConcurrentDictionary<int, StepsIncrement> _memberStepsIncrements;
        private int _stepsIncrementIdSequence;

        public StepsIncrementRepository()
        {
            _memberStepsIncrements = new ConcurrentDictionary<int, StepsIncrement>();
        }

        public Task<int> AddStepsIncrement(StepsIncrement stepsIncrement)
        {
            stepsIncrement.Id = GenerateId();

            if (!_memberStepsIncrements.TryAdd(stepsIncrement.Id, stepsIncrement))
            {
                throw new ConcurrencyException($"Team member's steps increment with id {stepsIncrement.Id} already exists.");
            }
            return Task.FromResult(stepsIncrement.Id);
        }

        public Task DeleteStepsIncrementsByMember(int memberId)
        {
            var keysToRemove = _memberStepsIncrements.Where(m => m.Value.MemberId == memberId).Select(m => m.Key);

            foreach (var key in keysToRemove)
            {
                _memberStepsIncrements.TryRemove(key, out _);
            }

            return Task.CompletedTask;
        }

        public Task DeleteStepsIncrementsByTeam(int teamId)
        {
            var keysToRemove = _memberStepsIncrements.Where(m => m.Value.TeamId == teamId).Select(m => m.Key);

            foreach (var key in keysToRemove)
            {
                _memberStepsIncrements.TryRemove(key, out _);
            }

            return Task.CompletedTask;
        }

        public Task<IImmutableList<StepsIncrement>> GetMemberStepsIncrements(int memberId)
        {
            IImmutableList<StepsIncrement> memberStepsIncrements = _memberStepsIncrements
                .Where(c => c.Value.MemberId == memberId)
                .Select(c => c.Value)
                .ToImmutableList();

            return Task.FromResult(memberStepsIncrements);
        }

        private int GenerateId()
        {
            return Interlocked.Increment(ref _stepsIncrementIdSequence);
        }
    }
}
