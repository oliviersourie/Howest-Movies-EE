using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_DAL.DTO.PersonDTO;
using Howest_Movies_EE_DAL.Models;

namespace Howest_Movies_EE_DAL.Repositories
{
    public class PersonRepository : IPersonRepository
    {

        private readonly MoviesContext db;
        private readonly IMapper personMapper;

        public PersonRepository(MoviesContext moviesContext, IMapper mapper)
        {
            db = moviesContext;
            personMapper = mapper;
        }

        public IQueryable<ListItemPersonDTO> All()
        {
            return db.Persons.ProjectTo<ListItemPersonDTO>(personMapper.ConfigurationProvider);
        }

        public FullPersonDTO Create(CreatePersonDTO newPerson)
        {
            Persons person = personMapper.Map<Persons>(newPerson);

            db.Persons.Add(person);
            Save();

            AddMoviesToPerson(newPerson.Movies, person, newPerson.Role);

          
            return personMapper.Map<FullPersonDTO>(person);
        }




        public UpdatePersonDTO Delete(int id)
        {
            Persons person = db.Persons
                .SingleOrDefault(p => p.Id == id);

            if(person != null)
            {
                RemoveAllMovieRelations(id);

                db.Persons.Remove(person);
                Save();
            }

            return personMapper.Map<UpdatePersonDTO>(person);
        }

       

        public FullPersonDTO GetPerson(int id)
        {
            FullPersonDTO person = db.Persons
                    .Where(p => p.Id == id)
                    .ProjectTo<FullPersonDTO>(personMapper.ConfigurationProvider)
                    .SingleOrDefault();
            return person;
        }

        public FullPersonDTO Update(FullPersonDTO personDTO)
        {
            Persons person = db.Persons
               .SingleOrDefault(p => p.Id == personDTO.Id);

            if(person != null)
            {
                RemoveAllMovieRelations(Convert.ToInt32(person.Id));
                AddMoviesToPerson(personDTO.MoviePeople, person, personDTO.Role);
                person.ImdbId = personDTO.ImdbId;
                person.Name = personDTO.Name;
                person.Biography = personDTO.Biography;
                Save();
            }

            return personMapper.Map<FullPersonDTO>(person);
        }

        private void AddMoviesToPerson(IEnumerable<MovieIdDTO> movies, Persons person, string role)
        {
            movies.ToList().ForEach(movie => {
                db.MovieRole.Add(new MovieRole()
                {
                    MovieId = movie.Id,
                    PersonId = person.Id,
                    Movie = db.Movies.Where(m => m.Id == movie.Id).FirstOrDefault(),
                    Person = db.Persons.Where(p => p.Id == person.Id).FirstOrDefault(),
                    Role = role
                });
            });
            Save();
        }

        private void RemoveAllMovieRelations(int id)
        {
            db.MovieRole.Where(mv => mv.PersonId == id)
                    .ToList()
                    .ForEach(mv => db.MovieRole.Remove(mv));
            Save();
        }

        #region Save
        private void Save()
        {
            db.SaveChanges();
        }
        #endregion
    }
}
