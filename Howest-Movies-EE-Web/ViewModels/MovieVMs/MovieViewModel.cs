using System;

namespace Mowest_Movies_EE_Web.ViewModels
{
    public class MovieViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public int Year { get; set; }
        public string Plot { get; set; }
        public decimal Rating { get; set; }
        public int Stars
        {
            get
            {
                return ((int)Math.Round(decimal.ToInt16(Rating) / 20.0));
            }
        }
        public string ScorePerception { get {
                string res;
                switch (Rating)
                {
                    case decimal r when r > 85:
                        res = "awesome";
                        break;
                    case decimal r when r > 70:
                        res = "good";
                        break;
                    case decimal r when r > 50:
                        res = "mediocre";
                        break;
                    default:
                        res = "bad";
                        break;
                }

                return res;
        } }
    }
}
