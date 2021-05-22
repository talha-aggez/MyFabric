using Core.DBModels;
using Core.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

using MyFabric.Infrastructure.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implements
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
       

        public async Task<bool> CheckPassword(string name,string password)
        {
            using var context = new StoreContext();
            var appUser = await context.AppUsers.FirstOrDefaultAsync(p => p.Name == name);
            
            return appUser.Password == password ? true : false;
        }

        public async Task<AppUser> FindByUserName(string userName)
        {
            using var context = new StoreContext();
            return await context.AppUsers.FirstOrDefaultAsync(p => p.Name == userName);
        }
      

        public async Task<List<AppRole>> GetRolesByUserName(string userName)
        {
            using var context = new StoreContext();

            return await context.AppUsers.Join(context.AppUserRoles, u => u.ID, ur => ur.AppUserId,
                (user, userRole) => new
                {
                    user = user,
                    userRole = userRole

                }).Join(context.Roles, two => two.userRole.AppRoleId, r => r.Id,
                    (twoTable, role) => new
                    {
                        user = twoTable.user,
                        userRole = twoTable.userRole,
                        role = role
                    }).Where(I => I.user.Name == userName).Select(I => new AppRole
                    {
                        Id = I.role.Id,
                        Name = I.role.Name,

                    }).ToListAsync();
        }
    }
}
