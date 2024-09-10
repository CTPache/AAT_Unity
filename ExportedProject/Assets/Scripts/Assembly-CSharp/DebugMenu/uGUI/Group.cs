using UnityEngine;
using UnityEngine.UI;

namespace DebugMenu.uGUI
{
	public class Group : Item
	{
		private DebugMenu.Group group_;

		private DebugMenu.Item last_selected_;

		private Item[] items_;

		private GameObject[] item_objects_;

		private Image image_;

		private Text text_;

		private ScrollRect scroll_rect_;

		private Transform content_trans_;

		public override DebugMenu.Item item
		{
			get
			{
				return group_;
			}
		}

		public Group(DebugMenu.Group group)
		{
			group_ = group;
		}

		public override GameObject CreateObject(Menu menu, Transform parent)
		{
			return CreateObject(menu, parent, false);
		}

		public GameObject CreateObject(Menu menu, Transform parent, bool current)
		{
			HideFlags hideFlags = HideFlags.DontSave;
			if (current)
			{
				Vector2 sizeDelta = parent.GetComponent<RectTransform>().sizeDelta;
				GameObject gameObject = new GameObject(group_.name);
				gameObject.transform.SetParent(parent, false);
				gameObject.hideFlags = hideFlags;
				gameObject.AddComponent<RectTransform>().sizeDelta = sizeDelta;
				GameObject gameObject2 = new GameObject("Title");
				gameObject2.transform.SetParent(gameObject.transform, false);
				gameObject2.hideFlags = hideFlags;
				Image image = gameObject2.AddComponent<Image>();
				image.color = menu.frame_color;
				RectTransform rectTransform = image.rectTransform;
				rectTransform.sizeDelta = new Vector2(menu.item_width, menu.item_height);
				rectTransform.anchorMin = new Vector2(0.5f, 1f);
				rectTransform.anchorMax = new Vector2(0.5f, 1f);
				rectTransform.pivot = new Vector2(0.5f, 1f);
				rectTransform.anchoredPosition = Vector2.zero;
				Button button = gameObject2.AddComponent<Button>();
				button.onClick.AddListener(delegate
				{
					group_.OnCancel(menu);
				});
				GameObject gameObject3 = new GameObject("Text");
				gameObject3.transform.SetParent(gameObject2.transform, false);
				gameObject3.transform.localPosition = Vector3.zero;
				gameObject3.hideFlags = hideFlags;
				Text text = gameObject3.AddComponent<Text>();
				text.text = "<< " + group_.name;
				text.font = menu.text_font;
				text.fontSize = menu.text_font_size;
				text.color = menu.text_color;
				text.alignment = TextAnchor.MiddleCenter;
				RectTransform rectTransform2 = text.rectTransform;
				rectTransform2.sizeDelta = rectTransform.sizeDelta;
				GameObject gameObject4 = new GameObject("ScrollRect");
				gameObject4.transform.SetParent(gameObject.transform, false);
				gameObject4.hideFlags = hideFlags;
				ScrollRect scrollRect = gameObject4.AddComponent<ScrollRect>();
				scrollRect.horizontal = false;
				gameObject4.AddComponent<RectMask2D>();
				RectTransform component = gameObject4.GetComponent<RectTransform>();
				component.sizeDelta = sizeDelta + new Vector2(0f, -menu.item_height);
				component.anchorMin = new Vector2(0.5f, 1f);
				component.anchorMax = new Vector2(0.5f, 1f);
				component.pivot = new Vector2(0.5f, 1f);
				component.anchoredPosition = new Vector2(0f, -menu.item_height);
				scrollRect.viewport = component;
				GameObject gameObject5 = new GameObject("Content");
				gameObject5.transform.SetParent(gameObject4.transform, false);
				gameObject5.transform.localPosition = Vector3.zero;
				RectTransform rectTransform3 = gameObject5.AddComponent<RectTransform>();
				rectTransform3.sizeDelta = new Vector2(menu.item_width, menu.item_height * group_.item_count);
				rectTransform3.anchorMin = new Vector2(0.5f, 1f);
				rectTransform3.anchorMax = new Vector2(0.5f, 1f);
				rectTransform3.pivot = new Vector2(0.5f, 1f);
				rectTransform3.anchoredPosition = Vector2.zero;
				scrollRect.content = rectTransform3;
				scroll_rect_ = scrollRect;
				content_trans_ = rectTransform3;
				CreateItemObject(menu);
				return gameObject;
			}
			GameObject gameObject6 = new GameObject(group_.name);
			gameObject6.transform.SetParent(parent, false);
			gameObject6.hideFlags = hideFlags;
			Image image2 = gameObject6.AddComponent<Image>();
			image2.color = menu.frame_color;
			RectTransform rectTransform4 = image2.rectTransform;
			rectTransform4.sizeDelta = new Vector2(menu.item_width, menu.item_height);
			Button button2 = gameObject6.AddComponent<Button>();
			button2.onClick.AddListener(delegate
			{
				group_.OnDecide(menu);
			});
			GameObject gameObject7 = new GameObject("Text");
			gameObject7.transform.SetParent(gameObject6.transform, false);
			gameObject7.transform.localPosition = Vector3.zero;
			gameObject7.hideFlags = hideFlags;
			Text text2 = gameObject7.AddComponent<Text>();
			text2.text = "(+)" + group_.name;
			text2.font = menu.text_font;
			text2.fontSize = menu.text_font_size;
			text2.color = menu.text_color;
			text2.alignment = TextAnchor.MiddleCenter;
			RectTransform rectTransform5 = text2.rectTransform;
			rectTransform5.sizeDelta = rectTransform4.sizeDelta;
			image_ = image2;
			text_ = text2;
			return gameObject6;
		}

