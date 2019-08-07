using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IAdminService : IDisposable
    {
        
        Task BlockAccount(int userId, string aLogin);
        Task UnblockAccount(int userId);
    }
}
