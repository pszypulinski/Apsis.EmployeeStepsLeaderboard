using Apsis.EmployeeStepsLeaderboard.Api.FunctionalTests.Base;
using Apsis.EmployeeStepsLeaderboard.Application.Features.Teams.Commands.CreateTeam;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Apsis.EmployeeStepsLeaderboard.Api.FunctionalTests.Controllers
{
    //As a User
    //I want to be able to add teams
    //So that I can manage teams

    public class CreateTeamTests : BaseFunctionalTests
    {
        public CreateTeamTests(FunctionalTestsWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_ReturnCreated_WhenTeamNameIsValid()
        {
            // Arrange
            var requestUri = "/api/teams";
            var requestContent = new StringContent("\"Team 1\"", Encoding.UTF8, "application/json");

            // Act
            var response = await HttpClient.PostAsync(requestUri, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var createTeamResponse = JsonConvert.DeserializeObject<CreateTeamResponse>(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(createTeamResponse);
            Assert.Equal(1, createTeamResponse.TeamId);
        }

        [Fact]
        public async Task Should_ReturnBadRequest_WhenTeamNameIsEmpty()
        {
            // Arrange
            var requestUri = "/api/teams";
            var requestContent = new StringContent("\"\"", Encoding.UTF8, "application/json");

            // Act
            var response = await HttpClient.PostAsync(requestUri, requestContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }

    //As a User
    //I want to be able to delete teams
    //So that I can manage teams

    public class DeleteTeamTests : BaseFunctionalTests
    {
        public DeleteTeamTests(FunctionalTestsWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_ReturnNoContent_WhenExistingTeamIsDeleted()
        {
            // Arrange
            var teamId = await CreateTeam("Team 1");
            var requestUri = $"/api/teams/{teamId}";

            // Act
            var response = await HttpClient.DeleteAsync(requestUri);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Should_ReturnBadRequest_WhenTeamDoesNotExist()
        {
            // Arrange
            var requestUri = $"/api/teams/999";

            // Act
            var response = await HttpClient.DeleteAsync(requestUri);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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
}
