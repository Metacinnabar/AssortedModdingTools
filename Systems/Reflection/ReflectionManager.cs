using System.Reflection;
using Terraria;
using System;

namespace AssortedModdingTools.Systems.Reflection
{
	/// <summary>
	/// Object class handling all reflection. Make sure to null out fields in Unload
	/// </summary>
	public class ReflectionManager
	{
		public Type ModCompile = null;

		public PropertyInfo DeveloperMode = null;

		public MethodInfo DeveloperModeReady = null;

		/// <summary>
		/// This is where you initialize any fields
		/// </summary>
		internal void Load()
		{
			try
			{
				//This may break with updates as it is reflection.
				ModCompile = ReflectionHelper.TerrariaAsb.GetType("Terraria.ModLoader.Core.ModCompile");
				DeveloperMode = ModCompile.GetProperty("DeveloperMode", ReflectionHelper.AllFlags);
				DeveloperModeReady = ModCompile.GetMethod("DeveloperModeReady", ReflectionHelper.AllFlags);
			}
			catch (ReflectionTypeLoadException) { }
		}

		/// <summary>
		/// This is where you unload any fields by nulling them.
		/// </summary>
		internal void Unload()
		{
			ModCompile = null;
			DeveloperMode = null;
		}
	}
}