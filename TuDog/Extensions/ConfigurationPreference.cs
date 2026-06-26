using DryIoc;
using TuDog.Interfaces.PreferenceServices.Impl;

namespace TuDog.Extensions;

public static class ConfigurationPreference
{
    public static void ConfigPreferenceFileName(this IContainer services, Func<string> fileName)
    {
        JsonPreferenceService.PreferencesFilePath = fileName();
    }
}