using MonoMod.Cil;
using System;
using Terraria;

namespace CowardlyNPCs.Common.Hooks
{
	internal partial class HookLoader
	{
		/// <summary>
		/// Makes NPCs go home if a boss is alive.
		/// </summary>
		private static void NPC_AI_007_TownEntities(ILContext il)
		{
			ILCursor c = new ILCursor(il);

			#region
			///	flag: If true, the NPC will go to their home tile. Local ID: 1.
			/// Match:
			///		bool flag = Main.raining;
			///	Change to:
			///		bool flag = Main.raining || Main.CurrentFrameFlags.AnyActiveBossNPC;

			if (!c.TryGotoNext(MoveType.Before,
					i => i.MatchLdsfld<Main>(nameof(Main.raining)),
					i => i.MatchStloc(1)
				))
			{
				LogHookFailed(il);
				return;
			}

			c.Index += 1;
			c.EmitDelegate<Func<bool, bool>>((bool flag) => flag || BossCheckWorld.AnyBoss);

			#endregion
		}
	}
}