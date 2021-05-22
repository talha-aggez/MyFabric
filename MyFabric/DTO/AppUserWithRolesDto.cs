using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFabric.DTO
{
    public class AppUserWithRolesDto
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}
