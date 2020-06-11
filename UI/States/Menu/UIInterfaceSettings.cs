using AssortedModdingTools.UI.Elements;
using Terraria;
using Terraria.Localization;
using Terraria.UI;

namespace AssortedModdingTools.UI.States.Menu
{
    public class UIInterfaceSettings : UIState
    {
        public override void OnInitialize()
        {
            base.OnInitialize();
            UIBigTextWithBorder pickuptext = new UIBigTextWithBorder(Main.showItemText ? Language.GetTextValue("LegacyMenu.71") : Language.GetTextValue("LegacyMenu.72"));
            UIBigTextWithBorder eventprogressbar = new UIBigTextWithBorder(Language.GetTextValue($"LegacyMenu.123") + Language.GetTextValue($"LegacyMenu.{124 + Main.invasionProgressMode}"));
            UIBigTextWithBorder placementPreview = new UIBigTextWithBorder(Main.placementPreview ? Language.GetTextValue("LegacyMenu.128") : Language.GetTextValue("LegacyMenu.129"));
            UIBigTextWithBorder highlightnewitems = new UIBigTextWithBorder(ItemSlot.Options.HighlightNewItems ? Language.GetTextValue("LegacyInterface.117") : Language.GetTextValue("LegacyInterface.116"));
            UIBigTextWithBorder tilegrid = new UIBigTextWithBorder(Main.MouseShowBuildingGrid ? Language.GetTextValue("LegacyMenu.229") : Language.GetTextValue("LegacyMenu.230"));
            UIBigTextWithBorder gamepadInstructions = new UIBigTextWithBorder(Main.GamepadDisableInstructionsDisplay ? Language.GetTextValue("LegacyMenu.241") : Language.GetTextValue("LegacyMenu.242"));
            UIBigTextWithBorder back = new UIBigTextWithBorder(Language.GetTextValue("LegacyMenu.5"));

            Append(pickuptext);
            Append(eventprogressbar);
            Append(placementPreview);
            Append(highlightnewitems);
            Append(tilegrid);
            Append(gamepadInstructions);
            Append(back);
        }
    }
}
