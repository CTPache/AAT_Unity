using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class titleSelectPlate : MonoBehaviour
{
    [Serializable]
    public class SelectPlate
    {
        public AssetBundleSprite select_;

        public AssetBundleSprite enable_;

        public Text text_;

        public InputTouch touch_;

        public bool active
        {
            get
            {
                return select_.active;
            }
            set
            {
                select_.active = value;
            }
        }
    }

    [Serializable]
    public class EnterCurve
    {
        public AnimationCurve mask_ = new AnimationCurve();

        public AnimationCurve scale_ = new AnimationCurve();

        public AnimationCurve alpha_ = new AnimationCurve();
    }

    [Serializable]
    public class EnableCurve
    {
        public AnimationCurve select_ = new AnimationCurve();

        public AnimationCurve cursor_ = new AnimationCurve();

        public AnimationCurve alpha_ = new AnimationCurve();

        public AnimationCurve enable_ = new AnimationCurve();
    }

    [Serializable]
    public class DisableCurve
    {
        public AnimationCurve scale_ = new AnimationCurve();

        public AnimationCurve alpha_ = new AnimationCurve();
    }

    [SerializeField]
    public List<SelectPlate> select_list_ = new List<SelectPlate>();

    [SerializeField]
    public AssetBundleSprite cursor_;

    [SerializeField]
    public GameObject body_;

    [SerializeField]
    public EnterCurve enter_curve_ = new EnterCurve();

    [SerializeField]
    public EnableCurve enable_curve_ = new EnableCurve();

    [SerializeField]
    public DisableCurve disable_curve_ = new DisableCurve();

    public float alpha_;

    private int active_cnt_;

    private int cursor_no_;

    private int def_cursor_no_;

    private float two_btn_space_ = 400f;

    private float three_btn_space_ = 400f;

    private IEnumerator enumerator_cursor_;

    private IEnumerator enumerator_enable_;

    private IEnumerator enumerator_disable_;

    private bool is_end_;

    private bool is_cancel_;

    private int type_;

    private bool igunore_input_;

    public bool body_active
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

    public Vector3 body_position
    {
        get
        {
            return body_.transform.localPosition;
        }
        set
        {
            body_.transform.localPosition = value;
        }
    }

    public AssetBundleSprite select_sprite
    {
        get
        {
            return select_list_[cursor_no_].select_;
        }
    }

    public AssetBundleSprite enable_sprite
    {
        get
        {
            return select_list_[cursor_no_].enable_;
        }
    }

    public int cursor_no
    {
        get
        {
            return cursor_no_;
        }
    }

    public bool is_end
    {
        get
        {
            return is_end_;
        }
    }

    public bool is_cancel
    {
        get
        {
            return is_cancel_;
        }
    }

    public int type
    {
        get
        {
            return type_;
        }
        set
        {
            type_ = value;
        }
    }

    public bool igunore_input
    {
        get
        {
            return igunore_input_;
        }
        set
        {
            igunore_input_ = value;
        }
    }

    public bool is_play_animation { get; private set; }

    public void mainTitleInit(string[] btn_msg, string bundle_name, int in_cursor_no)
    {
        int i;
        for (i = 0; i < btn_msg.Length; i++)
        {
            if (i >= select_list_.Count)
            {
                i = select_list_.Count;
                break;
            }
            setText(i, btn_msg[i]);
        }
        Init(i, bundle_name);
        cursor_.spriteNo(0);
        entryCursor(btn_msg.Length, in_cursor_no);
    }

    public void SetTextFontsize(int in_fontsize)
    {
        foreach (SelectPlate item in select_list_)
        {
            item.text_.fontSize = in_fontsize;
        }
    }

    public void SetTextPosition(Vector3 in_position)
    {
        foreach (SelectPlate item in select_list_)
        {
            item.text_.rectTransform.localPosition = in_position;
        }
    }

    private void Init(int l_num, string bundle_name)
    {
        AssetBundleCtrl instance = AssetBundleCtrl.instance;
        AssetBundle assetBundle = instance.load("/menu/common/", bundle_name, false, 0);
        cursor_.sprite_data_.Clear();
        cursor_.sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
        int num = ((l_num <= select_list_.Count) ? l_num : select_list_.Count);
        for (int i = 0; i < num; i++)
        {
            SelectPlate selectPlate = select_list_[i];
            selectPlate.select_.sprite_data_.Clear();
            selectPlate.enable_.sprite_data_.Clear();
            selectPlate.select_.active = true;
            selectPlate.select_.sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
            selectPlate.select_.spriteNo(2);
            selectPlate.select_.active = true;
            selectPlate.enable_.sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
            selectPlate.enable_.spriteNo((!(bundle_name == "select_button")) ? 5 : 3);
            selectPlate.enable_.sprite_renderer_.color = new Color(1f, 1f, 1f, 0f);
            selectPlate.touch_.touch_event = TouchSelect;
            selectPlate.touch_.argument_parameter = i;
            selectPlate.touch_.touch_key_type = KeyType.A;
            selectPlate.touch_.ActiveCollider();
        }
    }

    public void entryCursor(int in_num, int in_cursor)
    {
        active_cnt_ = ((in_num >= select_list_.Count) ? select_list_.Count : in_num);
        def_cursor_no_ = ((in_cursor < active_cnt_) ? in_cursor : 0);
        cursor_no_ = def_cursor_no_;
        float num = 0f;
        float num2 = 0f;
        if (active_cnt_ > 1)
        {
            if (active_cnt_ % 2 == 0)
            {
                num2 = two_btn_space_;
                num += two_btn_space_ / 2f;
            }
            else
            {
                num2 = three_btn_space_;
            }
            num -= num2 * (float)(active_cnt_ / 2);
            for (int i = 0; i < select_list_.Count; i++)
            {
                if (i < active_cnt_)
                {
                    select_list_[i].active = true;
                    select_list_[i].select_.transform.localPosition = new Vector3(num, 0f, 0f);
                    num += num2;
                }
                else
                {
                    select_list_[i].active = false;
                }
            }
        }
        cursor_.transform.localPosition = select_list_[cursor_no_].select_.transform.localPosition;
    }

    public void setText(int index, string text)
    {
        select_list_[index].text_.text = text;
    }

    public void playCursor(int in_type = 0)
    {
        type_ = in_type;
        cursor_no_ = def_cursor_no_;
        stopCursor();
        enumerator_cursor_ = CoroutineCursor();
        coroutineCtrl.instance.Play(enumerator_cursor_);
    }

    public void stopCursor()
    {
        if (enumerator_cursor_ == null)
        {
            return;
        }
        foreach (SelectPlate item in select_list_)
        {
            mainCtrl.instance.removeText(item.text_);
        }
        coroutineCtrl.instance.Stop(enumerator_cursor_);
        enumerator_cursor_ = null;
    }

    public void End()
    {
        if (enumerator_cursor_ != null)
        {
            coroutineCtrl.instance.Stop(enumerator_cursor_);
            enumerator_cursor_ = null;
        }
        if (enumerator_disable_ != null)
        {
            coroutineCtrl.instance.Stop(enumerator_disable_);
            enumerator_disable_ = null;
        }
        if (enumerator_enable_ != null)
        {
            coroutineCtrl.instance.Stop(enumerator_enable_);
            enumerator_enable_ = null;
        }
        foreach (SelectPlate item in select_list_)
        {
            mainCtrl.instance.removeText(item.text_);
        }
        is_end_ = true;
    }

    private void TouchSelect(TouchParameter touch)
    {
        int num = (int)touch.argument_parameter;
        cursor_no_ = num;
        cursor_.transform.localPosition = new Vector3(select_list_[cursor_no_].select_.transform.localPosition.x, 0f, 0f);
    }

    private IEnumerator CoroutineCursor()
    {
        is_end_ = false;
        is_cancel_ = false;
        foreach (SelectPlate item in select_list_)
        {
            mainCtrl.instance.addText(item.text_);
        }
        body_active = true;
        cursor_.transform.localPosition = new Vector3(select_list_[cursor_no_].select_.transform.localPosition.x, 0f, 0f);
        float time = 0f;
        while (true)
        {
            time += 0.1f;
            if (time > 1f)
            {
                break;
            }
            float num = enter_curve_.scale_.Evaluate(time);
            float a = enter_curve_.alpha_.Evaluate(time);
            Color white = Color.white;
            Color color = cursor_.sprite_renderer_.color;
            cursor_.sprite_renderer_.color = new Color(color.r, color.g, color.g, a);
            cursor_.transform.localScale = new Vector3(num, num, 1f);
            foreach (SelectPlate item2 in select_list_)
            {
                color = item2.select_.sprite_renderer_.color;
                item2.select_.sprite_renderer_.color = new Color(color.r, color.g, color.g, a);
                item2.select_.transform.localScale = new Vector3(num, num, 1f);
                color = item2.select_.sprite_renderer_.color;
                item2.text_.color = new Color(color.r, color.g, color.g, a);
            }
            yield return null;
        }
        Color white2 = Color.white;
        Color color2 = cursor_.sprite_renderer_.color;
        cursor_.sprite_renderer_.color = new Color(color2.r, color2.g, color2.g, 1f);
        cursor_.transform.localScale = new Vector3(1f, 1f, 1f);
        foreach (SelectPlate item3 in select_list_)
        {
            color2 = item3.select_.sprite_renderer_.color;
            item3.select_.sprite_renderer_.color = new Color(color2.r, color2.g, color2.g, 1f);
            item3.select_.transform.localScale = new Vector3(1f, 1f, 1f);
            color2 = item3.select_.sprite_renderer_.color;
            item3.text_.color = new Color(color2.r, color2.g, color2.g, 1f);
        }
        setAlpha(1f);
        float key_wait = systemCtrl.instance.key_wait;
        for (int i = 0; i < select_list_.Count; i++)
        {
            select_list_[i].touch_.ActiveCollider();
        }
        while (true)
        {
            if (GSStatic.global_work_.r.no_0 != 8)
            {
                if (key_wait > 0f)
                {
                    key_wait -= 1f;
                }
                else if (!igunore_input_)
                {
                    if (padCtrl.instance.GetKeyDown(KeyType.A))
                    {
                        if (!seriesTitleSelectCtrl.instance.body || !seriesTitleSelectCtrl.instance.is_decide)
                        {
                            InActiveTouch();
                            soundCtrl.instance.PlaySE(43);
                            playEnable();
                        }
                        break;
                    }
                    if (type_ == 1 && padCtrl.instance.GetKeyDown(KeyType.B))
                    {
                        soundCtrl.instance.PlaySE(44);
                        is_cancel_ = true;
                        playEnable();
                        TouchSystem.TouchInActive();
                        break;
                    }
                }
                if (select_list_.Select((SelectPlate data) => data.active).Count() > 1)
                {
                    if (padCtrl.instance.IsNextMove())
                    {
                        if (padCtrl.instance.GetKeyDown(KeyType.Left) || padCtrl.instance.GetKeyDown(KeyType.StickL_Left) || padCtrl.instance.GetWheelMoveUp())
                        {
                            cursor_no_--;
                            soundCtrl.instance.PlaySE(42);
                        }
                        if (padCtrl.instance.GetKeyDown(KeyType.Right) || padCtrl.instance.GetKeyDown(KeyType.StickL_Right) || padCtrl.instance.GetWheelMoveDown())
                        {
                            cursor_no_++;
                            soundCtrl.instance.PlaySE(42);
                        }
                    }
                    padCtrl.instance.WheelMoveValUpdate();
                    cursor_no_ = ((cursor_no_ >= 0) ? cursor_no_ : (active_cnt_ - 1));
                    cursor_no_ = ((cursor_no_ < active_cnt_) ? cursor_no_ : 0);
                    cursor_.transform.localPosition = new Vector3(select_list_[cursor_no_].select_.transform.localPosition.x, 0f, 0f);
                }
            }
            yield return null;
        }
        foreach (SelectPlate item4 in select_list_)
        {
            mainCtrl.instance.removeText(item4.text_);
        }
    }

    public void playDisable()
    {
        stopDisable();
        enumerator_disable_ = CoroutineDisable();
        coroutineCtrl.instance.Play(enumerator_disable_);
    }

    private void stopDisable()
    {
        if (enumerator_disable_ != null)
        {
            coroutineCtrl.instance.Stop(enumerator_disable_);
            enumerator_disable_ = null;
        }
    }

    private IEnumerator CoroutineDisable()
    {
        is_end_ = false;
        float time = 0f;
        bool is_end = false;
        while (true)
        {
            time += 0.1f;
            if (time > 1f)
            {
                time = 1f;
                is_end = true;
            }
            float a = disable_curve_.alpha_.Evaluate(time);
            Color color = new Color(1f, 1f, 1f, a);
            select_sprite.color = color;
            enable_sprite.color = color;
            cursor_.color = color;
            if (is_end)
            {
                break;
            }
            yield return null;
        }
        cursor_.transform.localScale = Vector3.one;
        select_sprite.transform.localScale = Vector3.one;
        body_active = false;
        is_end_ = true;
    }

    public void playEnable()
    {
        stopEnable();
        enumerator_enable_ = CoroutineEnable();
        coroutineCtrl.instance.Play(enumerator_enable_);
    }

    private void stopEnable()
    {
        if (enumerator_enable_ != null)
        {
            coroutineCtrl.instance.Stop(enumerator_enable_);
            enumerator_enable_ = null;
        }
    }

    private IEnumerator CoroutineEnable()
    {
        is_end_ = false;
        float time = 0f;
        bool is_end = false;
        is_play_animation = true;
        while (true)
        {
            time += 0.1f;
            if (time > 1f)
            {
                time = 1f;
                is_end = true;
            }
            setAlpha(enable_curve_.alpha_.Evaluate(time));
            float num = enable_curve_.cursor_.Evaluate(time);
            cursor_.transform.localScale = new Vector3(num, num, 1f);
            float num2 = enable_curve_.select_.Evaluate(time);
            select_sprite.transform.localScale = new Vector3(num2, num2, 1f);
            float a = enable_curve_.enable_.Evaluate(time);
            Color color = enable_sprite.sprite_renderer_.color;
            enable_sprite.sprite_renderer_.color = new Color(color.r, color.g, color.g, a);
            if (is_end)
            {
                break;
            }
            yield return null;
        }
        cursor_.transform.localScale = Vector3.one;
        select_sprite.transform.localScale = Vector3.one;
        body_active = false;
        is_end_ = true;
        is_play_animation = false;
    }

    private void setAlpha(float in_alpha)
    {
        Color white = Color.white;
        foreach (SelectPlate item in select_list_)
        {
            white = item.select_.sprite_renderer_.color;
            item.select_.sprite_renderer_.color = new Color(white.r, white.g, white.g, in_alpha);
            white = item.text_.color;
            item.text_.color = new Color(white.r, white.g, white.g, in_alpha);
        }
        white = cursor_.sprite_renderer_.color;
        cursor_.sprite_renderer_.color = new Color(white.r, white.g, white.g, in_alpha);
    }

    private void InActiveTouch()
    {
        for (int i = 0; i < select_list_.Count; i++)
        {
            select_list_[i].touch_.SetEnableCollider(false);
        }
    }
}
