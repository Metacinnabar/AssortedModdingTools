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

		public UISetLanguageButton(string text, GameCulture newCulture, TextBorderHoverColors? textBorderHoverColors = null, Vector2? origin = null, float maxscale = 1f) : base(text, textBorderHoverColors, origin, maxscale)
		{
			NewCulture = newCulture;
		}

		public override void Click(UIMouseEvent evt)
		{
			base.Click(evt);

			Main.chTitle = true;
			LanguageManager.Instance.SetLanguage(NewCulture);
			Main.PlaySound(SoundID.MenuTick);
			Main.SaveSettings();
		}
	}
}