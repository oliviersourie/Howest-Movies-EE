using System.Linq;
using AutoMapper;
using Howest_Movies_EE_DAL.Models;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_DAL.DTO.PersonDTO;
using Howest_Movies_EE_DAL.DTO.RoleDTO;
using Howest_Movies_EE_DAL.Extensions;
using Howest_Movies_EE_DAL.DTO.GenreDTO;

namespace Howest_Movies_EE_WebAPI.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movies, MovieIdDTO>().ReverseMap();
            CreateMap<Movies, SmallMovieDTO>();
            CreateMap<MovieIdDTO, SmallMovieDTO>().ReverseMap();
            CreateMap<Movies, ListItemMovieDTO>();
            CreateMap<Movies, FullMovieDTO>()
                .ForMember(dto => dto.Plot, 
                           opt => opt.MapFrom(m => m.Plot.RemoveChars("[]").RemoveDoubleQuotes()));
            CreateMap<Movies, CreateMovieDTO>();
            CreateMap<Movies, MovieDetailDTO>()
                .ForMember(dto => dto.Plot,
                           opt => opt.MapFrom(m => m.Plot.RemoveChars("[]").RemoveDoubleQuotes()))
                .ForMember(dto => dto.Country,
                           opt => opt.MapFrom(m => m.OriginalAirDate.GetCountryFromOriginalAirDate()))
                .ForMember(dto => dto.GenreMovie,
                           opt => opt.MapFrom(sg => sg.GenreMovie
                                                    .Select(
                                                        g => new FullGenreDTO
                                                        {
                                                            Id = g.Genre.Id,
                                                            ImdbName = g.Genre.ImdbName,
                                                            Name = g.Genre.Name
                                                        })
                                                    .ToList())
                           )
            .ForMember(dto => dto.Actors,
                       opt => opt.MapFrom(mr => mr.MovieRole
                                    .Where(p => p.Role.Equals("actor"))
                                    .Select(a => new FullPersonDTO
                                    {
                                        Id = a.Person.Id,
                                        ImdbId = a.Person.ImdbId,
                                        Name = a.Person.Name,
                                        Biography = a.Person.Biography,
                                        Role = a.Role,
                                    })
                                    )
                       );

            CreateMap<CreateMovieDTO, Movies>();
            CreateMap<MovieDetailDTO, SmallMovieDTO>();
            CreateMap<ListItemMovieDTO, SmallMovieDTO>();
            CreateMap<RoleDTO, MovieRole>();
        }
    }
}
