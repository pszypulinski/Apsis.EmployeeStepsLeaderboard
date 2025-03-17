using Apsis.EmployeeStepsLeaderboard.Application.Exceptions;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Members;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams;
using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Members.Commands.CreateMember
{
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, CreateMemberResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ITeamRepository _teamRepository;

        public CreateMemberCommandHandler(IMemberRepository memberRepository, ITeamRepository teamRepository)
        {
            _memberRepository = memberRepository;
            _teamRepository = teamRepository;
        }

        public async Task<CreateMemberResponse> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateMemberCommandValidator(_teamRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            var member = new Member
            {
                TeamId = request.TeamId,
                Name = request.Name
            };

            int memberId = await _memberRepository.AddMember(member);

            return new CreateMemberResponse
            {
                MemberId = memberId
            };
        }
    }
}
