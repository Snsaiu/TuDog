using TuDog.Interfaces.LocalPreferences;
using TuDog.Interfaces.PreferenceServices.Impl;
using TuDog.IocAttribute;

namespace Demo.Services;

public interface INameConfigService : ILocalConfigService<string>;

[Register<INameConfigService>]
public sealed class NameConfigService : LocalConfigService<string>, INameConfigService
{
    public override string Key => "name";
    public override string Default => "demo";
}