using System.Collections.Generic;

namespace Core.DBModels
{
    public class AppRole 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<AppUserRole> AppUserRoles { get; set; }
    }
}
