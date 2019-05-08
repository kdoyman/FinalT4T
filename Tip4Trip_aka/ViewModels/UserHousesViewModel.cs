using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tip4Trip_aka.Models;

namespace Tip4Trip_aka.ViewModels
{
    public class UserHousesViewModel
    {
       public ApplicationUser User{ get; set; } 
        public List<House> Houses { get; set; }
        public List<HouseImage> HouseImages { get; set; }
    }
}