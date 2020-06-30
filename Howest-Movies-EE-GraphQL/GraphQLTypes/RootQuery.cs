using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO.Movie;
using Howest_Movies_EE_DAL.Repositories;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(IMoviesRepository moviesRepository, 
                         IGenresRepository genresRepository, 
                         IPersonsRepository personsRepository)
        {
            #region All movies
            Field<ListGraphType<MovieDetailType>>(
                "movies",
                Description = "Get all ths movies",
                resolve: context =>
                {
                    return moviesRepository.GetAllMoviesDetailed();
                }
            );
            #endregion

            #region Search movie by country name
            Field<ListGraphType<MovieDetailType>>(
                "movieCountry",
                Description = "Get a movie by country name",
                arguments: new QueryArguments
                {
                    new QueryArgument<StringGraphType>{ Name = "country"}
                },
                resolve: context =>
                {
                    string searchCountry = context.GetArgument<string>("country");
                    IEnumerable<MovieDetailDTO> movies = moviesRepository.GetAllMoviesDetailed();
                    return movies.Where(m => m.Country.ToLower().Equals(searchCountry.ToLower()));
                }

            );
            #endregion

            #region Search movie by <n> many years released
            Field<ListGraphType<MovieDetailType>>(
                "movieRelease",
                Description = "Get movies released from n years ago",
                arguments: new QueryArguments
                {
                    new QueryArgument<IntGraphType>{ Name = "yearsago"}
                },
                resolve: context =>
                {
                    int yearsAgo = context.GetArgument<int>("yearsago");
                    IEnumerable<MovieDetailDTO> movies = moviesRepository.GetAllMoviesDetailed();
                    return movies.Where(m => m.Year == DateTime.Now.Year - yearsAgo);
                }
            );
            #endregion

            #region All genres
            Field<ListGraphType<GenreDetailType>>(
                "genres",
                Description = "Get all genres",
                resolve: context =>
                {
                    return genresRepository.GetAll();
                }

            );
            #endregion

            #region All persons
            Field<ListGraphType<PersonDetailType>>(
                "people",
                Description = "Get people",
                resolve: context =>
                {
                    return personsRepository.GetAll();
                }

            );
            #endregion
        }
    }
}
