using System;
using System.Collections.Generic;
using System.Linq;
using Howest_Movies_EE_DAL.DTO.GenreDTO;
using Howest_Movies_EE_DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howest_Movies_EE_WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.5")]
    [Route("api/v{version:apiVersion}/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository genreRespository;

        public GenreController(IGenreRepository genreRepo)
        {
            genreRespository = genreRepo;
        }

        #region Get all genres
        [HttpGet(Name = "GetGenres")]
        [ProducesResponseType(typeof(IEnumerable<FullGenreDTO>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<IEnumerable<FullGenreDTO>> GetGenres()
        {
            try
            {
                return Ok(genreRespository.All().ToList());
            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }
        }
        #endregion

        #region Get a genre by id

        [HttpGet("{id:int}", Name = "GetGenreById")]
        [ProducesResponseType(typeof(FullGenreDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<FullGenreDTO> GetGenreById(int id)
        {
            try
            {
                FullGenreDTO genreDTO = genreRespository.GetGenreById(id);

                if (genreDTO != null)
                {
                    return Ok(genreDTO);
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

        #region Create a genre
        [HttpPost(Name = "CreateGenre")]
        [ProducesResponseType(typeof(FullGenreDTO), 201)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<IEnumerable<FullGenreDTO>> CreateGenre([FromBody]CreateGenreDTO newGenre)
        {
            try
            {
                if(newGenre != null)
                {
                    FullGenreDTO created = genreRespository.Create(newGenre);
                    return Created(Url.Link("", new { id = created.Id, controller = "Genre"}), created);
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

        #region Update a genre
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(FullGenreDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<FullGenreDTO> UpdateGenre(int id, [FromBody] UpdateGenreDTO updatedGenre)
        {
            try
            {
                if(id == updatedGenre.Id)
                {
                    if (genreRespository.GetGenreById(id) != null)
                    {
                        return Ok(genreRespository.Update(updatedGenre));
                    }
                    else
                    {
                        return NotFound($"Genre with the id {id} was not found!");
                    }
                }
                else
                {
                    return BadRequest("Genre id mismatch!");
                }
            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }


        }


        #endregion

        #region Delete a genre
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(SmallGenreDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<SmallGenreDTO> DeleteGenre(int id)
        {
            try
            {
                if (genreRespository.GetGenreById(id) != null)
                {
                    return genreRespository.Delete(id);
                }
                else
                {
                    return NotFound($"Genre with id {id} not found");
                }
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