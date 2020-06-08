using AutoMapper;
using Howest_Movies_EE_DAL.DTO.PersonDTO;
using Howest_Movies_EE_DAL.Models;

namespace Howest_Movies_EE_GraphQL.Mappings
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Persons, ListItemPersonDTO>();
        }
    }
}
