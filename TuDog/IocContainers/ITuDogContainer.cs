namespace TuDog.IocContainers;

public interface ITuDogContainer
{
    public T Resolve<T>() where T : notnull;

    public object Resolve(Type serviceType);
}