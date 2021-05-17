using Core.DBModels;
using MyFabric.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
    }
}
