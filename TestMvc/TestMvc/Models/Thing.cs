using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestMvc.Models
{
    public class Thing
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }

        public int Size { get; set; }

        public bool IsEdited { get; set; }
    }
}