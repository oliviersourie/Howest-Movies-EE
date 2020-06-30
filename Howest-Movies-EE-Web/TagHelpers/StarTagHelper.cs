using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mowest_Movies_EE_Web.TagHelpers
{
    [HtmlTargetElement("movie-stars")]
    public class StarTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-amount")]
        public int Amount { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string res = "";
            string star = "<svg xmlns='http://www.w3.org/2000/svg' height='24' viewBox='0 0 24 24' width='24'><path d='M12 17.27L18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21z'/><path d='M0 0h24v24H0z' fill='none'/></svg>";

            for(int i = 0; i < Amount; i++)
            {
                res = $"{res}{star}";
            }

            output.AddClass("rate", HtmlEncoder.Default);
            output.Content.SetHtmlContent(res);

        }
    }
}
