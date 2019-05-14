using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tip4Trip_aka.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You need to fill in a name")]
        [DisplayName("Name*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You need to fill in an email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Your email address contains some errors")]
        [DisplayName("Email*")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You need to fill in a subject")]
        [DisplayName("Subject*")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "You need to fill in a message")]
        [DisplayName("Message*")]
        public string Message { get; set; }
    }
}