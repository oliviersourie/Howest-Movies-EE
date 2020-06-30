using AutoMapper;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_DAL.DTO.PersonDTO;
using Howest_Movies_EE_DAL.DTO.GenreDTO;
using Howest_Movies_EE_DAL.Extensions;
using Howest_Movies_EE_Web.ViewModels;
using Mowest_Movies_EE_Web.ViewModels;

namespace Howest_Movies_EE_WebAPI.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<ListItemMovieDTO, MovieViewModel>().ReverseMap();
            CreateMap<CreateMovieDTO, CreateMovieViewModel>().ReverseMap();
            CreateMap<SmallMovieDTO, MovieViewModel>().ReverseMap();
            CreateMap<UpdateMovieViewModel, MovieDetailDTO>().ReverseMap();
            CreateMap<MovieDetailDTO, MovieDetailViewModel>().ReverseMap();
                
            CreateMap<FullMovieDTO, MovieViewModel>()
                .ForMember(dto => dto.Plot, opt => opt.MapFrom(
                    m => m.Plot.TakeFirst(700)

                ));
            CreateMap<UpdateMovieViewModel, FullMovieDTO>().ReverseMap();

            CreateMap<UpdatePersonDTO, PersonViewModel>().ReverseMap();
            CreateMap<FullGenreDTO, GenreViewModel>().ReverseMap();
            CreateMap<MovieIdDTO, MovieIdViewModel>().ReverseMap();
            CreateMap<SmallMovieDTO, MovieIdViewModel>().ReverseMap();
        }
    }
}
