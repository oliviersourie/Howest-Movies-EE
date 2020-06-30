using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Howest_Movies_EE_DAL.DTO.PersonDTO;
using Howest_Movies_EE_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Mowest_Movies_EE_Web.ViewModels;

namespace Howest_Movies_EE_Web.Controllers
{
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMapper personMapper;
        private readonly JsonSerializerOptions jsonOptions;
        private readonly HttpClient http;


        #region Constructor
        public PersonController(IHttpClientFactory httpFactory, IMapper mapper)
        {
            httpClientFactory = httpFactory;
            personMapper = mapper;

            http = httpClientFactory.CreateClient("MovieAPI");
            jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        #endregion

        #region Index
        [Route("")]
        public async Task<ViewResult> Index()
        {
            HttpResponseMessage allPeople = await http.GetAsync("v1/persons");
            string allPeopleRes = await allPeople.Content.ReadAsStringAsync();


            if (allPeople.IsSuccessStatusCode)
            {
                IEnumerable<PersonDTO> personList =
                            JsonSerializer.Deserialize<IEnumerable<PersonDTO>>(allPeopleRes, jsonOptions);

                return View(CreatePeopleViewModel(personList));

            }
            else
            {
                return View("ErrorView");
            }
        }

        private ListPeopleViewModel CreatePeopleViewModel(IEnumerable<PersonDTO> personList)
        {
            return new ListPeopleViewModel()
            {
                People = personMapper.Map<IEnumerable<PersonViewModel>>(personList)
            };
        }
        #endregion

        #region Delete person
        [Route("[action]/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage deletePerson = await http.DeleteAsync($"v1/persons/{id}");
            string deletePersonRes = await deletePerson.Content.ReadAsStringAsync();

            if (deletePerson.IsSuccessStatusCode)
            {
                _ = JsonSerializer.Deserialize<UpdatePersonDTO>(deletePersonRes, jsonOptions);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("ErrorView");
            }

        }
        #endregion

        #region Edit person
        [Route("[action]/{id:int}")]
        public async Task<ViewResult> Edit(int id)
        {
            HttpResponseMessage getPerson = await http.GetAsync($"v1/persons/{id}");
            string getPersonRes = await getPerson.Content.ReadAsStringAsync();



            if (getPerson.IsSuccessStatusCode)
            {

                PersonDetailDTO person =
                            JsonSerializer.Deserialize<PersonDetailDTO>(getPersonRes, jsonOptions);


                return View("EditPerson", personMapper.Map<UpdatePersonViewModel>(person));

            }
            else
            {
                return View("ErrorView");
            }
        }


        #endregion

        #region Update person
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm]UpdatePersonViewModel updatePerson)
        {
            if (ModelState.IsValid)
            {
                
                PersonDetailDTO updatedPersonDTO = personMapper.Map<PersonDetailDTO>(updatePerson);

                HttpContent content =
                        new StringContent(JsonSerializer.Serialize(updatedPersonDTO), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await http.PutAsync($"v1/persons/{updatedPersonDTO.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("ErrorView");
                }
            }
            else
            {
                return View("EditPerson", updatePerson);
            }
        }
        #endregion

        #region Add person
        [Route("new")]
        public ViewResult AddPerson()
        {
            return View("CreatePerson", new CreatePersonViewModel());
        }

        #endregion

        #region Create a person
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]CreatePersonViewModel createPerson)
        {

            if (ModelState.IsValid)
            {
                CreatePersonDTO createdPersonDTO = personMapper.Map<CreatePersonDTO>(createPerson);

                HttpContent content =
                        new StringContent(JsonSerializer.Serialize(createdPersonDTO), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await http.PostAsync($"v1/persons", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("ErrorView");
                }
            }
            else
            {

                return View("CreatePerson", createPerson);

            }

        }
        #endregion

    }
}