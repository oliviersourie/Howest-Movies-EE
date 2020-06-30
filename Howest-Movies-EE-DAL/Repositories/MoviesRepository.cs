using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest_Movies_EE_DAL.DTO.Movie;
using Howest_Movies_EE_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howest_Movies_EE_DAL.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {

        private readonly MoviesContext db;
        private readonly IMapper movieMapper;

        public MoviesRepository(MoviesContext moviesContext, IMapper mapper)
        {
            db = moviesContext;
            movieMapper = mapper;
        }

        public IQueryable<MovieDTO> GetAll()
        {
            return db.Movies
               .ProjectTo<MovieDTO>(movieMapper.ConfigurationProvider)
               .OrderBy(m => m.Title);
        }
        public IEnumerable<MovieDetailDTO> GetAllMoviesDetailed()
        {
            return db.Movies
                     .ProjectTo<MovieDetailDTO>(movieMapper.ConfigurationProvider)
                     .OrderBy(m => m.Title);
        }

        public MovieDTO GetById(int id)
        {
            return db.Movies
                .Where(m => m.Id == id)
                .ProjectTo<MovieDTO>(movieMapper.ConfigurationProvider)
                .SingleOrDefault();
        }

        public MovieDetailDTO GetMovieDetailsById(long id)
        {
            return db.Movies
                .Where(m => m.Id == id)
                .ProjectTo<MovieDetailDTO>(movieMapper.ConfigurationProvider)
                .SingleOrDefault();
        }

        public MovieDTO Create(MovieDTO movieDTO)
        {
            Movies newMovie = movieMapper.Map<Movies>(movieDTO);
            db.Movies.Add(newMovie);
            Save();

            movieDTO.Id = newMovie.Id;
            return movieDTO;
        }

        public MovieDTO Delete(int id)
        {
            Movies movie = db.Movies.SingleOrDefault(m => m.Id == id);

            if (movie != null)
            {
                db.Movies.Remove(movie);
                Save();
            }

            return movieMapper.Map<MovieDTO>(movie);
        }

        public MovieDTO SoftDelete(int id)
        {
            Movies movie = db.Movies.SingleOrDefault(m => m.Id == id);

            if (movie != null)
            {
                movie.SoftDeleted = true;
                Save();
            }

            return movieMapper.Map<MovieDTO>(movie);
        }

        public MovieDTO Update(MovieDTO updateDTO)
        {
            Movies movie = db.Movies.SingleOrDefault(m => m.Id == updateDTO.Id);

            if (movie != null)
            {
                movieMapper.Map(updateDTO, movie);
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
