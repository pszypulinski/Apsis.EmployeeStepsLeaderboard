using Apsis.EmployeeStepsLeaderboard.Api.FunctionalTests.Base;
using Apsis.EmployeeStepsLeaderboard.Application.Features.Steps.Queries.GetAllTeamsSteps;
using Apsis.EmployeeStepsLeaderboard.Application.Features.Teams.Commands.CreateTeam;
using Newtonsoft.Json;
using System.Net;
using System.Text;
namespace Apsis.EmployeeStepsLeaderboard.Api.FunctionalTests.Controllers
{
    //As a User
    //I want to list all teams and see their step counts
    //So that I can compare my team with the others

    public class GetAllTeamsStepsTests : BaseFunctionalTests
    {
        public GetAllTeamsStepsTests(FunctionalTestsWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_ReturnNotFound_WhenNoTeamsExist()
        {
            // Arrange
            var requestUri = "/api/teams/steps";

            // Act
            var response = await HttpClient.GetAsync(requestUri);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Should_ReturnOk_WhenAnyTeamExists()
        {
            // Arrange
            await CreateTeam("Team 1");
            var requestUri = "/api/teams/steps";

            // Act
            var response = await HttpClient.GetAsync(requestUri);
            var responseContent = await response.Content.ReadAsStringAsync();
            var getAllTeamsStepsResponse = JsonConvert.DeserializeObject<IEnumerable<GetAllTeamsStepsResponse>>(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(getAllTeamsStepsResponse);
            Assert.Single(getAllTeamsStepsResponse);
        }

        private async Task<int> CreateTeam(string teamName)
        {
            var requestUri = "/api/teams";
            var requestContent = new StringContent($"\"{teamName}\"", Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(requestUri, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var createTeamResponse = JsonConvert.DeserializeObject<CreateTeamResponse>(responseContent);

            return createTeamResponse.TeamId;
        }
    }

    //As a User
    //I want to get the current total steps taken by a team
    //So that I can see how much that team have walked in total

    public class GetTeamStepsTests : BaseFunctionalTests
    {
        public GetTeamStepsTests(FunctionalTestsWebApplicationFactory factory) : base(factory)
        {
        }
    }
}
