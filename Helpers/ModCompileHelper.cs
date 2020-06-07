using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;

namespace AssortedModdingTools.Helpers
{
	// TODO test
	public static class ModCompileHelper
	{
		public static readonly string ModSourcePath = Path.Combine(Program.SavePath, "Mod Sources");

		public static readonly string modCompileDir = Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).Location), "ModCompile");

		public static readonly string modCompileVersionPath = Path.Combine(modCompileDir, "version");

		public static bool DeveloperMode => Debugger.IsAttached || File.Exists(modCompileVersionPath) || Directory.Exists(ModSourcePath) && FindModSources().Length > 0;

		public static string[] FindModSources()
		{
			Directory.CreateDirectory(ModSourcePath);
			return Directory.GetDirectories(ModSourcePath, "*", SearchOption.TopDirectoryOnly).Where(dir => new DirectoryInfo(dir).Name[0] != '.').ToArray();
		}
	}
}