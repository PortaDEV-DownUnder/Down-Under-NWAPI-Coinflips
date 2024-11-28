using System.Collections.Generic;
using PluginAPI.Core;

namespace BetterCoinflip.Extensions;

public static class Extensions
{
    public static void LogInfo(this string message) => Log.Info(message);

    public static void LogDebug(this string message)
    {
        if (Plugin.Instance.config.Debug)
            Log.Debug(message);
    }
    
    public static T GetRandomFromList<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}