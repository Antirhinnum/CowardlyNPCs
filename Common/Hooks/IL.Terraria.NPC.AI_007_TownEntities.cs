using MonoMod.Cil;
using Terraria;

namespace CowardlyNPCs.Common.Hooks
{
	internal partial class HookLoader
	{
		/// <summary>
		/// Makes NPCs go home if a boss is alive.
		/// </summary>
		private void NPC_AI_007_TownEntities(ILContext il)
		{
			ILCursor c = new(il);

			#region
			///	flag: If true, the NPC will go to their home tile. Local ID: 1.\
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
			c.EmitDelegate((bool flag) => flag || Main.CurrentFrameFlags.AnyActiveBossNPC);

			#endregion
		}
	}
}