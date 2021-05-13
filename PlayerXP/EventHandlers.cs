using System.Collections.Generic;
using MEC;
using System.Linq;
using System.Text.RegularExpressions;
using Qurre.API.Events;
using Qurre.API;
namespace PlayerXP
{
	public class EventHandlers
	{
		public Dictionary<string, Stats> Stats = new Dictionary<string, Stats>();
		private Regex regexSmartSiteReplacer => new Regex(@"#" + Cfg.Pn);
		public void Waiting()
        {
			Cfg.Reload();
			Stats.Clear();
		}
		public void RoundEnd(RoundEndEvent ev){try{foreach (Stats stats in Stats.Values) Methods.SaveStats(stats);}catch { }}

		public void Join(JoinEvent ev)
		{
			Timing.CallDelayed(100f, () => ev.Player.Broadcast(15, Cfg.Jm));
			if (string.IsNullOrEmpty(ev.Player.UserId) || ev.Player.IsHost || ev.Player.Nickname == "Dedicated Server") return;

			if (!Stats.ContainsKey(ev.Player.UserId))
				Stats.Add(ev.Player.UserId, Methods.LoadStats(ev.Player.UserId));
			Timing.CallDelayed(1.5f, () => addp(ev.Player));
		}
		public void Spawn(RoleChangeEvent ev)
		{
			if (string.IsNullOrEmpty(ev.Player.UserId) || ev.Player.IsHost || ev.Player.Nickname == "Dedicated Server") return;
			if (!Stats.ContainsKey(ev.Player.UserId)) Stats.Add(ev.Player.UserId, Methods.LoadStats(ev.Player.UserId));
			Timing.CallDelayed(1.5f, () => addp(ev.Player));
		}
		public void AddXP(Player player)
		{
			Stats[player.UserId].to = Stats[player.UserId].lvl * 250 + 750;
			if (Stats[player.UserId].xp >= Stats[player.UserId].to)
			{
				Stats[player.UserId].xp -= Stats[player.UserId].to;
				Stats[player.UserId].lvl++;
				Stats[player.UserId].to = Stats[player.UserId].lvl * 250 + 750;
				player.Broadcast(10, Cfg.Lvlup.Replace("%lvl%", $"{Stats[player.UserId].lvl}").Replace("%to.xp%", ((Stats[player.UserId].to) - Stats[player.UserId].xp).ToString()));
				Methods.SaveStats(Stats[player.UserId]);

				addp(player);
			}
		}
		public void addp(Player player)
		{
			string color = "red";
			var imain = Stats[player.UserId];
			int lvl = imain.lvl;
			string prefix = lvl.Prefix();
			string pref = $"{lvl} {Cfg.Lvl}{prefix}";
			try
			{
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
			}
			catch { }
			if (player.Adminsearch()) player.SetRank($"{player.Group.BadgeText} | {pref}", player.Group.BadgeColor);
			else player.SetRank(pref, color);
		}
		public void Escape(EscapeEvent ev)
		{
			Stats[ev.Player.UserId].xp += 100;
			AddXP(ev.Player);
			string nick = ev.Player?.Nickname;
			MatchCollection matches = regexSmartSiteReplacer.Matches(nick);
			if (matches.Count > 0)
			{
				Stats[ev.Player.UserId].xp += 100;
				AddXP(ev.Player);
				ev.Player.Broadcast(10, Cfg.Eb.Replace("%xp%", "200"), true);
			}
			else ev.Player.Broadcast(10, Cfg.Eb.Replace("%xp%", "100"), true);
		}
		public void Dies(DiesEvent ev)
		{
			if (!ev.Allowed) return;
			Player target = ev.Target;
			Player killer = ev.Killer;
			string targetname = ev.Target?.Nickname;
			addp(target);
			if (target == null || string.IsNullOrEmpty(target.UserId)) return;
			if (killer == null || string.IsNullOrEmpty(killer.UserId)) return;
			if (Stats.ContainsKey(killer.UserId))
			{
				string nick = ev.Killer?.Nickname;
				MatchCollection matches = regexSmartSiteReplacer.Matches(nick);
				if (matches.Count > 0)
				{
					if (killer != target)
					{
						ev.Killer.ClearBroadcasts();
						if (killer.Team == Team.CHI)
						{
							if (target.Team == Team.MTF)
							{
								Stats[killer.UserId].xp += 100;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "100").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.RSC)
							{
								Stats[killer.UserId].xp += 50;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "50").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.SCP)
							{
								Stats[killer.UserId].xp += 150;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "150").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.TUT)
							{
								Stats[killer.UserId].xp += 100;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "100").Replace("%player%", $"{targetname}"), true);
							}
						}
						else if (killer.Team == Team.TUT)
						{
							if (target.Team == Team.CDP)
							{
								Stats[killer.UserId].xp += 50;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "50").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.RSC)
							{
								Stats[killer.UserId].xp += 50;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "50").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.MTF)
							{
								Stats[killer.UserId].xp += 100;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "100").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.CHI)
							{
								Stats[killer.UserId].xp += 100;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "100").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.SCP)
							{
								Stats[killer.UserId].xp += 20;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "20").Replace("%player%", $"{targetname}"), true);
							}
						}
						else if (killer.Team == Team.RSC)
						{
							if (target.Team == Team.CDP)
							{
								Stats[killer.UserId].xp += 100;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "100").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.CHI)
							{
								Stats[killer.UserId].xp += 200;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "200").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.TUT)
							{
								Stats[killer.UserId].xp += 200;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "200").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.SCP)
							{
								Stats[killer.UserId].xp += 400;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "400").Replace("%player%", $"{targetname}"), true);
							}
						}
						else if (killer.Team == Team.CDP)
						{
							if (target.Team == Team.RSC)
							{
								Stats[killer.UserId].xp += 100;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "100").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.MTF)
							{
								Stats[killer.UserId].xp += 200;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "200").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.SCP)
							{
								Stats[killer.UserId].xp += 400;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "400").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.TUT)
							{
								Stats[killer.UserId].xp += 200;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "200").Replace("%player%", $"{targetname}"), true);
							}
						}
						else if (killer.Team == Team.SCP)
						{
							Stats[killer.UserId].xp += 50;
							AddXP(killer);
							ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "50").Replace("%player%", $"{targetname}"), true);
						}
					}
				}
				else
				{
					if (killer != target)
					{
						ev.Killer.ClearBroadcasts();
						if (killer.Team == Team.CHI)
						{
							if (target.Team == Team.MTF)
							{
								Stats[killer.UserId].xp += 50;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "50").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.RSC)
							{
								Stats[killer.UserId].xp += 25;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "25").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.SCP)
							{
								Stats[killer.UserId].xp += 75;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "75").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.TUT)
							{
								Stats[killer.UserId].xp += 50;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "50").Replace("%player%", $"{targetname}"), true);
							}
						}
						else if (killer.Team == Team.TUT)
						{
							if (target.Team == Team.CDP)
							{
								Stats[killer.UserId].xp += 25;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "25").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.RSC)
							{
								Stats[killer.UserId].xp += 25;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "25").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.MTF)
							{
								Stats[killer.UserId].xp += 50;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "50").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.CHI)
							{
								Stats[killer.UserId].xp += 50;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "50").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.SCP)
							{
								Stats[killer.UserId].xp += 10;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "10").Replace("%player%", $"{targetname}"), true);
							}
						}
						else if (killer.Team == Team.RSC)
						{
							if (target.Team == Team.CDP)
							{
								Stats[killer.UserId].xp += 50;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "50").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.CHI)
							{
								Stats[killer.UserId].xp += 100;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "100").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.TUT)
							{
								Stats[killer.UserId].xp += 100;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "100").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.SCP)
							{
								Stats[killer.UserId].xp += 200;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "200").Replace("%player%", $"{targetname}"), true);
							}
						}
						else if (killer.Team == Team.CDP)
						{
							if (target.Team == Team.RSC)
							{
								Stats[killer.UserId].xp += 50;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "50").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.MTF)
							{
								Stats[killer.UserId].xp += 100;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "100").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.SCP)
							{
								Stats[killer.UserId].xp += 200;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "200").Replace("%player%", $"{targetname}"), true);
							}
							else if (target.Team == Team.TUT)
							{
								Stats[killer.UserId].xp += 100;
								AddXP(killer);
								ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "100").Replace("%player%", $"{targetname}"), true);
							}
						}
						else if (killer.Team == Team.SCP)
						{
							Stats[killer.UserId].xp += 25;
							AddXP(killer);
							ev.Killer.Broadcast(10, Cfg.Kb.Replace("%xp%", "25").Replace("%player%", $"{targetname}"), true);
						}
					}
				}
			}
		}
		public void PocketDead(PocketDimensionFailEscapeEvent ev)
		{
			Player scp106 = Player.List.Where(x => x.Role == RoleType.Scp106).FirstOrDefault();
			Stats[scp106.UserId].xp += 25;
			AddXP(scp106);
			string nick = scp106?.Nickname;
			MatchCollection matches = regexSmartSiteReplacer.Matches(nick);
			if (matches.Count > 0)
			{
				Stats[scp106.UserId].xp += 25;
				AddXP(scp106);
				scp106.Broadcast(10, Cfg.Kb.Replace("%xp%", "50").Replace("%player%", $"{ev.Player?.Nickname}"), true);
			}
			else scp106.Broadcast(10, Cfg.Kb.Replace("%xp%", "25").Replace("%player%", $"{ev.Player?.Nickname}"), true);
		}
		public void Console(SendingConsoleEvent ev)
		{
			string cmd = ev.Name.ToLower();
			if (cmd.StartsWith("xp"))
			{
				ev.ReturnMessage = "\n----------------------------------------------------------- \nXP:\n" + Stats[ev.Player.UserId].xp + "/" + Stats[ev.Player.UserId].to + "\nlvl:\n" + Stats[ev.Player.UserId].lvl + "\n -----------------------------------------------------------";
				ev.Color = "red";
			}
			if (cmd.StartsWith("lvl"))
			{
				ev.ReturnMessage = "\n----------------------------------------------------------- \nXP:\n" + Stats[ev.Player.UserId].xp + "/" + Stats[ev.Player.UserId].to + "\nlvl:\n" + Stats[ev.Player.UserId].lvl + "\n -----------------------------------------------------------";
				ev.Color = "red";
			}
		}
	}
}