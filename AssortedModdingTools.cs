using MonoMod.RuntimeDetour.HookGen;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader.UI;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using AssortedModdingTools.DataStructures;
using AssortedModdingTools.UI.Elements;
using AssortedModdingTools.Extensions;

namespace AssortedModdingTools
{
	public class AssortedModdingTools : Mod
	{
		public static ReflectionManager Reflection { get; private set; } = null;

		public delegate void Hook_AddMenuButtons(Orig_AddMenuButtons orig, Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons);

		public delegate void Orig_AddMenuButtons(Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons);

		public static event Hook_AddMenuButtons On_AddMenuButtons
		{
			add
			{
				HookEndpointManager.Add<Hook_AddMenuButtons>(typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetMethod("AddMenuButtons", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static), value);
			}
			remove
			{
				HookEndpointManager.Remove<Hook_AddMenuButtons>(typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetMethod("AddMenuButtons", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static), value);
			}
		}

		public override void Load()
		{
			// Initialization
			Reflection = new ReflectionManager();
			HookLoader.Initialize();
			// Loading
			Reflection.Load();
			HookLoader.CallLoad();
			HookBase.BaseHookLoader.CallOnLoad(this);
			// Method Swaps and Injection Subscribing
			IL.Terraria.Main.DrawMenu += Main_DrawMenu;
			On_AddMenuButtons += Interface_AddMenuButtons;
			On.Terraria.Main.DrawMenu += Main_DrawMenu1;
		}

		private void Main_DrawMenu1(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
		{
			MenuBase.MenuBaseHookLoader.CallPreDrawMenu();
			orig(self, gameTime);
			MenuBase.MenuBaseHookLoader.CallPostDrawMenu();
		}

		private void Main_DrawMenu(ILContext il)
		{
			ILCursor c = new ILCursor(il);

			// else if (this.logoScaleSpeed > -50f & this.logoScaleDirection == -1f)
			//IL_02cc: ldarg.0
			//IL_02cd: ldfld float32 Terraria.Main::logoScaleSpeed
			//IL_02d2: ldc.r4 - 50
			//IL_02d7: cgt
			//IL_02d9: ldarg.0
			//IL_02da: ldfld float32 Terraria.Main::logoScaleDirection
			//IL_02df: ldc.r4 - 1
			//IL_02e4: ceq
			//IL_02e6: and

			/*
			FieldInfo logoScaleSpeed = null;
			FieldInfo logoScaleDirection = null;

			try
			{
				logoScaleSpeed = typeof(Main).GetField("logoScaleSpeed", BindingFlags.NonPublic | BindingFlags.Instance);
				logoScaleDirection = typeof(Main).GetField("logoScaleDirection", BindingFlags.NonPublic | BindingFlags.Instance);
			}
			catch (ReflectionTypeLoadException e) { }

			if (c.TryGotoNext(MoveType.After,
				e => e.MatchLdarg(0),
				e => e.MatchLdfld(logoScaleSpeed),
				e => e.MatchLdcR4(-50f),
				e => e.MatchCgt(),
				e => e.MatchLdarg(0),
				e => e.MatchLdfld(logoScaleDirection),
				e => e.MatchLdcR4(-1f),
				e => e.MatchCeq(),
				e => e.MatchAnd()))
			{
				c.Index -= 6;

				c.Emit(OpCodes.Pop);
				c.Emit(OpCodes.Ldc_R4, -20f);
			}*/

			c.Goto(0);

			for (int i = 0; i < 2; i++)
			{
				if (c.TryGotoNext(MoveType.After, e => e.MatchLdcR4(110f)))
				{
					c.Emit(OpCodes.Pop);
					//c.Remove();
					c.Emit(OpCodes.Ldc_R4, 125f);
				}
			}

		}

		public override void Unload()
		{
			// Method Swaps and Injection Unsubscribing
			On_AddMenuButtons -= Interface_AddMenuButtons;
			// Unloading
			Reflection.Unload();
			// Field Nulling
			Reflection = null;
		}

		private void Interface_AddMenuButtons(Orig_AddMenuButtons orig, Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons)
		{
			buttonNames[buttonIndex] = Language.GetTextValue("tModLoader.MenuMods");

			if (selectedMenu == buttonIndex)
			{
				Main.PlaySound(SoundID.MenuOpen);
				Main.menuMode = 10000;
			}

			buttonIndex++;
			numButtons++;

			//if (ModCompile.DeveloperMode)
			{
				buttonNames[buttonIndex] = "Mod Sauces";// Language.GetTextValue("tModLoader.MenuModSources");

				if (selectedMenu == buttonIndex)
				{
					Main.PlaySound(SoundID.MenuOpen);
					//Main.menuMode = (ModCompile.DeveloperModeReady(out _) ? 10001 : 10022);
					Main.menuMode = 10001;
				}

				buttonIndex++;
				numButtons++;

				buttonNames[buttonIndex] = "Modding Tools";

				if (selectedMenu == buttonIndex)
				{
					Main.PlaySound(SoundID.MenuOpen);
					Main.menuMode = (int)MenuMode.ModdingTools;
				}
			}

			buttonIndex++;
			numButtons++;

			buttonNames[buttonIndex] = Language.GetTextValue("tModLoader.MenuModBrowser");

			if (selectedMenu == buttonIndex)
			{
				Main.PlaySound(SoundID.MenuOpen);
				Main.menuMode = 10007;
			}

			buttonIndex++;
			numButtons++;

			offY = 220; //220  250

			for (int i = 0; i < numButtons; i++)
			{
				buttonScales[i] = .82f; //.82  1
			}

			spacing = 45; //45  55
		}
	}

