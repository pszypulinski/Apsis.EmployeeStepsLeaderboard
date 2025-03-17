using Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams;
using FluentValidation;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
    {
        private readonly ITeamRepository _teamRepository;

        public DeleteTeamCommandValidator(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;

            RuleFor(c => c.TeamId)
                .GreaterThan(0)
                .MustAsync(TeamExists)
                .WithMessage(c => $"To delete a team with id: {c.TeamId}, that team must exist.");
        }

        private async Task<bool> TeamExists(int teamId, CancellationToken token)
        {
            return await _teamRepository.DoesTeamExist(teamId);
        }
    }
}
