using System;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace AssortedModdingTools.UI.Elements
{
	public class UIToggle : UIElement
	{
		public bool Value { get; private set; }
		public bool playSound;

		public event Func<bool> PreChangeValue;
		public event Action PostChangeValue;

		public UIToggle(bool defaultValue = false, bool playSound = true)
		{
			Value = defaultValue;
			this.playSound = playSound;
		}

		public override void Click(UIMouseEvent evt)
		{
			if (PreChangeValue?.Invoke() == true || PreChangeValue == null)
				Value = !Value;

			PostChangeValue?.Invoke();

			if (playSound)
				Main.PlaySound(SoundID.MenuTick);
		}
	}
}