		public Item FindItem(DebugMenu.Item item)
		{
			if (items_ != null)
			{
				for (int i = 0; i < items_.Length; i++)
				{
					if (items_[i] != null && items_[i].item == item)
					{
						return items_[i];
					}
				}
			}
			return null;
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

		private void CreateItemObject(Menu menu)
		{
			if (item_objects_ != null)
			{
				for (int i = 0; i < item_objects_.Length; i++)
				{
					if (item_objects_[i] != null)
					{
						Object.Destroy(item_objects_[i]);
					}
				}
			}
			items_ = new Item[group_.item_count];
			item_objects_ = new GameObject[group_.item_count];
			Vector3 zero = Vector3.zero;
			DebugMenu.Item item = group_.first;
			int num = 0;
			while (item != null)
			{
				Item item2 = menu.NewItem(item);
				if (item2 != null)
				{
					items_[num] = item2;
					GameObject gameObject = item2.CreateObject(menu, content_trans_);
					RectTransform component = gameObject.GetComponent<RectTransform>();
					component.anchorMin = new Vector2(0.5f, 1f);
					component.anchorMax = new Vector2(0.5f, 1f);
					component.pivot = new Vector2(0.5f, 1f);
					component.anchoredPosition = zero;
					item_objects_[num] = gameObject;
				}
				zero.y += -menu.item_height;
				item = item.next;
				num++;
			}
		}

		public override void Update(Menu menu)
		{
			if (group_ == menu.current)
			{
				for (int i = 0; i < items_.Length; i++)
				{
					items_[i].Update(menu);
				}
				if (group_.update_item)
				{
					group_.update_item = false;
					OnChangeItem(menu);
				}
				if (last_selected_ != group_.selected)
				{
					DebugMenu.Item last = last_selected_;
					last_selected_ = group_.selected;
					OnChangeSelected(menu, last);
				}
			}
		}

		private void OnChangeItem(Menu menu)
		{
			CreateItemObject(menu);
		}

		private void OnChangeSelected(Menu menu, DebugMenu.Item last)
		{
			Item item = FindItem(last);
			if (item != null)
			{
				item.SetSelected(menu, false);
			}
			item = FindItem(group_.selected);
			if (item == null)
			{
				return;
			}
			item.SetSelected(menu, true);
			float y = scroll_rect_.viewport.sizeDelta.y;
			float y2 = scroll_rect_.content.sizeDelta.y;
			if (!(y < y2))
			{
				return;
			}
			float num = (1f - scroll_rect_.verticalNormalizedPosition) * (0f - (y2 - y));
			GameObject gameObject = FindItemObject(item);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			float y3 = component.anchoredPosition.y;
			if (num < y3)
			{
				scroll_rect_.verticalNormalizedPosition = 1f - y3 / (0f - (y2 - y));
				return;
			}
			num += 0f - y;
			y3 += 0f - component.sizeDelta.y;
			if (num > y3)
			{
				scroll_rect_.verticalNormalizedPosition = 1f - (y3 + y) / (0f - (y2 - y));
			}
		}

		private GameObject FindItemObject(Item item)
		{
			for (int i = 0; i < items_.Length; i++)
			{
				if (items_[i] == item)
				{
					return item_objects_[i];
				}
			}
			return null;
		}
	}
}
