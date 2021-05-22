using Core.DBModels;
using Core.Interfaces;
using MyFabric.Infrastructure.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implements
{
    public class AppUserRoleRepository : GenericRepository<AppUserRole>, IAppUserRoleRepository
    {
    }
}
