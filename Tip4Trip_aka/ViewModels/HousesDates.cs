using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tip4Trip_aka.Models;

namespace Tip4Trip_aka.ViewModels
{
    public class HousesDates
    {
        public List<House> houses { get; set; }

        public List<Reservation> reservations { get; set; }
        public List<HouseImage> HouseImages { get; set; }
    }
}