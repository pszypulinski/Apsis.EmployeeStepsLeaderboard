using Microsoft.AspNetCore.Mvc.Testing;

namespace Apsis.EmployeeStepsLeaderboard.Api.FunctionalTests.Base
{
    public class FunctionalTestsWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
        }
    }
}
