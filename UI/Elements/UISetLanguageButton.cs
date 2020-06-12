using AssortedModdingTools.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.UI;
using Terraria.ID;

namespace AssortedModdingTools.UI.Elements
{
	public class UISetLanguageButton : UIHoverBigTextWithBorder
	{
		public readonly GameCulture NewCulture;

		public UISetLanguageButton(string text, GameCulture newCulture, TextBorderHoverColors? textBorderHoverColors = null, Vector2? origin = null, float scale = 1f) : base(text, textBorderHoverColors, origin, scale)
		{
			NewCulture = newCulture;
			OnClick += UISetLanguageButton_OnClick;
		}

		private void UISetLanguageButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.chTitle = true;
			LanguageManager.Instance.SetLanguage(NewCulture);
			Main.PlaySound(SoundID.MenuTick);
			Main.SaveSettings();
		}
	}
}