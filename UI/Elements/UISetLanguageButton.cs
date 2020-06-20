using AssortedModdingTools.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace AssortedModdingTools.UI.Elements
{
	public class UISetLanguageButton : UIHoverBigTextWithBorder
	{
		public GameCulture NewCulture { get; private set; }

		public UISetLanguageButton(string text, GameCulture newCulture, TextBorderHoverColors? textBorderHoverColors = null, Vector2? origin = null, float maxscale = 1f) : base(text, textBorderHoverColors, origin, maxscale)
		{
			NewCulture = newCulture;
			OnClick += UISetLanguageButton_OnClick;
		}

		private void UISetLanguageButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.chTitle = true;
			LanguageManager.Instance.SetLanguage(NewCulture); //set the new language
			Main.PlaySound(SoundID.MenuTick);
			Main.SaveSettings();
		}
	}
}