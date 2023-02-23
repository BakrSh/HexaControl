using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HexaControl.Models
{
    public class Blog
    {

        public int Id { get; set; }
        public string MainArticle { get; set; }
        public bool isChecked { get; set; } = false;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PubDate { get; set; }


        public string DeclarativeParagraph { get; set; }
       
        public string? MainName { get; set; }
        public string? MainOriginalName { get; set; }
        [NotMapped]
        public IFormFile MainFile { get; set; }


        public string FirstParg { get; set; }

        public string? firstName { get; set; }
        public string? FirstOriginalName { get; set; }
        [NotMapped]
        public IFormFile FirstFile { get; set; }



        public string SecondParg { get; set; }

        public string? SecondName { get; set; }
        public string? SecondOriginalName { get; set; }
        [NotMapped]
        public IFormFile SecondFile { get; set; }
    }
}
