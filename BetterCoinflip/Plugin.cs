using PluginAPI.Core.Attributes;

namespace BetterCoinflip;

internal class Config
{
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;
    public float Cooldown { get; set; } = 8f;
}

internal class Plugin
{
    [PluginConfig] public Config config;
    public static Plugin Instance { get; private set; }

    [PluginEntryPoint("BetterCoinflipsDownUnder", "1.0.0", "mmm coin.", "Porta")]
    public void Entry()
    {
        
    }
}