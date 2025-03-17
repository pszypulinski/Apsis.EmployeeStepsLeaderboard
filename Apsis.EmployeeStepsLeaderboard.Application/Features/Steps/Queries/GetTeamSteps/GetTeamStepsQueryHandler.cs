using Apsis.EmployeeStepsLeaderboard.Domain.Model.Members;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Steps;
using MediatR;
using System.Collections.Concurrent;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Queries.GetTeamSteps
{
    public class GetTeamStepsQueryHandler : IRequestHandler<GetTeamStepsQuery, GetTeamStepsResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IStepsIncrementRepository _stepsIncrementRepository;

        public GetTeamStepsQueryHandler(IMemberRepository memberRepository, IStepsIncrementRepository stepsIncrementRepository)
        {
            _memberRepository = memberRepository;
            _stepsIncrementRepository = stepsIncrementRepository;
        }

        public async Task<GetTeamStepsResponse> Handle(GetTeamStepsQuery request, CancellationToken cancellationToken)
        {
            var teamMembers = await _memberRepository.GetMembers(request.TeamId);
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

            var teamStepsDto = new GetTeamStepsResponse
            {
                TeamId = request.TeamId,
                Steps = teamMembersStepsIncrements.Sum(c => c.Value)
            };

            return teamStepsDto;
        }
    }
}
