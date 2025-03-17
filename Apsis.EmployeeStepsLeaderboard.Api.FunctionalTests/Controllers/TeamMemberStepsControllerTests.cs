using Apsis.EmployeeStepsLeaderboard.Api.FunctionalTests.Base;

namespace Apsis.EmployeeStepsLeaderboard.Api.FunctionalTests.Controllers
{
    //As a User
    //I want to list all counters in a team
    //So that I can see how much each team member have walked

    public class GetTeamMembersStepsTests : BaseFunctionalTests
    {
        public GetTeamMembersStepsTests(FunctionalTestsWebApplicationFactory factory) : base(factory)
        {
        }


    }

    //As a User
    //I want to be able to increment the value of a stored counter
    //So that I can get steps counted towards my team's score

    public class CreateTeamMemberStepsIncrementTests : BaseFunctionalTests
    {
        public CreateTeamMemberStepsIncrementTests(FunctionalTestsWebApplicationFactory factory) : base(factory)
        {
        }


    }

    //As a User
    //I want to be able to delete counters
    //So that I can manage team member's counters

    public class DeleteTeamMemberStepsIncrementsTests : BaseFunctionalTests
    {
        public DeleteTeamMemberStepsIncrementsTests(FunctionalTestsWebApplicationFactory factory) : base(factory)
        {
        }


    }
}
