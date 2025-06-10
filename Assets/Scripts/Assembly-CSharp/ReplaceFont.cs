using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReplaceFont : MonoBehaviour
{



    public static ReplaceFont instance { get; private set; }


    private void Awake()
    {
        ReplaceFont.instance = this;
    }


    public void Init()
    {
        this.text_objects_.AddRange(Resources.FindObjectsOfTypeAll(typeof(Text)) as Text[]);
        this.ChangeFont(this.default_font_data_);
    }


    public void ChangeFont(string language)
    {

        ReplaceFont.FontType fontTyop = this.GetFontTyop(language);
        Font fontData = LangPackCtrl.GetFontAsset(language);
        Debug.Log(string.Concat(new object[] { "ReplaceFont AllChangeFont current=", this.current_font_, " next=", fontTyop }));
        if (fontData == null)
            fontData = this.GetFontData(fontTyop);
        current_font_data_ = fontData;
        this.current_font_ = fontTyop;
        this.ChangeFont(fontData);
    }


    public void ChangeFont(Font font_data)
    {
        for (int i = 0; i < this.text_objects_.Count; i++)
        {
            this.text_objects_[i].font = font_data;
        }
    }


    private Font GetFontData(ReplaceFont.FontType font_type)
    {
        switch (font_type)
        {
            case ReplaceFont.FontType.DEFAULT:
                return this.default_font_data_;
            case ReplaceFont.FontType.KOREA:
                return this.korea_font_data_;
            case ReplaceFont.FontType.CHINESE_S:
                return this.chinese_s_font_data_;
            case ReplaceFont.FontType.CHINESE_T:
                return this.chinese_t_font_data_;
            default:
                return this.default_font_data_;
        }
    }


    private ReplaceFont.FontType GetFontTyop(string language)
    {
        switch (language)
        {
            case "KOREA":
                return ReplaceFont.FontType.KOREA;
            case "CHINA_S":
                return ReplaceFont.FontType.CHINESE_S;
            case "CHINA_T":
                return ReplaceFont.FontType.CHINESE_T;
            default:
                return ReplaceFont.FontType.DEFAULT;
        }
    }


    [SerializeField]
    public Font default_font_data_;


    [SerializeField]
    public Font korea_font_data_;


    [SerializeField]
    public Font chinese_s_font_data_;


    [SerializeField]
    public Font chinese_t_font_data_;


    public List<Text> text_objects_ = new List<Text>();

    private FontType current_font_;

    public Font current_font_data_;

    private enum FontType
    {

        DEFAULT,

        KOREA,

        CHINESE_S,

        CHINESE_T,

        CUSTOM
    }
}
