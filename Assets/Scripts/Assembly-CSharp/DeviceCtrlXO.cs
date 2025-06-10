using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DeviceCtrlXO : MonoBehaviour
{
	[Serializable]
	public class GamerTag
	{
		[SerializeField]
public Text text_;

		[SerializeField]
public Image pic_;

		[SerializeField]
public GameObject body_;

		public bool active
		{
			get
			{
				return body_.activeSelf;
			}
			set
			{
				body_.SetActive(value);
			}
		}

		public float alpha
		{
			get
			{
				return pic_.color.a;
			}
			set
			{
				Image image = pic_;
				Color color = new Color(1f, 1f, 1f, value);
				text_.color = color;
				image.color = color;
			}
		}

		public void SetText(string in_text)
		{
			text_.text = in_text;
		}

		public void SetTexture(Texture2D in_texture)
		{
			pic_.sprite = Sprite.Create(in_texture, new Rect(0f, 0f, in_texture.width, in_texture.height), Vector2.zero);
		}

		public void Clear()
		{
			text_.text = string.Empty;
			pic_.sprite = null;
			active = false;
		}
	}

	private static DeviceCtrlXO instance_;

	[SerializeField]
public GamerTag gamer_tag_;

	[SerializeField]
public Text text_;

	public static DeviceCtrlXO instance
	{
		get
		{
			return instance_;
		}
	}

	public GamerTag gamer_tag
	{
		get
		{
			return gamer_tag_;
		}
	}

	public Text text
	{
		get
		{
			return text_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}
}
