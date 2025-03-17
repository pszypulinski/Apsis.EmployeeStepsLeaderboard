using Apsis.EmployeeStepsLeaderboard.Application.Exceptions;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams;
using MediatR;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, CreateTeamResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<CreateTeamResponse> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTeamCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            var team = new Team
            {
                Name = request.Name
            };

            var teamId = await _teamRepository.AddTeam(team);

            return new CreateTeamResponse
            {
                TeamId = teamId
            };
        }
    }
}
