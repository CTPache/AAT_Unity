using System;
using UnityEngine;
using UnityEngine.UI;

namespace DebugMenu.uGUI
{
	public class Number<T> : Item where T : IComparable<T>
	{
		private DebugMenu.Number<T> number_;

		private Image image_;

		private Text text_;

		private T last_;

		public override DebugMenu.Item item
		{
			get
			{
				return number_;
			}
		}

		public Number(DebugMenu.Number<T> number)
		{
			number_ = number;
			last_ = number_.number;
		}

		public override GameObject CreateObject(Menu menu, Transform parent)
		{
			HideFlags hideFlags = HideFlags.DontSave;
			GameObject gameObject = new GameObject(number_.name);
			gameObject.transform.SetParent(parent, false);
			gameObject.hideFlags = hideFlags;
			Image image = gameObject.AddComponent<Image>();
			image.color = menu.frame_color;
			RectTransform rectTransform = image.rectTransform;
			rectTransform.sizeDelta = new Vector2(menu.item_width, menu.item_height);
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
			GameObject gameObject3 = new GameObject("Left");
			gameObject3.transform.SetParent(rectTransform.transform, false);
			gameObject3.hideFlags = hideFlags;
			Image image2 = gameObject3.AddComponent<Image>();
			image2.color = Color.clear;
			RectTransform rectTransform3 = image2.rectTransform;
			rectTransform3.sizeDelta = new Vector2((float)menu.item_width * 0.5f, menu.item_height);
			rectTransform3.anchorMin = new Vector2(0f, 0.5f);
			rectTransform3.anchorMax = new Vector2(0f, 0.5f);
			rectTransform3.pivot = new Vector2(0f, 0.5f);
			rectTransform3.anchoredPosition = new Vector2(0f, 0f);
			Button button = gameObject3.AddComponent<Button>();
			button.onClick.AddListener(delegate
			{
				menu.current.selected = number_;
				number_.OnSub();
			});
			GameObject gameObject4 = new GameObject("[-]");
			gameObject4.transform.SetParent(rectTransform3, false);
			gameObject4.hideFlags = hideFlags;
			Text text2 = gameObject4.AddComponent<Text>();
			text2.text = string.Concat("[-", number_.step, "]");
			text2.font = menu.text_font;
			text2.fontSize = menu.text_font_size;
			text2.color = menu.text_color;
			text2.alignment = TextAnchor.MiddleLeft;
			RectTransform rectTransform4 = text2.rectTransform;
			rectTransform4.sizeDelta = rectTransform3.sizeDelta;
			GameObject gameObject5 = new GameObject("Right");
			gameObject5.transform.SetParent(rectTransform.transform, false);
			gameObject5.hideFlags = hideFlags;
			Image image3 = gameObject5.AddComponent<Image>();
			image3.color = Color.clear;
			RectTransform rectTransform5 = image3.rectTransform;
			rectTransform5.sizeDelta = new Vector2((float)menu.item_width * 0.5f, menu.item_height);
			rectTransform5.anchorMin = new Vector2(1f, 0.5f);
			rectTransform5.anchorMax = new Vector2(1f, 0.5f);
			rectTransform5.pivot = new Vector2(1f, 0.5f);
			rectTransform5.anchoredPosition = new Vector2(0f, 0f);
			Button button2 = gameObject5.AddComponent<Button>();
			button2.onClick.AddListener(delegate
			{
				menu.current.selected = number_;
				number_.OnAdd();
			});
			GameObject gameObject6 = new GameObject("[+]");
			gameObject6.transform.SetParent(rectTransform5, false);
			gameObject6.hideFlags = hideFlags;
			Text text3 = gameObject6.AddComponent<Text>();
			text3.text = string.Concat("[+", number_.step, "]");
			text3.font = menu.text_font;
			text3.fontSize = menu.text_font_size;
			text3.color = menu.text_color;
			text3.alignment = TextAnchor.MiddleRight;
			RectTransform rectTransform6 = text3.rectTransform;
			rectTransform6.sizeDelta = rectTransform5.sizeDelta;
			image_ = image;
			text_ = text;
			UpdateNumber();
			number_.update_number += UpdateNumber;
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
			if (last_.CompareTo(number_.number) != 0)
			{
				last_ = number_.number;
				UpdateNumber();
			}
		}

		private void UpdateNumber()
		{
			if (text_ != null)
			{
				text_.text = number_.name + ":" + number_.number.ToString();
			}
		}
	}
}
