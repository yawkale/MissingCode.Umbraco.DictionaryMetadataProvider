using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MissingCode.Demo.Controllers
{
    public class ContactModel
    {
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Message { get; set; }
    }
}