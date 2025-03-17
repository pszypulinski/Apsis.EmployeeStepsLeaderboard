using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Members.Commands.DeleteMember
{
    public class DeleteMemberCommand : IRequest
    {
        public int TeamId { get; set; }
        public int MemberId { get; set; }
    }
}
