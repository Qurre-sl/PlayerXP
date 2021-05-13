namespace PlayerXP
{
    internal class Cfg
    {
        internal static string Lvl { get; set; }
        internal static string Pn { get; set; }
        internal static string Jm { get; set; }
        internal static string Lvlup { get; set; }
        internal static string Eb { get; set; }
        internal static string Kb { get; set; }
        internal static string Prefixs { get; set; }
        internal static void Reload()
        {
            Lvl = Plugin.Config.GetString("playerxp_lvl", "level");
            Pn = Plugin.Config.GetString("playerxp_project", "fydne");
            Jm = Plugin.Config.GetString("playerxp_join", "<color=red>If you write in the nickname</color> <color=#9bff00>#fydne</color>,\n<color=#fdffbb>then you will get 2 times more experience</color>");
            Lvlup = Plugin.Config.GetString("playerxp_lvl_up", "<color=#fdffbb>You got %lvl% level! Until the next level you are missing %to.xp% xp.</color>");
            Eb = Plugin.Config.GetString("playerxp_escape", "<color=#fdffbb>You got %xp%xp for escaping</color>");
            Kb = Plugin.Config.GetString("playerxp_kill", "<color=#fdffbb>You got %xp%xp for killing</color> <color=red>%player%</color>");
            Prefixs = Plugin.Config.GetString("playerxp_prefixs", "");
        }
    }
}