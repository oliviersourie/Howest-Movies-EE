using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_DAL.Repositories;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(IMovieRepository movieRepository, 
                            IGenreRepository genreRepository, 
                            IPersonRepository personRepository)
        {
            #region All movies
            Field<ListGraphType<MovieType>>(
                "movies",
                Description = "Get all movies",
                resolve: context =>
                {
                    return movieRepository.All();
                }

            );
            #endregion

            #region Id filter

            Field<MovieDetailType>(
                "movieId",
                Description = "Get a movie by id",
                arguments: new QueryArguments
                {
                    new QueryArgument<IntGraphType>{ Name = "search"}
                },
                resolve: context =>
                {
                    int id = context.GetArgument<int>("search");
                    return movieRepository.GetMovie(id);
                }

            );
            #endregion

            #region Search by title
            Field<ListGraphType<MovieType>>(
                "movieSearch",
                Description = "Get a movie by name",
                arguments: new QueryArguments
                {
                    new QueryArgument<StringGraphType>{ Name = "search"}
                },
                resolve: context =>
                {
                    string search = context.GetArgument<string>("search");
                    return movieRepository.Search(search);
                }

            );
            #endregion

            #region Search movie by country
            Field<ListGraphType<MovieType>>(
                "movieCountry",
                Description = "Get a movie by country",
                arguments: new QueryArguments
                {
                    new QueryArgument<StringGraphType>{ Name = "search"}
                },
                resolve: context =>
                {
                    string search = context.GetArgument<string>("search");
                    IEnumerable<ListItemMovieDTO> movies = movieRepository.All();
                    return movies.Where(m => m.Country.ToLower().Equals(search.ToLower()));
                }

            );
            #endregion

            #region Search movie by release
            Field<ListGraphType<MovieType>>(
                "movieRelease",
                Description = "Get movies released :years ago",
                arguments: new QueryArguments
                {
                    new QueryArgument<IntGraphType>{ Name = "years"}
                },
                resolve: context =>
                {
                    int years = context.GetArgument<int>("years");
                    IEnumerable<ListItemMovieDTO> movies = movieRepository.All();
                    return movies.Where(m => m.Year == DateTime.Now.Year - years);
                }
            );
            #endregion

            #region All genres
            Field<ListGraphType<GenreType>>(
                "genres",
                Description = "Get all genres",
                resolve: context =>
                {
                    return genreRepository.All();
                }

            );
            #endregion

            #region All people
            Field<ListGraphType<ListItemPersonType>>(
                "persons",
                Description = "Get all people",
                resolve: context =>
                {
                    return personRepository.All();
                }

            );
            #endregion

        }
    }
}
