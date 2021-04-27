using MIS421_FinalProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS421_FinalProject
{
    public class RoleUserVM
    {
        public ApplicationUser AppUser { get; set; }
        public List<string> UserRoles { get; set; }
        public List<string> AllRoles { get; set; }
    }
}
