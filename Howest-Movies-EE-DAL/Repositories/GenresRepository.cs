using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest_Movies_EE_DAL.DTO.Genre;
using Howest_Movies_EE_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howest_Movies_EE_DAL.Repositories
{
    public class GenresRepository : IGenresRepository
    {

        private readonly MoviesContext db;
        private readonly IMapper genreMapper;
        public GenresRepository(MoviesContext moviesContext, IMapper mapper)
        {
            db = moviesContext;
            genreMapper = mapper;
        }

        public IQueryable<GenreDTO> GetAll()
        {
            return db.Genres
                .ProjectTo<GenreDTO>(genreMapper.ConfigurationProvider)
                .OrderBy(g => g.Name);
        }

        public GenreDTO GetById(int id)
        {
            return db.Genres
               .Where(m => m.Id == id)
               .ProjectTo<GenreDTO>(genreMapper.ConfigurationProvider)
               .SingleOrDefault();
        }

        public GenreDTO Create(GenreDTO genreDTO)
        {
            Genres newGenre = genreMapper.Map<Genres>(genreDTO);
            db.Genres.Add(newGenre);
            Save();

            genreDTO.Id = newGenre.Id;
            return genreDTO;
        }

        public GenreDTO Delete(int id)
        {
            Genres genre = db.Genres
                              .Include(g => g.GenreMovie)
                              .SingleOrDefault(m => m.Id == id);

            if (genre != null)
            {
                db.Genres.Remove(genre);
                Save();
            }

            return genreMapper.Map<GenreDTO>(genre);
        }

        public GenreDTO Update(GenreDTO updateDTO)
        {
            Genres genre = db.Genres.Include(g => g.GenreMovie)
                              .SingleOrDefault(m => m.Id == updateDTO.Id);

            if (genre != null)
            {
                genreMapper.Map(updateDTO, genre);
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
