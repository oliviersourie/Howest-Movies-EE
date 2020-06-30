using Howest_Movies_EE_DAL.Models;
using Howest_Movies_EE_DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Howest_Movies_EE_DAL.Extensions
{
    public static class ServicesExtensions
    {
        public static void InitRepositories(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<MoviesContext>(options =>
                            options.UseSqlServer(config.GetDatabaseString("MovieDb")));
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IGenresRepository, GenresRepository>();
            services.AddScoped<IPersonsRepository, PersonsRepository>();
        }
    }
}
