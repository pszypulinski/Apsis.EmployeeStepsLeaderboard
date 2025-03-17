using Apsis.EmployeeStepsLeaderboard.Application.Exceptions;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Members;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Steps;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams;
using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Commands.CreateStepsIncrement
{
    public class CreateStepsIncrementCommandHandler : IRequestHandler<CreateStepsIncrementCommand, CreateStepsIncrementResponse>
    {
        private readonly IStepsIncrementRepository _stepsIncrementRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ITeamRepository _teamRepository;

        public CreateStepsIncrementCommandHandler(IStepsIncrementRepository stepsIncrementRepository, IMemberRepository memberRepository, ITeamRepository teamRepository)
        {
            _stepsIncrementRepository = stepsIncrementRepository;
            _memberRepository = memberRepository;
            _teamRepository = teamRepository;
        }

        public async Task<CreateStepsIncrementResponse> Handle(CreateStepsIncrementCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateStepsIncrementCommandValidator(_teamRepository, _memberRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            var stepsIncrement = new StepsIncrement
            {
                TeamId = request.TeamId,
                MemberId = request.MemberId,
                Value = request.Steps
            };

            int stepsIncrementId = await _stepsIncrementRepository.AddStepsIncrement(stepsIncrement);

            return new CreateStepsIncrementResponse
            {
                StepsIncrementId = stepsIncrementId
            };
        }
    }
}
