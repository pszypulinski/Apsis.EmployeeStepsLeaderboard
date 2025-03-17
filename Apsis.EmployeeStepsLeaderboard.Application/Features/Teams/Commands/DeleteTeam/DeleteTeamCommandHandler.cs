using Apsis.EmployeeStepsLeaderboard.Application.Exceptions;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Members;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Steps;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams;
using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IStepsIncrementRepository _stepsIncrementRepository;

        public DeleteTeamCommandHandler(ITeamRepository teamRepository, IMemberRepository memberRepository, IStepsIncrementRepository stepsIncrementRepository)
        {
            _teamRepository = teamRepository;
            _memberRepository = memberRepository;
            _stepsIncrementRepository = stepsIncrementRepository;
        }

        public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteTeamCommandValidator(_teamRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            await _stepsIncrementRepository.DeleteStepsIncrementsByTeam(request.TeamId);
            await _memberRepository.DeleteMembersByTeam(request.TeamId);
            await _teamRepository.DeleteTeam(request.TeamId);
        }
    }
}
