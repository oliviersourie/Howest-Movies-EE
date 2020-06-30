using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mowest_Movies_EE_Web.ViewModels;


namespace Mowest_Movies_EE_Web.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class MovieController : Controller
    {

        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMapper movieMapper;
        private readonly JsonSerializerOptions jsonOptions;
        private readonly HttpClient http;

        #region Constructor
        public MovieController(IHttpClientFactory httpFactory, IMapper mapper)
        {
            httpClientFactory = httpFactory;
            movieMapper = mapper;

            http = httpClientFactory.CreateClient("MovieAPI");
            jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        #endregion

        #region Index
        [Route("")]
        public async Task<ViewResult> Index(string sortCategory = "TITLE", string sortDirection = "ASC")
        {
        
            HttpResponseMessage allMoviesRes = await http.GetAsync($"v1/movies?desc={sortDirection.Equals("DESC")}&cat={sortCategory}");
            string allMoviesApiRes = await allMoviesRes.Content.ReadAsStringAsync();

            HttpResponseMessage randomMovieRes = await http.GetAsync("v1/movies/random");
            string randomMovieApiRes = await randomMovieRes.Content.ReadAsStringAsync();


            if (allMoviesRes.IsSuccessStatusCode && randomMovieRes.IsSuccessStatusCode)
            {
                IEnumerable<ListItemMovieDTO> movieList =
                            JsonSerializer.Deserialize<IEnumerable<ListItemMovieDTO>>(allMoviesApiRes, jsonOptions);

                FullMovieDTO randomMovie =
                    JsonSerializer.Deserialize<FullMovieDTO>(randomMovieApiRes, jsonOptions);

                return View(CreateHomePageViewModel(movieList, randomMovie));
            }
            else
            {
                return View("ErrorView");
            }

        }
        #endregion

        #region Movie detail
        [Route("detail/{id:int}")]
        public async Task<ViewResult> MovieDetail(int id)
        {
            HttpResponseMessage findMovie = await http.GetAsync($"v1/movies/{id}");
            string findMovieRes = await findMovie.Content.ReadAsStringAsync();

            if (findMovie.IsSuccessStatusCode)
            {

                MovieDetailDTO findMovieDTO =
                    JsonSerializer.Deserialize<MovieDetailDTO>(findMovieRes, jsonOptions);

                return View(CreateMovieDetailViewModel(findMovieDTO));

            }
            else
            {
                return View("ErrorView");
            }
        }
        #endregion

        #region Delete movie
        [Route("[action]/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage deletedMovie = await http.DeleteAsync($"v1/movies/{id}");
            string deletedRes = await deletedMovie.Content.ReadAsStringAsync();

            if (deletedMovie.IsSuccessStatusCode)
            {

                _ = JsonSerializer.Deserialize<SmallMovieDTO>(deletedRes, jsonOptions);

                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View("ErrorView");
            }

        }
        #endregion

        #region Edit movie
        [Route("[action]/{id:int}")]
        public async Task<ViewResult> Edit(int id)
        {
            HttpResponseMessage getMovie = await http.GetAsync($"v1/movies/{id}");
            string getMovieRes = await getMovie.Content.ReadAsStringAsync();

            if (getMovie.IsSuccessStatusCode)
            {

                MovieDetailDTO movieDTO = JsonSerializer.Deserialize<MovieDetailDTO>(getMovieRes, jsonOptions);
                return View("EditMovie", CreateUpdateMovieViewModel(movieDTO));

            }
            else
            {
                return View("ErrorView");
            }
        }
        #endregion

        #region Add movie
        [Route("new")]
        public async Task<IActionResult> AddMovie()
        {
            return View("CreateMovie", new CreateMovieViewModel());
        }
        #endregion

        #region Update movie
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm]UpdateMovieViewModel updateMovie)
        {
            if (ModelState.IsValid)
            {


            FullMovieDTO updateMovieDTO = movieMapper.Map<FullMovieDTO>(updateMovie);

            HttpContent content =
                    new StringContent(JsonSerializer.Serialize(updateMovieDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await http.PutAsync($"v1/movies/{updateMovieDTO.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(MovieDetail), new { id = updateMovieDTO.Id});
                }
                else
                {
                    return View("ErrorView");
                }
            }
            else
            {
                return View("EditMovie", updateMovie);
            }
        }
        #endregion

        #region Create a movie
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]CreateMovieViewModel createMovie)
        {
            if (ModelState.IsValid)
            {
                createMovie.OriginalAirDate = $"{createMovie.OriginalAirDate} ({createMovie.Country})";
                createMovie.Kind = "movie";
             CreateMovieDTO updateMovieDTO = movieMapper.Map<CreateMovieDTO>(createMovie);

            HttpContent content =
                    new StringContent(JsonSerializer.Serialize(updateMovieDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await http.PostAsync($"v1/movies", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("ErrorView");
                }
            }
            return View("CreateMovie", createMovie);
        }
        #endregion

        #region Sort movies
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> SortMovies([FromForm]HomePageViewModel homePageVM)
        {
            return RedirectToAction(nameof(Index), new { sortCategory = homePageVM.SortItem, sortDirection = homePageVM.SortDirection });
        }
        #endregion

        #region Create ViewModels
        private UpdateMovieViewModel CreateUpdateMovieViewModel(MovieDetailDTO movieDetailDTO)
        {
            return movieMapper.Map<UpdateMovieViewModel>(movieDetailDTO);
        }

        private MovieDetailViewModel CreateMovieDetailViewModel(MovieDetailDTO movieDetailDTO)
        {
            return movieMapper.Map<MovieDetailViewModel>(movieDetailDTO);
        }



        private HomePageViewModel CreateHomePageViewModel(IEnumerable<ListItemMovieDTO> movieList, FullMovieDTO randomMovie)
        {
            return new HomePageViewModel()
            {
                AllMovies = movieMapper.Map<List<MovieViewModel>>(movieList),
                RandomMovie = movieMapper.Map<MovieViewModel>(randomMovie),
                FavMovies = movieMapper.Map<List<MovieViewModel>>(MapListToId(GetFavoriteMovieIds(), movieList))

            };

        

        }
        #endregion

        #region Favorite movie
        [Route("[action]")]
        public RedirectToActionResult FavoriteMovie(int id)
        {
            AddFavMovieId(id);
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Unfavorite movie
        [Route("[action]")]
        public RedirectToActionResult UnfavoriteMovie(int id)
        {
            RemoveMovieId(id);
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Session movies

        private IEnumerable<ListItemMovieDTO> MapListToId(List<int> movieIds, IEnumerable<ListItemMovieDTO> allMovies)
        {
            foreach (int id in movieIds)
            {
                ListItemMovieDTO movie = allMovies.ToList().Where(m => m.Id == id).FirstOrDefault(); 
                if(movie != null)
                {
                    yield return movie;

                }
            }
        }

        private List<int> GetFavoriteMovieIds()
        {
            string favMovies = HttpContext.Session.GetString("favMovies");

            if (favMovies != null)
            {
                return JsonSerializer.Deserialize<List<int>>(favMovies, jsonOptions);
            }
            else
            {
                HttpContext.Session.SetString("favMovies", JsonSerializer.Serialize(new List<int>()));
                return GetFavoriteMovieIds();
            }
        }

        private void AddFavMovieId(int id)
        {
            List<int> currFavMovies = GetFavoriteMovieIds();
           
            if (!currFavMovies.Contains(id))
            {
                currFavMovies.Add(id);
                SaveListToSession(currFavMovies);
            }

        }

        private void RemoveMovieId(int id)
        {
            List<int> currFavMovies = GetFavoriteMovieIds();
            currFavMovies.Remove(id);
            SaveListToSession(currFavMovies);
            
        }

        private void SaveListToSession(List<int> list)
        {
            HttpContext.Session.SetString("favMovies", JsonSerializer.Serialize(list));
        }

        #endregion


    }
}