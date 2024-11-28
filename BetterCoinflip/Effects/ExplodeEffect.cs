using BetterCoinflip.Interfaces;
using PluginAPI.Core;

namespace BetterCoinflip.Effects;

public class ExplodeEffect : ICoinflipEffectTails
{
    public bool DoEffect(Player player)
    {
        player.SendBroadcast("ka-boom", 3, Broadcast.BroadcastFlags.Normal, false);
        return true;
    }
}