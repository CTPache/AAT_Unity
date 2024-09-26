using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditCtrl : MonoBehaviour
{
    private enum CreditType
    {
        begin = 0,
        caption = 1,
        job = 2,
        staff = 3,
        center = 4
    }

    private class CreditData
    {
        public CreditType credit_type;

        public string credit_text;

        public float position_y;

        public CreditData(CreditType in_credit_type, string in_credit_text)
        {
            credit_type = in_credit_type;
            credit_text = in_credit_text;
        }
    }

    private class CreditText
    {
        private GameObject game_object_;

        private RectTransform rect_transform_;

        public Text text;

        public Vector3 position
        {
            get
            {
                return rect_transform_.localPosition;
            }
            set
            {
                rect_transform_.localPosition = value;
            }
        }

        public CreditText(GameObject in_text_object)
        {
            game_object_ = in_text_object;
            rect_transform_ = game_object_.GetComponent<RectTransform>();
            text = game_object_.GetComponent<Text>();
        }
    }

    private class TitleLogo
    {
        public string path = string.Empty;

        public string file = string.Empty;

        public TitleLogo(string in_path, string in_file)
        {
            path = in_path;
            file = in_file;
        }
    }

    [Serializable]
    private class CreditLogo
    {
        [Tooltip("credit/body/Canvas/logo/\n↑のImageをコピペしてアタッチしてください")]
        public Image image_object;

        [Tooltip("ストリーミングアセット以下のフォルダパス")]
        public string file_path = string.Empty;

        [Tooltip("ファイル名")]
        public string file_name = string.Empty;
    }

    [Serializable]
    private class FontSetting
    {
        [Header("フォントサイズ")]
        [Tooltip("一番最初に表示されるやつ")]
        public int begin_fontsize = 72;

        [Tooltip("会社名：中央に表示される")]
        public int caption_fontsize = 60;

        [Tooltip("役職名：左側に表示される")]
        public int job_fontsize = 36;

        [Tooltip("氏名：右側に表示される")]
        public int staff_fontsize = 48;

        [Tooltip("中央揃えされるやつ")]
        public int center_fontsize = 36;

        [Header("文字の間隔")]
        [Tooltip("会社名：中央に表示される")]
        public int caption_interval = 90;

        [Tooltip("役職名：左側に表示される")]
        public int job_interval = 50;

        [Tooltip("氏名：右側に表示される")]
        public int staff_interval = 10;

        [Tooltip("中央揃えされるやつ")]
        public int center_interval = 10;
    }

    [Serializable]
    private class TimeSetting
    {
        [Tooltip("クレジット開始時のフェードイン")]
        public int screen_fade_in = 180;

        [Tooltip("クレジット終了時のフェードアウト")]
        public int screen_fade_out = 120;

        [Tooltip("ＢＧＭ開始までの待ち")]
        public int bgm_start_wait;

        [Tooltip("タイトルロゴ表示までの待ち\nElement0 : GS1\nElement1 : GS2\nElement2 : GS3")]
        public int[] title_logo_start_wait = new int[3] { 120, 30, 30 };

        [Tooltip("タイトルロゴ表示中の待ち")]
        public int title_logo_on_wait = 240;

        [Tooltip("タイトルロゴのフェードイン")]
        public int title_logo_fade_in = 30;

        [Tooltip("タイトルロゴのフェードアウト")]
        public int title_logo_fade_out = 60;

        [Tooltip("スタッフロール開始テキスト表示までの待ち")]
        public int begin_text_start_wait;

        [Tooltip("スタッフロール開始テキスト表示中の待ち")]
        public int begin_text_on_wait = 180;

        [Tooltip("スタッフロール開始テキスト非表示中の待ち")]
        public int begin_text_off_wait = 30;

        [Tooltip("スタッフロール開始テキストのフェードイン")]
        public int begin_text_fade_in = 30;

        [Tooltip("スタッフロール開始テキストのフェードアウト")]
        public int begin_text_fade_out = 120;

        [Tooltip("CAPCOMロゴ表示までの待ち")]
        public int CAPCOM_logo_wait;

        [Tooltip("CAPCOMロゴ表示中の待ち")]
        public int CAPCOM_logo_on_wait = 420;

        [Tooltip("CAPCOMロゴのフェードイン")]
        public int CAPCOM_logo_fade_in = 30;

        [Header("スクロールの時間\u3000※秒で指定")]
        [Tooltip("総時間（秒）")]
        public float scroll_time = 57.5f;
    }

    [SerializeField]
    private GameObject body_;

    [SerializeField]
    private Image background_;

    [SerializeField]
    private Image image_;

    [SerializeField]
    private guideCtrl guide_ctrl_;

    [SerializeField]
    private RectTransform text_parent_;

    [SerializeField]
    private Text text_object_template_;

    [SerializeField]
    private RectTransform credit_logo_parent_;

    [Header("Setting")]
    [SerializeField]
    [Tooltip("スタッフロールの最後にスクロールする会社とかのロゴ")]
    private List<CreditLogo> credit_logo_;

    [SerializeField]
    [Tooltip("同時に描写されるテキスト数")]
    private int text_object_count_ = 40;

    [SerializeField]
    [Tooltip("テキストの設定")]
    private FontSetting font_setting_;

    [SerializeField]
    [Tooltip("表示時間の設定\nフレーム数で指定してください")]
    private TimeSetting time_setting_;

    private CreditText text_object_begin_;

    private List<CreditText> text_object_;

    private List<Sprite> sprites_;

    private IEnumerator enumerator_play_;

    private IEnumerator enumerator_main_;

    private ConvertLineData convert_credit_text_;

    private float screen_limit_;

    private float scroll_speed_;

    private float scroll_end_position_y_;

    private bool is_credit_end_;

    private static List<CreditData> credit_text_data_ = null;

    private const string credit_text_path_ = "/menu/text/credit_text.bin";

    private const float text_parent_position_x_ = -60f;

    private static readonly TitleLogo[] title_logo_jp_ = new TitleLogo[3]
    {
        new TitleLogo("/GS1/BG/", "titlegs1"),
        new TitleLogo("/GS2/BG/", "titlegs2"),
        new TitleLogo("/GS3/BG/", "titlegs3")
    };

    private static readonly TitleLogo[] title_logo_us_ = new TitleLogo[3]
    {
        new TitleLogo("/GS1/BG/", "titlegs1u"),
        new TitleLogo("/GS2/BG/", "titlegs2u"),
        new TitleLogo("/GS3/BG/", "titlegs3u")
    };

    public static creditCtrl instance { get; private set; }

    public guideCtrl guide_ctrl
    {
        get
        {
            return guide_ctrl_;
        }
    }

    private bool body_active_
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

    private bool image_active_
    {
        get
        {
            return image_.enabled;
        }
        set
        {
            image_.enabled = value;
        }
    }

    private bool credit_logo_active_
    {
        get
        {
            return credit_logo_parent_.gameObject.activeSelf;
        }
        set
        {
            credit_logo_parent_.gameObject.SetActive(value);
        }
    }

    private float image_alpha_
    {
        get
        {
            return image_.color.a;
        }
        set
        {
            image_.color = new Color(image_.color.r, image_.color.g, image_.color.b, value);
        }
    }

    public bool is_play { get; private set; }

    private static TitleLogo[] title_logo_
    {
        get
        {
            string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper(); 
            switch (lang)
            {
                case "JAPAN":
                    return title_logo_jp_;
                case "USA":
                    return title_logo_us_;
                default:
                    return title_logo_jp_;
            }
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Init()
    {
        Load();
        guide_ctrl_.init();
        screen_limit_ = (float)(systemCtrl.instance.ScreenHeight / 2) + 100f;
        is_credit_end_ = false;
        body_active_ = true;
        image_active_ = false;
        credit_logo_active_ = false;
        text_parent_.localPosition = Vector3.zero;
        credit_logo_parent_.localPosition = Vector3.zero;
        CreateTextObject();
        TouchSystem.TouchInActive();
    }

    private void End()
    {
        StopAllCoroutines();
        enumerator_play_ = null;
        enumerator_main_ = null;
        guide_ctrl_.guideIconSet(true, guideCtrl.GuideType.NO_GUIDE);
        guide_ctrl_.Close();
        soundCtrl.instance.StopBGM(600);
        is_credit_end_ = false;
        body_active_ = false;
        image_active_ = false;
        credit_logo_active_ = false;
        image_.sprite = null;
        text_parent_.localPosition = Vector3.zero;
        credit_logo_parent_.localPosition = Vector3.zero;
        sprites_.Clear();
        credit_text_data_.Clear();
        if (text_object_begin_ != null)
        {
            mainCtrl.instance.removeText(text_object_begin_.text);
            UnityEngine.Object.Destroy(text_object_begin_.text.gameObject);
            text_object_begin_ = null;
        }
        foreach (CreditText item in text_object_)
        {
            mainCtrl.instance.removeText(item.text);
            UnityEngine.Object.Destroy(item.text.gameObject);
        }
        text_object_.Clear();
        foreach (CreditLogo item2 in credit_logo_)
        {
            item2.image_object.sprite = null;
        }
        AssetBundleCtrl.instance.remove("/GS1/BG/", "bg044");
        TouchSystem.TouchInActive();
    }

    private void Load()
    {
        AssetBundleCtrl assetBundleCtrl = AssetBundleCtrl.instance;
        AssetBundle assetBundle = null;
        sprites_ = new List<Sprite>();
        for (int i = 0; i < title_logo_.Length; i++)
        {
            assetBundle = assetBundleCtrl.load(title_logo_[i].path, title_logo_[i].file);
            sprites_.Add(assetBundle.LoadAllAssets<Sprite>()[0]);
        }
        assetBundle = assetBundleCtrl.load("/GS1/BG/", "bg044");
        sprites_.Add(assetBundle.LoadAllAssets<Sprite>()[0]);
        foreach (CreditLogo item in credit_logo_)
        {
            assetBundle = assetBundleCtrl.load(item.file_path, item.file_name);
            item.image_object.sprite = assetBundle.LoadAllAssets<Sprite>()[0];
        }
        assetBundle = assetBundleCtrl.load("/menu/common/", "mask");
        background_.sprite = assetBundle.LoadAllAssets<Sprite>()[0];
        credit_text_data_ = new List<CreditData>();
        LoadTextData(ref convert_credit_text_, "/menu/text/credit_text.bin", "USA");
        ConvertLineData.Data[] data = convert_credit_text_.data;
        for (int j = 0; j < data.Length; j++)
        {
            ConvertLineData.Data data2 = data[j];
            credit_text_data_.Add(new CreditData((CreditType)int.Parse(data2.text[0]), data2.text[1]));
        }
    }

    private void LoadTextData(ref ConvertLineData data, string path, string language)
    {
        string in_path = Application.streamingAssetsPath + path;
        byte[] bytes = decryptionCtrl.instance.load(in_path);
        data = new ConvertLineData(bytes, language);
    }

    private void CreateTextObject()
    {
        text_object_template_.text = string.Empty;
        text_object_template_.rectTransform.sizeDelta = Vector3.zero;
        SetTextParameter(ref text_object_template_, font_setting_.begin_fontsize, TextAnchor.MiddleCenter, new Color(1f, 1f, 1f, 0f), Vector3.zero);
        text_object_begin_ = new CreditText(UnityEngine.Object.Instantiate(text_object_template_.gameObject));
        text_object_begin_.text.rectTransform.SetParent(text_parent_, false);
        text_object_begin_.text.enabled = false;
        mainCtrl.instance.addText(text_object_begin_.text);
        SetTextParameter(ref text_object_template_, font_setting_.caption_fontsize, TextAnchor.UpperCenter, Color.white, Vector3.zero);
        text_object_ = new List<CreditText>();
        for (int i = 0; i < text_object_count_; i++)
        {
            text_object_.Add(new CreditText(UnityEngine.Object.Instantiate(text_object_template_.gameObject)));
            text_object_[i].text.rectTransform.SetParent(text_parent_, false);
            text_object_[i].text.enabled = false;
            mainCtrl.instance.addText(text_object_[i].text);
        }
    }

    private void SetTextParameter(ref Text text, int in_fontsize, TextAnchor in_alignment, Color in_color, Vector3 in_position)
    {
        text.fontSize = in_fontsize;
        text.alignment = in_alignment;
        text.color = in_color;
        text.rectTransform.localPosition = in_position;
    }

    private void SetTextObject(CreditText credit_text, CreditData credit_data)
    {
        switch (credit_data.credit_type)
        {
            case CreditType.caption:
                SetTextParameter(ref credit_text.text, font_setting_.caption_fontsize, TextAnchor.UpperCenter, Color.white, Vector3.zero);
                break;
            case CreditType.job:
                SetTextParameter(ref credit_text.text, font_setting_.job_fontsize, TextAnchor.UpperRight, Color.white, new Vector3(-100f, 0f, 0f));
                break;
            case CreditType.staff:
                SetTextParameter(ref credit_text.text, font_setting_.staff_fontsize, TextAnchor.UpperLeft, Color.white, new Vector3(-20f, 0f, 0f));
                break;
            case CreditType.center:
                SetTextParameter(ref credit_text.text, font_setting_.center_fontsize, TextAnchor.UpperCenter, Color.white, Vector3.zero);
                break;
        }
        credit_text.position = new Vector3(credit_text.position.x, credit_data.position_y, credit_text.position.z);
        credit_text.text.text = credit_data.credit_text;
        credit_text.text.enabled = true;
    }

    private void TextPositionSetting()
    {
        float num = font_setting_.caption_interval;
        float num2 = font_setting_.job_interval + font_setting_.staff_fontsize;
        float num3 = font_setting_.staff_interval + font_setting_.staff_fontsize;
        float num4 = font_setting_.center_interval + font_setting_.staff_fontsize;
        float num5 = num * 3f;
        CreditType creditType = CreditType.begin;
        for (int i = 0; i < credit_text_data_.Count; i++)
        {
            switch (credit_text_data_[i].credit_type)
            {
                case CreditType.caption:
                    num5 -= num * 3f;
                    credit_text_data_[i].position_y = num5;
                    num5 -= num;
                    break;
                case CreditType.job:
                    num5 -= num2;
                    credit_text_data_[i].position_y = num5 - 8f;
                    break;
                case CreditType.staff:
                    if (creditType != CreditType.job)
                    {
                        num5 -= num3;
                    }
                    credit_text_data_[i].position_y = num5;
                    break;
                case CreditType.center:
                    num5 -= num4;
                    credit_text_data_[i].position_y = num5;
                    break;
            }
            creditType = credit_text_data_[i].credit_type;
        }
        credit_logo_parent_.localPosition = new Vector3(0f, num5 - num - screen_limit_, 0f);
        float num6 = 0f;
        foreach (CreditLogo item in credit_logo_)
        {
            item.image_object.SetNativeSize();
            if (item.image_object.rectTransform.localPosition.y < num6)
            {
                float y = item.image_object.rectTransform.localPosition.y;
                float num7 = item.image_object.rectTransform.sizeDelta.y / 2f;
                num6 = y - num7;
            }
        }
        scroll_end_position_y_ = 0f - num6;
        float num8 = 0f - (num6 + credit_logo_parent_.localPosition.y) + 270f;
        float num9 = systemCtrl.instance.ScreenHeight;
        scroll_speed_ = (num8 - num9) / (time_setting_.scroll_time * 60f);
    }

    private void TextPositionLoop(CreditText credit_text, ref int text_data_index)
    {
        if (credit_text.text.text == string.Empty && text_data_index < credit_text_data_.Count)
        {
            SetTextObject(credit_text, credit_text_data_[text_data_index]);
            text_data_index++;
        }
        if (text_parent_.localPosition.y + credit_text.position.y > screen_limit_)
        {
            credit_text.text.text = string.Empty;
            credit_text.text.enabled = false;
        }
    }

    private IEnumerator CoroutineStaffRoll()
    {
        int text_data_index = 1;
        TextPositionSetting();
        foreach (CreditText item in text_object_)
        {
            if (text_data_index >= credit_text_data_.Count)
            {
                break;
            }
            SetTextObject(item, credit_text_data_[text_data_index]);
            text_data_index++;
        }
        text_parent_.localPosition = new Vector3(0f, 0f - screen_limit_, 0f);
        credit_logo_active_ = true;
        while (true)
        {
            foreach (CreditText item2 in text_object_)
            {
                TextPositionLoop(item2, ref text_data_index);
            }
            if (credit_logo_parent_.localPosition.y + scroll_end_position_y_ > screen_limit_)
            {
                break;
            }
            text_parent_.localPosition += Vector3.up * scroll_speed_;
            credit_logo_parent_.localPosition += Vector3.up * scroll_speed_;
            yield return null;
        }
        credit_logo_active_ = false;
    }

    private IEnumerator CoroutineWait(int in_wait_time)
    {
        int timer = 0;
        while (timer < in_wait_time)
        {
            timer++;
            yield return null;
        }
    }

    private IEnumerator CoroutineEndJudge()
    {
        while (true)
        {
            if (padCtrl.instance.GetKeyDown(KeyType.B) && fadeCtrl.instance.is_end)
            {
                TouchSystem.TouchInActive();
                soundCtrl.instance.PlaySE(44);
                yield break;
            }
            if (is_credit_end_)
            {
                break;
            }
            yield return null;
        }
        TouchSystem.TouchInActive();
    }

    private IEnumerator CoroutineScreenFade(int in_time, bool in_type)
    {
        fadeCtrl.instance.play(in_time, in_type);
        while (!fadeCtrl.instance.is_end)
        {
            yield return null;
        }
    }

    private IEnumerator CoroutineImageFade(int in_time, bool in_type)
    {
        int timer = 0;
        float alpha_speed = ((!in_type) ? (1f / (float)in_time * -1f) : (1f / (float)in_time));
        image_alpha_ = ((!in_type) ? 1f : 0f);
        image_active_ = true;
        while (timer < in_time)
        {
            image_alpha_ += alpha_speed;
            timer++;
            yield return null;
        }
        image_alpha_ = ((!in_type) ? 0f : 1f);
        image_active_ = in_type;
    }

    private IEnumerator CoroutineTextFade(int in_time, bool in_type)
    {
        int timer = 0;
        float alpha_speed = ((!in_type) ? (1f / (float)in_time * -1f) : (1f / (float)in_time));
        Color text_color = text_object_begin_.text.color;
        text_color.a = ((!in_type) ? 1f : 0f);
        text_object_begin_.text.color = text_color;
        text_object_begin_.text.enabled = true;
        while (timer < in_time)
        {
            text_color.a += alpha_speed;
            text_object_begin_.text.color = text_color;
            timer++;
            yield return null;
        }
        text_color.a = ((!in_type) ? 0f : 1f);
        text_object_begin_.text.enabled = in_type;
    }

    private IEnumerator CoroutineMain()
    {
        yield return coroutineCtrl.instance.Play(CoroutineScreenFade(time_setting_.screen_fade_in, true));
        guide_ctrl_.ReLoadGuid();
        coroutineCtrl.instance.Play(guide_ctrl_.open(guideCtrl.GuideType.CREDIT));
        guide_ctrl_.ActiveTouch();
        yield return coroutineCtrl.instance.Play(CoroutineWait(time_setting_.bgm_start_wait));
        soundCtrl.instance.PlayBGM(600);
        for (int i = 0; i < 3; i++)
        {
            yield return coroutineCtrl.instance.Play(CoroutineWait(time_setting_.title_logo_start_wait[i]));
            image_.sprite = sprites_[i];
            image_.SetNativeSize();
            yield return coroutineCtrl.instance.Play(CoroutineImageFade(time_setting_.title_logo_fade_in, true));
            yield return coroutineCtrl.instance.Play(CoroutineWait(time_setting_.title_logo_on_wait));
            yield return coroutineCtrl.instance.Play(CoroutineImageFade(time_setting_.title_logo_fade_out, false));
        }
        yield return coroutineCtrl.instance.Play(CoroutineWait(time_setting_.begin_text_start_wait));
        text_object_begin_.text.text = credit_text_data_[0].credit_text;
        yield return coroutineCtrl.instance.Play(CoroutineTextFade(time_setting_.begin_text_fade_in, true));
        yield return coroutineCtrl.instance.Play(CoroutineWait(time_setting_.begin_text_on_wait));
        yield return coroutineCtrl.instance.Play(CoroutineTextFade(time_setting_.begin_text_fade_out, false));
        text_object_begin_.text.text = string.Empty;
        yield return coroutineCtrl.instance.Play(CoroutineWait(time_setting_.begin_text_off_wait));
        yield return coroutineCtrl.instance.Play(CoroutineStaffRoll());
        yield return coroutineCtrl.instance.Play(CoroutineWait(time_setting_.CAPCOM_logo_wait));
        image_.sprite = sprites_[3];
        image_.SetNativeSize();
        image_active_ = true;
        image_.rectTransform.localPosition = Vector3.zero;
        image_alpha_ = 0f;
        yield return coroutineCtrl.instance.Play(CoroutineImageFade(time_setting_.CAPCOM_logo_fade_in, true));
        yield return coroutineCtrl.instance.Play(CoroutineWait(time_setting_.CAPCOM_logo_on_wait));
        enumerator_main_ = null;
        is_credit_end_ = true;
    }

    private IEnumerator CoroutinePlay()
    {
        is_play = true;
        Init();
        enumerator_main_ = CoroutineMain();
        coroutineCtrl.instance.Play(enumerator_main_);
        yield return coroutineCtrl.instance.Play(CoroutineEndJudge());
        coroutineCtrl.instance.Play(guide_ctrl_.close());
        soundCtrl.instance.FadeOutBGM(time_setting_.screen_fade_out);
        yield return coroutineCtrl.instance.Play(CoroutineScreenFade(time_setting_.screen_fade_out, false));
        if (enumerator_main_ != null)
        {
            coroutineCtrl.instance.Stop(enumerator_main_);
            enumerator_main_ = null;
        }
        End();
        is_play = false;
    }

    public void Play()
    {
        if (enumerator_play_ != null)
        {
            End();
        }
        enumerator_play_ = CoroutinePlay();
        coroutineCtrl.instance.Play(enumerator_play_);
    }

    public void Stop()
    {
        End();
        is_play = false;
    }
}
