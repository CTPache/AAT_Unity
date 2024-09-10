using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evidenceCallbackManager : MonoBehaviour
{
	private delegate void InitFanc();

	private delegate void CheckFanc();

	[SerializeField]
	private AnimationCurve liner_;

	[SerializeField]
	private AnimationCurve curve_;

	private readonly List<InitFanc> init_func_list_ = new List<InitFanc>
	{
		Init_Dummy, Init_itm0460, Init_itm0465, Init_Dummy, Init_Dummy, Init_Dummy, Init_Dummy, Init_Dummy, Init_itm04e0, Init_Dummy,
		Init_Dummy, Init_Dummy, Init_itm0520, Init_Dummy, Init_Dummy, Init_Dummy, Init_Dummy, Init_Dummy, Init_Dummy, Init_Dummy,
		Init_Dummy, Init_Dummy, Init_Dummy, Init_Dummy, Init_Dummy, Init_Dummy, Init_Dummy, Init_itm0820, Init_Dummy, Init_itm0830
	};

	private readonly List<CheckFanc> check_func_list_ = new List<CheckFanc>
	{
		Body_Dummy, OnCheck_itm0460_0, OnCheck_itm0465, OnCheck_itm04e2_0, OnCheck_itm0520_0, OnCheck_itm0460_1, OnCheck_itm0820_0, OnCheck_itm0830_0, OnCheck_itm0060_0, OnCheck_itm0060_1,
		OnCheck_itm0480_0, OnCheck_itm0490_0, OnCheck_itm0490_1, OnCheck_itm04a0_0, OnCheck_itm04b0_0, OnCheck_itm04c0_0, OnCheck_itm04e0_3, OnCheck_itm04e0_1, OnCheck_itm04e0_2, OnCheck_itm0500_0,
		OnCheck_itm0500_1, OnCheck_itm0510_0, OnCheck_itm0520_1, OnCheck_itm0525_0, OnCheck_itm0525_1, OnCheck_itm0540_0, OnCheck_itm0560_0, OnCheck_itm05a0_0, OnCheck_itm05a0_1, OnCheck_itm05c0_0,
		OnCheck_itm05f0_0, OnCheck_itm0600_0, OnCheck_itm0620_0, OnCheck_itm0700_0, OnCheck_itm0700_1, OnCheck_itm0610_0, OnCheck_itm0550_0, OnCheck_itm05a0_2, OnCheck_itm0550_1, OnCheck_itm0730_0
	};

	private List<Transform> anim_trans_ = new List<Transform>();

	private List<GameObject> instance_object_list_ = new List<GameObject>();

	private bool request_reset_posture_;

	private bool request_force_close_;

	private string request_change_model_ = string.Empty;

	private uint request_event_mes_;

	private uint request_scenario_mes_;

	private Vector3 request_posture_pos_ = Vector3.zero;

	private Vector3 request_posture_ang_ = Vector3.zero;

	private float request_posture_scale_ = 1f;

	private const int ITM0460_INDEX = 0;

	private const int ITM0465_CARD_INDEX = 1;

	private const int ITM0465_SAIFU_INDEX = 2;

	private const int ITM04e0_ROT_INDEX = 0;

	private const int ITM0520_INDEX = 0;

	private const int ITM082_0_INDEX = 0;

	private const int ITM082_1_INDEX = 1;

	private const int ITM082_2_INDEX = 2;

	private const int ITM082_3_INDEX = 3;

	private const float BOOK_CENTER_X = -3f;

	private const int ITM083_0_INDEX = 0;

	private const int ITM083_1_INDEX = 1;

	private const int ITM083_2_INDEX = 2;

	private const int ITM083_3_INDEX = 3;

	private const int ITM083_4_INDEX = 4;

	private const int ITM083_5_INDEX = 5;

	private const int ITM083_6_INDEX = 6;

	private const float ITM083_CENTER_X = -3f;

	public static evidenceCallbackManager instance { get; private set; }

	public IEnumerator on_main { get; private set; }

	public int state { get; private set; }

	public bool runnning_mesasge { get; set; }

	public bool is_exist_mesasge
	{
		get
		{
			return request_event_mes_ != 0 || request_scenario_mes_ != 0;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	public void OnInit()
	{
		scienceInvestigationCtrl scienceInvestigationCtrl2 = scienceInvestigationCtrl.instance;
		evidenceObjectManager evidence_manager = scienceInvestigationCtrl2.evidence_manager;
		if (!string.IsNullOrEmpty(request_change_model_))
		{
			evidence_manager.ChangeModel(request_change_model_);
		}
		request_change_model_ = string.Empty;
		if (request_reset_posture_)
		{
			evidence_manager.SetRotate(0f, 0f);
			evidence_manager.SetScaleRatio(1f);
		}
		evidence_manager.SetPosition(0f, 0f);
		request_reset_posture_ = false;
		state = 0;
		CallInitEvent(evidence_manager.GetCurrentObjId());
	}

	private void CallInitEvent(int in_obj_id)
	{
		init_func_list_[in_obj_id]();
	}

	private static void Init_Dummy()
	{
	}

	public void OnCheck()
	{
		scienceInvestigationCtrl scienceInvestigationCtrl2 = scienceInvestigationCtrl.instance;
		evidenceObjectManager evidence_manager = scienceInvestigationCtrl2.evidence_manager;
		on_main = null;
		request_force_close_ = false;
		state = 1;
		CallCheckEvent(evidence_manager.GetCurrentObjId(), scienceInvestigationCtrl2.check_point_index);
		if (instance.request_scenario_mes_ == scenario.SC4_60870)
		{
			GSFlag.Set(0u, scenario.SCE40_FLAG_ST_G_KEITAI_MES, 1u);
		}
		if (instance.request_scenario_mes_ == scenario.SC4_60860)
		{
			GSFlag.Set(0u, scenario.SCE40_FLAG_ST_G_KEITAI_MES2, 1u);
		}
	}

	public uint[] OnCheckCursor()
	{
		scienceInvestigationCtrl scienceInvestigationCtrl2 = scienceInvestigationCtrl.instance;
		evidenceObjectManager evidence_manager = scienceInvestigationCtrl2.evidence_manager;
		CallCheckEvent(evidence_manager.GetCurrentObjId(), scienceInvestigationCtrl2.hit_point_index);
		on_main = null;
		request_force_close_ = false;
		uint num = 0u;
		uint num2 = 0u;
		if (request_scenario_mes_ != 0)
		{
			num = request_scenario_mes_;
		}
		else if (request_event_mes_ != 0)
		{
			num2 = 1u;
			num = request_event_mes_;
		}
		request_reset_posture_ = false;
		request_change_model_ = string.Empty;
		request_event_mes_ = 0u;
		request_scenario_mes_ = 0u;
		request_posture_pos_ = Vector3.zero;
		request_posture_ang_ = Vector3.zero;
		request_posture_scale_ = 1f;
		return new uint[2] { num, num2 };
	}

	private void CallCheckEvent(int in_obj_id, int in_event_table_id)
	{
		int num = 0;
		int[] event_tbl = polyDataCtrl.instance.obj_table[in_obj_id].event_tbl;
		num = ((event_tbl != null && event_tbl.Length > in_event_table_id) ? event_tbl[in_event_table_id] : 0);
		check_func_list_[num]();
	}

	private static void Body_Dummy()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = Vector3.zero;
		instance.request_posture_scale_ = 1f;
		instance.on_main = instance.Event_itmsp();
	}

	private IEnumerator Event_itmsp()
	{
		scienceInvestigationCtrl ctrl = scienceInvestigationCtrl.instance;
		evidenceObjectManager mng = ctrl.evidence_manager;
		yield return StartCoroutine(RequestTo());
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(EventRunScript());
		yield return StartCoroutine(Finish());
	}

	private IEnumerator Event_itmsp_open_board()
	{
		scienceInvestigationCtrl ctrl = scienceInvestigationCtrl.instance;
		evidenceObjectManager mng = ctrl.evidence_manager;
		yield return StartCoroutine(RequestTo());
		yield return new WaitForSeconds(0.5f);
		messageBoardCtrl.instance.board(true, true);
		yield return StartCoroutine(EventRunScript());
		yield return StartCoroutine(Finish());
	}

	private IEnumerator RequestTo(float in_time = 0.5f)
	{
		scienceInvestigationCtrl ctrl = scienceInvestigationCtrl.instance;
		evidenceObjectManager mng = ctrl.evidence_manager;
		request_posture_ang_.x *= -1f;
		request_posture_ang_.y *= -1f;
		yield return mng.RequestTo(request_posture_pos_, request_posture_ang_, request_posture_scale_, in_time);
	}

	private IEnumerator EventRunScript()
	{
		state = 2;
		if (request_scenario_mes_ != 0 || request_event_mes_ != 0)
		{
			if (scienceInvestigationCtrl.instance.IsEnding())
			{
				int fade_time = 90;
				yield return StartCoroutine(fadeCtrl.instance.play_coroutine(fade_time, false, Color.black));
			}
			if (request_scenario_mes_ != 0)
			{
				advCtrl.instance.message_system_.AddScript2(GSStatic.global_work_.scenario, 0u);
				yield return StartCoroutine(EventRunScript(request_scenario_mes_, false));
			}
			else if (request_event_mes_ != 0)
			{
				advCtrl.instance.message_system_.AddScript2(65535u, 0u);
				yield return StartCoroutine(EventRunScript(request_event_mes_, true));
			}
			request_scenario_mes_ = 0u;
			request_event_mes_ = 0u;
		}
	}

	private IEnumerator EventRunScript(uint in_start_no, bool system_mes)
	{
		byte set_scenario_flag = (byte)(system_mes ? 1 : 0);
		MessageSystem.setInspectTalkEndFlg(inspectCtrl.GetNextInspectNumber(in_start_no), set_scenario_flag);
		inspectCtrl.InspectCashReset();
		bool failed = false;
		try
		{
			advCtrl.instance.message_system_.SetMessage(in_start_no);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			failed = true;
		}
		if (failed)
		{
			request_scenario_mes_ = 0u;
			request_event_mes_ = 0u;
			yield break;
		}
		runnning_mesasge = true;
		advCtrl.instance.sub_window_.req_ = SubWindow.Req.NONE;
		yield return new WaitWhile(delegate
		{
			if (advCtrl.instance.sub_window_.req_ == SubWindow.Req.MESS_EXIT)
			{
				advCtrl.instance.sub_window_.req_ = SubWindow.Req.NONE;
				runnning_mesasge = false;
				messageBoardCtrl.instance.board(false, false);
				GSStatic.global_work_.Mess_move_flag = 0;
				return false;
			}
			return true;
		});
		request_scenario_mes_ = 0u;
		request_event_mes_ = 0u;
	}

	private IEnumerator Finish()
	{
		scienceInvestigationCtrl ctrl = scienceInvestigationCtrl.instance;
		state = 3;
		yield return new WaitForSeconds(0.5f);
		request_posture_pos_ = Vector3.zero;
		request_posture_ang_ = Vector3.zero;
		request_posture_scale_ = 1f;
		if (request_force_close_)
		{
			state = 5;
			ctrl.Close();
		}
		else
		{
			bool flag = request_reset_posture_ || !string.IsNullOrEmpty(request_change_model_);
			state = ((!flag) ? 4 : 0);
			ctrl.Resume(flag);
		}
	}

	public void Release()
	{
		anim_trans_.Clear();
		request_reset_posture_ = false;
		request_force_close_ = false;
		request_change_model_ = string.Empty;
		request_event_mes_ = 0u;
		request_scenario_mes_ = 0u;
		request_posture_pos_ = Vector3.zero;
		request_posture_ang_ = Vector3.zero;
		request_posture_scale_ = 1f;
		on_main = null;
		state = -1;
		runnning_mesasge = false;
		foreach (GameObject item in instance_object_list_)
		{
			if (item != null)
			{
				UnityEngine.Object.Destroy(item);
			}
		}
		instance_object_list_.Clear();
	}

	public GameObject CreateDummyObject(Transform parent = null)
	{
		return CreateDummyObject(parent, Vector3.zero, Vector3.zero, Vector3.one);
	}

	public GameObject CreateDummyObject(Transform parent, Vector3 pos, Vector3 ang, Vector3 scale)
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.parent = parent;
		gameObject.transform.localPosition = pos;
		gameObject.transform.localRotation = Quaternion.Euler(ang);
		gameObject.transform.localScale = scale;
		instance_object_list_.Add(gameObject);
		return gameObject;
	}

	private IEnumerator MoveTo(Transform trans, Vector3 in_to_pos, AnimationCurve curve, float in_time)
	{
		float ratio2 = 0f;
		float now = 0f;
		Vector3 last_pos = trans.localPosition;
		if (in_time > 0f)
		{
			while (now <= in_time)
			{
				now += Time.deltaTime;
				ratio2 = now / in_time;
				ratio2 = ((!(((!(ratio2 < 0f)) ? ratio2 : 0f) > 1f)) ? ratio2 : 1f);
				float value = curve.Evaluate(ratio2);
				LerpPosition(trans, last_pos, in_to_pos, value);
				yield return 0;
			}
		}
		LerpPosition(trans, last_pos, in_to_pos, 1f);
		yield return 0;
	}

	private void LerpPosition(Transform trans, Vector3 from, Vector3 to, float in_time)
	{
		float x = Mathf.Lerp(from.x, to.x, in_time);
		float y = Mathf.Lerp(from.y, to.y, in_time);
		float z = Mathf.Lerp(from.z, to.z, in_time);
		trans.localPosition = new Vector3(x, y, z);
	}

	private IEnumerator RotateTo(Transform trans, Vector3 in_angle, AnimationCurve curve, float in_time)
	{
		float ratio3 = 0f;
		float now = 0f;
		Vector3 last_euler = trans.localRotation.eulerAngles;
		if (in_time > 0f)
		{
			while (now <= in_time)
			{
				now += Time.deltaTime;
				ratio3 = now / in_time;
				ratio3 = ((!(((!(ratio3 < 0f)) ? ratio3 : 0f) > 1f)) ? ratio3 : 1f);
				float value = curve.Evaluate(ratio3);
				LerpRotate(trans, last_euler, in_angle, value);
				yield return 0;
			}
		}
		LerpRotate(trans, last_euler, in_angle, 1f);
		yield return 0;
	}

	private void LerpRotate(Transform trans, Vector3 from, Vector3 to, float in_time)
	{
		trans.localRotation = Quaternion.Slerp(Quaternion.Euler(from), Quaternion.Euler(to), in_time);
	}

	private IEnumerator ScaleTo(Transform trans, float in_to_scale, AnimationCurve curve, float in_time)
	{
		float ratio3 = 0f;
		float now = 0f;
		float last_scale = trans.localScale.x;
		if (in_time > 0f)
		{
			while (now <= in_time)
			{
				now += Time.deltaTime;
				ratio3 = now / in_time;
				ratio3 = ((!(((!(ratio3 < 0f)) ? ratio3 : 0f) > 1f)) ? ratio3 : 1f);
				float value = curve.Evaluate(ratio3);
				LerpScale(trans, last_scale, in_to_scale, value);
				yield return 0;
			}
		}
		LerpScale(trans, last_scale, in_to_scale, 1f);
		yield return 0;
	}

	private void LerpScale(Transform trans, float from, float to, float in_time)
	{
		float num = Mathf.Lerp(from, to, in_time);
		trans.localScale = new Vector3(num, num, num);
	}

	private IEnumerator BezierMoveTo(Transform trans, Vector3 in_to_pos, Vector3 ctrlPt, AnimationCurve curve, float in_time)
	{
		float ratio2 = 0f;
		float now = 0f;
		Vector3 last_pos = trans.localPosition;
		if (in_time > 0f)
		{
			while (now <= in_time)
			{
				now += Time.deltaTime;
				ratio2 = now / in_time;
				ratio2 = ((!(((!(ratio2 < 0f)) ? ratio2 : 0f) > 1f)) ? ratio2 : 1f);
				float value = curve.Evaluate(ratio2);
				trans.localPosition = BezierCurve(last_pos, in_to_pos, ctrlPt, value);
				yield return 0;
			}
		}
		trans.localPosition = BezierCurve(last_pos, in_to_pos, ctrlPt, 1f);
		yield return 0;
	}

	private Vector3 BezierCurve(Vector3 pt1, Vector3 pt2, Vector3 ctrlPt, float t)
	{
		if (t > 1f)
		{
			t = 1f;
		}
		Vector3 result = default(Vector3);
		float num = 1f - t;
		result.x = num * num * pt1.x + 2f * num * t * ctrlPt.x + t * t * pt2.x;
		result.y = num * num * pt1.y + 2f * num * t * ctrlPt.y + t * t * pt2.y;
		result.z = num * num * pt1.z + 2f * num * t * ctrlPt.z + t * t * pt2.z;
		return result;
	}

	private static void OnCheck_itm0060_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(28f, 232f, 41f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70000J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0060_1()
	{
		OnCheck_itm0060_0();
	}

	private static void OnCheck_itm0480_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(18f, 20f, 6f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70010J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0490_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(32f, 141f, 249f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70020J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0490_1()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(32f, 141f, 249f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70020J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm04a0_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(20f, 0f, 0f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70190J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm04b0_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(270f, 0f, 0f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70030J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm04c0_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(7f, 354f, 350f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70060;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm04e0_3()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(0f, 0f, 0f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70040J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm04e0_1()
	{
		bool flag = GSStatic.global_work_.title == TitleId.GS1 && (uint)GSStatic.global_work_.scenario < 19u;
		bool flag2 = GSFlag.Check(0u, scenario.SCE40_FLAG_ST_G_KEITAI_MES);
		instance.on_main = instance.Event_itmsp();
		instance.request_reset_posture_ = true;
		instance.request_force_close_ = !flag2 && flag;
		instance.request_posture_pos_ = new Vector2(1f, 2f);
		instance.request_posture_ang_ = new Vector3(13f, 32f, 29f);
		instance.request_posture_scale_ = 2f;
		if (!flag2 && flag)
		{
			instance.request_scenario_mes_ = scenario.SC4_60870;
		}
		else
		{
			instance.request_event_mes_ = scenario.EV0_70050J;
		}
	}

	private static void OnCheck_itm04e0_2()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(0f, 0f, 0f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70040J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0500_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(311f, 287f, 332f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70070J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0500_1()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(311f, 190f, 184f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70080J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0510_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(0f, 190f, 0f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70000J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0540_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(295f, 270f, 90f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70120J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0560_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(27f, 328f, 302f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70130J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm05a0_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(270f, 0f, 0f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70150J;
		instance.on_main = instance.Event_itmsp_open_board();
	}

	private static void OnCheck_itm05a0_1()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(0f, 0f, 0f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70160J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm05a0_2()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(0f, 0f, 0f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70140J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm05c0_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(6f, 340f, 317f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70180J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm05f0_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(320f, 25f, 4f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70170J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0600_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(47f, 30f, 31f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70210;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0620_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(48f, 40f, 50f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70200J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0700_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(320f, 285f, 255f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70000J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0700_1()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(40f, 252f, 72f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70000J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0610_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(18f, 20f, 6f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70220;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0550_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(329f, 142f, 229f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70240;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0550_1()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(353f, 329f, 326f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70230;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0730_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(351f, 218f, 110f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70250J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void Init_itm0460()
	{
		if (instance.anim_trans_.Count <= 2)
		{
			scienceInvestigationCtrl scienceInvestigationCtrl2 = scienceInvestigationCtrl.instance;
			evidenceObjectManager evidence_manager = scienceInvestigationCtrl2.evidence_manager;
			string in_prefab_name = ((GSStatic.global_work_.language != 0) ? "itm0463u" : "itm0463");
			GameObject @object = evidence_manager.GetObject(in_prefab_name);
			StModelSet stModelSet = evidence_manager.AddLoadEventModel(31);
			stModelSet.active = true;
			GameObject gameObject = instance.CreateDummyObject(@object.transform.parent);
			GameObject gameObject2 = instance.CreateDummyObject(gameObject.transform);
			gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
			gameObject2.transform.localRotation = Quaternion.Euler(30f, 41f, 19f);
			gameObject2.transform.localScale = Vector3.one;
			instance.anim_trans_.Add(@object.transform);
			@object.transform.parent = gameObject2.transform;
			@object.transform.localPosition = Vector3.zero;
			@object.transform.localRotation = Quaternion.identity;
			@object.transform.localScale = Vector3.one;
			stModelSet.transform.parent = gameObject2.transform;
			stModelSet.transform.localPosition = Vector3.zero;
			stModelSet.transform.localRotation = Quaternion.identity;
			stModelSet.transform.localScale = Vector3.one;
			GameObject gameObject3 = instance.CreateDummyObject(@object.transform);
			gameObject3.transform.localPosition = new Vector3(0f, 0f, 1f);
			gameObject3.transform.localRotation = Quaternion.Euler(90f, 0f, 90f);
			gameObject3.transform.localScale = Vector3.one;
			GameObject gameObject4 = instance.CreateDummyObject(gameObject3.transform);
			gameObject4.transform.localPosition = Vector3.zero;
			gameObject4.transform.localRotation = Quaternion.identity;
			gameObject4.transform.localScale = Vector3.one;
			Transform transform = @object.transform.Find("polySurface9");
			transform.parent = gameObject4.transform;
			instance.anim_trans_.Add(gameObject4.transform);
			instance.anim_trans_.Add(gameObject.transform);
		}
	}

	private static void Init_itm0460_Parent(evidenceObjectManager mng)
	{
	}

	private static void Init_itm0465()
	{
		if (instance.anim_trans_.Count <= 1)
		{
		}
	}

	private static void OnCheck_itm0460_0()
	{
		instance.request_change_model_ = polyDataCtrl.instance.GetPolyData(2).prefab_name;
		instance.request_reset_posture_ = true;
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = Vector3.zero;
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70000J;
		instance.on_main = instance.Event_0460_0_Open();
	}

	private static void OnCheck_itm0465()
	{
		instance.request_reset_posture_ = true;
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = Vector3.zero;
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70000J;
		instance.on_main = instance.Event_0460_0_Open();
	}

	private static void OnCheck_itm0460_1()
	{
		instance.request_force_close_ = true;
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(-26.75f, 49f, -251f);
		instance.request_posture_scale_ = 1f;
		instance.request_scenario_mes_ = scenario.SC4_60263;
		instance.on_main = instance.Event_0460_1_OpenAndTakeOut();
	}

	private IEnumerator Event_0460_0_Open()
	{
		scienceInvestigationCtrl ctrl = scienceInvestigationCtrl.instance;
		evidenceObjectManager mng = ctrl.evidence_manager;
		yield return StartCoroutine(RequestTo());
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(EventRunScript());
		yield return StartCoroutine(Finish());
	}

	private IEnumerator Event_itm0465_TakeOut()
	{
		scienceInvestigationCtrl ctrl = scienceInvestigationCtrl.instance;
		evidenceObjectManager mng = ctrl.evidence_manager;
		yield return StartCoroutine(RequestTo());
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(MoveTo(anim_trans_[1], new Vector3(0f, -10f, 0f), liner_, 0.5f));
		yield return StartCoroutine(EventRunScript());
		yield return StartCoroutine(Finish());
	}

	private IEnumerator Event_0460_1_OpenAndTakeOut()
	{
		scienceInvestigationCtrl ctrl = scienceInvestigationCtrl.instance;
		evidenceObjectManager mng = ctrl.evidence_manager;
		yield return StartCoroutine(RequestTo());
		yield return new WaitForSeconds(0.5f);
		soundCtrl.instance.PlaySE(421);
		yield return StartCoroutine(RotateTo(anim_trans_[0], new Vector3(181f, 0f, 0f), liner_, 0.3f));
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(MoveTo(in_to_pos: anim_trans_[2].InverseTransformVector(new Vector3(0f, -6f, 0f)), trans: anim_trans_[2], curve: liner_, in_time: 0.3f));
		yield return StartCoroutine(MoveTo(anim_trans_[1], new Vector3(0f, 4f, 0f), curve_, 0.3f));
		StartCoroutine(ScaleTo(anim_trans_[1], 2f, liner_, 0.3f));
		yield return StartCoroutine(MoveTo(in_to_pos: new Vector3(-1f, 3.5f, 0f), trans: anim_trans_[1], curve: liner_, in_time: 0.3f));
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(EventRunScript());
		yield return StartCoroutine(Finish());
	}

	private static void Init_itm04e0()
	{
		if (instance.anim_trans_.Count <= 0)
		{
			scienceInvestigationCtrl scienceInvestigationCtrl2 = scienceInvestigationCtrl.instance;
			evidenceObjectManager evidence_manager = scienceInvestigationCtrl2.evidence_manager;
			GameObject @object = evidence_manager.GetObject("itm04e02");
			GameObject gameObject = instance.CreateDummyObject(@object.transform);
			gameObject.transform.localPosition = new Vector3(-0.143f, 0.531f, -0.244f);
			gameObject.transform.localRotation = Quaternion.Euler(40f, 53f, 26f);
			gameObject.transform.localScale = Vector3.one;
			GameObject gameObject2 = instance.CreateDummyObject(gameObject.transform);
			gameObject2.name = "Open_RotX";
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localRotation = Quaternion.identity;
			gameObject2.transform.localScale = Vector3.one;
			instance.anim_trans_.Add(gameObject2.transform);
			Transform transform = @object.transform.Find("itm04e02_7");
			transform.parent = gameObject2.transform;
		}
	}

	private static void OnCheck_itm04e2_0()
	{
		bool flag = GSStatic.global_work_.title == TitleId.GS1 && (uint)GSStatic.global_work_.scenario < 19u;
		bool flag2 = GSFlag.Check(0u, scenario.SCE40_FLAG_ST_G_KEITAI_MES2);
		instance.on_main = instance.Event_itm04e_Open();
		instance.request_change_model_ = polyDataCtrl.instance.GetPolyData(9).prefab_name;
		instance.request_reset_posture_ = true;
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = Vector3.zero;
		instance.request_posture_scale_ = 1f;
		if (!flag2 && flag)
		{
			instance.request_scenario_mes_ = scenario.SC4_60860;
		}
	}

	private IEnumerator Event_itm04e_Open()
	{
		scienceInvestigationCtrl ctrl = scienceInvestigationCtrl.instance;
		evidenceObjectManager mng = ctrl.evidence_manager;
		yield return StartCoroutine(RequestTo());
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(RotateTo(anim_trans_[0], new Vector3(-168f, 0f, 0f), liner_, 0.2f));
		soundCtrl.instance.PlaySE(427);
		yield return new WaitForSeconds(0.5f);
		if (instance.request_scenario_mes_ != 0)
		{
			messageBoardCtrl.instance.board(true, true);
		}
		yield return StartCoroutine(EventRunScript());
		yield return StartCoroutine(Finish());
	}

	private static void Init_itm0520()
	{
		if (instance.anim_trans_.Count <= 0)
		{
			scienceInvestigationCtrl scienceInvestigationCtrl2 = scienceInvestigationCtrl.instance;
			evidenceObjectManager evidence_manager = scienceInvestigationCtrl2.evidence_manager;
			GameObject @object = evidence_manager.GetObject("itm0521");
			GameObject gameObject = instance.CreateDummyObject(@object.transform);
			gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			gameObject.transform.localRotation = Quaternion.Euler(35f, 52f, 33f);
			gameObject.transform.localScale = Vector3.one;
			GameObject gameObject2 = instance.CreateDummyObject(gameObject.transform);
			gameObject2.name = "Slide_PosZ";
			gameObject2.transform.parent = gameObject.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject2.transform.localRotation = Quaternion.identity;
			gameObject2.transform.localScale = Vector3.one;
			Transform transform = @object.transform.Find("cylinder49");
			transform.parent = gameObject2.transform;
			instance.anim_trans_.Add(gameObject2.transform);
			gameObject2.transform.localPosition = new Vector3(0f, 0f, -1.8f);
		}
	}

	private static void OnCheck_itm0520_0()
	{
		instance.request_force_close_ = true;
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(32f, 324f, 295f);
		instance.request_posture_scale_ = 1f;
		instance.request_scenario_mes_ = 171u;
		instance.on_main = instance.Event_0520_0_Open();
	}

	private static void OnCheck_itm0520_1()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(20f, 328f, 301f);
		instance.request_posture_scale_ = 1f;
		instance.request_scenario_mes_ = 172u;
		instance.on_main = instance.Event_itmsp_open_board();
	}

	private static void OnCheck_itm0525_0()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(24f, 322f, 300f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70110J;
		instance.on_main = instance.Event_itmsp();
	}

	private static void OnCheck_itm0525_1()
	{
		instance.request_posture_pos_ = Vector3.zero;
		instance.request_posture_ang_ = new Vector3(24f, 322f, 300f);
		instance.request_posture_scale_ = 1f;
		instance.request_event_mes_ = scenario.EV0_70110J;
		instance.on_main = instance.Event_itmsp();
	}

	private IEnumerator Event_0520_0_Open()
	{
		scienceInvestigationCtrl ctrl = scienceInvestigationCtrl.instance;
		evidenceObjectManager mng = ctrl.evidence_manager;
		yield return StartCoroutine(RequestTo());
		yield return new WaitForSeconds(0.5f);
		soundCtrl.instance.PlaySE(419);
		yield return StartCoroutine(MoveTo(anim_trans_[0], new Vector3(0f, 0f, 0f), liner_, 0.1f));
		messageBoardCtrl.instance.board(true, true);
		yield return StartCoroutine(EventRunScript());
		yield return StartCoroutine(Finish());
	}

	private static void Init_itm0820()
	{
		if (instance.anim_trans_.Count <= 3)
		{
			scienceInvestigationCtrl scienceInvestigationCtrl2 = scienceInvestigationCtrl.instance;
			evidenceObjectManager evidence_manager = scienceInvestigationCtrl2.evidence_manager;
			GameObject[] array = new GameObject[3];
			for (int i = 0; i < 3; i++)
			{
				array[i] = evidence_manager.AddLoadEventModel(38 + i).model_;
			}
			GameObject gameObject = instance.CreateDummyObject(array[0].transform);
			gameObject.transform.localPosition = new Vector3(-1.269f, 0f, 0f);
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			array[1].transform.parent = gameObject.transform;
			array[1].transform.position = array[0].transform.position;
			array[1].transform.localRotation = Quaternion.identity;
			array[1].transform.localScale = Vector3.one;
			array[2].transform.parent = array[1].transform;
			array[2].transform.localPosition = new Vector3(0.042f, -0.968f, -0.066f);
			array[2].transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
			array[2].transform.localScale = Vector3.one;
			instance.anim_trans_.Add(gameObject.transform);
			instance.anim_trans_.Add(array[0].transform);
			instance.anim_trans_.Add(array[1].transform);
			instance.anim_trans_.Add(array[2].transform);
		}
	}

	private static void OnCheck_itm0820_0()
	{
		instance.request_force_close_ = true;
		instance.request_posture_pos_ = new Vector3(-3f, 0f, 0f);
		instance.request_posture_ang_ = new Vector3(0f, 180f, 0f);
		instance.request_posture_scale_ = 1f;
		instance.on_main = instance.Event_0820_0_Open();
	}

	private IEnumerator Event_0820_0_Open()
	{
		scienceInvestigationCtrl ctrl = scienceInvestigationCtrl.instance;
		evidenceObjectManager mng = ctrl.evidence_manager;
		yield return StartCoroutine(RequestTo());
		yield return new WaitForSeconds(0.5f);
		string model_name = "itm0820" + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language);
		GameObject model = mng.GetObject(model_name);
		model.SetActive(false);
		for (int i = 1; i <= 3; i++)
		{
			anim_trans_[i].gameObject.SetActive(true);
		}
		anim_trans_[1].localPosition = model.transform.localPosition;
		anim_trans_[1].localRotation = model.transform.localRotation;
		anim_trans_[1].Rotate(new Vector3(0f, -180f, 0f));
		yield return StartCoroutine(RotateTo(anim_trans_[0], new Vector3(0f, -179.9f, 0f), liner_, 0.5f));
		yield return new WaitForSeconds(0.5f);
		soundCtrl.instance.PlaySE(14);
		yield return new WaitForSeconds(0.5f);
		Transform photo_trans = anim_trans_[3];
		photo_trans.parent = anim_trans_[1].parent;
		StartCoroutine(MoveTo(anim_trans_[1], new Vector3(0f, -5f, 0f), liner_, 0.4f));
		StartCoroutine(ScaleTo(photo_trans, 3f, liner_, 0.4f));
		StartCoroutine(BezierMoveTo(photo_trans, new Vector3(1.5f, 0f, photo_trans.localPosition.z), new Vector3(0.5f, -1f, photo_trans.localPosition.z), liner_, 0.4f));
		yield return new WaitForSeconds(1f);
		anim_trans_[1].gameObject.SetActive(false);
		yield return StartCoroutine(EventRunScript());
		yield return StartCoroutine(Finish());
	}

	private static void Init_itm0830()
	{
		if (instance.anim_trans_.Count <= 6)
		{
			scienceInvestigationCtrl scienceInvestigationCtrl2 = scienceInvestigationCtrl.instance;
			evidenceObjectManager evidence_manager = scienceInvestigationCtrl2.evidence_manager;
			GameObject[] array = new GameObject[5];
			for (int i = 0; i < 5; i++)
			{
				array[i] = evidence_manager.AddLoadEventModel(41 + i).model_;
			}
			GameObject gameObject = instance.CreateDummyObject(array[0].transform);
			gameObject.transform.localPosition = new Vector3(-1.269f, 0f, 0f);
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			array[1].transform.parent = gameObject.transform;
			array[1].transform.localPosition = new Vector3(1.269f, 0f, 0f);
			array[1].transform.localRotation = Quaternion.identity;
			array[1].transform.localScale = Vector3.one;
			instance.anim_trans_.Add(gameObject.transform);
			instance.anim_trans_.Add(array[0].transform);
			instance.anim_trans_.Add(array[1].transform);
			array[3].transform.parent = array[1].transform;
			array[3].transform.localPosition = Vector3.zero;
			array[3].transform.localRotation = Quaternion.identity;
			array[3].transform.localScale = Vector3.one;
			gameObject = instance.CreateDummyObject(array[3].transform);
			gameObject.transform.localPosition = new Vector3(-0.564f, 0f, 0f);
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			array[4].transform.parent = gameObject.transform;
			array[4].transform.position = array[3].transform.position;
			array[4].transform.localRotation = Quaternion.identity;
			array[4].transform.localScale = Vector3.one;
			array[2].transform.parent = array[3].transform;
			array[2].transform.localPosition = Vector3.zero;
			array[2].transform.localRotation = Quaternion.identity;
			array[2].transform.localScale = Vector3.one;
			for (int j = 0; j < 3; j++)
			{
				instance.anim_trans_.Add(array[2 + j].transform);
			}
			instance.anim_trans_.Add(gameObject.transform);
		}
	}

	private static void OnCheck_itm0830_0()
	{
		instance.request_force_close_ = true;
		instance.request_posture_pos_ = new Vector3(-3f, 0f, 0f);
		instance.request_posture_ang_ = new Vector3(0f, 180f, 0f);
		instance.request_posture_scale_ = 1f;
		instance.on_main = instance.Event_0830_0_Open();
	}

	private IEnumerator Event_0830_0_Open()
	{
		scienceInvestigationCtrl ctrl = scienceInvestigationCtrl.instance;
		evidenceObjectManager mng = ctrl.evidence_manager;
		yield return StartCoroutine(RequestTo());
		yield return new WaitForSeconds(0.5f);
		string object_name = ((GSStatic.global_work_.language != 0) ? "itm0830u" : "itm0830");
		GameObject model = mng.GetObject(object_name);
		model.SetActive(false);
		for (int i = 1; i <= 6; i++)
		{
			anim_trans_[i].gameObject.SetActive(true);
		}
		anim_trans_[1].localPosition = model.transform.localPosition;
		anim_trans_[1].localRotation = model.transform.localRotation;
		anim_trans_[1].Rotate(new Vector3(0f, -180f, 0f));
		yield return StartCoroutine(RotateTo(anim_trans_[0], new Vector3(0f, -179.9f, 0f), liner_, 0.5f));
		yield return new WaitForSeconds(0.5f);
		soundCtrl.instance.PlaySE(14);
		yield return new WaitForSeconds(0.5f);
		Transform envelope_trans = anim_trans_[4];
		envelope_trans.parent = anim_trans_[1].parent;
		StartCoroutine(MoveTo(anim_trans_[1], new Vector3(0f, -5f, 0f), liner_, 0.4f));
		StartCoroutine(ScaleTo(envelope_trans, 2f, liner_, 0.6f));
		yield return StartCoroutine(BezierMoveTo(envelope_trans, new Vector3(1.5f, -0.7f, envelope_trans.localPosition.z), new Vector3(envelope_trans.localPosition.x - 2f, envelope_trans.localPosition.y, envelope_trans.localPosition.z), liner_, 0.6f));
		yield return new WaitForSeconds(0.5f);
		anim_trans_[1].gameObject.SetActive(false);
		yield return StartCoroutine(RotateTo(envelope_trans, new Vector3(0f, 0f, -90f), liner_, 0.4f));
		yield return StartCoroutine(RotateTo(anim_trans_[6], new Vector3(0f, 180f, 0f), liner_, 0.5f));
		yield return new WaitForSeconds(0.5f);
		anim_trans_[5].localPosition += new Vector3(0f, 0f, -0.02f);
		Transform photo_trans = anim_trans_[3];
		photo_trans.parent = anim_trans_[1].parent;
		yield return StartCoroutine(MoveTo(photo_trans, new Vector3(photo_trans.localPosition.x, 5f, photo_trans.localPosition.z), liner_, 1f));
		yield return StartCoroutine(Finish());
	}
}
