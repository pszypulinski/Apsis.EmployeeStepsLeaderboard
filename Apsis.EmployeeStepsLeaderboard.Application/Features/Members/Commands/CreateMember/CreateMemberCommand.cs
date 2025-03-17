using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Members.Commands.CreateMember
{
    public class CreateMemberCommand : IRequest<CreateMemberResponse>
    {
        public int TeamId { get; set; }
        public required string Name { get; set; }
    }
}
