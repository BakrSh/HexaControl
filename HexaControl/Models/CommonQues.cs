using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HexaControl.Models
{
    public class CommonQues
    {

        public int Id { get; set; }
        public string Question { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PubDate { get; set; } = DateTime.Today;

        public string Answer { get; set; }
    }
    
}
