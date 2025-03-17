using System.Collections.Immutable;

namespace Apsis.EmployeeStepsLeaderboard.Domain.Model.Members
{
    public interface IMemberRepository
    {
        Task<int> AddMember(Member member);
        Task DeleteMember(int memberId);
        Task DeleteMembersByTeam(int teamId);
        Task<bool> DoesMemberBelongToTeam(int memberId, int teamId);
        Task<bool> DoesMemberExist(int memberId);
        Task<IImmutableList<Member>> GetMembers(int teamId);
    }
}
