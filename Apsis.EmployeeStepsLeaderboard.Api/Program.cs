using Apsis.EmployeeStepsLeaderboard.Api;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.Run();

public partial class Program { } // used for functional tests
