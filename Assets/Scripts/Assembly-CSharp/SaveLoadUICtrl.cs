using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadUICtrl : MonoBehaviour
{
	public enum SlotType
	{
		SAVE = 0,
		LOAD = 1
	}

	public enum InputType
	{
		KEY = 0,
		WHEEL = 1
	}

	[Serializable]
	public class AnnounceOperation
	{
		public AssetBundleSprite window_;

		public Text operation_text_;
	}

	private float scroll_lengh_;

	private bool is_keyboard_ = true;

	private bool is_drag_;

	private bool is_touch_scroll_bar_;

	private bool is_touch_move_list_;

	private float scroll_acceleration_;

	private Vector3 old_list_drag_position_ = default(Vector3);

	private IEnumerator scroll_acceleration_enumerator_;

	private bool is_manual_scrolling_;

	private int select_num_;

	private bool is_move_;

	private bool is_over_move_;

	private float list_init_pos_y_;

	private int disp_item_cnt_;

	private IEnumerator coroutine_;

	private string[] announce_text_list_;

	[SerializeField]
public GameObject body_;

	[SerializeField]
public SaveSlot[] slot_list_ = new SaveSlot[10];

	[SerializeField]
public GameObject slot_list_body_;

	[SerializeField]
public AssetBundleSprite cursor_;

	[SerializeField]
public AssetBundleSprite bg_;

	[SerializeField]
public AssetBundleSprite bg_fade_;

	[SerializeField]
public float slot_top_;

	[SerializeField]
public float slot_space_;

	[SerializeField]
public AnimationCurve move_curve_ = new AnimationCurve();

	[SerializeField]
public AnimationCurve over_move_curve_ = new AnimationCurve();

	[SerializeField]
public AnnounceOperation announce_;

	[SerializeField]
public SaveConfirmationWindow save_window_;

	[SerializeField]
public ScrollBarCtrl scrl_bar_;

	[SerializeField]
public AssetBundleSprite mask_;

	[SerializeField]
public SpriteMask save_data_mask_;

	[SerializeField]
public SaveKeyGuide key_guide_;

	[SerializeField]
public SavePriorConfirmation prior_cfm_;

	[SerializeField]
public List<InputTouch> slot_touch_list_ = new List<InputTouch>();

	[SerializeField]
public float scroll_move_resistance_ = 1f;

	private const float DRAG_POWER = 10f;

	private InputType input_type_;

	private int last_load_idx = -1;

	private DateTime latest_save_time = DateTime.Parse("1900/01/01 00:00:00");

	private const float CURSOR_POS_OFFSET = 220f;

	public static SaveLoadUICtrl instance { get; private set; }

	public float scroll_list_normalize
	{
		get
		{
			return slot_list_body_.transform.localPosition.y / scroll_lengh_;
		}
	}

	public bool is_keyboard
	{
		get
		{
			return is_keyboard_;
		}
		set
		{
			is_keyboard_ = value;
		}
	}

	public bool is_open { get; private set; }

	public bool is_loaded { get; private set; }

	public bool is_input_wait { get; private set; }

	public SlotType current_type_ { get; private set; }

	public SaveKeyGuide key_guide
	{
		get
		{
			return key_guide_;
		}
	}

	public bool is_touch_scroll_bar
	{
		get
		{
			return is_touch_scroll_bar_;
		}
		set
		{
			is_touch_scroll_bar_ = value;
		}
	}

	public bool is_touch_move_list
	{
		get
		{
			return is_touch_move_list_;
		}
		set
		{
			is_touch_move_list_ = value;
		}
	}

	public void SaveConfirmation()
	{
		coroutineCtrl.instance.Play(CoroutineConfirmation());
	}

	public void ClearSaveOpen()
	{
		if (prior_cfm_.is_save)
		{
			Open(SlotType.SAVE);
		}
	}

	public void Open(SlotType type)
	{
		current_type_ = type;
		Init();
		if (coroutine_ != null)
		{
			coroutineCtrl.instance.Stop(coroutine_);
		}
		coroutine_ = CoroutineSaveLoad();
		key_guide_.guideIconSet(false, guideCtrl.GuideType.SAVE);
		slot_list_body_.SetActive(true);
		coroutineCtrl.instance.Play(coroutine_);
	}

	public void Close()
	{
		mainCtrl.instance.removeText(announce_.operation_text_);
		SaveSlot[] array = slot_list_;
		foreach (SaveSlot saveSlot in array)
		{
			saveSlot.End();
		}
		key_guide_.Close();
		scrl_bar_.BarLengthReset();
		slot_list_body_.transform.localPosition = new Vector3(0f, list_init_pos_y_, slot_list_body_.transform.localPosition.z);
		body_.SetActive(false);
		is_keyboard_ = true;
	}

	public void UpdateSlotList()
	{
		float scroll_bar_normalize = scrl_bar_.scroll_bar_normalize;
		int num = (int)(scroll_lengh_ * scroll_bar_normalize);
		Transform transform = slot_list_body_.transform;
		Vector3 localPosition = transform.localPosition;
		transform.localPosition = new Vector3(localPosition.x, num, localPosition.z);
		float scroll_bar_normalize2 = transform.localPosition.y / scroll_lengh_;
		scrl_bar_.scroll_bar_normalize = scroll_bar_normalize2;
		scroll_bar_normalize = scrl_bar_.scroll_bar_normalize;
	}

	private void UpdateSlotListOfSlotSpace()
	{
		Transform transform = slot_list_body_.transform;
		float y = transform.localPosition.y;
		y -= (float)(int)(y % slot_space_);
		Vector3 localPosition = transform.localPosition;
		transform.localPosition = new Vector3(localPosition.x, y, localPosition.z);
		float scroll_bar_normalize = transform.localPosition.y / scroll_lengh_;
		scrl_bar_.scroll_bar_normalize = scroll_bar_normalize;
		UpdateCursorPosition();
	}

	private void UpdateScrollBarToList()
	{
		UpdateScrollBar();
		UpdateCursorPosition();
	}

	private void UpdateScrollBar()
	{
		float num = scroll_list_normalize;
		int num2 = (int)(scroll_lengh_ * num);
		slot_list_body_.transform.localPosition = new Vector3(0f, num2, 0f);
		scrl_bar_.scroll_bar_normalize = num;
	}

	private IEnumerator CoroutineConfirmation()
	{
		is_input_wait = true;
		yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play(0.5f, false));
		prior_cfm_.OpenConfirmation();
		while (!prior_cfm_.is_end)
		{
			yield return null;
		}
		yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play(0.5f, true));
		is_input_wait = false;
	}

	private void SlotItemInit()
	{
		foreach (var touch in slot_touch_list_.Select((InputTouch item, int index) => new { item, index }))
		{
			touch.item.touch_key_type = KeyType.None;
			touch.item.touch_event = delegate
			{
				if (is_drag_)
				{
					touch.item.touch_key_type = KeyType.None;
				}
				else
				{
					select_num_ = touch.index;
					UpdateCursorPosition();
					if (GSStatic.save_data[select_num_].in_data > 0)
					{
						touch.item.touch_key_type = KeyType.A;
						TouchSystem.TouchInActive();
					}
					else
					{
						touch.item.touch_key_type = KeyType.A;
					}
				}
			};
			touch.item.down_event = delegate
			{
				if (!IsKeyBoard())
				{
					old_list_drag_position_ = TouchUtility.GetTouchPosition();
					is_touch_move_list_ = true;
					is_drag_ = false;
					is_keyboard_ = false;
					StopScrollAcceleration();
				}
			};
			touch.item.drag_event = delegate
			{
				if (!IsKeyBoard() && touch.item.isActiveAndEnabled)
				{
					Vector3 touchPosition = TouchUtility.GetTouchPosition();
					Vector3 vector = old_list_drag_position_ - touchPosition;
					float sqrMagnitude = (old_list_drag_position_ - touchPosition).sqrMagnitude;
					if (sqrMagnitude > 100f && !is_drag_)
					{
						is_drag_ = true;
					}
					Transform transform = slot_list_body_.transform;
					scroll_acceleration_ = 0f - vector.y;
					transform.localPosition += new Vector3(0f, 0f - vector.y, 0f);
					Vector3 localPosition = transform.localPosition;
					if (localPosition.y > scroll_lengh_)
					{
						transform.localPosition = new Vector3(localPosition.x, scroll_lengh_, localPosition.z);
					}
					else if (localPosition.y < 0f)
					{
						transform.localPosition = new Vector3(localPosition.x, 0f, localPosition.z);
					}
					scrl_bar_.scroll_bar_normalize = scroll_list_normalize;
					old_list_drag_position_ = touchPosition;
					UpdateCursorwithScroll();
				}
			};
			touch.item.up_event = delegate
			{
				scroll_acceleration_enumerator_ = ScrollAcceleration();
				coroutineCtrl.instance.Play(scroll_acceleration_enumerator_);
			};
			touch.item.ActiveCollider();
		}
	}

	private void ResetTouchItem()
	{
		foreach (var item in slot_touch_list_.Select((InputTouch item, int index) => new { item, index }))
		{
			item.item.ActiveCollider();
		}
	}

	private IEnumerator ScrollAcceleration()
	{
		Transform t = slot_list_body_.transform;
		while (true)
		{
			if (scroll_acceleration_ > 0f)
			{
				scroll_acceleration_ -= scroll_move_resistance_;
			}
			else if (scroll_acceleration_ < 0f)
			{
				scroll_acceleration_ += scroll_move_resistance_;
			}
			scroll_acceleration_ = ((!(Mathf.Abs(scroll_acceleration_) <= 1f)) ? scroll_acceleration_ : 0f);
			t.localPosition += new Vector3(0f, scroll_acceleration_, 0f);
			Vector3 pos = t.localPosition;
			if (pos.y > scroll_lengh_)
			{
				t.localPosition = new Vector3(pos.x, scroll_lengh_, pos.z);
				scroll_acceleration_ = 0f;
			}
			else if (pos.y < 0f)
			{
				t.localPosition = new Vector3(pos.x, 0f, pos.z);
				scroll_acceleration_ = 0f;
			}
			UpdateCursorwithScroll();
			scrl_bar_.scroll_bar_normalize = scroll_list_normalize;
			if (Mathf.Abs(scroll_acceleration_) > 0f)
			{
				yield return null;
				continue;
			}
			break;
		}
	}

	private void StopScrollAcceleration()
	{
		if (scroll_acceleration_enumerator_ != null)
		{
			coroutineCtrl.instance.Stop(scroll_acceleration_enumerator_);
			scroll_acceleration_enumerator_ = null;
		}
	}

	private void UpdateSelectNum(float normalize)
	{
		select_num_ = (int)((float)(slot_list_.Length - 1) * normalize);
	}

	private void UpdateCursorPosition()
	{
		cursor_.transform.localPosition = new Vector3(cursor_.transform.localPosition.x, slot_list_[select_num_].transform.localPosition.y, cursor_.transform.localPosition.z);
	}

	public void UpdateCursorwithScroll()
	{
		Vector3 localPosition = slot_list_body_.transform.localPosition;
		float num = -80f;
		float num2 = 600f;
		float num3 = Mathf.Abs(cursor_.transform.localPosition.y - 220f);
		float num4 = num3 - localPosition.y;
		if (!is_drag_ && !(0f < scroll_acceleration_) && !is_touch_scroll_bar_)
		{
			return;
		}
		if (num4 < num)
		{
			select_num_++;
			UpdateCursorPosition();
			while (true)
			{
				num3 = Mathf.Abs(cursor_.transform.localPosition.y - 220f);
				num4 = num3 - localPosition.y;
				if (num4 < num)
				{
					select_num_++;
					UpdateCursorPosition();
					continue;
				}
				break;
			}
			soundCtrl.instance.PlaySE(42);
		}
		else
		{
			if (!(num2 < num4))
			{
				return;
			}
			select_num_--;
			UpdateCursorPosition();
			while (true)
			{
				num3 = Mathf.Abs(cursor_.transform.localPosition.y - 220f);
				num4 = num3 - localPosition.y;
				if (num2 < num4)
				{
					select_num_--;
					UpdateCursorPosition();
					continue;
				}
				break;
			}
			soundCtrl.instance.PlaySE(42);
		}
	}

	private void SlotSet()
	{
		int num = 0;
		float num2 = slot_top_;
		SaveSlot[] array = slot_list_;
		foreach (SaveSlot saveSlot in array)
		{
			saveSlot.transform.localPosition = new Vector3(saveSlot.transform.localPosition.x, num2, saveSlot.transform.localPosition.z);
			saveSlot.SlotDataSet(num, current_type_);
			num++;
			num2 -= slot_space_;
		}
	}

	private IEnumerator CoroutineSaveLoad()
	{
		int dir = 1;
		Func<bool> none_touch = delegate
		{
			TouchInfo touch = TouchUtility.GetTouch();
			return touch != TouchInfo.Move && touch != TouchInfo.Begin;
		};
		Action update_just_once = delegate
		{
			if (!is_keyboard_)
			{
				UpdateSlotListOfSlotSpace();
				is_keyboard_ = true;
			}
		};
		yield return null;
		while (true)
		{
			if (padCtrl.instance.GetKeyDown(KeyType.A))
			{
				if (current_type_ == SlotType.SAVE || GSStatic.save_data[select_num_].in_data > 0)
				{
					soundCtrl.instance.PlaySE(43);
					TouchSystem.TouchInActive();
					bool is_confirmation = ((GSStatic.save_data[select_num_].in_data > 0) ? true : false);
					save_window_.OpenWindow(select_num_, is_confirmation);
					if (save_window_.is_open)
					{
						mask_.active = true;
					}
				}
				else
				{
					soundCtrl.instance.PlaySE(53);
				}
			}
			else
			{
				if (padCtrl.instance.GetKeyDown(KeyType.B))
				{
					soundCtrl.instance.PlaySE(44);
					break;
				}
				if (IsUpperInput() || padCtrl.instance.GetWheelMoveUp())
				{
					if (padCtrl.instance.GetWheelMoveUp())
					{
						input_type_ = InputType.WHEEL;
					}
					else
					{
						input_type_ = InputType.KEY;
					}
					if (none_touch() && !is_manual_scrolling_)
					{
						update_just_once();
						if (select_num_ - 1 >= 0)
						{
							soundCtrl.instance.PlaySE(42);
							select_num_--;
							is_move_ = true;
							dir = -1;
						}
						else
						{
							select_num_ = slot_list_.Length - 1;
							is_over_move_ = true;
						}
					}
				}
				else if (IsLowerInput() || padCtrl.instance.GetWheelMoveDown())
				{
					if (padCtrl.instance.GetWheelMoveDown())
					{
						input_type_ = InputType.WHEEL;
					}
					else
					{
						input_type_ = InputType.KEY;
					}
					if (none_touch() && !is_manual_scrolling_)
					{
						update_just_once();
						if (select_num_ + 1 < slot_list_.Length)
						{
							soundCtrl.instance.PlaySE(42);
							select_num_++;
							is_move_ = true;
							dir = 1;
						}
						else
						{
							select_num_ = 0;
							is_over_move_ = true;
						}
					}
				}
				else if (is_manual_scrolling_ && !IsKeyBoard())
				{
					is_manual_scrolling_ = false;
				}
			}
			if (is_move_)
			{
				StopScrollAcceleration();
				while (true)
				{
					StopScrollAcceleration();
					yield return coroutineCtrl.instance.Play(SlotMove(dir));
					UpdateScrollBar();
					float timer = 0f;
					float wait_time2 = 0.2f;
					wait_time2 = ((input_type_ != InputType.WHEEL) ? 0.2f : 0.05f);
					while (timer < wait_time2)
					{
						timer += Time.deltaTime;
						yield return null;
					}
					if (!none_touch())
					{
						break;
					}
					if (IsUpperInput())
					{
						input_type_ = InputType.KEY;
						if (select_num_ <= 0)
						{
							break;
						}
						dir = -1;
						StopScrollAcceleration();
						soundCtrl.instance.PlaySE(42);
						select_num_--;
						is_manual_scrolling_ = true;
					}
					else
					{
						if (!IsLowerInput())
						{
							UpdateScrollBarToList();
							break;
						}
						input_type_ = InputType.KEY;
						if (select_num_ >= slot_list_.Length - 1)
						{
							break;
						}
						dir = 1;
						StopScrollAcceleration();
						soundCtrl.instance.PlaySE(42);
						select_num_++;
						is_manual_scrolling_ = true;
					}
					UpdateScrollBarToList();
				}
				UpdateScrollBarToList();
				is_move_ = false;
			}
			else if (is_over_move_)
			{
				StopScrollAcceleration();
				yield return coroutineCtrl.instance.Play(over_move(select_num_ == 0));
				UpdateCursorPosition();
				is_over_move_ = false;
				yield return null;
			}
			if (save_window_.is_open)
			{
				while (save_window_.is_open)
				{
					yield return null;
				}
				if (!save_window_.is_loaded)
				{
					mask_.active = false;
				}
				scrl_bar_.ResetBarCollider();
				ResetTouchItem();
				key_guide_.ActiveTouch();
				slot_list_[select_num_].SlotDataSet(select_num_, current_type_);
				if (save_window_.is_loaded)
				{
					is_loaded = true;
					break;
				}
			}
			yield return null;
		}
		if (save_window_.is_loaded)
		{
			save_window_.LoadingPlay();
			SaveControl.LoadGameDataRequest(select_num_);
			while (!SaveControl.is_load_)
			{
				yield return null;
			}
			last_load_idx = select_num_;
			if (SaveControl.is_load_error)
			{
				save_window_.LoadingStop();
				save_window_.End();
				mask_.active = false;
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, 30u, 16u);
				while (!fadeCtrl.instance.is_end)
				{
					yield return null;
				}
				GSMain.End();
				if (instance.is_open)
				{
					instance.Close();
				}
				if (optionCtrl.instance.is_open)
				{
					optionCtrl.instance.OptionCoroutineStop();
					yield return null;
				}
				messageBoxCtrl.instance.init();
				messageBoxCtrl.instance.SetWindowSize(new Vector2(1200f, 360f));
				messageBoxCtrl.instance.SetText(TextDataCtrl.GetTexts(TextDataCtrl.SaveTextID.LOAD_ERROR));
				messageBoxCtrl.instance.SetTextPosCenter();
				messageBoxCtrl.instance.OpenWindowSelect();
				while (!messageBoxCtrl.instance.select_end)
				{
					yield return null;
				}
				messageBoxCtrl.instance.CloseWindowSelect();
				if (messageBoxCtrl.instance.select_cursor_no == 0)
				{
					loadingCtrl.instance.init();
					loadingCtrl.instance.play(loadingCtrl.Type.DELETING);
					SaveControl.DeleteSaveData();
					GSStatic.init();
					TrophyCtrl.reset();
					yield return coroutineCtrl.instance.Play(loadingCtrl.instance.wait(1f));
					loadingCtrl.instance.stop();
					GSStatic.global_work_.system_language = SteamCtrl.ConvertLanguage();
					loadingCtrl.instance.play(loadingCtrl.Type.SAVEING);
					SaveControl.SaveCreateSystemDataRequest();
					while (!SaveControl.is_save_)
					{
						yield return null;
					}
					yield return coroutineCtrl.instance.Play(loadingCtrl.instance.wait(1f));
					loadingCtrl.instance.stop();
					if (SaveControl.is_save_error)
					{
						messageBoxCtrl.instance.init();
						messageBoxCtrl.instance.SetWindowSize(new Vector2(1200f, 360f));
						messageBoxCtrl.instance.SetText(TextDataCtrl.GetTexts(TextDataCtrl.SaveTextID.CREATE_ERROR));
						messageBoxCtrl.instance.SetTextPosCenter();
						messageBoxCtrl.instance.OpenWindow();
						while (messageBoxCtrl.instance.active)
						{
							yield return null;
							if (padCtrl.instance.GetKeyDown(KeyType.A))
							{
								messageBoxCtrl.instance.CloseWindow();
							}
						}
					}
				}
				else
				{
					GSStatic.init();
					TrophyCtrl.reset();
					GSStatic.global_work_.system_language = SteamCtrl.ConvertLanguage();
				}
				titleCtrlRoot.instance.End();
				titleCtrlRoot.instance.active = true;
				titleCtrlRoot.instance.Scene(titleCtrlRoot.SceneType.Start);
				yield break;
			}
			if (GSStatic.global_work_.r.no_0 == 17)
			{
				optionCtrl.instance.OptionCoroutineStop();
				GSMain.End();
			}
			if (!SaveControl.LoadGameData(GSUtility.GetLanguageSlotNum(select_num_, GSStatic.global_work_.language)))
			{
				Debug.LogWarning("data loading failed");
			}
			yield return coroutineCtrl.instance.Play(save_window_.LoadingWait());
			optionCtrl.instance.OptionSet();
			save_window_.LoadingStop();
			save_window_.End();
			mask_.active = false;
			SceneLoad();
		}
		Close();
		is_open = false;
	}

	private IEnumerator over_move(bool in_time)
	{
		soundCtrl.instance.PlaySE(42);
		float last_time = over_move_curve_.keys[over_move_curve_.length - 1].time;
		float timer_ = ((!in_time) ? 0f : last_time);
		Vector3 pos = slot_list_body_.transform.localPosition;
		while (true)
		{
			timer_ += ((!in_time) ? Time.deltaTime : (0f - Time.deltaTime));
			float curve_normal = over_move_curve_.Evaluate(timer_);
			slot_list_body_.transform.localPosition = new Vector3(pos.x, scroll_lengh_ * curve_normal, pos.z);
			UpdateScrollBarToList();
			if ((!in_time) ? (timer_ < last_time) : (timer_ > 0f))
			{
				yield return null;
				continue;
			}
			break;
		}
	}

	private IEnumerator SlotMove(int in_dir)
	{
		Vector3 list_pos = slot_list_body_.transform.localPosition;
		if (is_touch_move_list_)
		{
			float num = 520f;
			float num2 = Mathf.Abs(cursor_.transform.localPosition.y - 220f);
			float num3 = num2 - list_pos.y;
			if (num < num3 && in_dir == 1)
			{
				list_pos.y += slot_space_;
			}
			is_touch_move_list_ = false;
		}
		int before_list_pos_num = (int)((list_pos.y != 0f) ? (list_pos.y / slot_space_) : 0f);
		if (select_num_ < disp_item_cnt_ && list_pos.y <= list_init_pos_y_)
		{
			UpdateCursorPosition();
			yield break;
		}
		if (select_num_ > slot_list_.Length - disp_item_cnt_ && list_pos.y >= slot_space_ * (float)(slot_list_.Length - disp_item_cnt_))
		{
			UpdateCursorPosition();
			yield break;
		}
		if (0 <= select_num_ - before_list_pos_num && select_num_ - before_list_pos_num <= 2)
		{
			UpdateCursorPosition();
			yield break;
		}
		float move_pos = slot_space_ * (float)in_dir;
		float time = 0f;
		float add_val = 0.1f;
		cursor_.active = false;
		if (input_type_ == InputType.WHEEL)
		{
			add_val = 0.4f;
		}
		while (true)
		{
			time += add_val;
			float pos_y = list_pos.y + move_pos * move_curve_.Evaluate(time);
			if ((float)Mathf.FloorToInt(list_pos.y + move_pos) > scroll_lengh_ || (float)Mathf.FloorToInt(list_pos.y + move_pos) < list_init_pos_y_)
			{
				cursor_.active = true;
				UpdateCursorPosition();
				yield break;
			}
			slot_list_body_.transform.localPosition = new Vector3(list_pos.x, pos_y, list_pos.z);
			if (time >= 1f)
			{
				break;
			}
			yield return null;
		}
		slot_list_body_.transform.localPosition = new Vector3(list_pos.x, list_pos.y + move_pos, list_pos.z);
		cursor_.active = true;
		UpdateCursorPosition();
	}

	private void SceneLoad()
	{
		GlobalWork global_work_ = GSStatic.global_work_;
		if (global_work_.r_bk.no_0 == 5)
		{
			GSMain_TanteiPart.GS3DS_tantei_init();
		}
		else if (global_work_.r_bk.no_0 == 4 || global_work_.r_bk.no_0 == 6 || global_work_.r_bk.no_0 == 7)
		{
			GSMain_SaibanPart.GS3DS_saiban_init();
		}
		soundCtrl.instance.FadeOutBGM(30);
		advCtrl.instance.play((int)GSStatic.global_work_.title, GSStatic.global_work_.story, GSStatic.global_work_.scenario, true);
	}

	private void Load()
	{
		SaveSlot[] array = slot_list_;
		foreach (SaveSlot saveSlot in array)
		{
			saveSlot.Load();
		}
		cursor_.load("/menu/common/", "save_data_window");
		cursor_.spriteNo(0);
		bg_.load("/GS1/BG/", "bg043");
		bg_fade_.load("/GS1/BG/", "bg043_fade", true);
		scrl_bar_.Load();
		announce_.window_.load("/menu/common/", "save_window");
		mask_.load("/menu/common/", "mask");
		mask_.active = false;
		save_data_mask_.sprite = AssetBundleCtrl.instance.load("/menu/common/", "mask").LoadAllAssets<Sprite>()[0];
	}

	private void Init()
	{
		body_.SetActive(true);
		Load();
		announce_text_list_ = new string[2]
		{
			TextDataCtrl.GetText(TextDataCtrl.SaveTextID.SELECT_SLOT),
			TextDataCtrl.GetText(TextDataCtrl.SaveTextID.SELECT_DATA)
		};
		is_move_ = false;
		is_open = true;
		is_loaded = false;
		save_window_.Init(current_type_);
		float y = slot_list_[0].slot.board_.sprite_renderer_.size.y;
		disp_item_cnt_ = (int)((540f + slot_top_ + y / 2f) / slot_space_);
		scrl_bar_.BarLengthSet(slot_list_.Length, disp_item_cnt_);
		announce_.operation_text_.text = announce_text_list_[(int)current_type_];
		mainCtrl.instance.addText(announce_.operation_text_);
		scroll_lengh_ = Mathf.Abs((float)disp_item_cnt_ * slot_space_ - (float)slot_list_.Length * slot_space_);
		SlotSet();
		key_guide_.init();
		SlotItemInit();
		if (CheckLastSaveTime())
		{
			last_load_idx = -1;
		}
		int num = (select_num_ = ((last_load_idx >= 0) ? last_load_idx : GetLastSaveSlotIndex()));
		float num2 = 0f;
		num2 = ((select_num_ <= 2) ? 0f : ((select_num_ < slot_list_.Length - 2) ? Mathf.Abs((float)(select_num_ + 1) * slot_space_ - (float)disp_item_cnt_ * slot_space_) : scroll_lengh_));
		Vector3 localPosition = slot_list_body_.transform.localPosition;
		slot_list_body_.transform.localPosition = new Vector3(localPosition.x, num2, localPosition.z);
		select_num_ = num;
		UpdateCursorPosition();
		scrl_bar_.scroll_bar_normalize = scroll_list_normalize;
		is_touch_move_list_ = false;
	}

	private int GetLastSaveSlotIndex()
	{
		int result = 0;
		DateTime dateTime = DateTime.Parse("1900/01/01 00:00:00");
		for (int i = 0; i < GSStatic.save_data.Length; i++)
		{
			string time = GSStatic.save_data[i].time;
			if (!string.IsNullOrEmpty(time))
			{
				DateTime dateTime2 = GSUtility.DateTimeParse(time, GSStatic.global_work_.languageFallback);
				if (dateTime.CompareTo(dateTime2) == -1)
				{
					dateTime = dateTime2;
					result = i;
				}
			}
		}
		return result;
	}

	private bool CheckLastSaveTime()
	{
		bool result = false;
		for (int i = 0; i < GSStatic.save_data.Length; i++)
		{
			string time = GSStatic.save_data[i].time;
			if (!string.IsNullOrEmpty(time))
			{
				DateTime value = GSUtility.DateTimeParse(time, GSStatic.global_work_.languageFallback);
				if (latest_save_time.CompareTo(value) == -1)
				{
					latest_save_time = value;
					result = true;
				}
			}
		}
		return result;
	}

	private bool IsKeyBoard()
	{
		return IsUpperInput() || IsLowerInput();
	}

	private bool IsUpperInput()
	{
		return padCtrl.instance.GetKey(KeyType.Up) || padCtrl.instance.GetKey(KeyType.StickL_Up);
	}

	private bool IsLowerInput()
	{
		return padCtrl.instance.GetKey(KeyType.Down) || padCtrl.instance.GetKey(KeyType.StickL_Down);
	}

	private void Awake()
	{
		instance = this;
	}
}
