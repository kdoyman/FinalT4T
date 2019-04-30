using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tip4Trip_aka.Models
{
    public class Location
    {

        public int Id { get; set; }
        [Required]
        public string NameCity { get; set; }
        public string NameMunicipality { get; set; }
    }
}