namespace Apsis.EmployeeStepsLeaderboard.Api.FunctionalTests.Base
{
    public class BaseFunctionalTests : IClassFixture<FunctionalTestsWebApplicationFactory>
    {
        protected HttpClient HttpClient { get; }

        public BaseFunctionalTests(FunctionalTestsWebApplicationFactory factory)
        {
            HttpClient = factory.CreateClient();
        }
    }
}
