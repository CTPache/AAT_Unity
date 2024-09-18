using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GS1_sce4_opening : MonoBehaviour
{
	private class tomoe_data
	{
		public string path;

		public string file;

		public float pos_x;

		public float pos_y;

		public float speed_y;

		public tomoe_data(string in_path, string in_file, float in_pos_x, float in_pos_y, float in_speed_y)
		{
			path = in_path;
			file = in_file;
			pos_x = in_pos_x;
			pos_y = in_pos_y;
			speed_y = in_speed_y;
		}
	}

	private class biru_data
	{
		public float pos_x1;

		public float pos_x2;

		public float pos_y;

		public float pos_z;

		public float speed;

		public string path;

		public string file;

		public biru_data(float in_pos_x1, float in_pos_x2, float in_pos_y, float in_pos_z, float in_speed, string in_path, string in_file)
		{
			pos_x1 = in_pos_x1;
			pos_x2 = in_pos_x2;
			pos_y = in_pos_y;
			pos_z = in_pos_z;
			speed = in_speed;
			path = in_path;
			file = in_file;
		}
	}

	[SerializeField]
	private AssetBundleSprite[] sprites_;

	[SerializeField]
	private AssetBundleSprite[] front_sprites_;

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private GameObject front_body_;

	[SerializeField]
	private RawImage movie_renderer_;

	[SerializeField]
	private SpriteRenderer front_image_;

	private AnimationObject rain_animation_;

	private AnimationObject biru_animation_;

	private AnimationObject black_knife_animation_;

	private AnimationObject flash_knife_animation_;

	private IEnumerator enumerator_tomoe_;

	private IEnumerator enumerator_biru_;

	private IEnumerator enumerator_black_knife_;

	private IEnumerator enumerator_movie_;

	private IEnumerator enumerator_rain_;

	private bool is_tomoe_scroll_;

	private bool is_biru_scroll_;

	private bool is_black_knife_animation_;

	private bool is_movie_;

	public int op4_taimar;

	private Vector3 anim_defaulut_pos_ = new Vector3(0f, 0f, -30f);

	private string[] e_op5_demo_spr_ = new string[5] { "bg576", "bg577", "bg578", "bg575", "bg575" };

	private const int tomoe_body_index = 0;

	private const int tomoe_hand_index = 1;

	private static tomoe_data tomoe_body_data_ = new tomoe_data("/GS1/etc/", "spr450", 0f, 560f, 0.8f);

	private static tomoe_data tomoe_hand_data_ = new tomoe_data("/GS1/etc/", "spr40e", -60f, 70f, 2.6f);

	private static biru_data[] biru_data_list_ = new biru_data[5]
	{
		new biru_data(-1080f, -872f, 0f, -6.3f, -4f, "/GS1/BG/", "bg0d0"),
		new biru_data(0f, 0f, 0f, -6.2f, 0.8f, "/GS1/BG/", "bg0d1"),
		new biru_data(-1920f, -1920f, 0f, -6.1f, 0.8f, "/GS1/BG/", "bg0d1"),
		new biru_data(-1080f, 0f, 0f, -6f, 3.2f, "/GS1/BG/", "bg0d2"),
		new biru_data(-1080f, -1080f, 0f, 0.5f, -0.4f, "/GS1/BG/", "bg0d3")
	};

	private static biru_data[] gs3_sc1_biru_data_list_ = new biru_data[4]
	{
		new biru_data(-680f, 1242f, 0f, -10f, -18f, "/GS3/BG/", "bg217"),
		new biru_data(-680f, 1242f, 1242f, -10f, -18f, "/GS3/BG/", "bg217"),
		new biru_data(800f, 2048f, 640f, -10f, -18f, "/GS3/BG/", "bg218"),
		new biru_data(800f, 2048f, 2688f, -10f, -18f, "/GS3/BG/", "bg218")
	};

	private static biru_data biru_data_big = new biru_data(2460f, 2460f, -96f, -5f, -20f, string.Empty, string.Empty);

	public static GS1_sce4_opening instance { get; private set; }

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

	private bool movie_active_
	{
		get
		{
			return movie_renderer_.gameObject.activeSelf;
		}
		set
		{
			movie_renderer_.gameObject.SetActive(value);
		}
	}

	public bool is_tomoe_scroll
	{
		get
		{
			return is_tomoe_scroll_;
		}
	}

	public bool is_biru_scroll
	{
		get
		{
			return is_biru_scroll_;
		}
	}

	public bool is_black_knife_animation
	{
		get
		{
			return is_black_knife_animation_;
		}
	}

	public bool is_movie
	{
		get
		{
			return is_movie_;
		}
	}

	public AssetBundleSprite[] front_sprites
	{
		get
		{
			return front_sprites_;
		}
		set
		{
			front_sprites_ = value;
		}
	}

	public GameObject front_body
	{
		get
		{
			return front_body_;
		}
		set
		{
			front_body_ = value;
		}
	}

	public SpriteRenderer front_image
	{
		get
		{
			return front_image_;
		}
		set
		{
			front_image_ = value;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	public void end()
	{
	}

	public void set_tomoe_obj()
	{
		AssetBundleSprite[] array = sprites_;
		foreach (AssetBundleSprite assetBundleSprite in array)
		{
			assetBundleSprite.end();
			assetBundleSprite.sprite_renderer_.sprite = null;
			assetBundleSprite.transform.localPosition = Vector3.zero;
		}
		sprites_[0].load(tomoe_body_data_.path, tomoe_body_data_.file);
		sprites_[1].load(tomoe_hand_data_.path, tomoe_hand_data_.file);
		sprites_[0].transform.localPosition = new Vector3(tomoe_body_data_.pos_x, tomoe_body_data_.pos_y, -0.5f);
		sprites_[1].transform.localPosition = new Vector3(tomoe_hand_data_.pos_x, tomoe_hand_data_.pos_y, -1f);
	}

	public void play_tomoe_scroll()
	{
		if (enumerator_tomoe_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_tomoe_);
		}
		enumerator_tomoe_ = coroutine_tomoe_scroll();
		coroutineCtrl.instance.Play(enumerator_tomoe_);
	}

	public void stop_tomoe_scroll()
	{
		if (enumerator_tomoe_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_tomoe_);
			enumerator_tomoe_ = null;
		}
		body_active_ = false;
		is_tomoe_scroll_ = false;
		sprites_[0].end();
		sprites_[1].end();
		sprites_[0].sprite_renderer_.sprite = null;
		sprites_[1].sprite_renderer_.sprite = null;
		sprites_[0].transform.localPosition = Vector3.zero;
		sprites_[1].transform.localPosition = Vector3.zero;
	}

	private IEnumerator coroutine_tomoe_scroll()
	{
		is_tomoe_scroll_ = true;
		body_active_ = true;
		Vector3 speed_body = new Vector3(0f, tomoe_body_data_.speed_y, 0f);
		Vector3 speed_hand = new Vector3(0f, tomoe_hand_data_.speed_y, 0f);
		while (!bgCtrl.instance.is_scroll)
		{
			yield return null;
		}
		while (bgCtrl.instance.is_scroll)
		{
			sprites_[0].transform.localPosition += speed_body;
			sprites_[1].transform.localPosition += speed_hand;
			yield return null;
		}
		is_tomoe_scroll_ = false;
		enumerator_tomoe_ = null;
	}

	public void set_biru_obj(bool in_first)
	{
		for (int i = 0; i < sprites_.Length; i++)
		{
			if (in_first)
			{
				sprites_[i].load(biru_data_list_[i].path, biru_data_list_[i].file);
			}
			float x = ((!in_first) ? biru_data_list_[i].pos_x2 : biru_data_list_[i].pos_x1);
			sprites_[i].transform.localPosition = new Vector3(x, biru_data_list_[i].pos_y, biru_data_list_[i].pos_z);
		}
		biru_animation_ = AnimationSystem.Instance.PlayObject(0, 0, 115);
		float x2 = ((!in_first) ? biru_data_big.pos_x2 : biru_data_big.pos_x1);
		biru_animation_.transform.localPosition = new Vector3(x2, biru_data_big.pos_y, biru_data_big.pos_z) + anim_defaulut_pos_;
	}

	public void gs3_sc1_set_biru_obj()
	{
		for (int i = 0; i < gs3_sc1_biru_data_list_.Length; i++)
		{
			sprites_[i].load(gs3_sc1_biru_data_list_[i].path, gs3_sc1_biru_data_list_[i].file);
			sprites_[i].transform.localPosition = new Vector3(gs3_sc1_biru_data_list_[i].pos_x1, gs3_sc1_biru_data_list_[i].pos_y, gs3_sc1_biru_data_list_[i].pos_z);
		}
		sprites_[gs3_sc1_biru_data_list_.Length].load("/GS3/BG/", "bg215");
		sprites_[gs3_sc1_biru_data_list_.Length].transform.localPosition = new Vector3(0f, 1540f, -5f);
		body_active_ = true;
	}

	public void play_biru_scroll(int in_wait = 0)
	{
		if (enumerator_biru_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_biru_);
		}
		enumerator_biru_ = coroutine_biru_scroll(in_wait);
		coroutineCtrl.instance.Play(enumerator_biru_);
	}

	public void stop_biru_scroll()
	{
		if (enumerator_biru_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_biru_);
			enumerator_biru_ = null;
		}
		if (biru_animation_ != null)
		{
			biru_animation_.transform.localPosition = anim_defaulut_pos_;
			biru_animation_.Stop();
			biru_animation_ = null;
		}
		body_active_ = false;
		is_biru_scroll_ = false;
	}

	public void end_biru_scroll()
	{
		for (int i = 0; i < gs3_sc1_biru_data_list_.Length; i++)
		{
			sprites_[i].end();
			sprites_[i].sprite_renderer_.sprite = null;
			sprites_[i].transform.localPosition = Vector3.zero;
		}
		sprites_[gs3_sc1_biru_data_list_.Length].end();
		sprites_[gs3_sc1_biru_data_list_.Length].sprite_renderer_.sprite = null;
		sprites_[gs3_sc1_biru_data_list_.Length].transform.localPosition = Vector3.zero;
		body_active_ = false;
		is_biru_scroll_ = false;
	}

	private IEnumerator coroutine_biru_scroll(int in_wait)
	{
		is_biru_scroll_ = true;
		int time2 = 0;
		while (time2 < in_wait)
		{
			time2++;
			yield return null;
		}
		time2 = 0;
		body_active_ = true;
		while (time2 < 600)
		{
			for (int i = 0; i < biru_data_list_.Length; i++)
			{
				sprites_[i].transform.localPosition += new Vector3(biru_data_list_[i].speed, 0f, 0f);
			}
			biru_animation_.transform.localPosition += new Vector3(biru_data_big.speed, 0f, 0f);
			time2++;
			yield return null;
		}
		body_active_ = false;
		is_biru_scroll_ = false;
		enumerator_biru_ = null;
	}

	public void play_gs3_biru_scroll(bool scroll_out)
	{
		for (int i = 0; i < gs3_sc1_biru_data_list_.Length; i++)
		{
			Transform transform = sprites_[i].transform;
			float x = ((!scroll_out) ? 0f : ((!(transform.localPosition.x > 0f)) ? (-6.75f) : 6.75f));
			transform.localPosition += new Vector3(x, gs3_sc1_biru_data_list_[i].speed, 0f);
			float pos_x = gs3_sc1_biru_data_list_[i].pos_x2;
			if (0f - pos_x >= transform.localPosition.y)
			{
				float num = pos_x + transform.localPosition.y;
				transform.localPosition = new Vector3(transform.localPosition.x, pos_x + num, transform.localPosition.z);
			}
		}
		if (!scroll_out)
		{
			return;
		}
		Transform transform2 = sprites_[gs3_sc1_biru_data_list_.Length].transform;
		if (transform2.localPosition.y > 620f)
		{
			if (transform2.localPosition.y > 1000f)
			{
				transform2.localPosition -= Vector3.up * 4.5f * 2f;
			}
			else if (transform2.localPosition.y > 1200f)
			{
				transform2.localPosition -= Vector3.up * 4.5f * 3f;
			}
			else
			{
				transform2.localPosition -= Vector3.up * 4.5f;
			}
		}
		else
		{
			transform2.localPosition = new Vector3(0f, 620f, -5f);
		}
	}

	public void play_black_knife()
	{
		if (enumerator_black_knife_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_black_knife_);
			enumerator_black_knife_ = null;
		}
		enumerator_black_knife_ = coroutine_black_knife();
		coroutineCtrl.instance.Play(enumerator_black_knife_);
	}

	private IEnumerator coroutine_black_knife()
	{
		is_black_knife_animation_ = true;
		Vector3 speed = new Vector3(10f, 180f, 0f);
		Vector3 add_speed = new Vector3(3f, 3f, 0f);
		float end_pos_y = 1080f;
		float color_speed = 0.02f;
		black_knife_animation_ = AnimationSystem.Instance.PlayObject(0, 0, 122);
		black_knife_animation_.transform.localPosition = new Vector3(128f, 384f, 0f) + anim_defaulut_pos_;
		black_knife_animation_.Alpha = 1f;
		int time = 0;
		while (time < 60)
		{
			black_knife_animation_.transform.localPosition += speed * 0.01f;
			speed += add_speed;
			time++;
			yield return null;
		}
		play_movie();
		while (!(black_knife_animation_.transform.localPosition.y >= end_pos_y))
		{
			if (black_knife_animation_.Alpha > 0.5f)
			{
				black_knife_animation_.Alpha -= color_speed;
			}
			black_knife_animation_.transform.localPosition += speed * 0.01f;
			speed += add_speed;
			yield return null;
		}
		black_knife_animation_.transform.localPosition = anim_defaulut_pos_;
		black_knife_animation_.Alpha = 1f;
		black_knife_animation_.Stop();
		black_knife_animation_ = null;
		is_black_knife_animation_ = false;
		enumerator_black_knife_ = null;
	}

	public void play_movie()
	{
		if (enumerator_movie_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_movie_);
		}
		enumerator_movie_ = coroutine_movie();
		coroutineCtrl.instance.Play(enumerator_movie_);
	}

	private IEnumerator coroutine_movie()
	{
		is_movie_ = true;
		Color color_speed = new Color(0f, 0f, 0f, 0.01f);
		movie_renderer_.color = new Color(1f, 1f, 1f, 0f);
		movie_active_ = true;
		MovieAccessor.Instance.Play("film00", false);
		while (!(movie_renderer_.color.a > 1f))
		{
			movie_renderer_.color += color_speed;
			yield return null;
		}
		movie_renderer_.color = Color.white;
		while (MovieAccessor.Instance.Status == MovieAccessor.AccessorStatus.Playing)
		{
			yield return null;
		}
		movie_active_ = false;
		is_movie_ = false;
		enumerator_movie_ = null;
	}

	public void play_rain(bool in_play, int in_wait = 0)
	{
		if (enumerator_rain_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_rain_);
			enumerator_rain_ = null;
		}
		if (in_play)
		{
			enumerator_rain_ = coroutine_rain(in_wait);
			coroutineCtrl.instance.Play(enumerator_rain_);
		}
		else if (rain_animation_ != null)
		{
			rain_animation_.transform.localPosition = anim_defaulut_pos_;
			rain_animation_.Stop();
			rain_animation_ = null;
		}
	}

	private IEnumerator coroutine_rain(int in_wait)
	{
		int time = 0;
		while (time < in_wait)
		{
			time++;
			yield return null;
		}
		rain_animation_ = AnimationSystem.Instance.PlayObject(0, 0, 123);
		rain_animation_.transform.localPosition = new Vector3(0f, 0f, -10f) + anim_defaulut_pos_;
	}

	public void play_flash_knife()
	{
		flash_knife_animation_ = AnimationSystem.Instance.PlayObject(0, 0, 106);
	}

	public void stop_flash_knife()
	{
		if (flash_knife_animation_ != null)
		{
			flash_knife_animation_.transform.localPosition = anim_defaulut_pos_;
			flash_knife_animation_.Stop();
			flash_knife_animation_ = null;
		}
	}

	public void ChinamiBreakInit()
	{
		sprites_[0].load("/GS3/BG/", "bg003");
		sprites_[0].sprite_renderer_.enabled = false;
		sprites_[0].transform.localPosition = Vector3.zero;
		for (int i = 1; i < 3; i++)
		{
			sprites_[i].load("/GS3/BG/", e_op5_demo_spr_[i]);
			sprites_[i].sprite_renderer_.enabled = false;
			sprites_[i].transform.localPosition = Vector3.zero;
		}
		for (int j = 0; j < 2; j++)
		{
			front_sprites_[j].load("/GS3/BG/", e_op5_demo_spr_[j + 3]);
			front_sprites_[j].sprite_renderer_.enabled = false;
			front_sprites_[j].transform.localPosition = Vector3.zero;
		}
		body_active_ = true;
		front_body_.SetActive(true);
	}

	public void ChinamiBreak_ReloadBG01()
	{
		sprites_[0].end();
		sprites_[0].remove();
		sprites_[0].sprite_renderer_.sprite = null;
		sprites_[0].load("/GS3/BG/", e_op5_demo_spr_[0]);
		sprites_[0].sprite_renderer_.enabled = false;
		sprites_[0].transform.localPosition = Vector3.zero;
	}

	public void ChinamiBreak_ReloadBG04()
	{
		front_sprites_[0].end();
		front_sprites_[0].remove();
		front_sprites_[0].sprite_renderer_.sprite = null;
		front_sprites_[0].load("/GS3/BG/", "bg003");
		front_sprites_[0].sprite_renderer_.enabled = false;
		front_sprites_[0].transform.localPosition = Vector3.zero;
		ChinamiBreak_SetColor(0, Color.black);
		ChinamiBreak_SetBGPosY(0, -1380f);
		SetFrontDepth(0, -1f);
		ChinamiBreakBGEnabled(0, true, true);
	}

	public void ChinamiBreakEnd()
	{
		for (int i = 0; i < e_op5_demo_spr_.Length; i++)
		{
			sprites_[i].end();
			sprites_[i].remove();
			sprites_[i].sprite_renderer_.sprite = null;
			sprites_[i].transform.localPosition = Vector3.zero;
		}
		for (int j = 0; j < front_sprites_.Length; j++)
		{
			front_sprites_[j].end();
			front_sprites_[j].remove();
			front_sprites_[j].sprite_renderer_.sprite = null;
			front_sprites_[j].transform.localPosition = Vector3.zero;
		}
		body_active_ = false;
		front_body_.SetActive(false);
	}

	public void ChinamiBreak_BG_Scroll(float TinamiOfs0, float TinamiOfs1)
	{
		Vector3 localPosition = front_sprites_[0].transform.localPosition;
		localPosition.y = -338f + TinamiOfs0 * 1.8f * 5.2f;
		front_sprites_[0].transform.localPosition = localPosition;
		localPosition = front_sprites_[1].transform.localPosition;
		localPosition.y = -338f + TinamiOfs0 * 2f * 5.2f;
		front_sprites_[1].transform.localPosition = localPosition;
	}

	public void ChinamiBreak_SetBGPosX(int index, float x)
	{
		Vector3 localPosition = sprites_[index].transform.localPosition;
		localPosition.x = x;
		sprites_[index].transform.localPosition = localPosition;
	}

	public void ChinamiBreak_SetBGPosY(int index, float y)
	{
		Vector3 localPosition = front_sprites_[index].transform.localPosition;
		localPosition.y = y;
		front_sprites_[index].transform.localPosition = localPosition;
	}

	public void ChinamiBreak_AddBGPosY(int index, float y)
	{
		Vector3 localPosition = front_sprites_[index].transform.localPosition;
		localPosition.y += y;
		front_sprites_[index].transform.localPosition = localPosition;
	}

	public void ChinamiBreak_YureiInit()
	{
		float z = AnimationSystem.Instance.CharacterAnimationObject.transform.localPosition.z + 1f;
		SetFrontDepth(0, z);
		SetFrontAlpha(0, 0f);
		front_sprites_[0].sprite_renderer_.enabled = true;
		SetFrontDepth(1, z);
		SetFrontAlpha(1, 0f);
		front_sprites_[1].sprite_renderer_.enabled = true;
	}

	public void SetAlpha(int index, float alpha)
	{
		Color color = sprites_[index].sprite_renderer_.color;
		color.a = alpha;
		sprites_[index].sprite_renderer_.color = color;
	}

	public void SetFrontAlpha(int index, float alpha)
	{
		Color color = front_sprites_[index].sprite_renderer_.color;
		color.a = alpha;
		front_sprites_[index].sprite_renderer_.color = color;
	}

	public void SetDepth(int index, float z)
	{
		Vector3 localPosition = sprites_[index].transform.localPosition;
		localPosition.z = z;
		sprites_[index].transform.localPosition = localPosition;
	}

	public void SetFrontDepth(int index, float z)
	{
		Vector3 localPosition = front_sprites_[index].transform.localPosition;
		localPosition.z = z;
		front_sprites_[index].transform.localPosition = localPosition;
	}

	public void ChinamiBreakBGEnabled(int index, bool on, bool front = false)
	{
		if (front)
		{
			front_sprites_[index].sprite_renderer_.enabled = on;
		}
		else
		{
			sprites_[index].sprite_renderer_.enabled = on;
		}
	}

	public void ChinamiBreak_SetColor(int index, Color color)
	{
		front_sprites_[index].sprite_renderer_.color = color;
	}

	public void GodoFadeInit()
	{
		float num = -21f;
		for (int i = 0; i < 2; i++)
		{
			sprites_[i].load("/GS3/BG/", "bg800");
			sprites_[i].sprite_renderer_.enabled = true;
			SetDepth(i, num--);
		}
		body_active_ = true;
	}

	public void GodoFadeExit()
	{
		for (int i = 0; i < 2; i++)
		{
			SetAlpha(i, 1f);
			sprites_[i].end();
			sprites_[i].remove();
			sprites_[i].sprite_renderer_.sprite = null;
			sprites_[i].transform.localPosition = Vector3.zero;
		}
		body_active_ = false;
	}

	public void GS3LastSketchInit()
	{
		sprites_[0].load("/GS3/BG/", "bg57e");
		sprites_[0].sprite_renderer_.enabled = true;
		sprites_[0].transform.localPosition = new Vector3(0f, -540f, -1f);
		SetAlpha(0, 1f);
		body_active_ = true;
	}

	public void GS3LastSketchScroll()
	{
		float num = 5.625f;
		Vector3 localPosition = sprites_[0].transform.localPosition;
		localPosition.y -= num;
		if (localPosition.y <= -1620f)
		{
			localPosition.y = -1620f;
		}
		sprites_[0].transform.localPosition = localPosition;
	}

	public void GS3LastSketchExit()
	{
		sprites_[0].end();
		sprites_[0].remove();
		sprites_[0].sprite_renderer_.sprite = null;
		sprites_[0].transform.localPosition = Vector3.zero;
		body_active_ = false;
	}

	public void GS3YahariInit()
	{
		GSStatic.global_work_.status_flag |= 16u;
		recordListCtrl.instance.is_note_on = false;
		sprites_[0].load("/GS3/BG/", "bg54b");
		sprites_[0].sprite_renderer_.enabled = true;
		sprites_[0].transform.localPosition = new Vector3(0f, 0f, -1f);
		sprites_[0].transform.localScale = Vector3.one;
		SetAlpha(0, 1f);
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			sprites_[1].load("/GS3/BG/", "bg01c");
		}
		else if (GSStatic.global_work_.title == TitleId.GS2)
		{
			sprites_[1].load("/GS3/BG/", "bg00a");
		}
		else if (GSStatic.global_work_.title == TitleId.GS3)
		{
			sprites_[1].load("/GS3/BG/", "bg009");
		}
		sprites_[1].sprite_renderer_.enabled = true;
		sprites_[1].transform.localPosition = new Vector3(0f, 0f, -2f);
		sprites_[1].transform.localScale = Vector3.one;
		SetAlpha(1, 1f);
		Color color = front_image_.color;
		color.a = 0f;
		front_image_.color = color;
		front_image_.enabled = true;
		body_active_ = true;
	}

	public ushort GS3Yahari_screen_rotate(MessageWork pS)
	{
		ExplCharData explCharData = GSStatic.expl_char_work_.expl_char_data_[0];
		if ((int)(explCharData.vram_addr -= explCharData.para0) < 0)
		{
			return 0;
		}
		return 1;
	}

	public void GS3YahariFadeOutStart()
	{
		coroutineCtrl.instance.Play(GS3YahariFadeOut());
	}

	private IEnumerator GS3YahariFadeOut()
	{
		int cnt = 0;
		Color color = front_image_.color;
		while (cnt <= 96)
		{
			color.a = (float)cnt / 96f;
			front_image_.color = color;
			cnt++;
			yield return null;
		}
	}

	public void GS3YahariScale()
	{
		Vector3 localScale = sprites_[0].transform.localScale;
		localScale.x -= 0.005f;
		localScale.y -= 0.005f;
		if (localScale.x <= 0.5f)
		{
			localScale.x = 0.5f;
			localScale.y = 0.5f;
			GSStatic.expl_char_work_.expl_char_data_[0].para2 = 0;
		}
		sprites_[0].transform.localScale = localScale;
	}

	public void GS3YahariExit()
	{
		GSStatic.global_work_.status_flag &= 4294967279u;
		recordListCtrl.instance.is_note_on = true;
		Color color = front_image_.color;
		color.a = 0f;
		front_image_.color = color;
		front_image_.enabled = false;
		for (int i = 0; i < 2; i++)
		{
			sprites_[i].end();
			sprites_[i].remove();
			sprites_[i].sprite_renderer_.sprite = null;
			sprites_[i].transform.localPosition = Vector3.zero;
			sprites_[i].transform.localScale = Vector3.one;
			SetAlpha(i, 1f);
		}
		body_active_ = false;
	}
}
