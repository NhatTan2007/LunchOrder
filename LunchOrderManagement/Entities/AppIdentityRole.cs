using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Entities
{
    public class AppIdentityRole : IdentityRole
    {
        private bool isActive;

        public bool IsActive { get => isActive; set => isActive = value; }
    }
}
