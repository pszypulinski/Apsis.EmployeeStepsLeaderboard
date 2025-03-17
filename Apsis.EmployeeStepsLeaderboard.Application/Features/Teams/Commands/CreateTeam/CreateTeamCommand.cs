using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Teams.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest<CreateTeamResponse>
    {
        public required string Name { get; set; }
    }
}
