using UnityEngine;
using UnityEngine.UI;

namespace DebugMenu.uGUI
{
	public class Bool : Item
	{
		private DebugMenu.Bool bool_;

		private Image image_;

		private Text text_;

		private bool last_;

		public override DebugMenu.Item item
		{
			get
			{
				return bool_;
			}
		}

		public Bool(DebugMenu.Bool _bool)
		{
			bool_ = _bool;
			last_ = bool_.flag;
		}

		public override GameObject CreateObject(Menu menu, Transform parent)
		{
			HideFlags hideFlags = HideFlags.DontSave;
			GameObject gameObject = new GameObject(bool_.name);
			gameObject.transform.SetParent(parent, false);
			gameObject.hideFlags = hideFlags;
			Image image = gameObject.AddComponent<Image>();
			image.color = menu.frame_color;
			RectTransform rectTransform = image.rectTransform;
			rectTransform.sizeDelta = new Vector2(menu.item_width, menu.item_height);
			Button button = gameObject.AddComponent<Button>();
			button.onClick.AddListener(bool_.OnFlip);
			GameObject gameObject2 = new GameObject("Text");
			gameObject2.transform.SetParent(gameObject.transform, false);
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.hideFlags = hideFlags;
			Text text = gameObject2.AddComponent<Text>();
			text.font = menu.text_font;
			text.fontSize = menu.text_font_size;
			text.color = menu.text_color;
			text.alignment = TextAnchor.MiddleCenter;
			RectTransform rectTransform2 = text.rectTransform;
			rectTransform2.sizeDelta = rectTransform.sizeDelta;
			image_ = image;
			text_ = text;
			UpdateFlag();
			bool_.update_flag += UpdateFlag;
			return gameObject;
		}

		public override void SetSelected(Menu menu, bool selected)
		{
			if (image_ != null)
			{
				image_.color = ((!selected) ? menu.frame_color : menu.selected_frame_color);
			}
			if (text_ != null)
			{
				text_.color = ((!selected) ? menu.text_color : menu.selected_text_color);
			}
		}

		public override void Process(Menu menu)
		{
			if (last_ != bool_.flag)
			{
				last_ = bool_.flag;
				UpdateFlag();
			}
		}

		private void UpdateFlag()
		{
			if (text_ != null)
			{
				text_.text = bool_.name + ":" + bool_.flag;
			}
		}
	}
}
