using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Howest_Movies_EE_DAL.DTO.GenreDTO;
using Howest_Movies_EE_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Mowest_Movies_EE_Web.ViewModels;

namespace Howest_Movies_EE_Web.Controllers
{
    [Route("[controller]")]
    public class GenreController : Controller
    {

        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMapper genreMapper;
        private readonly JsonSerializerOptions jsonOptions;
        private readonly HttpClient http;

        #region Constructor
        public GenreController(IHttpClientFactory httpFactory, IMapper mapper)
        {
            httpClientFactory = httpFactory;
            genreMapper = mapper;

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
            HttpResponseMessage allGenres = await http.GetAsync("v1/genres");
            string allGenresApiRes = await allGenres.Content.ReadAsStringAsync();


            if (allGenres.IsSuccessStatusCode)
            {
                IEnumerable<FullGenreDTO> genreList =
                            JsonSerializer.Deserialize<IEnumerable<FullGenreDTO>>(allGenresApiRes, jsonOptions);

                return View(CreateGenresViewModel(genreList));

            }
            else
            {
                return View("ErrorView");
            }
        }
        #endregion

        #region Create View Models
        private object CreateGenresViewModel(IEnumerable<FullGenreDTO> genreList)
        {
            return new GenresViewModel()
            {
                AllGenres = genreMapper.Map<IEnumerable<GenreViewModel>>(genreList)
            };
        }
        #endregion

        #region Add genre
        [Route("new")]
        public ViewResult AddGenre()
        {

             return View("CreateGenre", new CreateGenreViewModel());
           
        }


        #endregion

        #region Create a genre
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]CreateGenreViewModel createGenre)
        {



            if (ModelState.IsValid)
            {
                CreateGenreDTO createdGenre = genreMapper.Map<CreateGenreDTO>(createGenre);

                HttpContent content =
                        new StringContent(JsonSerializer.Serialize(createdGenre), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await http.PostAsync($"v1/genres", content);

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

                return View("CreateGenre", createGenre);

            }
            
        }
        #endregion

        #region Delete genre
        [Route("[action]/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage deleteGenre = await http.DeleteAsync($"v1/genres/{id}");
            string deletedRes = await deleteGenre.Content.ReadAsStringAsync();

            if (deleteGenre.IsSuccessStatusCode)
            {

                _ = JsonSerializer.Deserialize<SmallGenreDTO>(deletedRes, jsonOptions);

                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View("ErrorView");
            }

        }
        #endregion

        #region Edit genres
        [Route("[action]/{id:int}")]
        public async Task<ViewResult> Edit(int id)
        {
            HttpResponseMessage getGenre = await http.GetAsync($"v1/genres/{id}");
            string getGenreRes = await getGenre.Content.ReadAsStringAsync();

      

            if (getGenre.IsSuccessStatusCode)
            {

                FullGenreDTO genre =
                            JsonSerializer.Deserialize<FullGenreDTO>(getGenreRes, jsonOptions);
                return View("EditGenre", genreMapper.Map<UpdateGenreViewModel>(genre));

            }
            else
            {
                return View("ErrorView");
            }
        }


        #endregion

        #region Update genre
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm]UpdateGenreViewModel updateGenre)
        {
            if (ModelState.IsValid)
            {

                UpdateGenreDTO updateGenreDTO = genreMapper.Map<UpdateGenreDTO>(updateGenre);

                HttpContent content =
                        new StringContent(JsonSerializer.Serialize(updateGenreDTO), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await http.PutAsync($"v1/genres/{updateGenreDTO.Id}", content);

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
                return View("EditGenre", updateGenre);
            }
        }
        #endregion



    }
}