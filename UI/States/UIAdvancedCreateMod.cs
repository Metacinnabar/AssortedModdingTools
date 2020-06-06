using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader.UI;
using System.IO;
using System.CodeDom.Compiler;
using System.Diagnostics;
using AssortedModdingTools.UI.Elements;
using AssortedModdingTools.Extensions;
using AssortedModdingTools.DataStructures.UI.Menu;

namespace AssortedModdingTools.UI.States
{
	public class UIAdvancedCreateMod : UIState
	{
		public static readonly string ModSourcePath = Path.Combine(Program.SavePath, "Mod Sources");

		private UITextPanel<string> infoTextPanel;
		private UIFocusTextInputPanel internalNamePanel;
		private UIFocusTextInputPanel displayNamePanel;
		private UIFocusTextInputPanel authorsPanel;

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

			infoTextPanel = new UITextPanel<string>("No Problems Found. If a problem occurs it will be shown here")
			{
				Width = { Percent = 1f },
				Height = { Pixels = 25 },
				VAlign = 1f,
				Top = { Pixels = -10 }
			};
			uIElement.Append(infoTextPanel);

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

			internalNamePanel = AddInput("Internal Name", mainPanel, ref top, true);

			displayNamePanel = AddInput("Display Name", mainPanel, ref top);

			authorsPanel = AddInput("Author(s)", mainPanel, ref top);

			AddToggle("Test 1?", mainPanel, ref top);
			AddToggle("Test 2?", mainPanel, ref top);
			AddToggle("Test 3?", mainPanel, ref top);
			AddToggle("Test 4?", mainPanel, ref top);
			AddToggle("Test 5?", mainPanel, ref top);
			AddInput("Test 6?", mainPanel, ref top);
		}

		private static void AddToggle(string text, UIPanel mainPanel, ref float top, int h = 40)
		{
			var boolElement = new UITextVisualTogglePanel(text);
			boolElement.SetPadding(0);
			boolElement.Width.Set(0, 1f);
			boolElement.Height.Set(h, 0f);
			boolElement.Top.Set(top, 0f);
			mainPanel.Append(boolElement);

			top += h + 6;
		}

		private static UIFocusTextInputPanel AddInput(string text, UIPanel mainPanel, ref float top, bool noSpaces = false, int h = 40)
		{
			var boolElement = new UIFocusTextInputPanel(text);
			boolElement.SetPadding(0);
			boolElement.Width.Set(0, 1f);
			boolElement.Height.Set(h, 0f);
			boolElement.Top.Set(top, 0f);

			if (noSpaces)
			{
				//boolElement.OnTextChange += (a, b) =>
				//{
				//	boolElement.SetText(boolElement.currentText.RemoveSpaces());
				//};
			}

			mainPanel.Append(boolElement);

			top += h + 6;
			return boolElement;
		}

		public override void OnActivate()
		{
			base.OnActivate();
			internalNamePanel.SetText("");
			displayNamePanel.SetText("");
			authorsPanel.SetText("");

			internalNamePanel.OnTextChange += (a, b) =>
			{
				internalNamePanel.SetText(internalNamePanel.currentText.RemoveSpaces());
			};
		}

		private void BackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(SoundID.MenuClose);
			Main.menuMode = (int)MenuMode.Main;
		}

		private void OKClick(UIMouseEvent evt, UIElement listeningElement)
		{
			string modNameTrimmed = internalNamePanel.currentText.Trim();
			string sourceFolder = Path.Combine(ModSourcePath, modNameTrimmed);

			CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");

			if (Directory.Exists(sourceFolder))
			{
				infoTextPanel.SetText("A mod with the same Internal Name already exists");
			}
			else if (!provider.IsValidIdentifier(modNameTrimmed))
			{
				infoTextPanel.SetText("Internal Name is an invalid C# identifier. Remove spaces");
			}
			else if (string.IsNullOrWhiteSpace(displayNamePanel.currentText))
			{
				infoTextPanel.SetText("Display Name can't be empty");
			}
			else if (string.IsNullOrWhiteSpace(authorsPanel.currentText))
			{
				infoTextPanel.SetText("Author(s) can't be empty");
			}
			else if (string.IsNullOrWhiteSpace(authorsPanel.currentText))
			{
				infoTextPanel.SetText("Author(s) can't be empty");
			}
			else
			{
				infoTextPanel.SetText("Creating mod...");
				Directory.CreateDirectory(sourceFolder);

				// TODO: verbatim line endings, localization.
				File.WriteAllText(Path.Combine(sourceFolder, "build.txt"), GetModBuild());
				File.WriteAllText(Path.Combine(sourceFolder, "description.txt"), GetModDescription());
				File.WriteAllText(Path.Combine(sourceFolder, $"{modNameTrimmed}.cs"), GetModClass(modNameTrimmed));
				File.WriteAllText(Path.Combine(sourceFolder, $"{modNameTrimmed}.csproj"), GetModCsproj(modNameTrimmed));
				string propertiesFolder = Path.Combine(sourceFolder, "Properties");
				Directory.CreateDirectory(propertiesFolder);
				File.WriteAllText(Path.Combine(propertiesFolder, $"launchSettings.json"), GetLaunchSettings());

				infoTextPanel.SetText("Mod created! Opening folder");
				Process.Start(sourceFolder);
			}
		}

		// TODO Let's embed all these files
		#region Files
		private string GetModBuild() => $"displayName = {displayNamePanel.currentText}\nauthor = {authorsPanel.currentText}\nversion = 1.0\nhomepage = ";

		private string GetModDescription() => $"{displayNamePanel.currentText}...\n\nVersion Changelog:\n\n- Initial Release (v1.0)";

		private string GetModClass(string modNameTrimmed)
		{
			return
$@"using Terraria.ModLoader;

namespace {modNameTrimmed}
{{
	public class {modNameTrimmed} : Mod
	{{
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
    <LangVersion>7.3</LangVersion>
  </PropertyGroup>
  <Target Name=""BuildMod"" AfterTargets=""Build"">
    <Exec Command=""&quot;$(tMLBuildServerPath)&quot; -build $(ProjectDir) -eac $(TargetPath) -define &quot;$(DefineConstants)&quot; -unsafe $(AllowUnsafeBlocks)"" />
  </Target>
  <ItemGroup>
    <PackageReference Include=""tModLoader.CodeAssist"" Version=""0.1.*"" />
  </ItemGroup>
</Project>";
		}

		internal bool CsprojUpdateNeeded(string fileContents)
		{
			if (!fileContents.Contains("tModLoader.targets"))
				return true;
			if (fileContents.Contains("<LangVersion>latest</LangVersion>"))
				return true;
			if (!fileContents.Contains(@"<PackageReference Include=""tModLoader.CodeAssist"" Version=""0.1.*"" />"))
				return true;
			if (!fileContents.Contains(@"-define &quot;$(DefineConstants)&quot;") && !ReLogic.OS.Platform.IsWindows)
				return true;

			return false;
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
		#endregion Files
	}
}