﻿using HarmonyLib;
using Qurre.API;
using System.Linq;
namespace PlayerXP
{
    [HarmonyPatch(typeof(ServerRoles), "SetText")]
    internal static class PrefixText
    {
        internal static void Prefix(ServerRoles __instance, ref string i)
        {
            Player pl = Player.List.Where(x => x.ServerRoles == __instance).FirstOrDefault();
            if (pl == null) return;
            if (!EventHandlers.Stats.TryGetValue(pl.UserId, out Stats imain)) return;
            int lvl = imain.lvl;
            string pref = $"{lvl} {Cfg.Lvl}";
            string admin = "";
            string your = "";
            if (pl.Adminsearch()) admin = $"{pl.Group.BadgeText} | ";
            if (i != "") your = $" | {i}";
            if (!i.Contains(pref)) i = $"{admin}{pref}{your}";
        }
    }
    [HarmonyPatch(typeof(ServerRoles), "SetColor")]
    internal static class PrefixColor
    {
        internal static void Prefix(ServerRoles __instance, ref string i)
        {
            if (i == "default")
            {
                Player pl = Player.List.Where(x => x.ServerRoles == __instance).FirstOrDefault();
                if (pl == null) return;
                if (!EventHandlers.Stats.TryGetValue(pl.UserId, out Stats imain)) return;
                int lvl = imain.lvl;
                string color = i;
                if (lvl == 1) color = "green";
                else if (lvl == 2) color = "crimson";
                else if (lvl == 3) color = "cyan";
                else if (lvl == 4) color = "deep_pink";
                else if (lvl == 5) color = "yellow";
                else if (lvl == 6) color = "orange";
                else if (lvl == 7) color = "lime";
                else if (lvl == 8) color = "pumpkin";
                else if (lvl == 9) color = "red";
                else if (lvl >= 10 && 20 >= lvl) color = "green";
                else if (lvl >= 20 && 30 >= lvl) color = "crimson";
                else if (lvl >= 30 && 40 >= lvl) color = "cyan";
                else if (lvl >= 40 && 50 >= lvl) color = "deep_pink";
                else if (lvl >= 50 && 60 >= lvl) color = "yellow";
                else if (lvl >= 60 && 70 >= lvl) color = "orange";
                else if (lvl >= 70 && 80 >= lvl) color = "lime";
                else if (lvl >= 80 && 90 >= lvl) color = "pumpkin";
                else if (lvl >= 90 && 100 >= lvl) color = "red";
                else if (lvl >= 100) color = "red";
                i = color;
            }
        }
    }
    [HarmonyPatch(typeof(CharacterClassManager), nameof(CharacterClassManager.UserCode_CmdRequestHideTag))]
    internal static class HideTagPatch
    {
        private static bool Prefix(CharacterClassManager __instance)
        {
            if (__instance.UserId.Contains("@northwood")) return true;
            try
            {
                var pl = Player.Get(__instance.UserId);
                if (pl.ServerRoles.NetworkGlobalBadge != "")
                {
                    pl.ServerRoles.NetworkGlobalBadge = "";
                    EventHandlers.Static.SetPrefix(pl);
                    pl.SendConsoleMessage("Успешно", "green");
                }
                else
                {
                    if (!EventHandlers.Stats.TryGetValue(pl.UserId, out Stats data)) return true;
                    data.anonymous = true;
                    EventHandlers.Static.SetPrefix(pl);
                    pl.SendConsoleMessage("Успешно", "green");
                }
            }
            catch
            {
                __instance.TargetConsolePrint(__instance.connectionToClient, "Зачем тебе убирать префикс?", "green");
            }
            return false;
        }
    }
    [HarmonyPatch(typeof(CharacterClassManager), nameof(CharacterClassManager.UserCode_CmdRequestShowTag))]
    internal static class ShowTagPatch
    {
        private static bool Prefix(CharacterClassManager __instance)
        {
            if (__instance.UserId.Contains("@northwood")) return true;
            try
            {
                var pl = Player.Get(__instance.UserId);
                if (!EventHandlers.Stats.TryGetValue(pl.UserId, out Stats data)) return true;
                data.anonymous = false;
                EventHandlers.Static.SetPrefix(pl);
                pl.SendConsoleMessage("Успешно", "green");
            }
            catch
            {
                __instance.TargetConsolePrint(__instance.connectionToClient, "Зачем тебе это?", "green");
            }
            return false;
        }
    }
}