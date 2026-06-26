using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;

namespace TuDog.Interfaces.PreferenceServices.Impl;

public sealed class JsonPreferenceService : IPreferenceService
{
    internal static string PreferencesFilePath = "preferences.json";
    private readonly string _fullFilePath;

    public JsonPreferenceService()
    {
        if (OperatingSystem.IsAndroid())
            // 返回应用的配置目录
        {
            _fullFilePath = Path.Combine(FileSystem.AppDataDirectory, PreferencesFilePath);
        }
        else
        {
            var exeName = Path.GetFileName(Environment.ProcessPath)?.Split(".").FirstOrDefault();
            if (exeName is null)
                throw new NullReferenceException("无法获得程序文件名称");

            var folder = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), exeName);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            _fullFilePath = Path.Combine(folder, PreferencesFilePath);
        }

        if (!File.Exists(_fullFilePath))
            File.WriteAllText(_fullFilePath, "{}");
    }

    public void Set<T>(string key, T value)
    {
        var json = File.ReadAllText(_fullFilePath);
        var config = JObject.Parse(json);

        if (value is null)
            config.Remove(key);
        else
            config[key] = JToken.FromObject(value);

        File.WriteAllText(_fullFilePath, config.ToString(Formatting.Indented));
    }

    public void SetNull(string key)
    {
        var json = File.ReadAllText(_fullFilePath);
        var config = JObject.Parse(json);
        if (config.ContainsKey(key))
            config.Remove(key);
        File.WriteAllText(_fullFilePath, config.ToString(Formatting.Indented));
    }

    public T? GetOrDefault<T>(string key)
    {
        var json = File.ReadAllText(_fullFilePath);
        var config = JObject.Parse(json);
        if (!config.TryGetValue(key, out var value)) return default;
        var obj = value.ToObject<T>();
        return obj;
    }

    public T Get<T>(string key, T defaultValue)
    {
        var json = File.ReadAllText(_fullFilePath);
        var config = JObject.Parse(json);
        if (!config.TryGetValue(key, out var value))
        {
            Set(key, defaultValue);
            return defaultValue;
        }

        var obj = value.ToObject<T>();
        return obj ?? defaultValue;
    }
}