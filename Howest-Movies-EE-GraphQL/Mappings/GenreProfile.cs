using AutoMapper;
using Howest_Movies_EE_DAL.DTO.GenreDTO;
using Howest_Movies_EE_DAL.Models;

namespace Howest_Movies_EE_GraphQL.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genres, FullGenreDTO>();
        }
    }
}
