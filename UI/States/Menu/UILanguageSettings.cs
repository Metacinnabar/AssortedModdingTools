using AssortedModdingTools.Systems.Menu;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.UI;

namespace AssortedModdingTools.UI.States.Menu
{
	public class UILanguageSettings : UIState, IMenuState
	{
		public MenuModes MenuMode => MenuModes.LanguageSettings;

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			array9[0] = Lang.menu[102].Value;
			array[0] = true;
			array9[1] = Language.GetTextValue("Language.English");
			array9[2] = Language.GetTextValue("Language.German");
			array9[3] = Language.GetTextValue("Language.Italian");
			array9[4] = Language.GetTextValue("Language.French");
			array9[5] = Language.GetTextValue("Language.Spanish");
			array9[6] = Language.GetTextValue("Language.Russian");
			array9[7] = Language.GetTextValue("Language.Chinese");
			array9[8] = Language.GetTextValue("Language.Portuguese");
			array9[9] = Language.GetTextValue("Language.Polish");
			array9[10] = Lang.menu[5].Value;
			num5 = 11;
			if ()
			{
				MenuSystem.MenuInterface.SetState(MenuSystem.Settings); //Main.menuMode = MenuModes.Settings;
				Main.PlaySound(11, -1, -1, 1, 1f, 0f);
			}


			else if (this.selectedMenu >= 1)
			{
				Main.chTitle = true;
				GameCulture gameCulture = GameCulture.
				LanguageManager.Instance.SetLanguage(this.selectedMenu);
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				Main.SaveSettings();
			}
			num4 = 33;
			num2 = 200;
			array4[0] = -20;
			array4[10] = 10;
			for (int l = 0; l < num5; l++)
			{
				array7[l] = 0.75f;
			}
			array7[0] = 0.85f;
			array7[10] = 0.95f;
		}
	}

	public class UISettings : UIState, IMenuState
	{
		public MenuModes MenuMode => MenuModes.Settings;
	}
}
