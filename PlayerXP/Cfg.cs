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
            Lvl = Plugin.Config.GetString("playerxp_lvl", "level", "needed for translation (ex: 1 level, 2 level, etc)");
            Pn = Plugin.Config.GetString("playerxp_project", "fydne", "If a player writes in his nickname \"# + the name of your project\", he will receive 2 times more experience");
            Jm = Plugin.Config.GetString("playerxp_join",
                "<color=red>If you write in the nickname</color> <color=#9bff00>#fydne</color>,\n<color=#fdffbb>then you will get 2 times more experience</color>",
                "BroadCast to the player when he joins the server.\n#fydne - # + the \"playerxp_project\" you specified");
            Lvlup = Plugin.Config.GetString("playerxp_lvl_up", "<color=#fdffbb>You got %lvl% level! Until the next level you are missing %to.xp% xp.</color>",
                "BroadCast to a player when they level up");
            Eb = Plugin.Config.GetString("playerxp_escape", "<color=#fdffbb>You got %xp%xp for escaping</color>",
                "BroadCast to a player when they get xp for escaping");
            Kb = Plugin.Config.GetString("playerxp_kill", "<color=#fdffbb>You got %xp%xp for killing</color> <color=red>%player%</color>",
                "BroadCast to a player when they get xp for killing a player");
            Prefixs = Plugin.Config.GetString("playerxp_prefixs", "", "Level prefixes\n" +
                "Example: (playerxp_prefixs: 1:beginner,2:player,3:thinking) - (1 level | beginner ; 2 level | player ; 3 level | thinking ; etc..");
        }
    }
}