using MonoMod.Cil;
using System;
using Terraria.ModLoader;

namespace CowardlyNPCs.Common.Hooks
{
	internal partial class HookLoader : ILoadable
	{
		private const string ILFailMessage = "{0} could not patch {1}.";
		private static Mod _mod;

		public void Load(Mod mod)
		{
			_mod = mod;

			IL.Terraria.NPC.AI_007_TownEntities += NPC_AI_007_TownEntities;
		}

		public void Unload()
		{
			_mod = null;

			IL.Terraria.NPC.AI_007_TownEntities -= NPC_AI_007_TownEntities;
		}

		private static void LogHookFailed(ILContext failingIL)
		{
			_mod.Logger.ErrorFormat(ILFailMessage, failingIL.Method.FullName);
			throw new Exception(string.Format(ILFailMessage, _mod.Name, failingIL.Method.FullName));
		}
	}
}