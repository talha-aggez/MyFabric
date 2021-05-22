using System.Collections.Generic;

namespace Core.DBModels
{
    public class AppUser 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual List<Order> Orders { get; set; }

        public virtual List<AppUserRole> AppUserRoles { get; set; }
    }
}
