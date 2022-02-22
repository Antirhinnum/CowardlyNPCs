using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CowardlyNPCs.Common
{
	public class BossCheckWorld : ModWorld
	{
		public static bool AnyBoss { get; private set; }

		public override void PostUpdate()
		{
			AnyBoss = false;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].active && (Main.npc[i].boss || NPCID.Sets.TechnicallyABoss[Main.npc[i].type]))
				{
					AnyBoss = true;
					return;
				}
			}
		}
	}
}