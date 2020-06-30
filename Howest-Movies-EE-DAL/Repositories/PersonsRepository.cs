using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest_Movies_EE_DAL.DTO.Person;
using Howest_Movies_EE_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Howest_Movies_EE_DAL.Repositories
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly MoviesContext db;
        private readonly IMapper personMapper;

        public PersonsRepository(MoviesContext moviesContext, IMapper mapper)
        {
            db = moviesContext;
            personMapper = mapper;
        }

        public IQueryable<PersonDTO> GetAll()
        {
            return db.Persons
                        .Where(p => !string.IsNullOrEmpty(p.Name))
                        .ProjectTo<PersonDTO>(personMapper.ConfigurationProvider)
                        .OrderBy(g => g.Name)
                        .Take(5);
        }

        public PersonDTO GetById(int id)
        {
            return db.Persons
                       .Where(m => m.Id == id)
                       .ProjectTo<PersonDTO>(personMapper.ConfigurationProvider)
                       .SingleOrDefault();
        }

        public PersonDTO Create(PersonDTO personDTO)
        {
            Persons newPerson = personMapper.Map<Persons>(personDTO);
            db.Persons.Add(newPerson);
            Save();

            personDTO.Id = newPerson.Id;
            return personDTO;
        }

        public PersonDTO Delete(int id)
        {
            Persons person = db.Persons
                              .Include(g => g.MovieRole)
                              .SingleOrDefault(m => m.Id == id);

            if (person != null)
            {
                db.Persons.Remove(person);
                Save();
            }

            return personMapper.Map<PersonDTO>(person);
        }

        public PersonDTO Update(PersonDTO updateDTO)
        {
            Persons person = db.Persons
                              .Include(g => g.MovieRole)
                              .SingleOrDefault(m => m.Id == updateDTO.Id);

            if (person != null)
            {
                personMapper.Map(updateDTO, person);
                Save();
            }

            return updateDTO;
        }

        private void Save()
        {
            db.SaveChanges();
        }

    }
}
