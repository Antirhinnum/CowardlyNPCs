using CowardlyNPCs.Common.Hooks;
using Terraria.ModLoader;

namespace CowardlyNPCs
{
	public class CowardlyNPCs : Mod
	{
		public override void Load()
		{
			HookLoader.Load(this);
		}

		public override void Unload()
		{
			HookLoader.Unload();
		}
	}
}