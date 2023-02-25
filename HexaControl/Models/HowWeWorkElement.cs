using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HexaControl.Models
{
    public class HowWeWorkElement
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string SubSecText { get; set; }

        public string? IconName { get; set; }
        public string? OriginalName { get; set; }
        [NotMapped]
        public IFormFile IconFile { get; set; }

        [ForeignKey(nameof(HowWorkId))]
        public int HowWorkId { get; set; }
        public HowWork howWork { get; set; }

    }
}
