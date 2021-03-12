using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Models.Account
{
    public class LoginAccountViewModel
    {
        private string _email;
        private string _password;
        private bool _isRemember;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get => _email; set => _email = value; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get => _password; set => _password = value; }
        public bool IsRemember { get => _isRemember; set => _isRemember = value; }
        public string ReturnUrl { get; set; }
    }
}
