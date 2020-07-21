using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ARPrj.WebManagement.Models
{
    public class AccountView
    {
        [Required(ErrorMessage ="Username required")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}