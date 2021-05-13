using System;
using System.IO;
namespace PlayerXP
{
	[Serializable]
	public class Stats
	{
		public string UserId;
		public int lvl;
		public int xp;
		public int to;
	}
	public class Methods
	{
		internal static string StatFilePath = Path.Combine(Qurre.PluginManager.PluginsDirectory, "PlayerXP");
		public static Stats LoadStats(string userId)
		{
			string path = Path.Combine(StatFilePath, $"{userId}.txt");
			if (File.Exists(path)) return DeserializeStats(path);
			else
			{
				return new Stats()
				{
					UserId = userId,
					lvl = 1,
					xp = 1,
					to = 750,
				};
			}
		}
		public static void SaveStats(Stats stats)
		{
			string[] write = new[]
			{
				stats.UserId,
				stats.lvl.ToString(),
				stats.xp.ToString(),
				stats.to.ToString(),
			};
			string path = Path.Combine(StatFilePath, $"{stats.UserId}.txt");
			File.WriteAllLines(path, write);
		}
		private static Stats DeserializeStats(string path)
		{
			string[] read = File.ReadAllLines(path);
			Stats stats = new Stats
			{
				UserId = read[0],
				lvl = int.Parse(read[1]),
				xp = int.Parse(read[2]),
				to = int.Parse(read[3]),
			};
			return stats;
		}
	}
}