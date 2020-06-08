using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest_Movies_EE_DAL.DTO;
using Howest_Movies_EE_DAL.DTO.GenreDTO;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_DAL.Models;

namespace Howest_Movies_EE_DAL.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MoviesContext db;
        private readonly IMapper genreMapper;


        public GenreRepository(MoviesContext moviesContext, IMapper mapper)
        {
            db = moviesContext;
            genreMapper = mapper;
        }

        #region Add genre
        public FullGenreDTO Create(CreateGenreDTO genreDTO)
        {
            Genres genre = genreMapper.Map<Genres>(genreDTO);

            db.Genres.Add(genre);
            Save();

            AddMoviesToGenre(genreDTO.Movies, genre);

            return genreMapper.Map<FullGenreDTO>(genre);

        }

        
        #endregion

        #region All genres
        public IQueryable<FullGenreDTO> All()
        {
            return db.Genres.OrderBy(g => g.Name).ProjectTo<FullGenreDTO>(genreMapper.ConfigurationProvider);
        }
        #endregion

        #region Get genre by id

        public FullGenreDTO GetGenreById(int id)
        {
            FullGenreDTO genre = db.Genres
                      .Where(g => g.Id == id)
                       .ProjectTo<FullGenreDTO>(genreMapper.ConfigurationProvider)
                      .SingleOrDefault();
            return genre;
        }

        #endregion

        #region Delete genre
        public SmallGenreDTO Delete(int id)
        {
            Genres genre = db.Genres
                .SingleOrDefault(g => g.Id == id);

            if(genre != null)
            {
                RemoveAllGenreMovieLinks(genre.Id);
                db.Genres.Remove(genre);
                Save();
            }

            return genreMapper.Map<SmallGenreDTO>(genre);
        }

        #endregion

        #region Update genre

        public FullGenreDTO Update(UpdateGenreDTO genreDTO)
        {
            Genres genre = db.Genres.Where(g => g.Id == genreDTO.Id)
                .SingleOrDefault();

            if(genre != null)
            {
                genre.ImdbName = genreDTO.ImdbName;
                genre.Name = genreDTO.Name;
                Save();
                RemoveAllGenreMovieLinks(genre.Id);
                AddMoviesToGenre(genreDTO.Movies, genre);
            }

            return genreMapper.Map<FullGenreDTO>(genre);
        }

        private void RemoveAllGenreMovieLinks(int id)
        {
            db.GenreMovie.Where(gm => gm.GenreId == id).ToList().ForEach(gm => db.GenreMovie.Remove(gm));
            Save();
        }

        #endregion

        #region Save
        private void Save()
        {
            db.SaveChanges();
        }

        #endregion

        #region Helper functions
        private void AddMoviesToGenre(IEnumerable<MovieIdDTO> movies, Genres genre)
        {
            movies.ToList()
                .Where(movie => !RecordExists(movie, genre))
                .ToList()
                .ForEach(movie => CreateMovieGenre(movie, genre));
            Save();
        }

        private bool RecordExists(MovieIdDTO movie, Genres genre)
        {
            return db.GenreMovie
                .Where(gm => gm.MovieId == movie.Id)
                .Where(gm => gm.GenreId == genre.Id)
                .FirstOrDefault() != null;
        }

        private void CreateMovieGenre(MovieIdDTO movie, Genres genre)
        {
            db.GenreMovie.Add(new GenreMovie()
            {
                GenreId = genre.Id,
                Genre = genre,
                MovieId = movie.Id,
                Movie = db.Movies.Where(m => m.Id == movie.Id).FirstOrDefault()
            });
        }

        #endregion

    }
}
