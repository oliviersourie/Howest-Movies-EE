using Microsoft.Extensions.Configuration;

namespace Howest_Movies_EE_DAL.Extensions
{
    public static class ConfigurationExtensions
    {
        //public static string GetMovieDataBaseString(this IConfiguration config)
        //{
        //    return config.GetDatabaseString("MovieDb");
        //}

        public static string GetDatabaseString(this IConfiguration config, string dbString)
        {
            return config[$"ConnectionStrings:{dbString}"];
        }

        public static string GetApiString(this IConfiguration config, string api, string field)
        {
            return config[$"WebAPIClients:{api}:{field}"];
        }
    }
}
