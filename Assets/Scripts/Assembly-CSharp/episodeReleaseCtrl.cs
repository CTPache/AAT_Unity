using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class episodeReleaseCtrl : MonoBehaviour
{
    [Serializable]
    public class StorySprite
    {
        public SpriteRenderer picture;

        public SpriteRenderer name;
    }

    private const float pos_interval_ = 730f;

    public const int SCE_CLR_SPEED = 27;

    public const int SCE_CLR_SPEED2 = 54;

    [SerializeField]
    public List<StorySprite> story_list_;

    [SerializeField]
    public SpriteRenderer story_mask_;

    [SerializeField]
    public SpriteRenderer story_focus_L_;

    [SerializeField]
    public SpriteRenderer story_focus_R_;

    [SerializeField]
    public SpriteRenderer focus_L_;

    [SerializeField]
    public SpriteRenderer focus_R_;

    [SerializeField]
    public SpriteRenderer back_ground_;

    [SerializeField]
    public GameObject sprites_;

    [SerializeField]
    public GameObject body_;

    [SerializeField]
    public AnimationCurve focus_scale_curve_ = new AnimationCurve();

    [SerializeField]
    public InputTouch touch_;

    private IEnumerator enumerator_play_;

    private bool is_play_;

    private bool is_scroll_;

    private int[] story_count_ = new int[3] { 5, 4, 5 };

    public static episodeReleaseCtrl instance { get; private set; }

    private Vector3 sprites_pos_
    {
        get
        {
            return sprites_.transform.localPosition;
        }
        set
        {
            sprites_.transform.localPosition = value;
        }
    }

    public bool body_active_
    {
        get
        {
            return body_.activeSelf;
        }
        private set
        {
            body_.SetActive(value);
        }
    }

    private TitleId current_title_
    {
        get
        {
            return GSStatic.global_work_.title;
        }
    }

    private int current_story_
    {
        get
        {
            return GSStatic.global_work_.story;
        }
    }

    public bool is_play
    {
        get
        {
            return is_play_;
        }
    }

    public bool is_scroll
    {
        get
        {
            return is_scroll_;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void play()
    {
        TouchSystem.TouchInActive();
        Initialize();
        Load();
        enumerator_play_ = CoroutinePlay();
        coroutineCtrl.instance.Play(enumerator_play_);
    }

    public void Initialize()
    {
        body_active_ = false;
        is_scroll_ = false;
        is_play_ = false;
        foreach (StorySprite item in story_list_)
        {
            item.picture.sprite = null;
            item.name.sprite = null;
        }
        focus_L_.sprite = null;
        focus_R_.sprite = null;
        story_focus_L_.sprite = null;
        story_focus_R_.sprite = null;
        story_mask_.sprite = null;
        back_ground_.sprite = null;
        if (enumerator_play_ != null)
        {
            coroutineCtrl.instance.Stop(enumerator_play_);
            enumerator_play_ = null;
        }
        touch_.touch_key_type = KeyType.A;
        touch_.SetEnableCollider(false);
    }

    private void Focus_SetActive(bool in_enabled)
    {
        if (in_enabled)
        {
            focus_L_.gameObject.SetActive((current_story_ != 0) ? true : false);
            focus_R_.gameObject.SetActive((current_story_ < story_count_[(int)current_title_]) ? true : false);
        }
        else
        {
            focus_L_.gameObject.SetActive(false);
            focus_R_.gameObject.SetActive(false);
        }
    }

    private IEnumerator CoroutinePlay()
    {
        is_play_ = true;
        is_scroll_ = true;
        Focus_SetActive(false);
        story_mask_.gameObject.SetActive(true);
        sprites_pos_ = new Vector3(0f, 0f, 0f);
        Vector3 speed = new Vector3(27f, 0f, 0f);
        float end_pos = 0f - 730f * (float)current_story_;
        body_active_ = true;
        int timer2 = 0;
        while (timer2 < 30)
        {
            timer2++;
            yield return null;
        }
        while (sprites_pos_.x > end_pos + 730f)
        {
            sprites_pos_ -= speed;
            yield return null;
        }
        speed.x *= 0.5f;
        while (sprites_.transform.localPosition.x > end_pos)
        {
            sprites_.transform.localPosition -= speed;
            yield return null;
        }
        sprites_pos_ = new Vector3(end_pos, 0f, 0f);
        story_mask_.gameObject.SetActive(false);
        Focus_SetActive(true);
        touch_.SetEnableCollider(true);
        Vector3 sprite_scale = story_list_[current_story_].picture.transform.localScale;
        float scale_curve = 0f;
        timer2 = 0;
        while (timer2 < 2)
        {
            sprite_scale.y = (sprite_scale.x = focus_scale_curve_.Evaluate(timer2 / 2));
            story_list_[current_story_].picture.transform.localScale = sprite_scale;
            timer2++;
            yield return null;
        }
        sprite_scale.y = (sprite_scale.x = focus_scale_curve_.Evaluate(1f));
        story_list_[current_story_].picture.transform.localScale = sprite_scale;
        is_scroll_ = false;
        switch (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language))
        {
            case "JAPAN":
                messageBoardCtrl.instance.SetPos(0f, 42f);
                break;
            case "USA":
                messageBoardCtrl.instance.SetPos(0f, 80f);
                break;
            default:
                messageBoardCtrl.instance.SetPos(0f, 80f);
                break;
        }
        MessageSystem.SetActiveMessageWindow(WindowType.SUB);
        switch (current_title_)
        {
            case TitleId.GS1:
                advCtrl.instance.message_system_.SetMessage(scenario.SYS_00050);
                break;
            case TitleId.GS2:
                advCtrl.instance.message_system_.SetMessage(scenario_GS2.SYS_00050);
                break;
            case TitleId.GS3:
                advCtrl.instance.message_system_.SetMessage(scenario_GS3.SYS_00050);
                break;
        }
        messageBoardCtrl.instance.board(true, false);
        messageBoardCtrl.instance.name_plate(false, 0, GSStatic.global_work_.win_name_set);
        while (!padCtrl.instance.GetKeyDown(KeyType.Start) && !padCtrl.instance.GetKeyDown(KeyType.Select) && !padCtrl.instance.GetKeyDown(KeyType.A) && !padCtrl.instance.GetKeyDown(KeyType.B))
        {
            yield return null;
        }
        soundCtrl.instance.PlaySE(43);
        messageBoardCtrl.instance.board(false, false);
        touch_.SetEnableCollider(false);
        fadeCtrl.instance.play(32, false);
        soundCtrl.instance.FadeOutBGM(32);
        timer2 = 0;
        while (timer2 < 32)
        {
            timer2++;
            yield return null;
        }
        Initialize();
    }

    private void Load()
    {
        List<Sprite> list = new List<Sprite>();
        List<Sprite> list2 = new List<Sprite>();
        Sprite sprite = null;
        string in_path = "/GS" + ((int)(current_title_ + 1)).ToString("D1") + "/BG/";
        string in_name = "storygs" + ((int)(current_title_ + 1)).ToString("D1");
        AssetBundle assetBundle = AssetBundleCtrl.instance.load(in_path, in_name);
        list.AddRange(assetBundle.LoadAllAssets<Sprite>());
        assetBundle = AssetBundleCtrl.instance.load("/menu/title/", "def_title");
        sprite = assetBundle.LoadAsset<Sprite>("def_title");
        string in_path2 = "/GS" + ((int)(current_title_ + 1)).ToString("D1") + "/BG/";
        string in_name2 = "title_textgs" + ((int)(current_title_ + 1)).ToString("D1") + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language);
        assetBundle = AssetBundleCtrl.instance.load(in_path2, in_name2);
        list2.AddRange(assetBundle.LoadAllAssets<Sprite>());
        assetBundle = AssetBundleCtrl.instance.load("/menu/title/", "title_g");
        focus_L_.sprite = assetBundle.LoadAsset<Sprite>("title_g");
        focus_R_.sprite = assetBundle.LoadAsset<Sprite>("title_g");
        assetBundle = AssetBundleCtrl.instance.load("/menu/title/", "title_g02");
        story_focus_L_.sprite = assetBundle.LoadAsset<Sprite>("title_g02");
        story_focus_R_.sprite = assetBundle.LoadAsset<Sprite>("title_g02");
        assetBundle = AssetBundleCtrl.instance.load("/menu/common/", "mask");
        story_mask_.sprite = assetBundle.LoadAsset<Sprite>("mask");
        assetBundle = AssetBundleCtrl.instance.load("/menu/title/", "title_select_bg");
        back_ground_.sprite = assetBundle.LoadAsset<Sprite>("title_select_bg");
        float num = focus_scale_curve_.Evaluate(0f);
        Vector3 localScale = new Vector3(num, num, 1f);
        for (int i = 0; i < story_list_.Count; i++)
        {
            if (i >= story_count_[(int)current_title_])
            {
                story_list_[i].picture.gameObject.SetActive(false);
                story_list_[i].name.gameObject.SetActive(false);
                continue;
            }
            story_list_[i].picture.gameObject.SetActive(true);
            story_list_[i].name.gameObject.SetActive(true);
            if (i < list.Count && i < list2.Count && i <= current_story_)
            {
                story_list_[i].picture.sprite = list[i];
                story_list_[i].name.sprite = list2[i];
            }
            else
            {
                story_list_[i].picture.sprite = sprite;
                story_list_[i].name.sprite = null;
            }
            story_list_[i].picture.transform.localPosition = new Vector3((float)i * 730f, 0f, 0f);
            story_list_[i].picture.transform.localScale = localScale;
        }
        float x = (float)story_count_[(int)current_title_] * 730f / story_mask_.size.x;
        float y = (float)systemCtrl.instance.ScreenHeight / story_mask_.size.y;
        story_mask_.transform.localScale = new Vector3(x, y, 1f);
        Vector3 localPosition = story_mask_.transform.localPosition;
        localPosition.x = (float)story_count_[(int)current_title_] * 730f / 2f - story_list_[0].picture.size.x / 2f;
        story_mask_.transform.localPosition = localPosition;
        Vector3 localPosition2 = story_focus_L_.transform.localPosition;
        Vector3 localPosition3 = story_focus_R_.transform.localPosition;
        float num2 = 70f;
        localPosition3.x = (float)story_count_[(int)current_title_] * 730f - num2;
        localPosition2.x = -730f + num2;
        story_focus_L_.transform.localPosition = localPosition2;
        story_focus_R_.transform.localPosition = localPosition3;
    }
}
