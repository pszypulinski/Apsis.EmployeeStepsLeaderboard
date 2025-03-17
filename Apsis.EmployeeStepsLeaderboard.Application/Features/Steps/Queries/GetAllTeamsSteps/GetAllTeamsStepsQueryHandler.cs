using Apsis.EmployeeStepsLeaderboard.Domain.Model.Members;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Steps;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams;
using MediatR;
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Queries.GetAllTeamsSteps
{
    public class GetAllTeamsStepsQueryHandler : IRequestHandler<GetAllTeamsStepsQuery, IImmutableList<GetAllTeamsStepsResponse>>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IStepsIncrementRepository _stepsIncrementRepository;

        public GetAllTeamsStepsQueryHandler(ITeamRepository teamRepository, IMemberRepository memberRepository, IStepsIncrementRepository stepsIncrementRepository)
        {
            _teamRepository = teamRepository;
            _memberRepository = memberRepository;
            _stepsIncrementRepository = stepsIncrementRepository;
        }

        public async Task<IImmutableList<GetAllTeamsStepsResponse>> Handle(GetAllTeamsStepsQuery request, CancellationToken cancellationToken)
        {
            var teams = await _teamRepository.GetTeams();
            var teamIds = teams.Select(t => t.Id);
            var teamSteps = new ConcurrentBag<GetAllTeamsStepsResponse>();

            await Parallel.ForEachAsync(teamIds, async (teamId, _) =>
            {
                var teamMembers = await _memberRepository.GetMembers(teamId);
                var teamMemberIds = teamMembers.Select(m => m.Id);
                var teamMembersStepsIncrements = new ConcurrentBag<StepsIncrement>();

                await Parallel.ForEachAsync(teamMemberIds, async (memberId, _) =>
                {
                    var teamMemberStepsIncrements = await _stepsIncrementRepository.GetMemberStepsIncrements(memberId);

                    foreach (var stepsIncrement in teamMemberStepsIncrements)
                    {
                        teamMembersStepsIncrements.Add(stepsIncrement);
                    }
                });

                var teamStepsDto = new GetAllTeamsStepsResponse
                {
                    TeamId = teamId,
                    Steps = teamMembersStepsIncrements.Sum(c => c.Value)
                };

                teamSteps.Add(teamStepsDto);
            });

            return teamSteps.ToImmutableList();
        }
    }
}
