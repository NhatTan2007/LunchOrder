using LunchOrderManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Models.User
{
    public class UserViewModel
    {
        private string _id;
        private string _fullName;
        private string _email;
        private IEnumerable<string> _rolesName;

        public string Id { get => _id; set => _id = value; }
        public string FullName { get => _fullName; set => _fullName = value; }
        public string Email { get => _email; set => _email = value; }
        public IEnumerable<string> RolesName { get => _rolesName; set => _rolesName = value; }
    }
}
