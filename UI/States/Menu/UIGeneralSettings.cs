using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using AssortedModdingTools.UI.Elements;
using Terraria.Localization;

namespace AssortedModdingTools.UI.States.Menu
{
    public class UIGeneralSettings : UIState
    {
        public override void OnInitialize()
        {
            base.OnInitialize();
            var Autosave = new UIHoverBigTextWithBorder((Main.autoSave ? Language.GetTextValue("LegacyMenu.67") : Language.GetTextValue("LegacyMenu.68")));
            var Autopause = new UIHoverBigTextWithBorder((Main.autoPause ? Language.GetTextValue("LegacyMenu.69") : Language.GetTextValue("LegacyMenu.70")));
            var Map = new UIHoverBigTextWithBorder((Main.mapEnabled ? Language.GetTextValue("LegacyMenu.112") : Language.GetTextValue("LegacyMenu.113")));
            var Passwords = new UIHoverBigTextWithBorder((Main.HidePassword ? Language.GetTextValue("LegacyMenu.212") : Language.GetTextValue("LegacyMenu.211")));

            Append(Autosave);
            Append(Autopause);
            Append(Map);
            Append(Passwords);
        }
    }
}
