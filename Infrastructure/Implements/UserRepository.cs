using Core.DBModels;
using Core.Interfaces;
using MyFabric.Infrastructure.Implements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implements
{
    public class UserRepository : GenericRepository<User> , IUserRepository
    {
       
    }
}
