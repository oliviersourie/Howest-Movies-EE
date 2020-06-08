using System;
using System.Collections.Generic;
using System.Linq;
using Howest_Movies_EE_DAL.DTO.PersonDTO;
using Howest_Movies_EE_DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howest_Movies_EE_WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.5")]
    [Route("api/v{version:apiVersion}/persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly IPersonRepository personRepository;

        public PersonController(IPersonRepository personRepo)
        {
            personRepository = personRepo;
        }


        #region Get people
        [HttpGet(Name = "GetPeople")]
        [ProducesResponseType(typeof(IEnumerable<ListItemPersonDTO>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<IEnumerable<ListItemPersonDTO>> GetPeople()
        {
            try
            {
                return Ok(personRepository.All().ToList());
            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }
        }

        #endregion

        #region Get person by id

        [HttpGet("{id:int}", Name = "GetPersonById")]
        [ProducesResponseType(typeof(FullPersonDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<FullPersonDTO> GetPersonById(int id)
        {
            try
            {
                FullPersonDTO person = personRepository.GetPerson(id);

                if (person != null)
                {
                    return Ok(person);
                }
                else
                {
                    return NotFound($"The person with the id {id}, was not found in the database!");
                }

            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }
        }

        #endregion

        #region Create a person

        [HttpPost(Name = "CreatePerson")]
        [ProducesResponseType(typeof(FullPersonDTO), 201)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<FullPersonDTO> CreatePerson([FromBody]CreatePersonDTO newPerson)
        {
            try
            {
                if (newPerson != null)
                {
                    FullPersonDTO created = personRepository.Create(newPerson);
                    return Created(Url.Link("", new { id = created.Id, controller = "Person" }), created);
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

        #region Delete a person


        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(UpdatePersonDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<UpdatePersonDTO> DeletePerson(int id)
        {
            try
            {
                if (personRepository.GetPerson(id) != null)
                {
                    return personRepository.Delete(id);
                }
                else
                {
                    return NotFound($"Person with id {id} not found");
                }
            }
            catch (Exception e)
            {
                return SendInternalServerError(e);
            }
        }

        #endregion

        #region Update a person
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(FullPersonDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public ActionResult<FullPersonDTO> UpdatePerson(int id, [FromBody] FullPersonDTO updatedPerson)
        {
            try
            {
                if (id == updatedPerson.Id)
                {
                    if (personRepository.GetPerson(id) != null)
                    {
                        return Ok(personRepository.Update(updatedPerson));
                    }
                    else
                    {
                        return NotFound($"Person with the id {id} was not found!");
                    }
                }
                else
                {
                    return BadRequest("Person id mismatch!");
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