	/// <summary>
	/// Object class handling all reflection. Make sure to null out fields in Unload
	/// </summary>
	public class ReflectionManager
	{
		public static Assembly TerrariaAsb => Assembly.GetAssembly(typeof(Main));

		public Type ModCompile = null;

		/// <summary>
		/// This is where you initialize any fields
		/// </summary>
		internal void Load()
		{
			try
			{
				//This may break with updates as it is reflection.
				ModCompile = TerrariaAsb.GetType("Terraria.ModLoader.Core.ModCompile");
			}
			catch (ReflectionTypeLoadException) { }
		}

		/// <summary>
		/// This is where you unload any fields by nulling them.
		/// </summary>
		internal void Unload()
		{
			ModCompile = null;
		}
	}

	internal static class HookLoader
	{
		public static void Initialize()
		{
			HookBase.BaseHookLoader.Initialize();
			MenuBase.MenuBaseHookLoader.Initialize();
		}

		public static void CallLoad()
		{
			foreach (Type classType in Assembly.GetAssembly(typeof(AssortedModdingTools)).GetTypes())
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

	public abstract class HookBase
	{
		internal static class BaseHookLoader
		{
			public static List<HookBase> hookBases;

			public static void Initialize()
			{
				hookBases = new List<HookBase>();
			}

			public static void CallOnLoad(Mod mod)
			{
				for (int i = 0; i < hookBases.Count; i++)
				{
					HookBase menuBase = hookBases[i];
					menuBase.HookOnLoad?.Invoke(mod);
				}
			}

			public static void CallOnUnload()
			{
				for (int i = 0; i < hookBases.Count; i++)
				{
					HookBase menuBase = hookBases[i];
					menuBase.HookOnUnload?.Invoke();
				}
			}

			public static void CallOnUpdate()
			{
				for (int i = 0; i < hookBases.Count; i++)
				{
					HookBase menuBase = hookBases[i];
					menuBase.HookOnUpdate?.Invoke();
				}
			}
		}

		public event Action HookOnUpdate;

		public event Action<Mod> HookOnLoad;

		public event Action HookOnUnload;
	}

	public interface IHookBase
	{
		void Load();
	}

	public abstract class MenuBase : HookBase
	{
		internal static class MenuBaseHookLoader
		{
			public static List<MenuBase> menuBases;

			public static void Initialize()
			{
				menuBases = new List<MenuBase>();
			}

			public static void CallPreDrawMenu()
			{
				for (int i = 0; i < menuBases.Count; i++)
				{
					MenuBase menuBase = menuBases[i];
					menuBase.HookPreDrawMenu?.Invoke();
				}
			}

			public static void CallPostDrawMenu()
			{
				for (int i = 0; i < menuBases.Count; i++)
				{
					MenuBase menuBase = menuBases[i];
					menuBase.HookPostDrawMenu?.Invoke();
				}
			}
		}

		public event Action HookPreDrawMenu;

		public event Action HookPostDrawMenu;
	}

	public class ModdingToolsMenu : MenuBase, IHookBase
	{
		internal static UIAdvancedCreateMod createMod = new UIAdvancedCreateMod();

		private void OnLoad(Mod mod)
		{
			mod.Logger.Debug("Hook \"OnLoad\" was called!");
		}

		private void PreDrawMenu()
		{
			if (Main.menuMode == (int)MenuMode.ModdingTools)
			{
				DrawModdingToolsMenu();
			}
		}

		private void DrawModdingToolsMenu()
		{
			Main.MenuUI.SetState(createMod);
			Main.menuMode = 888;
		}

		public void Load()
		{
			BaseHookLoader.hookBases.Add(this);
			MenuBaseHookLoader.menuBases.Add(this);
			HookOnLoad += OnLoad;
			HookPreDrawMenu += PreDrawMenu;
		}
	}

	public enum MenuMode
	{
		Main = 0,
		ModdingTools = 5006
	}

	public class UIAdvancedCreateMod : UIState
	{
		private UITextPanel<string> _messagePanel;
		private UIFocusTextInput _modName;
		private UIFocusTextInput _modDiplayName;
		private UIFocusTextInput _modAuthor;
		//private UIFocusInputTextField _basicSword;

		public override void OnInitialize()
		{
			var uIElement = new UIElement
			{
				Width = { Percent = 0.8f },
				MaxWidth = UICommon.MaxPanelWidth,
				Top = { Pixels = 220 },
				Height = { Pixels = -220, Percent = 1f },
				HAlign = 0.5f
			};
			Append(uIElement);

			var mainPanel = new UIPanel
			{
				Width = { Percent = 1f },
				Height = { Pixels = -110, Percent = 1f },
				BackgroundColor = UICommon.MainPanelBackground,
				PaddingTop = 0f
			};
			uIElement.Append(mainPanel);

			var uITextPanel = new UITextPanel<string>("Advanced Create Mod", 0.8f, true)
			{
				HAlign = 0.5f,
				Top = { Pixels = -45 },
				BackgroundColor = UICommon.DefaultUIBlue
			}.WithPadding(15);
			uIElement.Append(uITextPanel);

			_messagePanel = new UITextPanel<string>("No Problems Found. If a problem occurs it will be shown here")
			{
				Width = { Percent = 1f },
				Height = { Pixels = 25 },
				VAlign = 1f,
				Top = { Pixels = -10 } //yikes why no visible bool
			};
			uIElement.Append(_messagePanel);

			var buttonBack = new UITextPanel<string>(Language.GetTextValue("UI.Back"))
			{
				Width = { Pixels = -10, Percent = 0.5f },
				Height = { Pixels = 25 },
				VAlign = 1f,
				Top = { Pixels = -65 }
			}.WithFadedMouseOver();
			buttonBack.OnClick += BackClick;
			uIElement.Append(buttonBack);

			var buttonCreate = new UITextPanel<string>("Create"); // Create
			buttonCreate.CopyStyle(buttonBack);
			buttonCreate.HAlign = 1f;
			buttonCreate.WithFadedMouseOver();
			buttonCreate.OnClick += OKClick;
			uIElement.Append(buttonCreate);

			float top = 16;
			_modName = createAndAppendTextInputWithLabel("Internal Name", "Type here");
			_modName.OnTextChange += (a, b) =>
			{
				_modName.SetText(_modName.currentText.RemoveSpaces());
			};
			_modDiplayName = createAndAppendTextInputWithLabel("Display Name", "Type here");
			_modAuthor = createAndAppendTextInputWithLabel("Author(s)", "Type here");
			//_basicSword = createAndAppendTextInputWithLabel("BasicSword (no spaces)", "Leave Blank to Skip");
			_modName.OnTab += (a, b) => _modDiplayName.focused = true;
			_modDiplayName.OnTab += (a, b) => _modAuthor.focused = true;
			//_modAuthor.OnTab += (a, b) => _basicSword.Focused = true;
			//_basicSword.OnTab += (a, b) => _modName.Focused = true;

			_modAuthor.OnTab += (a, b) => _modName.focused = true;

			UIFocusTextInput createAndAppendTextInputWithLabel(string label, string hint)
			{
				var panel = new UIPanel();
				panel.SetPadding(0);
				panel.Width.Set(0, 1f);
				panel.Height.Set(40, 0f);
				panel.Top.Set(top, 0f);
				top += 46;

				var modNameText = new UIText(label)
				{
					Left = { Pixels = 10 },
					Top = { Pixels = 10 }
				};

				panel.Append(modNameText);

				var textBoxBackground = new UIPanel();
				textBoxBackground.SetPadding(0);
				textBoxBackground.Top.Set(6f, 0f);
				textBoxBackground.Left.Set(-10, .5f);
				textBoxBackground.Width.Set(0, .5f);
				textBoxBackground.Height.Set(30, 0f);
				panel.Append(textBoxBackground);

				var uIInputTextField = new UIFocusTextInput(hint)
				{
					unfocusOnTab = true
				};
				uIInputTextField.Top.Set(5, 0f);
				uIInputTextField.Left.Set(10, 0f); //10
				uIInputTextField.Width.Set(-20, 1f);
				uIInputTextField.Height.Set(20, 0);
				textBoxBackground.Append(uIInputTextField);

				mainPanel.Append(panel);

				return uIInputTextField;
			}
		}

		public override void OnActivate()
		{
			base.OnActivate();
			_modName.SetText("");
			_modDiplayName.SetText("");
			_modAuthor.SetText("");
			//_messagePanel.SetText("");
		}

		private void BackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(SoundID.MenuClose);
			Main.menuMode = (int)MenuMode.Main;
		}

		public static readonly string ModSourcePath = Path.Combine(Program.SavePath, "Mod Sources");

		private void OKClick(UIMouseEvent evt, UIElement listeningElement)
		{
			string modNameTrimmed = _modName.currentText.Trim();
			//string basicSwordTrimmed = _basicSword.CurrentString.Trim();
			string sourceFolder = Path.Combine(ModSourcePath, modNameTrimmed);
			var provider = CodeDomProvider.CreateProvider("C#");

			if (Directory.Exists(sourceFolder))
			{
				_messagePanel.SetText("A mod with the same Internal Name already exists");
			}
			else if (!provider.IsValidIdentifier(modNameTrimmed))
			{
				_messagePanel.SetText("Internal Name is invalid C# identifier. Remove spaces");
			}
			//else if (!string.IsNullOrEmpty(basicSwordTrimmed) && !provider.IsValidIdentifier(basicSwordTrimmed))
			//	_messagePanel.SetText("BasicSword is invalid C# identifier. Remove spaces.");
			else if (string.IsNullOrWhiteSpace(_modDiplayName.currentText))
			{
				_messagePanel.SetText("Display Name can't be empty");
			}
			else if (string.IsNullOrWhiteSpace(_modAuthor.currentText))
			{
				_messagePanel.SetText("Author(s) can't be empty");
			}
			else if (string.IsNullOrWhiteSpace(_modAuthor.currentText))
			{
				_messagePanel.SetText("Author(s) can't be empty");
			}
			else
			{
				_messagePanel.SetText("Creating mod...");
				//Main.PlaySound(SoundID.MenuOpen);
				//Main.menuMode = (int)MenuMode.ModdingTools;
				Directory.CreateDirectory(sourceFolder);

				// TODO: verbatim line endings, localization.
				File.WriteAllText(Path.Combine(sourceFolder, "build.txt"), GetModBuild());
				File.WriteAllText(Path.Combine(sourceFolder, "description.txt"), GetModDescription());
				File.WriteAllText(Path.Combine(sourceFolder, $"{modNameTrimmed}.cs"), GetModClass(modNameTrimmed));
				File.WriteAllText(Path.Combine(sourceFolder, $"{modNameTrimmed}.csproj"), GetModCsproj(modNameTrimmed));
				string propertiesFolder = Path.Combine(sourceFolder, "Properties");
				Directory.CreateDirectory(propertiesFolder);
				File.WriteAllText(Path.Combine(propertiesFolder, $"launchSettings.json"), GetLaunchSettings());
				//if (!string.IsNullOrEmpty(basicSwordTrimmed))
				//{
				//	string itemsFolder = Path.Combine(sourceFolder, "Items");
				//	Directory.CreateDirectory(itemsFolder);
				//File.WriteAllText(Path.Combine(itemsFolder, $"{basicSwordTrimmed}.cs"), GetBasicSword(modNameTrimmed, basicSwordTrimmed));
				//File.WriteAllBytes(Path.Combine(itemsFolder, $"{basicSwordTrimmed}.png"), ExampleSwordPNG);
				//}
				_messagePanel.SetText("Mod created! Opening folder");
				Process.Start(sourceFolder);
			}
		}

		// TODO Let's embed all these files
		private string GetModBuild()
		{
			return $"displayName = {_modDiplayName.currentText}" +
				$"{Environment.NewLine}author = {_modAuthor.currentText}" +
				$"{Environment.NewLine}version = 0.1";
		}

		private string GetModDescription()
		{
			return $"{_modDiplayName.currentText} is a pretty cool mod, it does...this. Modify this file with a description of your mod.";
		}

		private string GetModClass(string modNameTrimmed)
		{
			return
$@"using Terraria.ModLoader;

namespace {modNameTrimmed}
{{
	public class {modNameTrimmed} : Mod
	{{
		public {modNameTrimmed}()
		{{
		}}
	}}
}}";
		}

		internal string GetModCsproj(string modNameTrimmed)
		{
			return
$@"<?xml version=""1.0"" encoding=""utf-8""?>
<Project Sdk=""Microsoft.NET.Sdk"">
  <Import Project=""..\..\references\tModLoader.targets"" />
  <PropertyGroup>
    <AssemblyName>{modNameTrimmed}</AssemblyName>
    <TargetFramework>net45</TargetFramework>
    <PlatformTarget>x86</PlatformTarget>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>
  <Target Name=""BuildMod"" AfterTargets=""Build"">
    <Exec Command=""&quot;$(tMLBuildServerPath)&quot; -build $(ProjectDir) -eac $(TargetPath) -define $(DefineConstants) -unsafe $(AllowUnsafeBlocks)"" />
  </Target>
</Project>";
		}

		internal string GetLaunchSettings()
		{
			return
$@"{{
  ""profiles"": {{
    ""Terraria"": {{
      ""commandName"": ""Executable"",
      ""executablePath"": ""$(tMLPath)"",
      ""workingDirectory"": ""$(TerrariaSteamPath)""
    }},
    ""TerrariaServer"": {{
      ""commandName"": ""Executable"",
      ""executablePath"": ""$(tMLServerPath)"",
      ""workingDirectory"": ""$(TerrariaSteamPath)""
    }}
  }}
}}";
		}
	}
}