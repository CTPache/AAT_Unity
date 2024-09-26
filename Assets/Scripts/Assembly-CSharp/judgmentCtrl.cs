using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judgmentCtrl : MonoBehaviour
{
	[SerializeField]
	private List<AssetBundleSprite> sprite_list_;

	[SerializeField]
	private AssetBundleSprite sprite_flash_;

	[SerializeField]
	private AnimationCurve curve_L_ = new AnimationCurve();

	[SerializeField]
	private AnimationCurve curve_R_ = new AnimationCurve();

	[SerializeField]
	private AnimationCurve curve_USA_ = new AnimationCurve();

	[SerializeField]
	private AnimationCurve curve_flash_ = new AnimationCurve();

	[SerializeField]
	private GameObject effect_;

	[SerializeField]
	private GameObject result_;

	[SerializeField]
	private GameObject body_;

	private List<Sprite> sprite_data_;

	private IEnumerator enumerator_play_;

	private IEnumerator enumerator_sub_;

	private bool is_not_guilty_play_;

	private bool is_guilty_play_;

	public static judgmentCtrl instance { get; private set; }

	private int not_count_
	{
		get
        {
            string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
            switch (lang)
            {
			case "JAPAN":
				return 1;
			case "KOREA":
				return 1;
			case "CHINA_S":
				return 1;
			case "CHINA_T":
				return 1;
			case "USA":
				return 3;
			case "FRANCE":
				return 3;
			case "GERMAN":
				return 5;
			default:
				return 3;
			}
		}
	}

	private int guilty_count_
	{
		get
        {
            string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
            switch (lang)
            {
			case "JAPAN":
				return 1;
			case "KOREA":
				return 1;
			case "CHINA_S":
				return 1;
			case "CHINA_T":
				return 1;
			case "USA":
				return 6;
			case "FRANCE":
				return 8;
			case "GERMAN":
				return 8;
			default:
				return 6;
			}
		}
	}

	public bool is_not_guilty_play
	{
		get
		{
			return is_not_guilty_play_;
		}
	}

	public bool is_guilty_play
	{
		get
		{
			return is_guilty_play_;
		}
	}

	public AssetBundleSprite sprite_L
	{
		get
		{
			return sprite_list_[0];
		}
	}

	public AssetBundleSprite sprite_R
	{
		get
		{
			return sprite_list_[1];
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

	public bool eff_active
	{
		get
		{
			return effect_.activeSelf;
		}
		set
		{
			effect_.SetActive(value);
		}
	}

	public bool result_active
	{
		get
		{
			return result_.activeSelf;
		}
		set
		{
			result_.SetActive(value);
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void load()
	{
		string in_path = "/GS1/etc/";
		string in_name = "etc014" + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language);
		AssetBundle assetBundle = AssetBundleCtrl.instance.load(in_path, in_name);
		sprite_data_ = new List<Sprite>();
		sprite_data_.AddRange(assetBundle.LoadAllAssets<Sprite>());
	}

	public void init()
	{
		load();
	}

	public void end()
	{
		sprite_data_.Clear();
		sprite_flash_.active = false;
		body_active = false;
	}

	public void judgment(int in_type)
	{
		play(in_type);
	}

	private void play(int in_type)
	{
		stop();
		enumerator_play_ = CoroutinePlay(in_type);
		coroutineCtrl.instance.Play(enumerator_play_);
	}

	private void stop()
	{
		if (enumerator_play_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_play_);
			enumerator_play_ = null;
		}
		if (enumerator_sub_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_sub_);
			enumerator_sub_ = null;
		}
	}

	private void SetSprite(int in_type)
	{
		load();
		Vector3 localScale = new Vector3(0f, 0f, 1f);
		foreach (AssetBundleSprite item in sprite_list_)
		{
			item.transform.localScale = localScale;
			item.active = false;
		}
		if (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) == "JAPAN")
		{
			sprite_L.sprite_renderer_.sprite = sprite_data_[(in_type != 1) ? 2 : 0];
			sprite_R.sprite_renderer_.sprite = sprite_data_[(in_type == 1) ? 1 : 3];
			sprite_L.transform.localPosition = new Vector3(-650f, 260f, 0f);
			sprite_R.transform.localPosition = new Vector3(650f, 260f, 0f);
			sprite_L.active = true;
			sprite_R.active = true;
			sprite_flash_.active = true;
			return;
		}
		Vector2[] array = ((GSStatic.global_work_.language == "FRANCE") ? ((in_type != 0) ? new Vector2[8]
		{
			new Vector2(-740f, 30f),
			new Vector2(-490f, 30f),
			new Vector2(-270f, 30f),
			new Vector2(-30f, -80f),
			new Vector2(200f, 30f),
			new Vector2(410f, 30f),
			new Vector2(590f, 30f),
			new Vector2(750f, 30f)
		} : new Vector2[11]
		{
			new Vector2(-680f, 200f),
			new Vector2(-430f, 200f),
			new Vector2(-200f, 200f),
			new Vector2(-720f, -200f),
			new Vector2(-490f, -200f),
			new Vector2(-270f, -200f),
			new Vector2(-30f, -310f),
			new Vector2(200f, -200f),
			new Vector2(410f, -200f),
			new Vector2(590f, -200f),
			new Vector2(750f, -200f)
		}) : ((GSStatic.global_work_.language == "GERMAN") ? ((in_type != 0) ? new Vector2[8]
		{
			new Vector2(-770f, 30f),
			new Vector2(-530f, 30f),
			new Vector2(-290f, 30f),
			new Vector2(-40f, 30f),
			new Vector2(160f, 30f),
			new Vector2(360f, 30f),
			new Vector2(560f, 30f),
			new Vector2(750f, -20f)
		} : new Vector2[13]
		{
			new Vector2(-700f, 200f),
			new Vector2(-470f, 200f),
			new Vector2(-300f, 200f),
			new Vector2(-60f, 200f),
			new Vector2(130f, 200f),
			new Vector2(-750f, -200f),
			new Vector2(-530f, -200f),
			new Vector2(-290f, -200f),
			new Vector2(-40f, -200f),
			new Vector2(160f, -200f),
			new Vector2(360f, -200f),
			new Vector2(560f, -200f),
			new Vector2(750f, -250f)
		}) : ((in_type != 0) ? new Vector2[6]
		{
			new Vector2(-370f, 75f),
			new Vector2(-150f, 70f),
			new Vector2(20f, 80f),
			new Vector2(150f, 80f),
			new Vector2(280f, 75f),
			new Vector2(460f, 22f)
		} : new Vector2[9]
		{
			new Vector2(-730f, 80f),
			new Vector2(-500f, 80f),
			new Vector2(-330f, 80f),
			new Vector2(-40f, 75f),
			new Vector2(180f, 70f),
			new Vector2(350f, 80f),
			new Vector2(480f, 80f),
			new Vector2(610f, 75f),
			new Vector2(790f, 22f)
		})));
		int num = ((in_type != 0) ? (not_count_ + guilty_count_) : 0);
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < array.Length; i++)
		{
			sprite_list_[i].sprite_renderer_.sprite = sprite_data_[i + num];
			zero.x = array[i].x;
			zero.y = array[i].y;
			sprite_list_[i].transform.localPosition = zero;
			sprite_list_[i].active = true;
		}
		sprite_flash_.active = false;
	}

	private IEnumerator CoroutinePlay(int in_type)
	{
		SetSprite(in_type);
		body_active = true;
		is_not_guilty_play_ = true;
		is_guilty_play_ = true;
		result_active = true;
		if (GSUtility.GetLanguageLayoutType(GSStatic.global_work_.language) == "JAPAN")
		{
			enumerator_sub_ = CoroutineJapanese(in_type);
		}
		else
		{
			enumerator_sub_ = CoroutineUSA(in_type);
		}
		yield return coroutineCtrl.instance.Play(enumerator_sub_);
		result_active = false;
		is_not_guilty_play_ = false;
		yield return null;
		if (in_type == 0)
		{
			if (GSStatic.message_work_.op_work[7] == 65532)
			{
				GSStatic.global_work_.r.no_1 = 9;
				GSStatic.message_work_.op_work[7] = 0;
				yield return null;
			}
			else
			{
				eff_active = true;
				soundCtrl.instance.PlaySE(102);
				float time = 0f;
				float wait = 5f;
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
		is_guilty_play_ = false;
		body_active = false;
		enumerator_play_ = null;
		end();
	}

	private IEnumerator CoroutineJapanese(int in_type)
	{
		float time = 0f;
		int se_count = 0;
		while (true)
		{
			time += ((in_type != 0) ? 0.006f : 0.005f);
			if (se_count <= 0 && time > 0.1f)
			{
				soundCtrl.instance.PlaySE(86);
				se_count++;
			}
			else if (se_count <= 1 && time > 0.35f)
			{
				soundCtrl.instance.StopSE(86);
				soundCtrl.instance.PlaySE(86);
				se_count++;
			}
			if (time > 1f)
			{
				break;
			}
			float scl_L = curve_L_.Evaluate(time);
			float scl_R = curve_R_.Evaluate(time);
			float flash = curve_flash_.Evaluate(time);
			sprite_L.transform.localScale = new Vector3(scl_L, scl_L, 1f);
			sprite_R.transform.localScale = new Vector3(scl_R, scl_R, 1f);
			Color color = sprite_flash_.sprite_renderer_.color;
			sprite_flash_.sprite_renderer_.color = new Color(color.r, color.g, color.g, flash);
			yield return null;
		}
		enumerator_sub_ = null;
	}

	private IEnumerator CoroutineUSA(int in_type)
	{
		int time = 0;
		int max_time = 200;
		int se_count = 0;
		if (in_type == 0)
		{
			while (time < max_time)
			{
				time++;
				if (se_count <= 0 && time > 20)
				{
					soundCtrl.instance.PlaySE(86);
					se_count++;
				}
				else if (se_count <= 1 && time > 90)
				{
					soundCtrl.instance.StopSE(86);
					soundCtrl.instance.PlaySE(86);
					se_count++;
				}
				float curve_L_evaluate = curve_USA_.Evaluate((float)time / (float)max_time);
				float curve_R_evaluate = curve_USA_.Evaluate((float)(time - 70) / (float)max_time);
				Vector3 scalel_L = new Vector3(curve_L_evaluate, curve_L_evaluate, 1f);
				Vector3 scalel_R = new Vector3(curve_R_evaluate, curve_R_evaluate, 1f);
				for (int i = 0; i < not_count_; i++)
				{
					sprite_list_[i].transform.localScale = scalel_L;
				}
				for (int j = not_count_; j < not_count_ + guilty_count_; j++)
				{
					sprite_list_[j].transform.localScale = scalel_R;
				}
				yield return null;
			}
		}
		else
		{
			while (time < max_time - 40)
			{
				time++;
				if (se_count < 6 && time % 10 == 0)
				{
					soundCtrl.instance.StopSE(86);
					soundCtrl.instance.PlaySE(86);
					se_count++;
				}
				for (int k = 0; k < not_count_ + guilty_count_; k++)
				{
					float curve_USA_evaluate = curve_USA_.Evaluate((float)(time - k * 10) / (float)max_time);
					Vector3 sprite_scale_ = new Vector3(curve_USA_evaluate, curve_USA_evaluate, 1f);
					sprite_list_[k].transform.localScale = sprite_scale_;
				}
				yield return null;
			}
			Vector3 scale_init = new Vector3(0f, 0f, 1f);
			foreach (AssetBundleSprite item in sprite_list_)
			{
				item.transform.localScale = scale_init;
				item.sprite_renderer_.sprite = null;
				item.active = false;
			}
			while (time < max_time)
			{
				time++;
				yield return null;
			}
		}
		enumerator_sub_ = null;
	}
}
