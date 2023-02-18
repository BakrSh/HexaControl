using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HexaControl.Models
{
    public class HowWork
    {
        public int Id { get; set; }
        public string SecTitle { get; set; }
        public string SubSecText { get; set; }
        

        public string? SecIconName { get; set; }
        public string? OriginalName { get; set; }
        [NotMapped]
        public IFormFile SecIconFile { get; set; }

        public ICollection<HowWeWorkElement> howWeWorks { get; set; }
    }
}
