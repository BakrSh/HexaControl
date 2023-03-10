using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HexaControl.Models
{
    public class Social
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string? IconName { get; set; }
        public string? OriginalName { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        [ForeignKey(nameof(FooterId))]
        public int FooterId { get; set; }
        public Footer footer { get; set; }


    }
}
