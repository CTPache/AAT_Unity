using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeGaugeCtrl : MonoBehaviour
{
	public enum Gauge_State
	{
		LIFE_OFF = 0,
		LIFE_ON = 1,
		LIFE_OFF_MOMENT = 2,
		LIFE_ON_MOMENT = 3,
		UPDATE_REST = 4,
		NOTICE_DAMAGE = 5,
		STOP_NOTICE = 6,
		DAMAGE = 7,
		WAIT = 8
	}

	public enum State
	{
		LIFEGAUGE_WAIT = 0,
		LIFEGAUGE_APPEAR = 1,
		LIFEGAUGE_DISAPPEAR = 2,
		LIFEGAUGE_DISAPPEAR2 = 3,
		LIFEGAUGE_DMG = 4,
		LIFEGAUGE_DIE = 5,
		LIFEGAUGE_MOVE_UP = 6,
		LIFEGAUGE_MOVE_DOWN = 7,
		LIFEGAUGE_RETURN = 8,
		LIFEGAUGE_APPEAR2 = 9
	}

	private static lifeGaugeCtrl instance_;

	private const int GAUGE_NUM = 10;

	[SerializeField]
public GameObject body_;

	[SerializeField]
public GameObject gauge_;

	[SerializeField]
public AnimationCurve move_curve_;

	[SerializeField]
public AnimationCurve alpha_curve_;

	[SerializeField]
public List<AssetBundleSprite> frame_list_;

	[SerializeField]
public List<AssetBundleSprite> gauge_blue_;

	[SerializeField]
public List<AssetBundleSprite> gauge_red_;

	[SerializeField]
public Material material_default_;

	[SerializeField]
public Material material_white_;

	[SerializeField]
public AssetBundleSprite explosion_sprite_1_;

	[SerializeField]
public AssetBundleSprite explosion_sprite_2_;

	private bool fadeout_dropfrag_;

	private bool is_recover_flag_;

	private Vector3 gauge_active_pos = new Vector3(770f, 475f, -20f);

	private Vector3 gauge_inactive_pos = new Vector3(1170f, 475f, -20f);

	private Vector3 gauge_psy_jpn_active_pos = new Vector3(0f, -160f, -20f);

	private Vector3 gauge_psy_usa_active_pos = new Vector3(0f, -260f, -20f);

	private IEnumerator move_enumerator_;

	private IEnumerator frame_out_enumerator_;

	private bool to_active_pos_;

	private int gauge_mode_ = 8;

	private int active_notice_cnt_;

	private bool debug_no_damage_;

	private bool debug_instant_death_;

	public static lifeGaugeCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public AssetBundleSprite gauge_Frame
	{
		get
		{
			return frame_list_[0];
		}
	}

	public AssetBundleSprite life_scale
	{
		get
		{
			return frame_list_[1];
		}
	}

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

	public bool gauge_active
	{
		get
		{
			return gauge_.activeSelf;
		}
		set
		{
			gauge_.SetActive(value);
		}
	}

	public bool is_recover_flag
	{
		get
		{
			return is_recover_flag_;
		}
		set
		{
			is_recover_flag_ = value;
		}
	}

	public int gauge_mode
	{
		get
		{
			return gauge_mode_;
		}
		set
		{
			gauge_mode_ = value;
		}
	}

	public bool debug_no_damage
	{
		get
		{
			return debug_no_damage_;
		}
		set
		{
			debug_no_damage_ = value;
		}
	}

	public bool debug_instant_death
	{
		get
		{
			return debug_instant_death_;
		}
		set
		{
			debug_instant_death_ = value;
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance_ = this;
		}
	}

	private void OnDisable()
	{
		if (move_enumerator_ != null)
		{
			coroutineCtrl.instance.Stop(move_enumerator_);
			move_enumerator_ = null;
		}
	}

	public void load()
	{
		GSStatic.global_work_.gauge_disp_flag = 1;
		gauge_.transform.localPosition = gauge_inactive_pos;
		for (int i = 0; i <= 9; i++)
		{
			if (i <= 1)
			{
				frame_list_[i].load("/menu/common/", "gauge");
				frame_list_[i].spriteNo(i);
			}
			gauge_blue_[i].load("/menu/common/", "gauge");
			gauge_red_[i].load("/menu/common/", "gauge");
		}
		explosion_sprite_1_.load("/menu/common/", "etc001a");
		explosion_sprite_2_.load("/menu/common/", "etc001b");
		for (int j = 0; j <= 9; j++)
		{
			switch (j)
			{
			case 0:
				gauge_blue_[j].spriteNo(4);
				gauge_red_[j].spriteNo(7);
				break;
			case 9:
				gauge_blue_[j].spriteNo(2);
				gauge_red_[j].spriteNo(5);
				break;
			default:
				gauge_blue_[j].spriteNo(3);
				gauge_red_[j].spriteNo(6);
				break;
			}
		}
	}

	public void init()
	{
		gauge_mode_ = 8;
		load();
	}

	public void LowestGaugeSet()
	{
		if (GSStatic.global_work_.gauge_hp == 1)
		{
			gauge_blue_[0].spriteNo(8);
			gauge_red_[0].spriteNo(9);
		}
		else
		{
			gauge_blue_[0].spriteNo(4);
			gauge_red_[0].spriteNo(7);
		}
	}

	public void ActionLifeGauge(Gauge_State _state)
	{
		switch (_state)
		{
		case Gauge_State.LIFE_OFF:
			coroutineCtrl.instance.Play(FrameOut());
			break;
		case Gauge_State.LIFE_ON:
			FrameIn();
			break;
		case Gauge_State.LIFE_OFF_MOMENT:
			GaugeMoveMoment(false);
			break;
		case Gauge_State.LIFE_ON_MOMENT:
			GaugeMoveMoment(true);
			break;
		case Gauge_State.UPDATE_REST:
			UpdateRest();
			break;
		case Gauge_State.NOTICE_DAMAGE:
			if (GetDamage() != 0)
			{
				coroutineCtrl.instance.Play(FrameInFadeColor());
			}
			break;
		case Gauge_State.STOP_NOTICE:
			coroutineCtrl.instance.Play(StopFadeColor());
			break;
		case Gauge_State.DAMAGE:
			coroutineCtrl.instance.Play(ReduceRest());
			break;
		}
	}

	private void IsActive(bool _active)
	{
		if (!body_active)
		{
			body_active = true;
		}
		if (gauge_active != _active)
		{
			gauge_active = _active;
		}
	}

	private void FrameIn()
	{
		if (!to_active_pos_)
		{
			to_active_pos_ = true;
			UpdateRest();
			if (move_enumerator_ != null)
			{
				coroutineCtrl.instance.Stop(move_enumerator_);
				move_enumerator_ = null;
			}
			move_enumerator_ = CoroutineGaugeMove(true);
			coroutineCtrl.instance.Play(move_enumerator_);
		}
		else
		{
			lifegauge_do_next_move();
		}
	}

	private IEnumerator FrameOut()
	{
		if (to_active_pos_)
		{
			to_active_pos_ = false;
			if (move_enumerator_ != null)
			{
				coroutineCtrl.instance.Stop(move_enumerator_);
				move_enumerator_ = null;
			}
			fadeout_dropfrag_ = false;
			move_enumerator_ = CoroutineGaugeMove(false);
			yield return coroutineCtrl.instance.Play(move_enumerator_);
		}
		else
		{
			lifegauge_do_next_move();
		}
	}

	private void UpdateRest()
	{
		int nowLife = GetNowLife();
		for (int i = 0; i < 10; i++)
		{
			gauge_blue_[i].active = i < nowLife;
		}
		LowestGaugeSet();
	}

	private IEnumerator StopFadeColor()
	{
		fadeout_dropfrag_ = true;
		float time = 0f;
		float wait = 0.2f;
		while (true)
		{
			time += Time.deltaTime;
			if (time > wait)
			{
				break;
			}
			yield return null;
		}
		fadeout_dropfrag_ = false;
	}

	private IEnumerator FrameInFadeColor()
	{
		if (debug_no_damage_)
		{
			yield break;
		}
		while (move_enumerator_ != null)
		{
			yield return null;
		}
		int life = GetNowLife();
		int notice_damage = GetDamage();
		int notice_start_index = life - notice_damage;
		if (notice_start_index < 0)
		{
			notice_start_index = 0;
		}
		if (gauge_mode_ == 5)
		{
			if (active_notice_cnt_ != notice_start_index)
			{
				active_notice_cnt_ = notice_start_index;
			}
			yield break;
		}
		for (int i = notice_start_index; i < life; i++)
		{
			gauge_red_[i].active = true;
		}
		gauge_mode_ = 5;
		active_notice_cnt_ = notice_start_index;
		int old_notice_cnt = notice_start_index;
		float time = 0f;
		float alpha2 = 0f;
		while (gauge_.transform.localPosition.x <= gauge_active_pos.x && !fadeout_dropfrag_ && GetDamage() != 0)
		{
			time = ((!(time >= 1f)) ? (time + 0.02f) : 0f);
			alpha2 = alpha_curve_.Evaluate(time);
			if (old_notice_cnt != active_notice_cnt_)
			{
				if (old_notice_cnt < active_notice_cnt_)
				{
					for (int j = old_notice_cnt; j < active_notice_cnt_; j++)
					{
						gauge_red_[j].sprite_renderer_.color = new Color(1f, 1f, 1f, 1f);
						gauge_red_[j].active = false;
					}
					old_notice_cnt = active_notice_cnt_;
				}
				else
				{
					old_notice_cnt = active_notice_cnt_;
					for (int k = old_notice_cnt; k < life; k++)
					{
						gauge_red_[k].sprite_renderer_.color = new Color(1f, 1f, 1f, alpha2);
						gauge_red_[k].active = true;
					}
				}
			}
			for (int l = old_notice_cnt; l < life; l++)
			{
				gauge_red_[l].sprite_renderer_.color = new Color(1f, 1f, 1f, alpha2);
			}
			yield return null;
		}
		for (int m = old_notice_cnt; m < life; m++)
		{
			gauge_red_[m].sprite_renderer_.color = new Color(1f, 1f, 1f, 1f);
			gauge_red_[m].active = false;
		}
		active_notice_cnt_ = 0;
	}

	public IEnumerator ReduceRest()
	{
		int now_life = GetNowLife();
		int old_life = GetOldLife();
		int damage2 = old_life - now_life;
		fadeout_dropfrag_ = true;
		if (GSStatic.global_work_.title != 0)
		{
			if (damage2 == 0)
			{
				damage2 = 1;
			}
			if (damage2 < 0)
			{
				LowestGaugeSet();
				damage2 *= -1;
				yield return coroutineCtrl.instance.Play(RecoveryLife(damage2));
				lifegauge_do_next_move();
				yield break;
			}
		}
		if (debug_no_damage_)
		{
			GSStatic.global_work_.gauge_dmg_cnt = 0;
			GSStatic.global_work_.rest = (sbyte)GSStatic.global_work_.rest_old;
		}
		else
		{
			if (debug_instant_death_ && GSStatic.global_work_.title == TitleId.GS1)
			{
				damage2 = old_life;
				now_life = 0;
			}
			gauge_mode_ = 7;
			for (int i = 0; i <= 2; i++)
			{
				for (int j = now_life; j < old_life; j++)
				{
					gauge_red_[j].active = true;
					gauge_red_[j].sprite_renderer_.color = new Color(1f, 1f, 1f, 1f);
				}
				float time2 = 0f;
				float wait2 = 0.08f;
				while (true)
				{
					time2 += Time.deltaTime;
					if (time2 > wait2)
					{
						break;
					}
					yield return null;
				}
				for (int k = now_life; k < old_life; k++)
				{
					gauge_red_[k].sprite_renderer_.color = new Color(0.1f, 0.1f, 0.1f, 1f);
				}
				float time = 0f;
				float wait = 0.08f;
				while (true)
				{
					time += Time.deltaTime;
					if (time > wait)
					{
						break;
					}
					yield return null;
				}
				if (i == 2)
				{
					UpdateRest();
					for (int l = now_life; l < old_life; l++)
					{
						gauge_red_[l].sprite_renderer_.color = new Color(1f, 1f, 1f, 1f);
					}
					yield return coroutineCtrl.instance.Play(PlayExplosion(damage2));
				}
			}
			if (debug_instant_death_)
			{
				GSStatic.global_work_.rest = 0;
				GSStatic.global_work_.gauge_hp = 0;
			}
		}
		fadeout_dropfrag_ = false;
		GSStatic.global_work_.gauge_hp_disp = GSStatic.global_work_.gauge_hp;
		lifegauge_do_next_move();
		if ((GSStatic.global_work_.r.no_0 == 4 || GSStatic.global_work_.r.no_0 == 7) && GSStatic.global_work_.title != 0 && GSStatic.global_work_.gauge_hp <= 0)
		{
			MessageWork message_work = MessageSystem.GetActiveMessageWork();
			if (frame_out_enumerator_ != null)
			{
				coroutineCtrl.instance.Stop(frame_out_enumerator_);
				frame_out_enumerator_ = null;
			}
			frame_out_enumerator_ = FrameOut();
			if (!message_work.game_over)
			{
				message_work.game_over = true;
				if (GSStatic.global_work_.title == TitleId.GS3)
				{
					advCtrl.instance.message_system_.LoadScenarioMdtFromStreamingAssets(GSScenario.GetScenarioMdtPath(GSStatic.global_work_.scenario));
				}
				advCtrl.instance.message_system_.SetMessage(GSScenario.GetGameOverMesData());
			}
			GSStatic.global_work_.gauge_disp_flag = 0;
			yield return coroutineCtrl.instance.Play(frame_out_enumerator_);
		}
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			if (frame_out_enumerator_ != null)
			{
				coroutineCtrl.instance.Stop(frame_out_enumerator_);
				frame_out_enumerator_ = null;
			}
			frame_out_enumerator_ = FrameOut();
			yield return coroutineCtrl.instance.Play(frame_out_enumerator_);
		}
		yield return null;
	}

	private IEnumerator PlayExplosion(int _damageScale)
	{
		float shake_gauge = 5f;
		int now_rest_pos_x = GetNowLife();
		int old_rest_pos_x = GetOldLife();
		AssetBundleSprite use_explosion = null;
		now_rest_pos_x--;
		old_rest_pos_x--;
		GSStatic.global_work_.gauge_hp_disp = GSStatic.global_work_.gauge_hp;
		if (_damageScale <= 5)
		{
			explosion_sprite_1_.active = true;
			use_explosion = explosion_sprite_1_;
		}
		else
		{
			explosion_sprite_2_.active = true;
			use_explosion = explosion_sprite_2_;
		}
		if (_damageScale <= 5)
		{
			for (int j = 0; j < 5; j++)
			{
				if (j == 1)
				{
					soundCtrl.instance.PlaySE(76);
				}
				if (j < _damageScale)
				{
					use_explosion.transform.localPosition = new Vector3(gauge_red_[old_rest_pos_x - j].transform.localPosition.x, 0f, -20f);
					gauge_red_[old_rest_pos_x - j].active = false;
				}
				else
				{
					use_explosion.transform.localPosition = new Vector3(gauge_red_[now_rest_pos_x + 1].transform.localPosition.x, 0f, -20f);
					gauge_red_[now_rest_pos_x + 1].active = false;
				}
				use_explosion.spriteNo(j);
				gauge_.transform.localPosition = new Vector3(gauge_active_pos.x + ((j % 2 != 0) ? (0f - shake_gauge) : shake_gauge), gauge_active_pos.y + ((j % 2 != 0) ? (0f - shake_gauge) : shake_gauge), -20f);
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
			}
		}
		else
		{
			for (int i = 0; i < 10; i++)
			{
				if (i == 1)
				{
					soundCtrl.instance.PlaySE(76);
				}
				if (i < _damageScale)
				{
					use_explosion.transform.localPosition = new Vector3(gauge_red_[old_rest_pos_x - i].transform.localPosition.x, 0f, -20f);
					gauge_red_[old_rest_pos_x - i].active = false;
				}
				else
				{
					use_explosion.transform.localPosition = new Vector3(gauge_red_[now_rest_pos_x + 1].transform.localPosition.x, 0f, -20f);
					gauge_red_[now_rest_pos_x + 1].active = false;
				}
				use_explosion.spriteNo(i);
				gauge_.transform.localPosition = new Vector3(gauge_active_pos.x + ((i % 2 != 0) ? (0f - shake_gauge) : shake_gauge), gauge_active_pos.y + ((i % 2 != 0) ? (0f - shake_gauge) : shake_gauge), -20f);
				float time2 = 0f;
				float wait2 = 0.1f;
				while (true)
				{
					time2 += Time.deltaTime;
					if (time2 > wait2)
					{
						break;
					}
					yield return null;
				}
			}
		}
		use_explosion.active = false;
		use_explosion.spriteNo(0);
		gauge_.transform.localPosition = new Vector3(gauge_active_pos.x, gauge_active_pos.y, -20f);
	}

	private IEnumerator RecoveryLife(int _recover_num)
	{
		float shake_gauge = 2f;
		float time = -1f;
		int vibrate_count = 0;
		int recover_count = 0;
		int old_life = GetOldLife();
		int now_life = GetNowLife() - 1;
		Vector3 use_position = ((GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) != "USA") ? gauge_psy_jpn_active_pos : gauge_psy_usa_active_pos);
		while (move_enumerator_ != null)
		{
			yield return null;
		}
		while (recover_count < _recover_num)
		{
			if (time < 0f)
			{
				for (int i = old_life; i <= now_life; i++)
				{
					gauge_blue_[i].active = true;
					gauge_blue_[i].sprite_renderer_.material = material_white_;
				}
				time = 0f;
				soundCtrl.instance.PlaySE(156);
			}
			vibrate_count++;
			time += 0.06f;
			if (1f < time)
			{
				gauge_blue_[old_life + recover_count].sprite_renderer_.material = material_default_;
				recover_count++;
				time = 0f;
			}
			gauge_.transform.localPosition = new Vector3(use_position.x, use_position.y + ((vibrate_count % 2 != 0) ? (0f - shake_gauge) : shake_gauge), -20f);
			yield return null;
		}
		gauge_.transform.localPosition = new Vector3(use_position.x, use_position.y, -20f);
		soundCtrl.instance.StopSE(156);
		float frame = 0f;
		float wait = 1f;
		while (true)
		{
			frame += Time.deltaTime;
			if (frame > wait)
			{
				break;
			}
			yield return null;
		}
		fadeout_dropfrag_ = false;
		GSStatic.global_work_.gauge_hp_disp = GSStatic.global_work_.gauge_hp;
		lifegauge_do_next_move();
	}

	private IEnumerator CoroutineGaugeMove(bool _act)
	{
		float gauge_pos_X = 0f;
		float another_pos_X = 0f;
		Vector3 gauge_psy_active_pos = ((GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) != "USA") ? gauge_psy_jpn_active_pos : gauge_psy_usa_active_pos);
		float gauge_pos_Y = ((!is_recover_flag_) ? gauge_active_pos.y : gauge_psy_active_pos.y);
		gauge_mode_ = (_act ? 1 : 0);
		if (is_recover_flag_)
		{
			gauge_pos_X = ((!_act) ? gauge_psy_active_pos.x : gauge_inactive_pos.x);
			another_pos_X = ((!_act) ? gauge_inactive_pos.x : gauge_psy_active_pos.x);
		}
		else
		{
			gauge_pos_X = ((!_act) ? gauge_active_pos.x : gauge_inactive_pos.x);
			another_pos_X = ((!_act) ? gauge_inactive_pos.x : gauge_active_pos.x);
		}
		float distance = another_pos_X - gauge_pos_X;
		float time = 0f;
		body_active = true;
		gauge_active = true;
		if (!_act)
		{
			float frame2 = 0f;
			float wait2 = 0.4f;
			while (true)
			{
				frame2 += Time.deltaTime;
				if (frame2 > wait2)
				{
					break;
				}
				yield return null;
			}
		}
		while (time < 1f)
		{
			time += 0.06f;
			if (time > 1f)
			{
				time = 1f;
			}
			float move_pos_X = distance * move_curve_.Evaluate(time);
			gauge_.transform.localPosition = new Vector3(gauge_pos_X + move_pos_X, gauge_pos_Y, -20f);
			yield return null;
		}
		if (!_act)
		{
			if (is_recover_flag_)
			{
				is_recover_flag_ = false;
			}
			gauge_mode_ = 8;
			body_active = false;
			gauge_active = false;
		}
		else if (GSStatic.global_work_.title == TitleId.GS2 && GSStatic.global_work_.gauge_dmg_cnt != 0)
		{
			MessageWork message_work_ = GSStatic.message_work_;
			switch (GSStatic.global_work_.scenario)
			{
			case 16:
				if (message_work_.now_no == scenario_GS2.SC3_20190 || message_work_.now_no == scenario_GS2.SC3_20360)
				{
					coroutineCtrl.instance.Play(FrameInFadeColor());
				}
				break;
			case 17:
				if (message_work_.now_no == scenario_GS2.SC3_20820 || message_work_.now_no == scenario_GS2.SC3_21060 || message_work_.now_no == scenario_GS2.SC3_21080 || message_work_.now_no == scenario_GS2.SC3_21110)
				{
					coroutineCtrl.instance.Play(FrameInFadeColor());
				}
				break;
			case 20:
				if (message_work_.now_no == scenario_GS2.SC3_26040 || message_work_.now_no == scenario_GS2.SC3_26290)
				{
					coroutineCtrl.instance.Play(FrameInFadeColor());
				}
				break;
			case 21:
				if (message_work_.now_no == scenario_GS2.SC3_26410)
				{
					coroutineCtrl.instance.Play(FrameInFadeColor());
				}
				break;
			}
		}
		float frame = 0f;
		float wait = 0.3f;
		while (true)
		{
			frame += Time.deltaTime;
			if (frame > wait)
			{
				break;
			}
			yield return null;
		}
		move_enumerator_ = null;
		lifegauge_do_next_move();
	}

	private void GaugeMoveMoment(bool _act)
	{
		if (move_enumerator_ != null)
		{
			coroutineCtrl.instance.Stop(move_enumerator_);
			move_enumerator_ = null;
		}
		to_active_pos_ = _act;
		if (_act)
		{
			gauge_.transform.localPosition = new Vector3(gauge_active_pos.x, gauge_active_pos.y, -20f);
			body_active = true;
			gauge_active = true;
		}
		else
		{
			gauge_.transform.localPosition = new Vector3(gauge_inactive_pos.x, gauge_active_pos.y, -20f);
			body_active = false;
			gauge_active = false;
		}
	}

	public void ResumeFromPause()
	{
		if (gauge_mode_ != 0 && gauge_mode_ != 8)
		{
			ActionLifeGauge(Gauge_State.LIFE_ON_MOMENT);
			if (gauge_mode_ == 5)
			{
				gauge_mode_ = 8;
				active_notice_cnt_ = 0;
				ActionLifeGauge(Gauge_State.NOTICE_DAMAGE);
			}
		}
		else
		{
			gauge_mode_ = 8;
		}
	}

	private void lifegauge_do_next_move()
	{
		if (GSStatic.global_work_.gauge_cnt_1 > 0)
		{
			GSStatic.global_work_.gauge_rno_0 = (byte)GSStatic.global_work_.gauge_cnt_1;
		}
		else
		{
			GSStatic.global_work_.gauge_rno_0 = 0;
		}
		GSStatic.global_work_.gauge_rno_1 = 0;
		GSStatic.global_work_.gauge_cnt_0 = 0;
		GSStatic.global_work_.gauge_cnt_1 = 0;
		lifegauge_rno_action(GSStatic.global_work_.gauge_rno_0);
	}

	public void lifegauge_set_move(int gauge_rno_0)
	{
		if (gauge_rno_0 == 8 && (GSStatic.global_work_.gauge_rno_0 == 2 || GSStatic.global_work_.gauge_rno_0 == 3))
		{
			return;
		}
		if (GSStatic.global_work_.title == TitleId.GS2)
		{
			switch (gauge_rno_0)
			{
			case 3:
				gauge_rno_0 = 4;
				break;
			case 4:
				gauge_rno_0 = 5;
				break;
			case 5:
				gauge_rno_0 = 9;
				break;
			}
		}
		GSStatic.global_work_.gauge_rno_1 = 0;
		GSStatic.global_work_.gauge_cnt_0 = 0;
		GSStatic.global_work_.gauge_cnt_1 = 0;
		if (GSStatic.global_work_.gauge_rno_0 == 0)
		{
			GSStatic.global_work_.gauge_rno_0 = (byte)gauge_rno_0;
			lifegauge_rno_action(GSStatic.global_work_.gauge_rno_0);
		}
		else
		{
			GSStatic.global_work_.gauge_cnt_1 = (short)gauge_rno_0;
		}
	}

	private void lifegauge_rno_action(int gauge_rno_0)
	{
		switch (gauge_rno_0)
		{
		case 1:
		case 9:
			UpdateRest();
			FrameIn();
			break;
		case 2:
		case 3:
			ActionLifeGauge(Gauge_State.LIFE_OFF);
			break;
		case 4:
			GSStatic.global_work_.gauge_hp_disp = GSStatic.global_work_.gauge_hp;
			if (GSStatic.global_work_.gauge_dmg_cnt < 0 && GSStatic.global_work_.gauge_hp == 1)
			{
				if (debug_no_damage_)
				{
					GSStatic.global_work_.gauge_dmg_cnt = 0;
				}
				else if (debug_instant_death_)
				{
					GSStatic.global_work_.gauge_dmg_cnt = (short)((GSStatic.global_work_.gauge_dmg_cnt <= GSStatic.global_work_.gauge_hp) ? 80 : GSStatic.global_work_.gauge_hp);
				}
				GSStatic.global_work_.gauge_hp = (short)(-GSStatic.global_work_.gauge_dmg_cnt);
			}
			else
			{
				if (debug_no_damage_)
				{
					GSStatic.global_work_.gauge_dmg_cnt = 0;
				}
				else if (debug_instant_death_)
				{
					GSStatic.global_work_.gauge_dmg_cnt = (short)((GSStatic.global_work_.gauge_dmg_cnt <= GSStatic.global_work_.gauge_hp) ? 80 : GSStatic.global_work_.gauge_hp);
				}
				GSStatic.global_work_.gauge_hp -= GSStatic.global_work_.gauge_dmg_cnt;
			}
			if (GSStatic.global_work_.gauge_hp < 0)
			{
				GSStatic.global_work_.gauge_hp = 0;
			}
			if (GSStatic.global_work_.gauge_hp > 80)
			{
				GSStatic.global_work_.gauge_hp = 80;
			}
			ActionLifeGauge(Gauge_State.DAMAGE);
			break;
		case 5:
			ActionLifeGauge(Gauge_State.LIFE_OFF);
			break;
		case 6:
			lifegauge_do_next_move();
			break;
		case 7:
			lifegauge_do_next_move();
			break;
		case 8:
			lifegauge_do_next_move();
			break;
		}
	}

	public bool is_lifegauge_moving()
	{
		if (GSStatic.global_work_.gauge_rno_0 == 0 || GSStatic.global_work_.gauge_rno_0 == 5 || GSStatic.global_work_.gauge_disp_flag == 0 || GSStatic.global_work_.gauge_rno_0 == 9)
		{
			return false;
		}
		return true;
	}

	public bool is_lifegauge_moving_dam()
	{
		if (GSStatic.global_work_.gauge_rno_0 == 0 || GSStatic.global_work_.gauge_rno_0 == 5 || GSStatic.global_work_.gauge_rno_0 == 9 || GSStatic.global_work_.gauge_rno_0 == 4)
		{
			return false;
		}
		return true;
	}

	public bool is_lifegauge_disp()
	{
		return body_active;
	}

	private int GetNowLife()
	{
		if (GSStatic.global_work_.gauge_hp == 1)
		{
			return 1;
		}
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			if (debug_no_damage_)
			{
				return GSStatic.global_work_.rest_old * 10 / 5;
			}
			return GSStatic.global_work_.rest * 10 / 5;
		}
		if (debug_no_damage_)
		{
			return GSStatic.global_work_.gauge_hp_disp * 10 / 80;
		}
		return GSStatic.global_work_.gauge_hp * 10 / 80;
	}

	private int GetOldLife()
	{
		if (GSStatic.global_work_.gauge_hp_disp == 1)
		{
			return 1;
		}
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			return GSStatic.global_work_.rest_old * 10 / 5;
		}
		return GSStatic.global_work_.gauge_hp_disp * 10 / 80;
	}

	private int GetDamage()
	{
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			if (debug_instant_death_)
			{
				return GSStatic.global_work_.rest * 10 / 5;
			}
			return 2;
		}
		if (debug_instant_death_)
		{
			return GSStatic.global_work_.gauge_hp * 10 / 5;
		}
		return GSStatic.global_work_.gauge_dmg_cnt * 10 / 80;
	}

	public static int DamageAjust(int damage)
	{
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			return damage;
		}
		if (damage == 38)
		{
			damage = 32;
		}
		return (damage + 4) / 8 * 8;
	}
}
