using System;
using System.Collections.Generic;
using System.Linq;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howest_Movies_EE_WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.5")]
    [Route("api/v{version:apiVersion}/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository movieRepository;

        public MovieController(IMovieRepository movieRepo)
        {
            movieRepository = movieRepo;
        }

        #region GetMovies
        [HttpGet(Name = "GetMovies")]
        [ProducesResponseType(typeof(IEnumerable<MovieDetailDTO>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<IEnumerable<MovieDetailDTO>> GetMovies(bool desc = false, string cat = "TITLE")
        {
            try
            {
                return Ok(movieRepository.All(desc, cat).ToList());
            }
            catch(Exception e)
            {
                return SendInternalServerError(e);
            } 
        }
        #endregion

        #region GetRandomMovie
        [HttpGet("random", Name = "GetRandomMovie")]
        [ProducesResponseType(typeof(FullMovieDTO), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<FullMovieDTO> GetRandomMovie()
        {
            try
            {
                return Ok(movieRepository.GetRandom());
            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }
        }
        #endregion

        #region GetMovieById
        [HttpGet("{id:int}", Name = "GetMovieById")]
        [ProducesResponseType(typeof(MovieDetailDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<MovieDetailDTO> GetMovieById(int id)
        {
            try
            {
                MovieDetailDTO movie = movieRepository.GetMovie(id);

                if (movie != null) {
                    return Ok(movie);
                }
                else
                {
                    return NotFound($"The movie with the id {id}, was not found in the database!");
                }

            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }
        }
        #endregion

        #region Update a movie
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(FullMovieDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<FullMovieDTO> UpdateMovie(int id, [FromBody] FullMovieDTO updatedMovie)
        {
            try
            {
                if (id == updatedMovie.Id)
                {
                    if (movieRepository.GetMovie(id) != null)
                    {
                        return Ok(movieRepository.Update(updatedMovie));
                    }
                    else
                    {
                        return NotFound($"Movie with the id {id} was not found!");
                    }
                }
                else
                {
                    return BadRequest("Movie id mismatch!");
                }
            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }


        }


        #endregion

        #region Delete a movie


        [HttpDelete("{id:int}"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(SmallMovieDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<SmallMovieDTO> DeleteMovie(int id)
        {
            try
            {
                if (movieRepository.GetMovie(id) != null)
                {
                    return movieRepository.Delete(id);
                }
                else
                {
                    return NotFound($"Movie with id {id} not found");
                }
            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }
        }

        #endregion

        #region Soft delete a movie v1.5


        [HttpDelete("{id:int}"), MapToApiVersion("1.5")]
        [ProducesResponseType(typeof(SmallMovieDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<SmallMovieDTO> SoftDeleteMovie(int id)
        {
            try
            {
                if (movieRepository.GetMovie(id) != null)
                {
                    return movieRepository.SoftDelete(id);
                }
                else
                {
                    return NotFound($"Movie with id {id} not found");
                }
            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }
        }

        #endregion

        #region Create a movie
        [HttpPost(Name = "CreateMovie")]
        [ProducesResponseType(typeof(FullMovieDTO), 201)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<FullMovieDTO> CreateMovie([FromBody]CreateMovieDTO newMovie)
        {
            try
            {
                if (newMovie != null)
                {
                    FullMovieDTO created = movieRepository.Create(newMovie);
                    return Created(Url.Link("", new { id = created.Id, controller = "Movie" }), created);
                }
                else
                {
                    return NotFound($"Form body was not correct!");
                }
            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }
        }
        #endregion

        #region Find a movie v1.5
        [HttpGet("search",Name = "Find a movie by id"), MapToApiVersion("1.5")]
        [ProducesResponseType(typeof(IEnumerable<SmallMovieDTO>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<IEnumerable<SmallMovieDTO>> FindMovieByTitle([FromQuery] string title = "")
        {
            try
            {
                return Ok(movieRepository.Search(title).ToList());
            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }
        }
        #endregion



        #region Dynamic movies
        // NOTE: this route is for testing only!
        //You can change the returned properties in the movie repository.
        [HttpGet("dynamic", Name = "Get the dynamic representation of a movie")]
        [ProducesResponseType(typeof(IEnumerable<dynamic>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<IEnumerable<dynamic>> GetMoviesInDynamicFormat()
        {
            try
            {
                return Ok(movieRepository.MoviesToDynamic());
            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }
        }


        #endregion

        private ActionResult SendInternalServerError(Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured: {e}");
        }

    }
}