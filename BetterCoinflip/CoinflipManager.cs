using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BetterCoinflip.Extensions;
using BetterCoinflip.Interfaces;
using MEC;
using PluginAPI.Events;

namespace BetterCoinflip;

public static class CoinflipManager
{
    private static readonly Dictionary<string, bool> CooldownTimers = new();
    private static List<ICoinflipEffect> GetAllEffects()
    {
        var list =  Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(ICoinflipEffect).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).ToList();
        return list.Select(type => (ICoinflipEffect)Activator.CreateInstance(type)).ToList();
    }
    
    private static List<ICoinflipEffectHeads> GetHeadsEffects()
    {
        var list =  Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(ICoinflipEffectHeads).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).ToList();
        return list.Select(type => (ICoinflipEffectHeads)Activator.CreateInstance(type)).ToList();
    }

    private static List<ICoinflipEffectTails> GetTailsEffects()
    {
        var list =  Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(ICoinflipEffectTails).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).ToList();
        return list.Select(type => (ICoinflipEffectTails)Activator.CreateInstance(type)).ToList();
    }

    public static void CoinEntrypoint(PlayerCoinFlipEvent ev)
    {
        if (CooldownTimers.TryGetValue(ev.Player.UserId, out var val))
        {
            ev.Player.SendBroadcast($"Coinflip is on Cooldown for {val} seconds.", 3);
            return;
        }


        if (!ev.Player.IsAlive) return;
        var ret = false;
        switch (ev.IsTails)
        {
            case true:
            {
                var effects = GetTailsEffects();
                ;
                ret = effects.GetRandomFromList().DoEffect(ev.Player);
                break;
            }
            default:
            {
                var effects = GetHeadsEffects();
                ret = effects.GetRandomFromList().DoEffect(ev.Player);
                break;
            }
        }

        if (!ret)
        {
            ev.Player.SendBroadcast("Internal Coinflip Error", 3);
            return;
        }

        CooldownTimers[ev.Player.UserId] = true;
        Timing.CallDelayed(Plugin.Instance.config.Cooldown, () => CooldownTimers.Remove(ev.Player.UserId));
    }
}