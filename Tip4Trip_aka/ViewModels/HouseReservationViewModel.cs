using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tip4Trip_aka.Models;

namespace Tip4Trip_aka.ViewModels
{
    public class HouseReservationViewModel
    {
        public House House { get; set; }
        public List<Reservation> Reservations1 { get; set; }
    }
}