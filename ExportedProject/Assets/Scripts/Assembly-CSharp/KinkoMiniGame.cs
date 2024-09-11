using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinkoMiniGame : MonoBehaviour
{
	private enum KinkoState
	{
		Wait = 0,
		StartAnim = 1,
		Input = 2,
		CheckAnim = 3,
		CorrectAnim = 4,
		ResultAnim = 5,
		OpenAnim = 6,
		EndFadeOut = 7,
		EndFadeIn = 8
	}

	private KinkoState state_;

	private uint correct_next_message_ = scenario.SC4_67640;

	private uint not_correct_next_message_ = scenario.SC4_67630;

	private int[] correct_pass = new int[7] { 7, 7, 7, 7, 7, 7, 7 };

	private int[] lock_pass = new int[7] { -1, -1, -1, -1, -1, -1, -1 };

	private int current_number_;

	private bool is_equal_ = true;

	private int[,] button_layout_ = new int[4, 3]
	{
		{ 1, 2, 3 },
		{ 4, 5, 6 },
		{ 7, 8, 9 },
		{ 0, 10, 10 }
	};

	private int cursor_x;

	private int cursor_y;

	private padCtrl pad;

	private AssetBundleCtrl asset_bundle_ctrl_;

	private int time_;

	public List<Sprite> cursor_sprite_data_ = new List<Sprite>();

	public List<Sprite> active_cursor_sprite_data_ = new List<Sprite>();

	public List<Sprite> number_sprite_data_ = new List<Sprite>();

	private bool is_answer_ = true;

	private bool is_end_ = true;

	private uint next_message_ = 133u;

	[SerializeField]
	private Transform body_;

	[SerializeField]
	private List<SpriteRenderer> panel_list_;

	[SerializeField]
	private List<SpriteRenderer> button_list_;

	[SerializeField]
	private AssetBundleSprite label_text_;

	[SerializeField]
	private AssetBundleSprite bg_sprite_;

	[SerializeField]
	private AssetBundleSprite cursor_sprite_;

	[SerializeField]
	private AnimationSprite check_cursor_sprite_;

	[SerializeField]
	private AssetBundleSprite comp_cur_down_;

	[SerializeField]
	private AssetBundleSprite comp_cur_up_;

	[SerializeField]
	private GameObject open_anum_;

	[SerializeField]
	private Evaluator open_evaluator_;

	[SerializeField]
	private AssetBundleSprite result_sprite_;

	[SerializeField]
	private AssetBundleSprite open_bg_sprite_;

	[SerializeField]
	private AssetBundleSprite opne_handle_sprite_;

	[SerializeField]
	private AssetBundleSprite opne_lamp_sprite_;

	[SerializeField]
	private List<InputTouch> kinko_number_touch_list_ = new List<InputTouch>();

	[SerializeField]
	private InputTouch kinko_back_touch_;

	[SerializeField]
	private List<AssetBundleSprite> lamp_ = new List<AssetBundleSprite>();

	private int trans_frame;

	public static KinkoMiniGame instance { get; private set; }

	public bool is_end
	{
		get
		{
			return is_end_;
		}
	}

	public uint next_message
	{
		get
		{
			return next_message_;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	public void Init()
	{
		is_end_ = false;
		SubWindow sub_window_ = advCtrl.instance.sub_window_;
		sub_window_.routine_[0].r.Set(5, 0, 0, 0);
		sub_window_.stack_ = 1;
		Routine currentRoutine = sub_window_.GetCurrentRoutine();
		currentRoutine.r.Set(28, 0, 0, 0);
		GSStatic.global_work_.r.Set(5, 10, 0, 0);
		for (int i = 0; i < lock_pass.Length; i++)
		{
			lock_pass[i] = -1;
		}
		for (int j = 0; j < panel_list_.Count; j++)
		{
			panel_list_[j].sprite = null;
		}
		current_number_ = 0;
		open_evaluator_.Initialize();
		open_evaluator_.enabled = false;
		SetInputNumVisible(true);
		SetLampVisible(false);
		trans_frame = 0;
		result_sprite_.obj.SetActive(false);
		label_text_.obj.SetActive(true);
		body_.gameObject.SetActive(true);
		cursor_sprite_.obj.SetActive(false);
		pad = padCtrl.instance;
		LoadAssetBundle();
		time_ = 0;
		state_ = KinkoState.StartAnim;
		cursor_sprite_.transform.localPosition = new Vector2(-221f, 375f);
		int num = 0;
		int num2 = 0;
		foreach (InputTouch item in kinko_number_touch_list_)
		{
			Vector2Int vector2Int = new Vector2Int(num, num2);
			if (num >= 3)
			{
				num = 0;
				num2++;
				vector2Int = new Vector2Int(num, num2);
			}
			item.argument_parameter = vector2Int;
			num++;
			item.touch_key_type = KeyType.A;
			item.touch_event = delegate(TouchParameter p)
			{
				if (!is_answer_)
				{
					Vector2Int vector2Int2 = (Vector2Int)p.argument_parameter;
					cursor_x = vector2Int2.x;
					cursor_y = vector2Int2.y;
				}
			};
			item.ActiveCollider();
		}
		kinko_back_touch_.touch_key_type = KeyType.A;
		kinko_back_touch_.touch_event = delegate
		{
			if (!is_answer_)
			{
				cursor_x = 1;
				cursor_y = 3;
			}
		};
		kinko_back_touch_.ActiveCollider();
	}

	public void LoadAssetBundle()
	{
		asset_bundle_ctrl_ = AssetBundleCtrl.instance;
		AssetBundle assetBundle = null;
		comp_cur_down_.load("/GS1/minigame/", "comp_cur_d");
		comp_cur_up_.load("/GS1/minigame/", "comp_cur_u");
		label_text_.load("/GS1/minigame/", "label0" + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language));
		bg_sprite_.load("/GS1/BG/", "bg122" + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language));
		cursor_sprite_.load("/GS1/minigame/", "cur");
		open_bg_sprite_.load("/GS1/BG/", "bg10c" + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language));
		opne_handle_sprite_.load("/GS1/minigame/", "spr424");
		for (int i = 0; i < 10; i++)
		{
			assetBundle = asset_bundle_ctrl_.load("/GS1/minigame/", "lcd" + i);
			number_sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
		}
		for (int j = 0; j < 6; j++)
		{
			assetBundle = asset_bundle_ctrl_.load("/GS1/minigame/", "comp" + j);
			check_cursor_sprite_.sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
		}
		for (int k = 74; k < 84; k++)
		{
			string in_name = "eff0" + string.Format("{000:x}", k) + "_l";
			assetBundle = asset_bundle_ctrl_.load("/GS1/minigame/", in_name);
			cursor_sprite_data_.Add(assetBundle.LoadAllAssets<Sprite>()[0]);
			active_cursor_sprite_data_.Add(assetBundle.LoadAllAssets<Sprite>()[1]);
		}
		assetBundle = asset_bundle_ctrl_.load("/GS1/minigame/", "eff054_l" + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language));
		cursor_sprite_data_.Add(assetBundle.LoadAllAssets<Sprite>()[0]);
		active_cursor_sprite_data_.Add(assetBundle.LoadAllAssets<Sprite>()[1]);
		for (int l = 1; l < 10; l++)
		{
			button_list_[l].sprite = cursor_sprite_data_[l - 1];
		}
		button_list_[0].sprite = cursor_sprite_data_[9];
		button_list_[10].sprite = cursor_sprite_data_[10];
		lamp_[0].load("/GS1/minigame/", "lamp_g");
		lamp_[1].load("/GS1/minigame/", "lamp_r");
	}

	private void FixedUpdate()
	{
		Process();
	}

	private void Process()
	{
		if (!is_end_)
		{
			switch (state_)
			{
			case KinkoState.StartAnim:
				StartAnimUpdate();
				break;
			case KinkoState.Input:
				CursorUpDate();
				break;
			case KinkoState.CheckAnim:
				CheckAnimUpdate();
				break;
			case KinkoState.CorrectAnim:
				CorrectAnimUpdate();
				break;
			case KinkoState.ResultAnim:
				ResultAnimUpdate();
				break;
			case KinkoState.OpenAnim:
				OpenAnimUpdate();
				break;
			case KinkoState.EndFadeOut:
				EndFadeOutUpdate();
				break;
			case KinkoState.EndFadeIn:
				EndFadeInUpdate();
				break;
			}
		}
	}

	private void StartAnimUpdate()
	{
		time_++;
		if (time_ > 120)
		{
			time_ = 0;
			label_text_.obj.SetActive(false);
			cursor_sprite_.obj.SetActive(true);
			if (messageBoardCtrl.instance != null)
			{
				messageBoardCtrl.instance.InActiveNormalMessageNextTouch();
			}
			state_ = KinkoState.Input;
			is_answer_ = false;
		}
	}

	private void CursorUpDate()
	{
		if (pad.GetKeyDown(KeyType.Up) || pad.GetKeyDown(KeyType.StickL_Up))
		{
			soundCtrl.instance.PlaySE(42);
			cursor_y--;
			if (cursor_y < 0)
			{
				cursor_y = 0;
			}
		}
		if (pad.GetKeyDown(KeyType.Down) || pad.GetKeyDown(KeyType.StickL_Down))
		{
			soundCtrl.instance.PlaySE(42);
			cursor_y++;
			if (cursor_y >= button_layout_.GetLength(0))
			{
				cursor_y = button_layout_.GetLength(0) - 1;
			}
		}
		if (pad.GetKeyDown(KeyType.Right) || pad.GetKeyDown(KeyType.StickL_Right))
		{
			soundCtrl.instance.PlaySE(42);
			if (button_layout_[cursor_y, cursor_x] < 10)
			{
				cursor_x++;
				if (cursor_x >= button_layout_.GetLength(1))
				{
					cursor_x = button_layout_.GetLength(1) - 1;
				}
			}
		}
		if (pad.GetKeyDown(KeyType.Left) || pad.GetKeyDown(KeyType.StickL_Left))
		{
			soundCtrl.instance.PlaySE(42);
			if (button_layout_[cursor_y, cursor_x] < 10)
			{
				cursor_x--;
				if (cursor_x < 0)
				{
					cursor_x = 0;
				}
			}
			else
			{
				cursor_x = 0;
			}
		}
		if (pad.GetKeyDown(KeyType.A))
		{
			SetNumber(button_layout_[cursor_y, cursor_x]);
		}
		for (int i = 0; i < button_list_.Count; i++)
		{
			button_list_[i].gameObject.SetActive(i == button_layout_[cursor_y, cursor_x]);
		}
	}

	private void SetNumber(int num)
	{
		coroutineCtrl.instance.Play(CoroutineEnable(num));
		if (num == 10)
		{
			soundCtrl.instance.PlaySE(432);
			BackNumber();
		}
		else
		{
			soundCtrl.instance.PlaySE(431);
			panel_list_[current_number_].sprite = number_sprite_data_[num];
			lock_pass[current_number_++] = num;
		}
		if (current_number_ >= lock_pass.Length)
		{
			is_answer_ = true;
			CheckPassWord();
		}
		else
		{
			cursor_sprite_.transform.localPosition = panel_list_[current_number_].transform.localPosition;
		}
	}

	private IEnumerator CoroutineEnable(int num)
	{
		int index2 = 0;
		switch (num)
		{
		case 0:
			index2 = 9;
			break;
		case 10:
			index2 = 10;
			break;
		default:
			index2 = num - 1;
			break;
		}
		button_list_[num].sprite = active_cursor_sprite_data_[index2];
		float time = 0f;
		float wait = 0.1f;
		while (true)
		{
			time += Time.deltaTime;
			if (time > wait)
			{
				break;
			}
			yield return null;
		}
		button_list_[num].sprite = cursor_sprite_data_[index2];
	}

	private void BackNumber()
	{
		if (current_number_ > 0)
		{
			lock_pass[--current_number_] = -1;
			panel_list_[current_number_].sprite = null;
		}
	}

	private void CheckPassWord()
	{
		is_equal_ = true;
		for (int i = 0; i < correct_pass.Length; i++)
		{
			if (!correct_pass[i].Equals(lock_pass[i]))
			{
				is_equal_ = false;
				break;
			}
		}
		current_number_ = 0;
		cursor_sprite_.obj.SetActive(false);
		check_cursor_sprite_.transform.localPosition = panel_list_[current_number_].transform.localPosition;
		state_ = KinkoState.CheckAnim;
		trans_frame = 1;
	}

	private void CheckAnimUpdate()
	{
		time_++;
		if (time_ > 20)
		{
			time_ = 0;
			if (current_number_ < 7)
			{
				soundCtrl.instance.PlaySE(433);
			}
			if (current_number_ > 12)
			{
				if (is_equal_)
				{
					next_message_ = correct_next_message_;
					state_ = KinkoState.CorrectAnim;
				}
				else
				{
					next_message_ = not_correct_next_message_;
					state_ = KinkoState.CorrectAnim;
				}
				return;
			}
			if (current_number_ < 7)
			{
				check_cursor_sprite_.gameObject.SetActive(true);
				check_cursor_sprite_.transform.localPosition = panel_list_[current_number_].transform.localPosition;
				for (int i = 0; i < panel_list_.Count; i++)
				{
					panel_list_[i].gameObject.SetActive(i != current_number_);
				}
			}
			current_number_++;
			if (current_number_ == 8)
			{
				check_cursor_sprite_.gameObject.SetActive(false);
				SetInputNumVisible(true);
			}
		}
		if (trans_frame == 0)
		{
			SetInputNumVisible(true);
			SetLamp(0u);
		}
		if (trans_frame == 50)
		{
			if (current_number_ >= 7)
			{
				SetInputNumVisible(false);
			}
			SetLampVisible(false);
		}
		trans_frame++;
		trans_frame %= 60;
	}

	private void CorrectAnimUpdate()
	{
		time_++;
		if (time_ >= 30)
		{
			time_ = 0;
			state_ = KinkoState.ResultAnim;
			if (is_equal_)
			{
				soundCtrl.instance.PlaySE(435);
			}
			else
			{
				soundCtrl.instance.PlaySE(434);
			}
			return;
		}
		if (trans_frame == 0)
		{
			result_sprite_.obj.SetActive(true);
			if (is_equal_)
			{
				result_sprite_.load("/GS1/minigame/", "label1" + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language));
				SetInputNumVisible(false);
				SetLamp(1u);
				opne_lamp_sprite_.obj.SetActive(true);
				opne_lamp_sprite_.load("/GS1/minigame/", "spr401");
			}
			else
			{
				result_sprite_.load("/GS1/minigame/", "label2" + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language));
				SetInputNumVisible(false);
				SetLampVisible(false);
			}
		}
		trans_frame++;
		trans_frame %= 60;
	}

	private void ResultAnimUpdate()
	{
		time_++;
		if (time_ > 90)
		{
			time_ = 0;
			if (is_equal_)
			{
				state_ = KinkoState.OpenAnim;
				open_anum_.SetActive(true);
				open_evaluator_.enabled = true;
			}
			else
			{
				fadeCtrl.instance.play(2u, 1u, 1u, 31u);
				state_ = KinkoState.EndFadeOut;
			}
		}
	}

	private void OpenAnimUpdate()
	{
		time_++;
		if (time_ >= 124)
		{
			time_ = 0;
			state_ = KinkoState.EndFadeOut;
		}
	}

	private void EndFadeOutUpdate()
	{
		if (fadeCtrl.instance.status != 0)
		{
			return;
		}
		open_evaluator_.enabled = false;
		opne_lamp_sprite_.sprite_renderer_.sprite = null;
		opne_lamp_sprite_.end();
		opne_lamp_sprite_.obj.SetActive(false);
		number_sprite_data_.Clear();
		cursor_sprite_data_.Clear();
		active_cursor_sprite_data_.Clear();
		foreach (AssetBundleSprite item in lamp_)
		{
			item.sprite_renderer_.sprite = null;
			item.sprite_renderer_.enabled = false;
			item.end();
		}
		open_anum_.SetActive(false);
		body_.gameObject.SetActive(false);
		bgCtrl.instance.Bg256_set_ex2(116u, 16u);
		GSDemo.CheckBGChange(116u, 0u);
		fadeCtrl.instance.play(1u, 1u, 1u, 31u);
		state_ = KinkoState.EndFadeIn;
	}

	private void EndFadeInUpdate()
	{
		if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
		{
			state_ = KinkoState.Wait;
			is_end_ = true;
			GSStatic.global_work_.r.Set(5, 1, 0, 0);
			advCtrl.instance.sub_window_.stack_--;
			advCtrl.instance.message_system_.SetMessage(next_message_);
		}
	}

	private void SetLamp(uint lampID)
	{
		switch (lampID)
		{
		case 0u:
			lamp_[0].sprite_renderer_.enabled = true;
			lamp_[1].sprite_renderer_.enabled = false;
			break;
		case 1u:
			lamp_[0].sprite_renderer_.enabled = false;
			lamp_[1].sprite_renderer_.enabled = true;
			break;
		default:
			SetLampVisible(false);
			break;
		}
	}

	private void SetLampVisible(bool v)
	{
		foreach (AssetBundleSprite item in lamp_)
		{
			item.sprite_renderer_.enabled = v;
		}
	}

	private void SetInputNumVisible(bool v)
	{
		foreach (SpriteRenderer item in panel_list_)
		{
			item.gameObject.SetActive(v);
		}
	}
}
