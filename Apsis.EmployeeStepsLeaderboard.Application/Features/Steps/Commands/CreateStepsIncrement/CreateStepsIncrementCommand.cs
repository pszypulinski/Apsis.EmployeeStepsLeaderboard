using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Commands.CreateStepsIncrement
{
    public class CreateStepsIncrementCommand : IRequest<CreateStepsIncrementResponse>
    {
        public int TeamId { get; set; }
        public int MemberId { get; set; }
        public int Steps { get; set; }
    }
}
