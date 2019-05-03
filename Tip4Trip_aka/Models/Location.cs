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
        [StringLength(50)]
        public string NameCity { get; set; }
        [Required]
        [StringLength(50)]
        public string NameMunicipality { get; set; }
    }
}