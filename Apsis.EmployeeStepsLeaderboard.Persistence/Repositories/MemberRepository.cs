using Apsis.EmployeeStepsLeaderboard.Application.Exceptions;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Members;
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace Apsis.EmployeeStepsLeaderboard.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ConcurrentDictionary<int, Member> _members;
        private int _memberIdSequence;

        public MemberRepository()
        {
            _members = new ConcurrentDictionary<int, Member>();
        }

        public Task<int> AddMember(Member member)
        {
            member.Id = GenerateId();

            if (!_members.TryAdd(member.Id, member))
            {
                throw new ConcurrencyException($"Team member with id {member.Id} already exists.");
            }

            return Task.FromResult(member.Id);
        }

        public Task DeleteMember(int memberId)
        {
            if (!_members.TryRemove(memberId, out _))
            {
                throw new NotFoundException($"Team member with id: {memberId} has not be found.");
            }

            return Task.CompletedTask;
        }

        public Task DeleteMembersByTeam(int teamId)
        {
            var keysToRemove = _members.Where(m => m.Value.TeamId == teamId).Select(m => m.Key);

            foreach (var key in keysToRemove)
            {
                _members.TryRemove(key, out _);
            }

            return Task.CompletedTask;
        }

        public Task<bool> DoesMemberBelongToTeam(int memberId, int teamId)
        {
            if (!_members.TryGetValue(memberId, out Member member))
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(member.TeamId == teamId);
        }

        public Task<bool> DoesMemberExist(int memberId)
        {
            return Task.FromResult(_members.ContainsKey(memberId));
        }

        public Task<IImmutableList<Member>> GetMembers(int teamId)
        {
            IImmutableList<Member> members = _members.Values
                .Where(m => m.TeamId == teamId)
                .ToImmutableList();

            return Task.FromResult(members);
        }

        private int GenerateId()
        {
            return Interlocked.Increment(ref _memberIdSequence);
        }
    }
}
