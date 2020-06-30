using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Howest_Movies_EE_DAL.DTO.MovieDTO;
using Howest_Movies_EE_Web.ViewModels;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Howest_Movies_EE_Web.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("movie-check")]
    public class MovieCheckboxTagHelper : TagHelper
    {


        [HtmlAttributeName("asp-model")]
        public String Model { get; set; }

        [HtmlAttributeName("asp-checked")]
        public IEnumerable<MovieIdViewModel> CheckedMovies { get; set; } = Enumerable.Empty<MovieIdViewModel>();

        private readonly IHttpClientFactory httpClientFactory;
        private readonly JsonSerializerOptions jsonOptions;
        private readonly HttpClient http;



        public MovieCheckboxTagHelper(IHttpClientFactory httpFactory)
        {
            httpClientFactory = httpFactory;

            http = httpClientFactory.CreateClient("MyMovieClient");
            jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            HttpResponseMessage allMovies = await http.GetAsync("v1/movies");
            string allMoviesApiRes = await allMovies.Content.ReadAsStringAsync();

            if (allMovies.IsSuccessStatusCode)
            {
                IEnumerable<ListItemMovieDTO> movieList =
                            JsonSerializer.Deserialize<IEnumerable<ListItemMovieDTO>>(allMoviesApiRes, jsonOptions);



                string res = "";

                    foreach (ListItemMovieDTO m in movieList)
                    {
                        res = $"{res} <div class='movie-item'><input type='checkbox' {((CheckedMovies != null && CheckedMovies.Where(movie => movie.Id == m.Id).FirstOrDefault() != null) ? "checked" : "")} name={Model} value='{m.Id}' id='{m.Id}'/><label for='{m.Id}'>{m.Title}</label></div>";
                    }
               
                output.Content.SetHtmlContent(res);
            }

            
        }
    }
}
