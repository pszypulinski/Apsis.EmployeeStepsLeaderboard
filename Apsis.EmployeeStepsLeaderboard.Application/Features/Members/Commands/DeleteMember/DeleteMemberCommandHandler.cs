using Apsis.EmployeeStepsLeaderboard.Application.Exceptions;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Members;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Steps;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams;
using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Members.Commands.DeleteMember
{
    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IStepsIncrementRepository _stepsIncrementRepository;
        private readonly ITeamRepository _teamRepository;

        public DeleteMemberCommandHandler(IMemberRepository memberRepository, IStepsIncrementRepository stepsIncrementRepository, ITeamRepository teamRepository)
        {
            _memberRepository = memberRepository;
            _stepsIncrementRepository = stepsIncrementRepository;
            _teamRepository = teamRepository;
        }

        public async Task Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteMemberCommandValidator(_teamRepository, _memberRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            await _stepsIncrementRepository.DeleteStepsIncrementsByMember(request.MemberId);
            await _memberRepository.DeleteMember(request.MemberId);
        }
    }
}
