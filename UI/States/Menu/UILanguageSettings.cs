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
			int y = 220;
			int spacing = 33;

			UIBigTextWithBorder header = new UIBigTextWithBorder(Language.GetTextValue("LegacyMenu.102"), TextBorderColors.WhiteBlack, null, 0.85f); //Select language
			header.Top.Set(y, 0f);
			header.HAlign = 0.5f;
			Append(header); //idk how append works

			y += spacing;

			UISetLanguageButton english = new UISetLanguageButton(Language.GetTextValue("Language.English"), GameCulture.English, TextBorderHoverColors.GrayBlackYellow, null, maxscale: 0.75f);
			english.Top.Set(y, 0f);
			english.HAlign = 0.5f;
			Append(english); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton german = new UISetLanguageButton(Language.GetTextValue("Language.German"), GameCulture.English, TextBorderHoverColors.GrayBlackYellow, null, 0.75f);
			german.Top.Set(y, 0f);
			german.HAlign = 0.5f;
			Append(german); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton italian = new UISetLanguageButton(Language.GetTextValue("Language.Italian"), GameCulture.English, TextBorderHoverColors.GrayBlackYellow, null, 0.75f);
			italian.Top.Set(y, 0f);
			italian.HAlign = 0.5f;
			Append(italian); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton french = new UISetLanguageButton(Language.GetTextValue("Language.French"), GameCulture.English, TextBorderHoverColors.GrayBlackYellow, null, 0.75f);
			french.Top.Set(y, 0f);
			french.HAlign = 0.5f;
			Append(french); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton spanish = new UISetLanguageButton(Language.GetTextValue("Language.Spanish"), GameCulture.English, TextBorderHoverColors.GrayBlackYellow, null, 0.75f);
			spanish.Top.Set(y, 0f);
			spanish.HAlign = 0.5f;
			Append(spanish); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton russian = new UISetLanguageButton(Language.GetTextValue("Language.Russian"), GameCulture.English, TextBorderHoverColors.GrayBlackYellow, null, 0.75f);
			russian.Top.Set(y, 0f);
			russian.HAlign = 0.5f;
			Append(russian); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton chinese = new UISetLanguageButton(Language.GetTextValue("Language.Chinese"), GameCulture.English, TextBorderHoverColors.GrayBlackYellow, null, 0.75f);
			chinese.Top.Set(y, 0f);
			chinese.HAlign = 0.5f;
			Append(chinese); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton portuguese = new UISetLanguageButton(Language.GetTextValue("Language.Portuguese"), GameCulture.English, TextBorderHoverColors.GrayBlackYellow, null, 0.75f);
			portuguese.Top.Set(y, 0f);
			portuguese.HAlign = 0.5f;
			Append(portuguese); //is this correct? is it english.Append(this);?

			y += spacing;

			UISetLanguageButton polish = new UISetLanguageButton(Language.GetTextValue("Language.Polish"), GameCulture.English, TextBorderHoverColors.GrayBlackYellow, null, 0.75f);
			polish.Top.Set(y, 0f);
			polish.HAlign = 0.5f;
			Append(polish); //is this correct? is it english.Append(this);?

			y += spacing;

			UIBigTextWithBorder back = new UIHoverBigTextWithBorder(Language.GetTextValue("LegacyMenu.5"), TextBorderHoverColors.GrayBlackYellow, null, 0.95f); //Back
			back.Top.Set(y + 10, 0f);
			back.HAlign = 0.5f;
			back.OnClick += (evt, listeningElement) =>
			{
				Main.menuMode = 0;
				MenuSystem.MenuInterface.SetState(MenuSystem.Settings); //Main.menuMode = MenuModes.Settings;
				Main.PlaySound(SoundID.MenuClose);
			};
			Append(back); //idk how append works
		}
	}
}
