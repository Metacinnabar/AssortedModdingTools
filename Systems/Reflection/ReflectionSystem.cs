using System;
using System.Reflection;
using Terraria;
using static AssortedModdingTools.Systems.Reflection.ReflectionHelper;

namespace AssortedModdingTools.Systems.Reflection
{
	/// <summary>
	/// Object class handling all reflection. Make sure to null out fields in Unload
	/// </summary>
	public class ReflectionSystem : SystemBase
	{
		public static Type ModCompile = null;

		public static PropertyInfo DeveloperMode = null;

		public static MethodInfo DeveloperModeReady = null;

		public static FieldInfo IsEngineLoaded = null;

		public static FieldInfo OnEngineLoadField = null; //but this is an event?

		public static MethodInfo OnEngineLoad = null; //but this is an event?

		/// <summary>
		/// This is where you initialize any fields
		/// </summary>
		public override void Load()
		{
			try
			{
				//This may break with updates as it is reflection.
				ModCompile = TerrariaAsb.GetType("Terraria.ModLoader.Core.ModCompile");
				DeveloperMode = ModCompile.GetProperty("DeveloperMode", AllFlags);
				DeveloperModeReady = ModCompile.GetMethod("DeveloperModeReady", AllFlags);
				IsEngineLoaded = typeof(Main).GetField("IsEngineLoaded", AllFlags);
				OnEngineLoadField = typeof(Main).GetField("OnEngineLoad", AllFlags);
				OnEngineLoad = OnEngineLoadField.GetValue(null).GetType().GetMethod("Invoke");

			}
			catch (ReflectionTypeLoadException) { }
		}

		/// <summary>
		/// This is where you unload any fields by nulling them. This is automatically handled
		/// </summary>
		public override void Unload()
		{
			FieldInfo[] staticFields = typeof(ReflectionSystem).GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);

			for (int i = 0; i < staticFields.Length; i++)
				staticFields[i].SetValue(null, null);
		}
	}
}