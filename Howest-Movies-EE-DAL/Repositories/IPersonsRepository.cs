using Howest_Movies_EE_DAL.DTO.Person;
using System.Collections.Generic;
using System.Linq;

namespace Howest_Movies_EE_DAL.Repositories
{
    public interface IPersonsRepository
    {
        PersonDTO Create(PersonDTO personDTO);
        PersonDTO Delete(int id);
        IQueryable<PersonDTO> GetAll();
        PersonDTO GetById(int id);
        PersonDTO Update(PersonDTO updateDTO);
    }
}