using Apsis.EmployeeStepsLeaderboard.Domain.Model.Members;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Steps;
using Apsis.EmployeeStepsLeaderboard.Domain.Model.Teams;
using Apsis.EmployeeStepsLeaderboard.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Apsis.EmployeeStepsLeaderboard.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddSingleton<ITeamRepository, TeamRepository>();
            services.AddSingleton<IMemberRepository, MemberRepository>();
            services.AddSingleton<IStepsIncrementRepository, StepsIncrementRepository>();

            return services;
        }
    }
}
