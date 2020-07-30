using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Achievements;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Graphics;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace AssortedModdingTools.UI.States.Menu
{
	public class UIAchievementsMenu : UIState
	{
		private UIList _achievementsList;

		private List<UIAchievementListItem> _achievementElements = new List<UIAchievementListItem>();

		private List<UIToggleImage> _categoryButtons = new List<UIToggleImage>();

		private UIElement _backpanel;

		private UIElement _outerContainer;

		public void InitializePage()
		{
			RemoveAllChildren();
			_categoryButtons.Clear();
			_achievementElements.Clear();
			_achievementsList = null;
			bool flag = true;
			int num = flag.ToInt() * 100;
			UIElement uIElement3 = new UIElement();
			uIElement3.Width.Set(0f, 0.8f);
			uIElement3.MaxWidth.Set(800f + (float)num, 0f);
			uIElement3.MinWidth.Set(600f + (float)num, 0f);
			uIElement3.Top.Set(220f, 0f);
			uIElement3.Height.Set(-220f, 1f);
			uIElement3.HAlign = 0.5f;
			_outerContainer = uIElement3;
			Append(uIElement3);
			UIPanel uIPanel = new UIPanel();
			uIPanel.Width.Set(0f, 1f);
			uIPanel.Height.Set(-110f, 1f);
			uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			uIPanel.PaddingTop = 0f;
			uIElement3.Append(uIPanel);
			_achievementsList = new UIList();
			_achievementsList.Width.Set(-25f, 1f);
			_achievementsList.Height.Set(-50f, 1f);
			_achievementsList.Top.Set(50f, 0f);
			_achievementsList.ListPadding = 5f;
			uIPanel.Append(_achievementsList);
			UITextPanel<LocalizedText> uITextPanel3 = new UITextPanel<LocalizedText>(Language.GetText("UI.Achievements"), 1f, large: true);
			uITextPanel3.HAlign = 0.5f;
			uITextPanel3.Top.Set(-33f, 0f);
			uITextPanel3.SetPadding(13f);
			uITextPanel3.BackgroundColor = new Color(73, 94, 171);
			uIElement3.Append(uITextPanel3);
			UITextPanel<LocalizedText> uITextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, large: true);
			uITextPanel2.Width.Set(-10f, 0.5f);
			uITextPanel2.Height.Set(50f, 0f);
			uITextPanel2.VAlign = 1f;
			uITextPanel2.HAlign = 0.5f;
			uITextPanel2.Top.Set(-45f, 0f);
			uITextPanel2.OnMouseOver += FadedMouseOver;
			uITextPanel2.OnMouseOut += FadedMouseOut;
			uITextPanel2.OnClick += GoBackClick;
			uIElement3.Append(uITextPanel2);
			_backpanel = uITextPanel2;
			List<Achievement> list = Main.Achievements.CreateAchievementsList();
			for (int j = 0; j < list.Count; j++)
			{
				UIAchievementListItem item = new UIAchievementListItem(list[j], flag);
				_achievementsList.Add(item);
				_achievementElements.Add(item);
			}
			//_achievementsList.Clear();
			UIScrollbar uIScrollbar = new UIScrollbar();
			uIScrollbar.SetView(100f, 1000f);
			uIScrollbar.Height.Set(-50f, 1f);
			uIScrollbar.Top.Set(50f, 0f);
			uIScrollbar.HAlign = 1f;
			uIPanel.Append(uIScrollbar);
			_achievementsList.SetScrollbar(uIScrollbar);
			UIElement uIElement2 = new UIElement();
			uIElement2.Width.Set(0f, 1f);
			uIElement2.Height.Set(32f, 0f);
			uIElement2.Top.Set(10f, 0f);
			Texture2D texture = TextureManager.Load("Images/UI/Achievement_Categories");
			for (int i = 0; i < 4; i++)
			{
				UIToggleImage uIToggleImage = new UIToggleImage(texture, 32, 32, new Point(34 * i, 0), new Point(34 * i, 34));
				uIToggleImage.Left.Set(i * 36 + 8, 0f);
				uIToggleImage.SetState(value: true);
				uIToggleImage.OnClick += FilterList;
				_categoryButtons.Add(uIToggleImage);
				uIElement2.Append(uIToggleImage);
			}
			uIPanel.Append(uIElement2);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			for (int i = 0; i < _categoryButtons.Count; i++)
			{
				if (_categoryButtons[i].IsMouseHovering)
				{
					string text2 = "";
					switch (i)
					{
						case 3:
							text2 = Language.GetTextValue("Achievements.ChallengerCategory");
							break;
						case 1:
							text2 = Language.GetTextValue("Achievements.CollectorCategory");
							break;
						case 2:
							text2 = Language.GetTextValue("Achievements.ExplorerCategory");
							break;
						case 0:
							text2 = Language.GetTextValue("Achievements.SlayerCategory");
							break;
						case -1:
							text2 = Language.GetTextValue("Achievements.NoCategory");
							break;
						default:
							text2 = Language.GetTextValue("Achievements.NoCategory");
							break;
					}
					float x = Main.fontMouseText.MeasureString(text2).X;
					Vector2 vector = new Vector2(Main.mouseX, Main.mouseY) + new Vector2(16f);
					if (vector.Y > (float)(Main.screenHeight - 30))
					{
						vector.Y = Main.screenHeight - 30;
					}
					if (vector.X > (float)Main.screenWidth - x)
					{
						vector.X = Main.screenWidth - 460;
					}
					Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text2, vector.X, vector.Y, new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), Color.Black, Vector2.Zero);
					break;
				}
			}
			SetupGamepadPoints(spriteBatch);
		}

		public void GotoAchievement(Achievement achievement)
		{
			_achievementsList.Goto(delegate (UIElement element)
			{
				UIAchievementListItem uIAchievementListItem = element as UIAchievementListItem;
				return uIAchievementListItem != null && uIAchievementListItem.GetAchievement() == achievement;
			});
		}

		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.menuMode = 0;
			IngameFancyUI.Close();
		}

		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(12);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
		}

		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
		}

		private void FilterList(UIMouseEvent evt, UIElement listeningElement)
		{
			_achievementsList.Clear();
			foreach (UIAchievementListItem achievementElement in _achievementElements)
			{
				if (_categoryButtons[(int)achievementElement.GetAchievement().Category].IsOn)
				{
					_achievementsList.Add(achievementElement);
				}
			}
			Recalculate();
		}

		public override void OnActivate()
		{
			InitializePage();
			if (Main.gameMenu)
			{
				_outerContainer.Top.Set(220f, 0f);
				_outerContainer.Height.Set(-220f, 1f);
			}
			else
			{
				_outerContainer.Top.Set(120f, 0f);
				_outerContainer.Height.Set(-120f, 1f);
			}
			_achievementsList.UpdateOrder();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3002);
			}
		}

		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 3;
			int num4 = 3000;
			UILinkPointNavigator.SetPosition(num4, _backpanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(num4 + 1, _outerContainer.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int num3 = num4;
			UILinkPoint uILinkPoint = UILinkPointNavigator.Points[num3];
			uILinkPoint.Unlink();
			uILinkPoint.Up = num3 + 1;
			num3++;
			UILinkPoint uILinkPoint2 = UILinkPointNavigator.Points[num3];
			uILinkPoint2.Unlink();
			uILinkPoint2.Up = num3 + 1;
			uILinkPoint2.Down = num3 - 1;
			for (int i = 0; i < _categoryButtons.Count; i++)
			{
				num3 = (UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = num3 + 1);
				UILinkPointNavigator.SetPosition(num3, _categoryButtons[i].GetInnerDimensions().ToRectangle().Center.ToVector2());
				UILinkPoint uILinkPoint3 = UILinkPointNavigator.Points[num3];
				uILinkPoint3.Unlink();
				uILinkPoint3.Left = ((i == 0) ? (-3) : (num3 - 1));
				uILinkPoint3.Right = ((i == _categoryButtons.Count - 1) ? (-4) : (num3 + 1));
				uILinkPoint3.Down = num4;
			}
		}
	}
}
