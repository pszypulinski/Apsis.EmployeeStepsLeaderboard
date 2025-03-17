using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Commands.DeleteMemberSteps
{
    public class DeleteMemberStepsCommand : IRequest
    {
        public int TeamId { get; set; }
        public int MemberId { get; set; }
    }
}
