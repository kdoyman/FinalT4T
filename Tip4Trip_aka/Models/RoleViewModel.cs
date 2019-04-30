using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tip4Trip_aka.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        public string name { get; set; }
        public RoleViewModel() { }
        public RoleViewModel(AppRole role)
        {
            Id = role.Id;
            name = role.Name;
        }
            }
}
