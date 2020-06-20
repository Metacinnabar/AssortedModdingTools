using System;
using System.Reflection;
using Terraria;

namespace AssortedModdingTools.Systems.Reflection
{
	public static class ReflectionHelper
	{
		public static readonly BindingFlags AllFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

		public static readonly Assembly TerrariaAsb = Assembly.GetAssembly(typeof(Main));

		public static TMemberInfo GetReflection<TMemberInfo>(Type classType, string name, MemberTypes type, Type[] methodTypes = null) where TMemberInfo : MemberInfo
		{
			switch (type)
			{
				case MemberTypes.Field:
					return classType.GetField(name, AllFlags) as TMemberInfo;

				case MemberTypes.Method:
					if (methodTypes == null)
						return classType.GetMethod(name, AllFlags) as TMemberInfo;
					else
						return classType.GetMethod(name, methodTypes) as TMemberInfo;

				case MemberTypes.Property:
					return classType.GetProperty(name, AllFlags) as TMemberInfo;

				case MemberTypes.Event:
					return classType.GetEvent(name, AllFlags) as TMemberInfo;

				default:
					return null;
			}
		}

		public static TMemberInfo GetReflection<TMemberInfo, ClassType>(string name, MemberTypes type, Type[] methodTypes = null) where TMemberInfo : MemberInfo
		{
			return GetReflection<TMemberInfo>(typeof(ClassType), name, type, methodTypes);
		}

		public static TType GetReflectionValue<TMemberInfo, ClassType, TType>(string name, MemberTypes type, ClassType classInstance = default, Type[] methodTypes = null) where TMemberInfo : MemberInfo
		{
			TMemberInfo info = GetReflection<TMemberInfo, ClassType>(name, type, methodTypes);

			switch (type)
			{
				case MemberTypes.Field:
					return (TType)(info as FieldInfo).GetValue(classInstance);

				case MemberTypes.Property:
					return (TType)(info as PropertyInfo).GetValue(classInstance);

				default:
					return default;
			}
		}
	}
}