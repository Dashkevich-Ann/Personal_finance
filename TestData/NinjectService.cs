using DataLayer.Interfaces;
using DataLayer.Repositories;
using Ninject.Modules;

namespace TestData
{
    internal class NinjectService : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("FinanceConnection");
        }
    }   
}