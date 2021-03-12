using LunchOrderManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Models.Account
{
    public class AccountViewModel
    {
        private string _userId;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _className;

        public string UserId { get => _userId; set => _userId = value; }
        public string FullName => $"{_firstName} {_lastName}";
        [Display(Name = "First name")]
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string MiddleName { get => _middleName; set => _middleName = value; }
        [Display(Name = "Last name")]
        public string LastName { get => _lastName; set => _lastName = value; }
        public string Email { get => _email; set => _email = value; }
        [Display(Name = "Phone number")]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        [Display(Name = "Class name")]
        public string ClassName { get => _className; set => _className = value; }
    }
}
