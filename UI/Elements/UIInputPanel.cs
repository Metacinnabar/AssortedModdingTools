using Terraria.GameContent.UI.Elements;
using AssortedModdingTools.UI.Elements;

namespace AssortedModdingTools.UI.Elements
{
	public class UIInputPanel : UIPanel
	{
		public string textInputHint = string.Empty;
		public string label = string.Empty;

		public UIInputPanel()
		{
			Height.Set(40, 0f);
		}

		public override void OnInitialize()
		{
			var panel = new UIPanel();
			panel.SetPadding(0);
			panel.Width = Width;
			panel.Height = Height;
			Append(panel);

			var uiLabel = new UIText(label)
			{
				Left = { Pixels = 10 },
				Top = { Pixels = 10 }
			};
			panel.Append(uiLabel);

			var textBoxBackground = new UIPanel();
			textBoxBackground.SetPadding(0);
			textBoxBackground.Top.Set(6f, 0f);
			textBoxBackground.Left.Set(-10, .5f);
			textBoxBackground.Width.Set(0, .5f);
			textBoxBackground.Height.Set(30, 0f);
			panel.Append(textBoxBackground);

			var uIInputTextField = new UIFocusTextInput(textInputHint)
			{
				unfocusOnTab = true
			};
			uIInputTextField.Top.Set(5, 0f);
			uIInputTextField.Left.Set(10, 0f);
			uIInputTextField.Width.Set(-20, 1f);
			uIInputTextField.Height.Set(20, 0);
			textBoxBackground.Append(uIInputTextField);
		}
	}
}