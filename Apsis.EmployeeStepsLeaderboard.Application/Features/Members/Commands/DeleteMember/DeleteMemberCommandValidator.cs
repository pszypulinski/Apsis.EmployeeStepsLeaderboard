using Apsis.EmployeeStepsLeaderboard.Application.Features.Members.Commands.DeleteMember;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Members;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams;
using FluentValidation;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Members.Commands.DeleteMember
{
    public class DeleteMemberCommandValidator : AbstractValidator<DeleteMemberCommand>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMemberRepository _memberRepository;

        public DeleteMemberCommandValidator(ITeamRepository teamRepository, IMemberRepository memberRepository)
        {
            _teamRepository = teamRepository;
            _memberRepository = memberRepository;

            RuleFor(c => c.TeamId)
                .GreaterThan(0)
                .MustAsync(TeamExists)
                .WithMessage(c => $"To delete a team member from a team with id: {c.TeamId}, that team must exist.");

            RuleFor(c => c.MemberId)
                .GreaterThan(0)
                .MustAsync(MemberExists)
                .WithMessage(c => $"To delete a team member with id: {c.MemberId}, that member must exist.");

            RuleFor(c => c)
                .MustAsync(MemberBelongsToTeam)
                .WithMessage(c => $"To delete a team member with id: {c.MemberId} and a team with id: {c.TeamId}, that member must belong to that team.");
        }

        private async Task<bool> TeamExists(int teamId, CancellationToken token)
        {
            return await _teamRepository.DoesTeamExist(teamId);
        }

        private async Task<bool> MemberExists(int memberId, CancellationToken token)
        {
            return await _memberRepository.DoesMemberExist(memberId);
        }

        private async Task<bool> MemberBelongsToTeam(DeleteMemberCommand command, CancellationToken token)
        {
            return await _memberRepository.DoesMemberBelongToTeam(command.MemberId, command.TeamId);
        }
    }
}
