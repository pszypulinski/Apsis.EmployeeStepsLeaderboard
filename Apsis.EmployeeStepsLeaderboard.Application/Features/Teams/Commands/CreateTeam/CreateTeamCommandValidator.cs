using FluentValidation;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Missing team's name.");
        }
    }
}
