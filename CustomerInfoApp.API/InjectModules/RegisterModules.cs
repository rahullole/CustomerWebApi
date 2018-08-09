using CustomerInfoApp.API.Validators.Customer;
using CustomerInfoApp.Models;
using CustomerInfoApp.Models.Models;
using CustomerInfoApp.Repositories.GenericRepository;
using CustomerInfoApp.Service;
using CustomerInfoApp.Services;
using System.Data.Entity;
using Unity;
using Unity.Lifetime;

namespace CustomerInfoApp.API.InjectModules
{
    public class RegisterModules
    {
        public static UnityContainer GetUnityContainer()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<ICustomerService, CustomerService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerContractValidator, CustomerContractValidator>(new HierarchicalLifetimeManager());
            container.RegisterType<IGenericRepository<Customer>, GenericRepository<Customer>>(new HierarchicalLifetimeManager());
            container.RegisterType<DbContext, CustomerDBContext>(new HierarchicalLifetimeManager());

            container = Services.InjectModules.RegisterModules.RegisterDependency(container);

            return container;
        }
    }
}