using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommand : IRequest
    {
        public int TeamId { get; set; }
    }
}
