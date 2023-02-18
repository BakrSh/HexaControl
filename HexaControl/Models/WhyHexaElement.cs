using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HexaControl.Models
{
    public class WhyHexaElement
    {

        public int Id { get; set; }
        public string Text { get; set; }

        public string? IconName { get; set; }
        public string? OriginalName { get; set; }
        [NotMapped]
        public IFormFile IconFile { get; set; }

        [ForeignKey(nameof(WhyHexaId))]
        public int WhyHexaId { get; set; }
        public WhyHexa whyHexa { get; set; }

    }
}
