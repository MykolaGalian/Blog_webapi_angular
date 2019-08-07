using DAL.Contracts;
using DAL.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Infrastructure
{
    public class DIResolver: NinjectModule  // class in  BLL, since in API can't refer to DLL
    {
        private readonly string _connectionString;
        public DIResolver(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Loads the module into the kernel
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(_connectionString);
        }

        //WithConstructorArgument - during mapping types we must set the argument passed to the constructor, since
        //UnitOfWork uses the database connection string argument in the class constructor
    }
}
