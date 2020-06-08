using System.Linq;
using Howest_Movies_EE_DAL.DTO.PersonDTO;

namespace Howest_Movies_EE_DAL.Repositories
{
    public interface IPersonRepository
    {
        IQueryable<ListItemPersonDTO> All();
        FullPersonDTO GetPerson(int id);
        UpdatePersonDTO Delete(int id);
        FullPersonDTO Update(FullPersonDTO personDTO);
        FullPersonDTO Create(CreatePersonDTO newPerson);
    }
}
