using Application.Services;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interfaces;

namespace Application
{
    /// <summary>
    /// The application dependency resolver.
    /// </summary>
    public static class ApplicationDependencyResolver
    {
        /// <summary>Registers the application dependencies.</summary>
        /// <param name="services">The services.</param>
        public static void RegisterApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddTransient<IAwardsService, AwardsService>();
        }
    }
}
