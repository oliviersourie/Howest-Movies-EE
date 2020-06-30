using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mowest_Movies_EE_Web.ViewModels
{
    public class PersonViewModel
    {
        public long Id { get; set; }
        public string ImdbId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
    }
}
