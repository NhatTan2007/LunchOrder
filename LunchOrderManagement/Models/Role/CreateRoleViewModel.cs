using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Models.Role
{
    public class CreateRoleViewModel
    {
        private string _roleName;
        //private bool _isActive;
        [Required(ErrorMessage = "Role name is required")]
        [Display(Name = "Role name")]
        public string RoleName { get => _roleName; set => _roleName = value; }
    }
}
