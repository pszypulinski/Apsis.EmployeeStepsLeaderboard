using Apsis.EmployeeStepsLeaderboard.Application.Exceptions;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Members;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Steps;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams;
using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Commands.DeleteMemberSteps
{
    public class DeleteMemberStepsCommandHandler : IRequestHandler<DeleteMemberStepsCommand>
    {
        private readonly IStepsIncrementRepository _stepsIncrementRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ITeamRepository _teamRepository;

        public DeleteMemberStepsCommandHandler(IStepsIncrementRepository stepsIncrementRepository, IMemberRepository memberRepository, ITeamRepository teamRepository)
        {
            _stepsIncrementRepository = stepsIncrementRepository;
            _memberRepository = memberRepository;
            _teamRepository = teamRepository;
        }

        public async Task Handle(DeleteMemberStepsCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteMemberStepsCommandValidator(_teamRepository, _memberRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            await _stepsIncrementRepository.DeleteStepsIncrementsByMember(request.MemberId);
        }
    }
}
