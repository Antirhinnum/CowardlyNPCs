using MonoMod.Cil;
using Terraria;

namespace CowardlyNPCs.Common.Hooks;

internal sealed partial class HookLoader
{
	/// <summary>
	/// Makes NPCs go home if a boss is alive.
	/// </summary>
	private static void NPC_AI_007_TownEntities(ILContext il)
	{
		ILCursor c = new(il);

		///	flag: If true, the NPC will go to their home tile. Local ID: 1.
		/// Match:
		///		bool flag = Main.raining;
		///	Change to:
		///		bool flag = Main.raining || Main.CurrentFrameFlags.AnyActiveBossNPC;

		const int local_shouldGoToHomeTile = 1;

		if (!c.TryGotoNext(MoveType.Before,
				i => i.MatchLdsfld<Main>(nameof(Main.raining)),
				i => i.MatchStloc(local_shouldGoToHomeTile)
			))
		{
			LogHookFailed(il);
			return;
		}

		c.Index += 1;
		c.EmitDelegate((bool flag) => flag || Main.CurrentFrameFlags.AnyActiveBossNPC);
	}
}