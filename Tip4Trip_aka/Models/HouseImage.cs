using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Tip4Trip_aka.Models
{
    public class HouseImage
    {
        public int Id { get; set; }

        public int housId { get; set; }


        [DisplayName("Upload File")]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}