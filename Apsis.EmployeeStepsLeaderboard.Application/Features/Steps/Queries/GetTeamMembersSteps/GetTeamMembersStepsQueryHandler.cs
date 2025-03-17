using Apsis.EmployeeStepsLeaderboard.Domain.Model.Members;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Steps;
using MediatR;
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Queries.GetTeamMembersSteps
{
    public class GetTeamMembersStepsQueryHandler : IRequestHandler<GetTeamMembersStepsQuery, IImmutableList<GetTeamMembersStepsResponse>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IStepsIncrementRepository _stepsIncrementRepository;

        public GetTeamMembersStepsQueryHandler(IMemberRepository memberRepository, IStepsIncrementRepository stepsIncrementRepository)
        {
            _memberRepository = memberRepository;
            _stepsIncrementRepository = stepsIncrementRepository;
        }

        public async Task<IImmutableList<GetTeamMembersStepsResponse>> Handle(GetTeamMembersStepsQuery request, CancellationToken cancellationToken)
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

            var teamMembersStepsResponses = teamMembersStepsIncrements
                .GroupBy(c => c.MemberId)
                .Select(g =>
                {
                    return new GetTeamMembersStepsResponse
                    {
                        MemberId = g.Key,
                        Steps = g.Sum(c => c.Value)
                    };
                })
                .ToList();

            foreach (var memberId in teamMemberIds)
            {
                if (!teamMembersStepsResponses.Any(r => r.MemberId == memberId))
                {
                    var teamMembersStepsResponse = new GetTeamMembersStepsResponse
                    {
                        MemberId = memberId,
                        Steps = 0
                    };

                    teamMembersStepsResponses.Add(teamMembersStepsResponse);
                }
            }

            return teamMembersStepsResponses.ToImmutableList();
        }
    }
}
