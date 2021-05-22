using Core.DBModels;
using Core.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MyFabric.Infrastructure.Implements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implements
{
    public class AppRoleRepository : GenericRepository<AppRole>, IAppRoleRepository
    {
        public async Task<AppRole> FindByRoleName(string roleName)
        {
            using var context = new StoreContext();
            return await context.Roles.FirstOrDefaultAsync(p => p.Name==roleName);
        }
 
    }
}
