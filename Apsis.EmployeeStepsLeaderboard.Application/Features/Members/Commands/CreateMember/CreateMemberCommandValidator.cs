using Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams;
using FluentValidation;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Members.Commands.CreateMember
{
    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {
        private readonly ITeamRepository _teamRepository;

        public CreateMemberCommandValidator(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;

            RuleFor(c => c.TeamId)
                .GreaterThan(0)
                .MustAsync(TeamExists)
                .WithMessage(c => $"To create a team member of a team with id: {c.TeamId}, that team must exist.");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Missing team member's name.");
        }

        private async Task<bool> TeamExists(int teamId, CancellationToken token)
        {
            return await _teamRepository.DoesTeamExist(teamId);
        }
    }
}
