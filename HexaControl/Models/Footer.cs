using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexaControl.Models
{
    public class Footer
    {

        public int Id { get; set; }
        public string FirstNum { get; set; }
        public string SecondNum { get; set; }

        public string FirstNumLocation { get; set; }
        public string SecondLocation { get; set; }

        public string FirstEmail { get; set; }
        public string SecondEmail { get; set; }

        public ICollection<Social> Socials { get; set; }



    }
}
