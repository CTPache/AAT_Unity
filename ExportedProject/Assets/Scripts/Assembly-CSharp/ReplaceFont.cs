using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplaceFont : MonoBehaviour
{
	private enum FontType
	{
		DEFAULT = 0,
		KOREA = 1,
		CHINESE_S = 2,
		CHINESE_T = 3
	}

	[SerializeField]
	private Font default_font_data_;

	[SerializeField]
	private Font korea_font_data_;

	[SerializeField]
	private Font chinese_s_font_data_;

	[SerializeField]
	private Font chinese_t_font_data_;

	private List<Text> text_objects_ = new List<Text>();

	private FontType current_font_;

	public static ReplaceFont instance { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	public void Init()
	{
		text_objects_.AddRange(Resources.FindObjectsOfTypeAll(typeof(Text)) as Text[]);
		ChangeFont(default_font_data_);
	}

	public void ChangeFont(Language language)
	{
		FontType fontTyop = GetFontTyop(language);
		Debug.Log(string.Concat("ReplaceFont AllChangeFont current=", current_font_, " next=", fontTyop));
		if (current_font_ != fontTyop)
		{
			Font fontData = GetFontData(fontTyop);
			current_font_ = fontTyop;
			ChangeFont(fontData);
		}
	}

	private void ChangeFont(Font font_data)
	{
		for (int i = 0; i < text_objects_.Count; i++)
		{
			text_objects_[i].font = font_data;
		}
	}

	private Font GetFontData(FontType font_type)
	{
		switch (font_type)
		{
		case FontType.DEFAULT:
			return default_font_data_;
		case FontType.KOREA:
			return korea_font_data_;
		case FontType.CHINESE_S:
			return chinese_s_font_data_;
		case FontType.CHINESE_T:
			return chinese_t_font_data_;
		default:
			return default_font_data_;
		}
	}

	private FontType GetFontTyop(Language language)
	{
		switch (language)
		{
		case Language.KOREA:
			return FontType.KOREA;
		case Language.CHINA_S:
			return FontType.CHINESE_S;
		case Language.CHINA_T:
			return FontType.CHINESE_T;
		default:
			return FontType.DEFAULT;
		}
	}
}
