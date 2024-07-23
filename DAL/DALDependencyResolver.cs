using DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    /// <summary>
    /// The DAL dependency resolver.
    /// </summary>
    public static class DALDependencyResolver
    {
        /// <summary>Registers the DAL dependencies.</summary>
        /// <param name="services">The services.</param>
        public static void RegisterDALDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
        }
    }
}
