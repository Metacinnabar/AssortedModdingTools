using AssortedModdingTools.DataStructures;
using AssortedModdingTools.Systems.Menu;
using AssortedModdingTools.UI.Elements;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace AssortedModdingTools.UI.States.Menu
{
	public class UILanguageSettings : MenuState
	{
		public override MenuModes MenuMode => MenuModes.LanguageSettings;

		public override void OnInitialize()
		{
			int y = 200;
			int spacing = 33;

			UIBigTextWithBorder header = new UIBigTextWithBorder(Language.GetTextValue("LegacyMenu.102"), TextBorderColors.WhiteBlack, null, 0.85f); //Select language
			header.Top.Set(y - 20, 0f);
			header.Left.Set(Main.screenWidth / 2, 0f);
			Append(header); //idk how append works

			y += spacing;

			UISetLanguageButton english = new UISetLanguageButton(Language.GetTextValue("Language.English"), GameCulture.English, TextBorderHoverColors.DefaultHover, null, 0.75f);
			english.Top.Set(y, 0f);
			english.Left.Set(Main.screenWidth / 2, 0f);
			Append(english); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton german = new UISetLanguageButton(Language.GetTextValue("Language.German"), GameCulture.German, TextBorderHoverColors.DefaultHover, null, 0.75f);
			german.Top.Set(y, 0f);
			german.Left.Set(Main.screenWidth / 2, 0f);
			Append(german); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton italian = new UISetLanguageButton(Language.GetTextValue("Language.Italian"), GameCulture.Italian, TextBorderHoverColors.DefaultHover, null, 0.75f);
			italian.Top.Set(y, 0f);
			italian.Left.Set(Main.screenWidth / 2, 0f);
			Append(italian); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton french = new UISetLanguageButton(Language.GetTextValue("Language.French"), GameCulture.French, TextBorderHoverColors.DefaultHover, null, 0.75f);
			french.Top.Set(y, 0f);
			french.Left.Set(Main.screenWidth / 2, 0f);
			Append(french); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton spanish = new UISetLanguageButton(Language.GetTextValue("Language.Spanish"), GameCulture.Spanish, TextBorderHoverColors.DefaultHover, null, 0.75f);
			spanish.Top.Set(y, 0f);
			spanish.Left.Set(Main.screenWidth / 2, 0f);
			Append(spanish); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton russian = new UISetLanguageButton(Language.GetTextValue("Language.Russian"), GameCulture.Russian, TextBorderHoverColors.DefaultHover, null, 0.75f);
			russian.Top.Set(y, 0f);
			russian.Left.Set(Main.screenWidth / 2, 0f);
			Append(russian); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton chinese = new UISetLanguageButton(Language.GetTextValue("Language.Chinese"), GameCulture.Chinese, TextBorderHoverColors.DefaultHover, null, 0.75f);
			chinese.Top.Set(y, 0f);
			chinese.Left.Set(Main.screenWidth / 2, 0f);
			Append(chinese); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton portuguese = new UISetLanguageButton(Language.GetTextValue("Language.Portuguese"), GameCulture.Portuguese, TextBorderHoverColors.DefaultHover, null, 0.75f);
			portuguese.Top.Set(y, 0f);
			portuguese.Left.Set(Main.screenWidth / 2, 0f);
			Append(portuguese); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton polish = new UISetLanguageButton(Language.GetTextValue("Language.Polish"), GameCulture.Polish, TextBorderHoverColors.DefaultHover, null, 0.75f);
			polish.Top.Set(y, 0f);
			polish.Left.Set(Main.screenWidth / 2, 0f);
			Append(polish); //is this correct? is it english.Append(this);?

			y += spacing;

			UIBigTextWithBorder back = new UIHoverBigTextWithBorder(Language.GetTextValue("LegacyMenu.5"), TextBorderHoverColors.DefaultHover, null, 0.95f); //Back
			back.Top.Set(y + 10, 0f);
			back.Left.Set(Main.screenWidth / 2, 0f);
			back.OnClick += (evt, listeningElement) =>
			{
				MenuSystem.MenuInterface.SetState(MenuSystem.Settings); 
				Main.menuMode = (int)MenuModes.Settings;
				Main.PlaySound(SoundID.MenuClose);
			};
			Append(back); //idk how append works
		}
	}
}
