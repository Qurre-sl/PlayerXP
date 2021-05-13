using Qurre.API;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PlayerXP
{
	internal static class Extensions
	{
		internal static void SetRank(this Player player, string rank, string color = "default")
		{
			player.RoleName = rank;
			player.RoleColor = color;
		}
		internal static string GetGroupName(this UserGroup group) => ServerStatic.GetPermissionsHandler().GetAllGroups().Where(p => p.Value == group).Select(p => p.Key).FirstOrDefault();
		internal static bool Adminsearch(this Player hub)
		{
			string group = hub.Group.GetGroupName();
			return !string.IsNullOrEmpty(group);
		}
		internal static string Prefix(this int lvl)
		{
			string prfx = Cfg.Prefixs;
			if (prfx.Contains(','))
			{
				var array = prfx.Split(',');
				foreach (string txt in array.Where(x => x.Contains(':')))
				{
					try
					{
						var array2 = txt.Split(':');
						if (Convert.ToInt32(array2[0]) == lvl) return $" | {array2[1]}";
					}
					catch { }
				}
			}
			else if (prfx.Contains(':'))
			{
				try
				{
					var array2 = prfx.Split(':');
					if (Convert.ToInt32(array2[0]) == lvl) return $" | {array2[1]}";
				}
				catch { }
			}
			return "";
		}
	}
}