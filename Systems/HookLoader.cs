using System.Reflection;
using Terraria.ModLoader;
using System;
using AssortedModdingTools.Systems;

namespace AssortedModdingTools.Systems
{
	internal static class HookLoader
	{
		public static void Initialize()
		{
			HookBase.BaseHookLoader.Initialize();
			MenuBase.MenuBaseHookLoader.Initialize();
		}

		public static void CallLoad()
		{
			foreach (Type classType in Assembly.GetAssembly(typeof(MainMod)).GetTypes())
			{
				try
				{
					if (!classType.IsClass || classType.IsAbstract)
						continue;

					Type iloadableType = classType.GetInterface(nameof(IHookBase));
					if (iloadableType == null)
					{
						continue;
					}

					var loadable = (IHookBase)SafelyGetInstanceForType(classType);

					//IHookBase hookBase = classType as IHookBase;

					//if (hookBase == null)
					//{
					//	continue;
					//}

					loadable?.Load();
				}
				catch { }
			}
		}

		public static T SafelyGetInstance<T>() where T : class
		{
			T instance = ModContent.GetInstance<T>();
			if (instance != null)
			{
				return instance;
			}

			instance = (T)Activator.CreateInstance(
				typeof(T),
				BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
				null,
				new object[] { },
				null
			);
			if (instance == null)
			{
				throw new Exception("Could not generate singleton for " + typeof(T).Name);
			}

			ContentInstance.Register(instance);

			return instance;

		}

		public static object SafelyGetInstanceForType(Type classType)
		{
			MethodInfo method = typeof(HookLoader).GetMethod("SafelyGetInstance");
			MethodInfo genericMethod = method.MakeGenericMethod(classType);

			object rawInstance = genericMethod.Invoke(null, new object[] { });
			if (rawInstance == null)
			{
				throw new Exception("Could not get singleton of " + classType.Name);
			}

			return rawInstance;
		}
	}
}