using UnityEngine;
using UnityEngine.UI;

namespace DebugMenu.uGUI
{
	public class Func : Item
	{
		private DebugMenu.Func func_;

		private Image image_;

		private Text text_;

		public override DebugMenu.Item item
		{
			get
			{
				return func_;
			}
		}

		public Func(DebugMenu.Func func)
		{
			func_ = func;
		}

		public override GameObject CreateObject(Menu menu, Transform parent)
		{
			HideFlags hideFlags = HideFlags.DontSave;
			GameObject gameObject = new GameObject(func_.name);
			gameObject.transform.SetParent(parent, false);
			gameObject.hideFlags = hideFlags;
			Image image = gameObject.AddComponent<Image>();
			image.color = menu.frame_color;
			RectTransform rectTransform = image.rectTransform;
			rectTransform.sizeDelta = new Vector2(menu.item_width, menu.item_height);
			Button button = gameObject.AddComponent<Button>();
			button.onClick.AddListener(func_.OnDecide);
			GameObject gameObject2 = new GameObject("Text");
			gameObject2.transform.SetParent(gameObject.transform, false);
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.hideFlags = hideFlags;
			Text text = gameObject2.AddComponent<Text>();
			text.text = "(x)" + func_.name;
			text.font = menu.text_font;
			text.fontSize = menu.text_font_size;
			text.color = menu.text_color;
			text.alignment = TextAnchor.MiddleCenter;
			RectTransform rectTransform2 = text.rectTransform;
			rectTransform2.sizeDelta = rectTransform.sizeDelta;
			image_ = image;
			text_ = text;
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
	}
}
