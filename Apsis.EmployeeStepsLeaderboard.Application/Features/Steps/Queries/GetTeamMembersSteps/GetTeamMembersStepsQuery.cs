using MediatR;
using System.Collections.Immutable;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Queries.GetTeamMembersSteps
{
    public class GetTeamMembersStepsQuery : IRequest<IImmutableList<GetTeamMembersStepsResponse>>
    {
        public int TeamId { get; set; }
    }
}
