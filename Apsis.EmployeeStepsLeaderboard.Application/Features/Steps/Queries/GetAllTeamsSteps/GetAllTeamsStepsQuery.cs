using MediatR;
using System.Collections.Immutable;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Queries.GetAllTeamsSteps
{
    public class GetAllTeamsStepsQuery : IRequest<IImmutableList<GetAllTeamsStepsResponse>>
    {
    }
}
