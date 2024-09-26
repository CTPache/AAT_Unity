using System.Collections.Generic;
using System;
using UnityEngine;

public class optionLanguage : optionSelectItem
{
    public override void Init()
    {
        SetText(TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ITEM_LANGUAGE));
        List<string> langs = new List<string>();
        string[] originalLangs = {
            TextDataCtrl.GetText(TextDataCtrl.OptionTextID.JAPANESE),
            TextDataCtrl.GetText(TextDataCtrl.OptionTextID.ENGLISH),
            TextDataCtrl.GetText(TextDataCtrl.OptionTextID.FRENCH),
            TextDataCtrl.GetText(TextDataCtrl.OptionTextID.GERMAN),
            TextDataCtrl.GetText(TextDataCtrl.OptionTextID.KOREAN),
            TextDataCtrl.GetText(TextDataCtrl.OptionTextID.CHINESE_SIMPLIFIED),
            TextDataCtrl.GetText(TextDataCtrl.OptionTextID.CHINESE_TRADITIONAL),
        };
        langs.AddRange(originalLangs);
        langs.AddRange(LangPackCtrl.getMenuStrings(GSStatic.global_work_.language));
        select_text_ = langs.ToArray();
        max_value_size_ = select_text_.Length;
        base.Init();

        setting_value_ = GSStatic.option_work.language_type;
    }

    public override bool SelectDecision()
    {
        GSStatic.option_work.language_type = (ushort)setting_value_;
        Debug.Log("About to set the language to " + setting_value_);
        return GSStatic.global_work_.language != Language.languages[setting_value_];
    }

    public override void EnterValue()
    {
        ChangeLanguage();
    }

    private bool ChangeLanguage()
    {
        string language = Language.languages[setting_value_];
        if (GSStatic.global_work_.language != language)
        {
            GSStatic.global_work_.language = language;
            ReplaceFont.instance.ChangeFont(GSStatic.global_work_.language);
            TextDataCtrl.SetLanguage(GSStatic.global_work_.language);
            return true;
        }
        return false;
    }

    public override void InitValueSet()
    {
        setting_value_ = GSStatic.option_work.language_type;
        ChangeLanguage();
    }

    public override bool ConfirmChange()
    {
        if (GSStatic.save_slot_language_ != GSStatic.global_work_.language)
        {
            return true;
        }
        return false;
    }
}
