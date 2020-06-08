using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest_Movies_EE_DAL.DTO;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_DAL.Extensions;
using Howest_Movies_EE_DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Howest_Movies_EE_DAL.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesContext db;
        private readonly IMapper movieMapper;

        public MovieRepository(MoviesContext moviesContext, IMapper mapper)
        {
            db = moviesContext;
            movieMapper = mapper;
        }
        

        #region All movies
        public IQueryable<ListItemMovieDTO> All(bool desc = false, string sortCategory="TITLE")
        {
            IQueryable<Movies> movies = db.Movies;
            return SortMovieList(movies, desc, sortCategory);
        }
        #endregion

        #region Sort Movies helper function
        private IQueryable<ListItemMovieDTO> SortMovieList(IQueryable<Movies> movieList, bool desc, string sortCategory)
        {
            IQueryable<Movies> sortedList = movieList;
            switch (sortCategory)
            {
                case "TITLE":
                    sortedList = desc ? movieList.OrderByDescending(m => m.Title) : movieList.OrderBy(m => m.Title);
                    break;
                case "RATING":
                    sortedList = desc ? movieList.OrderByDescending(m => m.Rating) : movieList.OrderBy(m => m.Rating);
                    break;
                case "YEAR":
                    sortedList = desc ? movieList.OrderByDescending(m => m.Year) : movieList.OrderBy(m => m.Year);
                    break;
                case "IMDBID":
                    sortedList = desc ? movieList.OrderByDescending(m => m.ImdbId) : movieList.OrderBy(m => m.ImdbId);
                    break;

            }
            return sortedList.ProjectTo<ListItemMovieDTO>(movieMapper.ConfigurationProvider); ;
        }
        #endregion

        #region Random movie
        public FullMovieDTO GetRandom()
        {
            IEnumerable<FullMovieDTO> movies = db.Movies.ProjectTo<FullMovieDTO>(movieMapper.ConfigurationProvider);
            return movies.GetRandom();
            
        }
        #endregion

        #region Get movie by Id
        public MovieDetailDTO GetMovie(int id)
        {
            MovieDetailDTO movie = db.Movies
                     .Where(m => m.Id == id)
                      .ProjectTo<MovieDetailDTO>(movieMapper.ConfigurationProvider)
                     .SingleOrDefault();

            return movie;
        }

        #endregion

        #region Delete a movie

        public SmallMovieDTO Delete(int id)
        {
           
                Movies movie = db.Movies
                    .SingleOrDefault(m => m.Id == id);

                if (movie != null)
                {
                    RemoveLinksToMovie(id);
                    db.Movies.Remove(movie);
                    Save();
                }

                return movieMapper.Map<SmallMovieDTO>(movie);
            
        }
        #endregion

        #region Soft delete a movie

        public SmallMovieDTO SoftDelete(int id)
        {

            Movies movie = db.Movies
                .SingleOrDefault(m => m.Id == id);

            if (movie != null)
            {
                movie.SoftDeleted = true;
                Save();
            }

            return movieMapper.Map<SmallMovieDTO>(movie);

        }
        #endregion

        #region Update a movie

        public FullMovieDTO Update(FullMovieDTO movieDTO)
        {
            Movies movie = db.Movies.Where(m => m.Id == movieDTO.Id)
                .SingleOrDefault();

            if (movie != null)
            {
                movie.Id = movieDTO.Id;
                movie.ImdbId = movieDTO.ImdbId;
                movie.Title = movieDTO.Title;
                movie.CoverUrl = movieDTO.CoverUrl;
                movie.Year = movieDTO.Year;
                movie.OriginalAirDate = movieDTO.OriginalAirDate;
                movie.Kind = movieDTO.Kind;
                movie.Rating = movieDTO.Rating;
                movie.Plot = movieDTO.Plot;
                movie.Top250Rank = movieDTO.Top250Rank;

                Save();
            }

            return movieMapper.Map<FullMovieDTO>(movie);
        }


        #endregion

        #region Create a movie

        public FullMovieDTO Create(CreateMovieDTO movieDTO)
        {
            Movies movie = movieMapper.Map<Movies>(movieDTO);

            db.Movies.Add(movie);
            Save();

            return movieMapper.Map<FullMovieDTO>(movie);
        }

        #endregion

        #region Search for a movie

        public IQueryable<ListItemMovieDTO> Search(string searchString)
        {
            return db.Movies
                .Where(m => m.Title.Contains(searchString))
                .ProjectTo<ListItemMovieDTO>(movieMapper.ConfigurationProvider);
        }

        #endregion

        #region Delete links to Movie
        public void RemoveLinksToMovie(int id)
        {
            SqlParameter idParam = new SqlParameter("@movieId", id);

            #region DeleteLinksToMovie
            //create or alter procedure DeleteGenreLinksToMovie @id int
            //as
            //delete from genre_movie
            //where movie_id = @id
            //go

            //create or alter procedure DeleteRoleLinksToMovie @id int
            //as
            //delete from movie_role
            //where movie_id = @id
            //go

            //create or alter procedure DeleteLinksToMovie @movieId int
            //as
            //exec DeleteRoleLinksToMovie @id = @movieId
            //exec DeleteGenreLinksToMovie @id = @movieId
            //go
            #endregion

            _ = db.Database.ExecuteSqlRaw("DeleteLinksToMovie @movieId", idParam);

            Save();
        }
        #endregion  


        #region Save
        private void Save()
        {
            db.SaveChanges();
        }
        #endregion

        #region Dynamic
        public IEnumerable<dynamic> MoviesToDynamic()
        {
            IEnumerable<Movies> movies = db.Movies;
            return movies.ApplyFunc(CreateDynamic);
        }

        public dynamic CreateDynamic(Movies movie)
        {
            return new
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Country = movie.OriginalAirDate.GetCountryFromOriginalAirDate()
            };
        }

        #endregion

    }
}
