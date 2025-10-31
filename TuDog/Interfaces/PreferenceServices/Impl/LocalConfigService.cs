
using DryIoc;
using TuDog.Bootstrap;
using TuDog.Interfaces.LocalPreferences;

namespace TuDog.Interfaces.PreferenceServices.Impl;

public abstract class LocalConfigService<T>() : ILocalConfigService<T>
{
    private IPreferenceService PreferenceService =>
        TuDogApplication.ServiceProvider.Resolve<IPreferenceService>();

    public abstract string Key { get; }

    public void SetNull()
    {
        PreferenceService.SetNull(Key);
    }

    public abstract T Default { get; }

    public T Get()
    {
        return PreferenceService.Get(Key, Default);
    }

    public T? GetOrDefault()
    {
        return PreferenceService.GetOrDefault<T?>(Key);
    }

    public void Set(T value)
    {
        PreferenceService.Set(Key, value);
    }
}