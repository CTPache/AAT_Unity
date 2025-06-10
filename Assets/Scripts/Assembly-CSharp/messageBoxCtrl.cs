using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class messageBoxCtrl : MonoBehaviour
{
    [SerializeField]
public GameObject body_;

    [SerializeField]
public GameObject text_obj_;

    [SerializeField]
public AssetBundleSprite window_;

    [SerializeField]
public AssetBundleSprite mask_;

    [SerializeField]
public Text[] text_list_;

    [SerializeField]
public InputTouch touch_area_;

    [SerializeField]
public textKeyIconCtrl key_icon_;

    [SerializeField]
public Text icon_text_;

    [SerializeField]
public titleSelectPlate select_plate_;

    public static messageBoxCtrl instance { get; private set; }

    public bool select_end
    {
        get
        {
            return select_plate_.is_end;
        }
    }

    public int select_cursor_no
    {
        get
        {
            return select_plate_.cursor_no;
        }
    }

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

    private void Awake()
    {
        instance = this;
    }

    public void init()
    {
        window_.load("/menu/common/", "save_window");
        mask_.load("/menu/common/", "mask");
        Text[] array = text_list_;
        foreach (Text text in array)
        {
            text.text = string.Empty;
        }
        active = false;
        mask_.active = false;
        touch_area_.touch_key_type = KeyType.A;
        touch_area_.ActiveCollider();
        select_plate_.mainTitleInit(new string[2]
        {
            TextDataCtrl.GetText(TextDataCtrl.TitleTextID.YES),
            TextDataCtrl.GetText(TextDataCtrl.TitleTextID.NO)
        }, "select_button", 0);
        select_plate_.body_active = false;
    }

    public void SetWindowSize(Vector2 size)
    {
        window_.sprite_renderer_.size = size;
    }

    public void SetText(string in_text, int text_line = 0)
    {
        text_list_[text_line].text = in_text;
        keyIconSet(text_line);
    }

    public void SetText(string[] in_texts)
    {
        for (int i = 0; i < text_list_.Length; i++)
        {
            if (i < in_texts.Length)
            {
                text_list_[i].text = in_texts[i];
                keyIconSet(i);
            }
            else
            {
                text_list_[i].text = string.Empty;
            }
        }
    }

    public void SetTextPosCenter()
    {
        int num = text_list_.Count((Text data) => data.text == string.Empty);
        float num2 = 0f;
        if (text_list_.Length > 1)
        {
            num2 = Mathf.Abs(text_list_[0].transform.transform.localPosition.y - text_list_[1].transform.transform.localPosition.y) / 2f;
        }
        Vector3 localPosition = text_obj_.transform.localPosition;
        localPosition.y = (0f - num2) * (float)num + window_.transform.localPosition.y;
        SetTextPos(localPosition);
    }

    public void SetTextPos(Vector3 pos)
    {
        text_obj_.transform.localPosition = pos;
    }

    public void OpenWindow()
    {
        active = true;
        mask_.active = true;
        touch_area_.ActiveCollider();
    }

    public void CloseWindow()
    {
        active = false;
        mask_.active = false;
        touch_area_.SetEnableCollider(false);
        key_icon_.keyIconActiveSet(false);
    }

    public void OpenWindowSelect()
    {
        active = true;
        mask_.active = true;
        touch_area_.SetEnableCollider(false);
        select_plate_.playCursor(0);
        Vector3 zero = Vector3.zero;
        zero.y = window_.transform.localPosition.y - window_.sprite_renderer_.size.y / 2f - select_plate_.select_sprite.sprite_renderer_.size.y;
        select_plate_.body_position = zero;
    }

    public void CloseWindowSelect()
    {
        select_plate_.stopCursor();
        select_plate_.body_active = false;
        active = false;
        mask_.active = false;
    }

    public void keyIconSet(int text_line)
    {
        if (text_list_[text_line].text.IndexOf("【】") >= 0)
        {
            string text = text_list_[text_line].text;
            text_list_[text_line].text = text.Replace("【】", "\u3000\u3000\u3000");
            string text2 = text_list_[text_line].text;
            float preferredWidth = text_list_[text_line].preferredWidth;
            icon_text_.text = text.Remove(text.IndexOf("【"));
            float preferredWidth2 = icon_text_.preferredWidth;
            icon_text_.text += "\u3000\u3000\u3000";
            float preferredWidth3 = icon_text_.preferredWidth;
            float x = (preferredWidth2 - preferredWidth / 2f + (preferredWidth3 - preferredWidth / 2f)) / 2f;
            key_icon_.load("option_list_bg_7");
            key_icon_.iconSet(KeyType.A);
            key_icon_.iconPosSet(text_list_[text_line].transform, new Vector3(x, 0f, 0f));
            key_icon_.key_icon[0].icon.transform.localScale = Vector3.one;
        }
    }
}
