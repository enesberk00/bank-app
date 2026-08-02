using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.Repositories
{
    internal interface IUnitOfWork:IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}
