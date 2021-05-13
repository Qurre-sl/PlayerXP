using System;
using System.IO;
namespace PlayerXP
{
    public class Plugin : Qurre.Plugin
    {
        #region override
        public override int Priority { get; } = 9999999;
        public override string Developer { get; } = "fydne";
        public override string Name { get; } = "PlayerXP";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version NeededQurreVersion { get; } = new Version(1, 3, 9);
        public override void Enable() => RegisterEvents();
        public override void Disable() => UnregisterEvents();
        #endregion
        #region Events
        public EventHandlers EventHandlers;
        private void RegisterEvents()
        {
            Cfg.Reload();
            EventHandlers = new EventHandlers();
            Qurre.Events.Round.WaitingForPlayers += EventHandlers.Waiting;
            Qurre.Events.Round.End += EventHandlers.RoundEnd;
            Qurre.Events.Server.SendingConsole += EventHandlers.Console;

            Qurre.Events.Player.Join += EventHandlers.Join;
            Qurre.Events.Player.RoleChange += EventHandlers.Spawn;
            Qurre.Events.Player.Escape += EventHandlers.Escape;
            Qurre.Events.Player.Dies += EventHandlers.Dies;
            Qurre.Events.SCPs.SCP106.PocketDimensionFailEscape += EventHandlers.PocketDead;
            if (!Directory.Exists(Methods.StatFilePath)) Directory.CreateDirectory(Methods.StatFilePath);
        }
        private void UnregisterEvents()
        {
            Qurre.Events.Round.WaitingForPlayers -= EventHandlers.Waiting;
            Qurre.Events.Round.End -= EventHandlers.RoundEnd;
            Qurre.Events.Server.SendingConsole -= EventHandlers.Console;

            Qurre.Events.Player.Join -= EventHandlers.Join;
            Qurre.Events.Player.RoleChange -= EventHandlers.Spawn;
            Qurre.Events.Player.Escape -= EventHandlers.Escape;
            Qurre.Events.Player.Dies -= EventHandlers.Dies;
            Qurre.Events.SCPs.SCP106.PocketDimensionFailEscape -= EventHandlers.PocketDead;
            EventHandlers = null;
        }
        #endregion
    }
}