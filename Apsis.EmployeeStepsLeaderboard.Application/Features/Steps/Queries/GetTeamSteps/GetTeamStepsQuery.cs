using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Queries.GetTeamSteps
{
    public class GetTeamStepsQuery : IRequest<GetTeamStepsResponse>
    {
        public int TeamId { get; set; }
    }
}
