using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingMessageMiniGame : MonoBehaviour
{
	private enum SwDiemesProc
	{
		sw_die_mes_none = 0,
		sw_die_mes_init = 1,
		sw_die_mes_main = 2,
		sw_die_mes_thrust = 3,
		sw_die_mes_status = 4,
		sw_die_mes_down_scroll = 5
	}

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private bool force_clear_;

	[SerializeField]
	private GameObject prefab_touch_range_;

	[SerializeField]
	private GameObject touch_points_;

	private List<GameObject> touch_range_list_ = new List<GameObject>();

	private Vector2[] touch_range_JPN_ = new Vector2[15];

	private Vector2[] touch_range_USA_ = new Vector2[12];

	private Vector2[] touch_range_KOREA_ = new Vector2[15];

	private Vector2 cursor_scroll_limit_ = Vector2.zero;

	private ushort DIE_Rno_2;

	private ushort DIE_Rno_3;

	private const int DIE_MESS_LINE_MAX = 136;

	private SwDiemesProc SwDiemesProcState_;

	private List<KeyValuePair<GameObject, AssetBundleSprite>> sprite_list_ = new List<KeyValuePair<GameObject, AssetBundleSprite>>();

	[SerializeField]
	private Canvas main_canvas_;

	public static DyingMessageMiniGame instance { get; private set; }

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

	public Canvas main_canvas
	{
		get
		{
			return main_canvas_;
		}
		set
		{
			main_canvas_ = value;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	public void StartDyingMessage()
	{
		debugLogger.instance.Log("DyingMessage", "StartDyingMessage.");
		_chengeStaete(SwDiemesProc.sw_die_mes_init);
		base.enabled = true;
	}

	public AssetBundleSprite GetAssetBundleSprite(string in_name)
	{
		foreach (KeyValuePair<GameObject, AssetBundleSprite> item in sprite_list_)
		{
			if (item.Key.name == in_name)
			{
				return item.Value;
			}
		}
		Debug.LogWarning("スプライトが見つかりませんでした:" + in_name);
		return null;
	}

	private void FixedUpdate()
	{
		Process();
	}

	private void Process()
	{
		switch (SwDiemesProcState_)
		{
		case SwDiemesProc.sw_die_mes_none:
			DIE_Rno_2 = 0;
			base.enabled = false;
			break;
		case SwDiemesProc.sw_die_mes_init:
			_sw_die_mes_init();
			break;
		case SwDiemesProc.sw_die_mes_main:
			sw_die_mes_main();
			break;
		case SwDiemesProc.sw_die_mes_thrust:
			sw_die_mes_thrust();
			break;
		case SwDiemesProc.sw_die_mes_status:
			sw_die_mes_status();
			break;
		case SwDiemesProc.sw_die_mes_down_scroll:
			sw_die_mes_down_scroll();
			break;
		}
	}

	private void _chengeStaete(SwDiemesProc in_proc_id)
	{
		debugLogger.instance.Log("DyingMessage", string.Concat("_chengeStaete:", SwDiemesProcState_, " > ", in_proc_id));
		SwDiemesProcState_ = in_proc_id;
	}

	private void _sw_die_mes_init()
	{
		body_.SetActive(true);
		if (DIE_Rno_2 == 0)
		{
			coroutineCtrl.instance.Play(init_col());
			DIE_Rno_2++;
		}
	}

	private IEnumerator init_col()
	{
		float time2 = 0f;
		float wait = 1f;
		while (true)
		{
			time2 += Time.deltaTime;
			if (time2 > wait)
			{
				break;
			}
			yield return null;
		}
		messageBoardCtrl.instance.board(false, false);
		DyingMessageUtil.instance.init_die_message();
		float appear_time_ = 1f;
		Vector2 cursor_area_size = DyingMessageUtil.instance.cursor.cursor_area_size;
		soundCtrl.instance.PlaySE(49);
		Vector3 src_position = DyingMessageUtil.instance.cursor.cursor_position;
		Vector3 dest_position = new Vector3(cursor_area_size.x * 0.5f, cursor_area_size.y * 0.5f, 0f);
		for (float time = 0f; time < appear_time_; time += Time.deltaTime)
		{
			DyingMessageUtil.instance.cursor.cursor_position = Vector3.Lerp(src_position, dest_position, time / appear_time_);
			yield return null;
		}
		DyingMessageUtil.instance.cursor.cursor_position = dest_position;
		DyingMessageUtil.instance.CreatePoint();
		_loadAssetBundle(GSStatic.global_work_.language);
		_setSpritePosition();
		SetPointRange();
		main_canvas_.sortingOrder = 1;
		yield return coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.DYING_MESSAGE));
		MiniGameCursor.instance.cursor_exception_limit = Vector2.zero;
		cursor_scroll_limit_ = MiniGameCursor.instance.cursor_exception_limit;
		DIE_Rno_2 = 0;
		_chengeStaete(SwDiemesProc.sw_die_mes_main);
	}

	private void sw_die_mes_main()
	{
		DyingMessageUtil.instance.body_die_message();
		if (padCtrl.instance.GetKeyDown(KeyType.R))
		{
			DIE_Rno_2 = 0;
			debugLogger.instance.Log("DyingMessage", "Push KeyType.R.");
			_chengeStaete(SwDiemesProc.sw_die_mes_status);
		}
		else if (padCtrl.instance.GetKeyDown(KeyType.X))
		{
			DIE_Rno_2 = 0;
			debugLogger.instance.Log("DyingMessage", "Push KeyType.X.");
			_chengeStaete(SwDiemesProc.sw_die_mes_thrust);
		}
	}

	private void sw_die_mes_thrust()
	{
		switch (DIE_Rno_2)
		{
		case 0:
			DyingMessageUtil.instance.cursor.icon_visible = false;
			fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 1u, 8u);
			Balloon.PlayTakeThat();
			DIE_Rno_2++;
			DIE_Rno_3 = 0;
			break;
		case 1:
			if (DIE_Rno_3++ >= 60)
			{
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_OUT, 1u, 1u);
				DIE_Rno_2++;
			}
			break;
		case 2:
			if (!fadeCtrl.instance.is_end)
			{
				break;
			}
			foreach (GameObject item in touch_range_list_)
			{
				Object.Destroy(item);
			}
			touch_range_list_.Clear();
			if (_checkDieMessage(GSStatic.global_work_.language) || force_clear_)
			{
				advCtrl.instance.message_system_.SetMessage(scenario.SC4_68640);
				bgCtrl.instance.SetSprite(4095);
				_chengeStaete(SwDiemesProc.sw_die_mes_none);
			}
			else
			{
				advCtrl.instance.message_system_.SetMessage(scenario.SC4_70370);
				AnimationSystem.Instance.PlayCharacter(0, 2, 8, 8);
				bgCtrl.instance.SetSprite(3);
				fadeCtrl.instance.play(fadeCtrl.Status.FADE_IN, 1u, 1u);
				DIE_Rno_2++;
			}
			DyingMessageUtil.instance.end_die_message();
			main_canvas_.sortingOrder = 0;
			_freeAssetBundle();
			break;
		case 3:
			if (fadeCtrl.instance.is_end)
			{
				_chengeStaete(SwDiemesProc.sw_die_mes_none);
			}
			break;
		}
	}

	private void sw_die_mes_status()
	{
		switch (DIE_Rno_2)
		{
		case 0:
			GSStatic.global_work_.r_bk.CopyFrom(ref GSStatic.global_work_.r);
			GSStatic.global_work_.r.Set(8, 0, 0, 0);
			DyingMessageUtil.instance.cursor.icon_visible = false;
			DIE_Rno_2++;
			break;
		case 1:
			DIE_Rno_2++;
			break;
		case 2:
			if (!advCtrl.instance.sub_window_.IsBusy() && advCtrl.instance.sub_window_.stack_ == 0)
			{
				DIE_Rno_2++;
			}
			break;
		case 3:
			foreach (GameObject item in touch_range_list_)
			{
				item.GetComponent<InputTouch>().ActiveCollider();
			}
			keyGuideCtrl.instance.UpdateActiveIconTouch();
			DyingMessageUtil.instance.cursor.icon_visible = true;
			DyingMessageUtil.instance.cursor.ActiveCursorTouch();
			MiniGameCursor.instance.cursor_exception_limit = cursor_scroll_limit_;
			_chengeStaete(SwDiemesProc.sw_die_mes_main);
			break;
		}
	}

	private void sw_die_mes_down_scroll()
	{
		_chengeStaete(SwDiemesProc.sw_die_mes_main);
	}

	private bool _checkDieMessage(Language in_language)
	{
		switch (in_language)
		{
		case Language.JAPAN:
		case Language.CHINA_S:
		case Language.CHINA_T:
			return DyingMessageUtil.instance.checkDieMessage_jp();
		case Language.KOREA:
			return DyingMessageUtil.instance.checkDieMessage_korea();
		default:
			return DyingMessageUtil.instance.checkDieMessage_us();
		}
	}

	private void _loadAssetBundle(Language in_language)
	{
		List<string> list2;
		switch (in_language)
		{
		case Language.JAPAN:
		case Language.CHINA_S:
		case Language.CHINA_T:
		{
			List<string> list = new List<string>();
			list.Add("eff029");
			list.Add("eff02a");
			list.Add("eff02b");
			list.Add("eff02c");
			list.Add("eff02d");
			list.Add("eff02e");
			list.Add("eff02f");
			list.Add("eff030");
			list.Add("eff031");
			list.Add("eff032");
			list.Add("eff033");
			list.Add("eff034");
			list.Add("eff035");
			list.Add("eff036");
			list.Add("eff037");
			list2 = list;
			break;
		}
		case Language.KOREA:
		{
			List<string> list = new List<string>();
			list.Add("eff029k");
			list.Add("eff02ak");
			list.Add("eff02bk");
			list.Add("eff02ck");
			list.Add("eff02dk");
			list.Add("eff02ek");
			list.Add("eff02fk");
			list.Add("eff030k");
			list.Add("eff031k");
			list.Add("eff032k");
			list.Add("eff033k");
			list.Add("eff034k");
			list.Add("eff035k");
			list.Add("eff036k");
			list.Add("eff037k");
			list2 = list;
			break;
		}
		default:
		{
			List<string> list = new List<string>();
			list.Add("eff03e");
			list.Add("eff03f");
			list.Add("eff040");
			list.Add("eff041");
			list.Add("eff042");
			list.Add("eff043");
			list.Add("eff044");
			list.Add("eff045");
			list.Add("eff046");
			list.Add("eff047");
			list.Add("eff048");
			list.Add("eff049");
			list2 = list;
			break;
		}
		}
		foreach (string item in list2)
		{
			_AddAssetBundle("/GS1/minigame/DieMessage/", item);
		}
	}

	private void _AddAssetBundle(string in_path, string in_name)
	{
		debugLogger.instance.Log("DyingMessage", "load AssetBundle > path[" + in_path + "] name[" + in_name + "]");
		GameObject gameObject = new GameObject();
		gameObject.transform.parent = body_.transform;
		gameObject.name = in_name;
		gameObject.layer = base.gameObject.layer;
		gameObject.transform.localScale = Vector3.one;
		SpriteRenderer sprite_renderer_ = gameObject.AddComponent<SpriteRenderer>();
		AssetBundleSprite assetBundleSprite = new AssetBundleSprite();
		assetBundleSprite.sprite_renderer_ = sprite_renderer_;
		assetBundleSprite.load(in_path, in_name);
		sprite_list_.Add(new KeyValuePair<GameObject, AssetBundleSprite>(gameObject, assetBundleSprite));
		gameObject.SetActive(false);
	}

	private void _freeAssetBundle()
	{
		foreach (KeyValuePair<GameObject, AssetBundleSprite> item in sprite_list_)
		{
			GameObject key = item.Key;
			Object.Destroy(key);
		}
		sprite_list_.Clear();
	}

	private void _setSpritePosition()
	{
		switch (GSStatic.global_work_.language)
		{
		case Language.JAPAN:
		case Language.CHINA_S:
		case Language.CHINA_T:
			sprite_list_[0].Key.transform.localPosition = new Vector3(-28f, 374f, -10f);
			sprite_list_[1].Key.transform.localPosition = new Vector3(-335f, 306f, -10f);
			sprite_list_[2].Key.transform.localPosition = new Vector3(192f, 294f, -10f);
			sprite_list_[3].Key.transform.localPosition = new Vector3(582f, 339f, -10f);
			sprite_list_[4].Key.transform.localPosition = new Vector3(-494f, 169f, -10f);
			sprite_list_[5].Key.transform.localPosition = new Vector3(-61f, -292f, -10f);
			sprite_list_[6].Key.transform.localPosition = new Vector3(-27f, 189f, -10f);
			sprite_list_[7].Key.transform.localPosition = new Vector3(-138f, -62f, -10f);
			sprite_list_[8].Key.transform.localPosition = new Vector3(-35f, -138f, -10f);
			sprite_list_[9].Key.transform.localPosition = new Vector3(-488f, -221f, -10f);
			sprite_list_[10].Key.transform.localPosition = new Vector3(-163f, -221f, -10f);
			sprite_list_[11].Key.transform.localPosition = new Vector3(463f, -217f, -10f);
			sprite_list_[12].Key.transform.localPosition = new Vector3(-279f, 113f, -10f);
			sprite_list_[13].Key.transform.localPosition = new Vector3(545f, -440f, -10f);
			sprite_list_[14].Key.transform.localPosition = new Vector3(-66f, -433f, -10f);
			break;
		case Language.KOREA:
			sprite_list_[0].Key.transform.localPosition = new Vector3(-571f, 324f, -10f);
			sprite_list_[1].Key.transform.localPosition = new Vector3(-565f, -156f, -10f);
			sprite_list_[2].Key.transform.localPosition = new Vector3(-440f, 264f, -10f);
			sprite_list_[3].Key.transform.localPosition = new Vector3(-447f, -66f, -10f);
			sprite_list_[4].Key.transform.localPosition = new Vector3(-270f, 281f, -10f);
			sprite_list_[5].Key.transform.localPosition = new Vector3(-249f, -19f, -10f);
			sprite_list_[6].Key.transform.localPosition = new Vector3(-84f, 100f, -10f);
			sprite_list_[7].Key.transform.localPosition = new Vector3(-87f, -413f, -10f);
			sprite_list_[8].Key.transform.localPosition = new Vector3(89f, 18f, -10f);
			sprite_list_[9].Key.transform.localPosition = new Vector3(79f, -323f, -10f);
			sprite_list_[10].Key.transform.localPosition = new Vector3(249f, -309f, -10f);
			sprite_list_[11].Key.transform.localPosition = new Vector3(397f, 266f, -10f);
			sprite_list_[12].Key.transform.localPosition = new Vector3(385f, 81f, -10f);
			sprite_list_[13].Key.transform.localPosition = new Vector3(390f, -193f, -10f);
			sprite_list_[14].Key.transform.localPosition = new Vector3(395f, -291f, -10f);
			break;
		default:
			sprite_list_[0].Key.transform.localPosition = new Vector3(-386f, 355f, -10f);
			sprite_list_[1].Key.transform.localPosition = new Vector3(-84f, 382f, -10f);
			sprite_list_[2].Key.transform.localPosition = new Vector3(403f, 101f, -10f);
			sprite_list_[3].Key.transform.localPosition = new Vector3(175f, 64f, -10f);
			sprite_list_[4].Key.transform.localPosition = new Vector3(567f, -26f, -10f);
			sprite_list_[5].Key.transform.localPosition = new Vector3(-619f, -259f, -10f);
			sprite_list_[6].Key.transform.localPosition = new Vector3(-249f, -196f, -10f);
			sprite_list_[7].Key.transform.localPosition = new Vector3(407f, -217f, -10f);
			sprite_list_[8].Key.transform.localPosition = new Vector3(614f, -207f, -10f);
			sprite_list_[9].Key.transform.localPosition = new Vector3(179f, -246f, -10f);
			sprite_list_[10].Key.transform.localPosition = new Vector3(274f, -373f, -10f);
			sprite_list_[11].Key.transform.localPosition = new Vector3(660f, -375f, -10f);
			break;
		}
	}

	private void SetPointRange()
	{
		Vector2[] array;
		switch (GSStatic.global_work_.language)
		{
		case Language.JAPAN:
		case Language.CHINA_S:
		case Language.CHINA_T:
			array = touch_range_JPN_;
			break;
		case Language.KOREA:
			array = touch_range_KOREA_;
			break;
		default:
			array = touch_range_USA_;
			break;
		}
		int count = sprite_list_.Count;
		for (int i = 0; i < count; i++)
		{
			touch_range_list_.Add(Object.Instantiate(prefab_touch_range_));
			touch_range_list_[i].transform.SetParent(touch_points_.transform, false);
			SetPointRangeOffset(i);
			InputTouch component = touch_range_list_[i].GetComponent<InputTouch>();
			component.SetColliderSize(array[i]);
			component.touch_key_type = KeyType.A;
		}
	}

	private void SetPointRangeOffset(int _no)
	{
		switch (GSStatic.global_work_.language)
		{
		case Language.JAPAN:
		case Language.CHINA_S:
		case Language.CHINA_T:
		{
			touch_range_list_[_no].transform.localPosition = sprite_list_[_no].Key.transform.localPosition;
			float x2 = touch_range_JPN_[_no].x / 2f;
			float num3 = touch_range_JPN_[_no].y / 2f;
			touch_range_list_[_no].transform.localPosition += new Vector3(x2, 0f - num3, 6f);
			float num4 = sprite_list_[_no].Value.sprite_renderer_.sprite.rect.size.x / 2f;
			float y2 = sprite_list_[_no].Value.sprite_renderer_.sprite.rect.size.y / 2f;
			touch_range_list_[_no].transform.localPosition += new Vector3(0f - num4, y2, 0f);
			float num5 = 0f;
			float num6 = 0f;
			if (_no == 10)
			{
				num5 = 30f;
			}
			if (num5 != 0f || num6 != 0f)
			{
				touch_range_list_[_no].transform.localPosition += new Vector3(num5, num6, 0f);
			}
			break;
		}
		case Language.KOREA:
			touch_range_list_[_no].transform.localPosition = sprite_list_[_no].Key.transform.localPosition;
			break;
		default:
		{
			touch_range_list_[_no].transform.localPosition = sprite_list_[_no].Key.transform.localPosition;
			float x = touch_range_USA_[_no].x / 2f;
			float num = touch_range_USA_[_no].y / 2f;
			touch_range_list_[_no].transform.localPosition += new Vector3(x, 0f - num, 6f);
			float num2 = sprite_list_[_no].Value.sprite_renderer_.sprite.rect.size.x / 2f;
			float y = sprite_list_[_no].Value.sprite_renderer_.sprite.rect.size.y / 2f;
			touch_range_list_[_no].transform.localPosition += new Vector3(0f - num2, y, 0f);
			break;
		}
		}
	}

	public void AddOffsetList(int _count, float _x, float _y)
	{
		float num = 0f;
		float num2 = 0f;
		switch (GSStatic.global_work_.language)
		{
		case Language.JAPAN:
		case Language.CHINA_S:
		case Language.CHINA_T:
			switch (_count)
			{
			case 0:
				num2 = -30f;
				break;
			case 6:
				num = -102f;
				break;
			}
			touch_range_JPN_[_count] = new Vector2(_x + num, _y + num2);
			break;
		case Language.KOREA:
			touch_range_KOREA_[_count] = new Vector2(_x + num, _y + num2);
			break;
		default:
			touch_range_USA_[_count] = new Vector2(_x + num, _y + num2);
			break;
		}
	}
}
