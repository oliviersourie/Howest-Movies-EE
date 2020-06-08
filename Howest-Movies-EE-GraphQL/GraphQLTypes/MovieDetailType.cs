using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;
using Howest_Movies_EE_DAL.DTO.MovieDTO;

namespace Howest_Movies_EE_GraphQL.GraphQLTypes
{
    public class MovieDetailType : ObjectGraphType<MovieDetailDTO>
    {
        public MovieDetailType()
        {
            Field(m => m.Id);
            Field(m => m.Title);
            Field(m => m.CoverUrl);
            Field(m => m.Year);
            Field(m => m.OriginalAirDate);
            Field(m => m.Kind);
            Field(m => m.Top250Rank);
            Field(m => m.Rating);
            Field(m => m.Plot);

            Field(m => m.GenreMovie, type: typeof(ListGraphType<GenreType>));
            Field(m => m.Actors, type: typeof(ListGraphType<PersonType>));
        }
    }
}
