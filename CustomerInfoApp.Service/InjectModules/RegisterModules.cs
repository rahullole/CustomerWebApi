using CustomerInfoApp.Models.Models;
using CustomerInfoApp.Repositories.GenericRepository;
using Unity;
using Unity.Lifetime;

namespace CustomerInfoApp.Services.InjectModules
{
    public static class RegisterModules
    {
        //Method where we need to register all the dependencies and return the container
        //This container gets resolved in the WebApiConfig
        public static UnityContainer RegisterDependency(UnityContainer container)
        {
            container.RegisterType<IGenericRepository<Customer>, GenericRepository<Customer>>(new HierarchicalLifetimeManager());
            return container;
        }

    }
}
