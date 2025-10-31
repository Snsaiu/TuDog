using DryIoc;
using TuDog.Bootstrap;

namespace TuDog.IocContainers.Impl;

public class TuDogContainer() : ITuDogContainer
{
    private readonly IContainer _container = TuDogApplication.ServiceProvider;

    public T Resolve<T>() where T : notnull
    {
        return _container.Resolve<T>();
    }

    public object Resolve(Type serviceType)
    {
        return _container.Resolve(serviceType);
    }
}