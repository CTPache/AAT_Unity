using System.Collections;
using UnityEngine;

public class staffrollCtrl : MonoBehaviour
{
	private enum MainState
	{
		wait = 0,
		play = 1,
		stop = 2
	}

	private class staffroll_data
	{
		public int no;

		public string path;

		public string file;

		public staffroll_data(int in_no, string in_path, string in_file)
		{
			no = in_no;
			path = in_path;
			file = in_file;
		}
	}

	[SerializeField]
	private GameObject body_;

	[SerializeField]
	private SpriteRenderer background_sprite_;

	[SerializeField]
	private SpriteRenderer staffroll_sprite_;

	[SerializeField]
	private MeshRenderer render_image_;

	private Sprite[] staffroll_sprite_data_;

	private Sprite powder_sprite_data_;

	private RenderTexture render_texture_;

	private Material render_material_;

	private Mesh render_image_mesh_;

	private MainState main_state_;

	private IEnumerator enumerator_state_;

	private int current_index_;

	private readonly Vector2 SCREEN_SIZE = new Vector2(1920f, 1080f);

	private readonly staffroll_data[] staffroll_list_j_ = new staffroll_data[32]
	{
		new staffroll_data(0, "/staffroll/", "edst00"),
		dummy_data,
		new staffroll_data(1, "/staffroll/", "edst01"),
		new staffroll_data(2, "/staffroll/", "edst02"),
		dummy_data,
		new staffroll_data(3, "/staffroll/", "edst03"),
		new staffroll_data(4, "/staffroll/", "edst04"),
		new staffroll_data(5, "/staffroll/", "edst05"),
		dummy_data,
		new staffroll_data(6, "/staffroll/", "edst06"),
		new staffroll_data(7, "/staffroll/", "edst07"),
		dummy_data,
		new staffroll_data(8, "/staffroll/", "edst08"),
		new staffroll_data(9, "/staffroll/", "edst09"),
		dummy_data,
		new staffroll_data(10, "/staffroll/", "edst0a"),
		dummy_data,
		new staffroll_data(11, "/staffroll/", "edst0b"),
		new staffroll_data(12, "/staffroll/", "edst0c"),
		dummy_data,
		new staffroll_data(13, "/staffroll/", "edst0d"),
		new staffroll_data(14, "/staffroll/", "edst0e"),
		new staffroll_data(15, "/staffroll/", "edst0f"),
		dummy_data,
		new staffroll_data(16, "/staffroll/", "edst10"),
		new staffroll_data(17, "/staffroll/", "edst11"),
		new staffroll_data(18, "/staffroll/", "edst12"),
		new staffroll_data(21, "/staffroll/", "edst15"),
		dummy_data,
		new staffroll_data(19, "/staffroll/", "edst13"),
		new staffroll_data(20, "/staffroll/", "edst14"),
		dummy_data
	};

	private readonly staffroll_data[] staffroll_list_u_ = new staffroll_data[32]
	{
		new staffroll_data(0, "/staffroll/", "edst00u"),
		dummy_data,
		new staffroll_data(1, "/staffroll/", "edst01u"),
		new staffroll_data(2, "/staffroll/", "edst02u"),
		dummy_data,
		new staffroll_data(3, "/staffroll/", "edst03u"),
		new staffroll_data(4, "/staffroll/", "edst04u"),
		new staffroll_data(5, "/staffroll/", "edst05u"),
		dummy_data,
		new staffroll_data(6, "/staffroll/", "edst06u"),
		new staffroll_data(7, "/staffroll/", "edst07u"),
		dummy_data,
		new staffroll_data(8, "/staffroll/", "edst08u"),
		new staffroll_data(9, "/staffroll/", "edst09u"),
		dummy_data,
		new staffroll_data(10, "/staffroll/", "edst0au"),
		dummy_data,
		new staffroll_data(11, "/staffroll/", "edst0bu"),
		new staffroll_data(12, "/staffroll/", "edst0cu"),
		dummy_data,
		new staffroll_data(13, "/staffroll/", "edst0du"),
		new staffroll_data(14, "/staffroll/", "edst0eu"),
		new staffroll_data(15, "/staffroll/", "edst0fu"),
		dummy_data,
		new staffroll_data(16, "/staffroll/", "edst10u"),
		new staffroll_data(17, "/staffroll/", "edst11u"),
		new staffroll_data(18, "/staffroll/", "edst12u"),
		new staffroll_data(21, "/staffroll/", "edst15u"),
		dummy_data,
		new staffroll_data(19, "/staffroll/", "edst13u"),
		new staffroll_data(20, "/staffroll/", "edst14u"),
		dummy_data
	};

	private readonly staffroll_data[] staffroll_list_f_ = new staffroll_data[33]
	{
		new staffroll_data(0, "/staffroll/", "edst00f"),
		dummy_data,
		new staffroll_data(1, "/staffroll/", "edst01f"),
		new staffroll_data(2, "/staffroll/", "edst02f"),
		dummy_data,
		new staffroll_data(3, "/staffroll/", "edst03u"),
		new staffroll_data(4, "/staffroll/", "edst04u"),
		new staffroll_data(5, "/staffroll/", "edst05u"),
		dummy_data,
		new staffroll_data(6, "/staffroll/", "edst06f"),
		new staffroll_data(7, "/staffroll/", "edst07f"),
		dummy_data,
		new staffroll_data(8, "/staffroll/", "edst08f"),
		new staffroll_data(9, "/staffroll/", "edst09f"),
		dummy_data,
		new staffroll_data(10, "/staffroll/", "edst0af"),
		dummy_data,
		new staffroll_data(11, "/staffroll/", "edst0bf"),
		dummy_data,
		new staffroll_data(13, "/staffroll/", "edst0df"),
		new staffroll_data(14, "/staffroll/", "edst0ef"),
		new staffroll_data(15, "/staffroll/", "edst0ff"),
		new staffroll_data(22, "/staffroll/", "edst16f"),
		new staffroll_data(23, "/staffroll/", "edst17f"),
		dummy_data,
		new staffroll_data(16, "/staffroll/", "edst10f"),
		new staffroll_data(17, "/staffroll/", "edst11u"),
		new staffroll_data(18, "/staffroll/", "edst12f"),
		new staffroll_data(21, "/staffroll/", "edst15u"),
		dummy_data,
		new staffroll_data(19, "/staffroll/", "edst13f"),
		new staffroll_data(20, "/staffroll/", "edst14f"),
		dummy_data
	};

	private readonly staffroll_data[] staffroll_list_g_ = new staffroll_data[31]
	{
		new staffroll_data(0, "/staffroll/", "edst00g"),
		dummy_data,
		new staffroll_data(1, "/staffroll/", "edst01g"),
		new staffroll_data(2, "/staffroll/", "edst02u"),
		dummy_data,
		new staffroll_data(3, "/staffroll/", "edst03g"),
		new staffroll_data(4, "/staffroll/", "edst04g"),
		new staffroll_data(5, "/staffroll/", "edst05u"),
		dummy_data,
		new staffroll_data(6, "/staffroll/", "edst06g"),
		new staffroll_data(7, "/staffroll/", "edst07g"),
		dummy_data,
		new staffroll_data(8, "/staffroll/", "edst08g"),
		new staffroll_data(9, "/staffroll/", "edst09g"),
		dummy_data,
		new staffroll_data(10, "/staffroll/", "edst0au"),
		dummy_data,
		new staffroll_data(11, "/staffroll/", "edst0bg"),
		dummy_data,
		new staffroll_data(13, "/staffroll/", "edst0dg"),
		new staffroll_data(22, "/staffroll/", "edst16g"),
		new staffroll_data(23, "/staffroll/", "edst17g"),
		dummy_data,
		new staffroll_data(16, "/staffroll/", "edst10g"),
		new staffroll_data(17, "/staffroll/", "edst11u"),
		new staffroll_data(18, "/staffroll/", "edst12g"),
		new staffroll_data(21, "/staffroll/", "edst15u"),
		dummy_data,
		new staffroll_data(19, "/staffroll/", "edst13g"),
		new staffroll_data(20, "/staffroll/", "edst14g"),
		dummy_data
	};

	private readonly staffroll_data[] staffroll_list_k_ = new staffroll_data[34]
	{
		new staffroll_data(0, "/staffroll/", "edst00k"),
		dummy_data,
		new staffroll_data(1, "/staffroll/", "edst01k"),
		new staffroll_data(2, "/staffroll/", "edst02k"),
		dummy_data,
		new staffroll_data(3, "/staffroll/", "edst03k"),
		new staffroll_data(4, "/staffroll/", "edst04k"),
		new staffroll_data(5, "/staffroll/", "edst05k"),
		dummy_data,
		new staffroll_data(6, "/staffroll/", "edst06k"),
		new staffroll_data(7, "/staffroll/", "edst07k"),
		new staffroll_data(22, "/staffroll/", "edst16k"),
		dummy_data,
		new staffroll_data(8, "/staffroll/", "edst08k"),
		new staffroll_data(9, "/staffroll/", "edst09k"),
		new staffroll_data(23, "/staffroll/", "edst17k"),
		dummy_data,
		new staffroll_data(10, "/staffroll/", "edst0ak"),
		dummy_data,
		new staffroll_data(11, "/staffroll/", "edst0bk"),
		new staffroll_data(12, "/staffroll/", "edst0ck"),
		dummy_data,
		new staffroll_data(13, "/staffroll/", "edst0dk"),
		new staffroll_data(14, "/staffroll/", "edst0ek"),
		new staffroll_data(15, "/staffroll/", "edst0fk"),
		dummy_data,
		new staffroll_data(16, "/staffroll/", "edst10k"),
		new staffroll_data(17, "/staffroll/", "edst11k"),
		new staffroll_data(18, "/staffroll/", "edst12k"),
		new staffroll_data(21, "/staffroll/", "edst15k"),
		dummy_data,
		new staffroll_data(19, "/staffroll/", "edst13k"),
		new staffroll_data(20, "/staffroll/", "edst14k"),
		dummy_data
	};

	private readonly staffroll_data[] staffroll_list_s_ = new staffroll_data[30]
	{
		new staffroll_data(0, "/staffroll/", "edst00s"),
		dummy_data,
		new staffroll_data(1, "/staffroll/", "edst01s"),
		new staffroll_data(2, "/staffroll/", "edst02s"),
		dummy_data,
		new staffroll_data(3, "/staffroll/", "edst03s"),
		new staffroll_data(4, "/staffroll/", "edst04s"),
		new staffroll_data(5, "/staffroll/", "edst05s"),
		dummy_data,
		new staffroll_data(6, "/staffroll/", "edst06s"),
		new staffroll_data(7, "/staffroll/", "edst07s"),
		dummy_data,
		new staffroll_data(8, "/staffroll/", "edst08s"),
		new staffroll_data(9, "/staffroll/", "edst09s"),
		dummy_data,
		new staffroll_data(10, "/staffroll/", "edst0as"),
		dummy_data,
		new staffroll_data(11, "/staffroll/", "edst0bs"),
		new staffroll_data(12, "/staffroll/", "edst0cs"),
		dummy_data,
		new staffroll_data(13, "/staffroll/", "edst0ds"),
		dummy_data,
		new staffroll_data(16, "/staffroll/", "edst10s"),
		new staffroll_data(17, "/staffroll/", "edst11s"),
		new staffroll_data(18, "/staffroll/", "edst12s"),
		new staffroll_data(21, "/staffroll/", "edst15s"),
		dummy_data,
		new staffroll_data(19, "/staffroll/", "edst13s"),
		new staffroll_data(20, "/staffroll/", "edst14s"),
		dummy_data
	};

	private readonly staffroll_data[] staffroll_list_t_ = new staffroll_data[30]
	{
		new staffroll_data(0, "/staffroll/", "edst00t"),
		dummy_data,
		new staffroll_data(1, "/staffroll/", "edst01t"),
		new staffroll_data(2, "/staffroll/", "edst02t"),
		dummy_data,
		new staffroll_data(3, "/staffroll/", "edst03t"),
		new staffroll_data(4, "/staffroll/", "edst04t"),
		new staffroll_data(5, "/staffroll/", "edst05t"),
		dummy_data,
		new staffroll_data(6, "/staffroll/", "edst06t"),
		new staffroll_data(7, "/staffroll/", "edst07t"),
		dummy_data,
		new staffroll_data(8, "/staffroll/", "edst08t"),
		new staffroll_data(9, "/staffroll/", "edst09t"),
		dummy_data,
		new staffroll_data(10, "/staffroll/", "edst0at"),
		dummy_data,
		new staffroll_data(11, "/staffroll/", "edst0bt"),
		new staffroll_data(12, "/staffroll/", "edst0ct"),
		dummy_data,
		new staffroll_data(13, "/staffroll/", "edst0dt"),
		dummy_data,
		new staffroll_data(16, "/staffroll/", "edst10t"),
		new staffroll_data(17, "/staffroll/", "edst11t"),
		new staffroll_data(18, "/staffroll/", "edst12t"),
		new staffroll_data(21, "/staffroll/", "edst15t"),
		dummy_data,
		new staffroll_data(19, "/staffroll/", "edst13t"),
		new staffroll_data(20, "/staffroll/", "edst14t"),
		dummy_data
	};

	private static staffroll_data dummy_data = new staffroll_data(-1, string.Empty, string.Empty);

	public static staffrollCtrl instance { get; private set; }

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

	private staffroll_data[] staffroll_list_
	{
		get
		{
			switch (GSStatic.global_work_.language)
			{
			case Language.JAPAN:
				return staffroll_list_j_;
			case Language.USA:
				return staffroll_list_u_;
			case Language.FRANCE:
				return staffroll_list_f_;
			case Language.GERMAN:
				return staffroll_list_g_;
			case Language.KOREA:
				return staffroll_list_k_;
			case Language.CHINA_S:
				return staffroll_list_s_;
			case Language.CHINA_T:
				return staffroll_list_t_;
			default:
				return staffroll_list_j_;
			}
		}
	}

	private void Awake()
	{
		instance = this;
	}

	public void init()
	{
		load();
		main_state_ = MainState.wait;
		current_index_ = 0;
		staffroll_sprite_.color = new Color(1f, 1f, 1f, 0f);
		body_active_ = true;
		enumerator_state_ = coroutine_state();
		coroutineCtrl.instance.Play(enumerator_state_);
	}

	public void end()
	{
		if (enumerator_state_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_state_);
			enumerator_state_ = null;
		}
		body_active_ = false;
		background_sprite_.sprite = null;
		staffroll_sprite_.sprite = null;
		render_image_.GetComponent<MeshFilter>().mesh = null;
		render_image_mesh_ = null;
		render_image_.material.mainTexture = null;
		render_material_ = null;
		powder_sprite_data_ = null;
		for (int i = 0; i < staffroll_sprite_data_.Length; i++)
		{
			staffroll_sprite_data_[i] = null;
		}
	}

	public void play()
	{
		main_state_ = MainState.play;
	}

	public void stop()
	{
		main_state_ = MainState.stop;
	}

	private IEnumerator coroutine_state()
	{
		bool is_chain = false;
		while (current_index_ < staffroll_list_.Length)
		{
			switch (main_state_)
			{
			case MainState.play:
				if (staffroll_list_[current_index_].no != -1)
				{
					body_active_ = true;
					if (!is_chain)
					{
						is_chain = true;
						yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play_coroutine(30, true, Color.black));
						int timer = 0;
						while (timer < 30)
						{
							timer++;
							yield return null;
						}
					}
					yield return coroutineCtrl.instance.Play(coroutine_play());
				}
				else
				{
					is_chain = false;
					body_active_ = false;
				}
				current_index_++;
				main_state_ = MainState.wait;
				break;
			case MainState.stop:
				if (staffroll_sprite_.sprite != null)
				{
					coroutineCtrl.instance.Play(coroutine_staffroll_fade(15, false));
					yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play_coroutine(15, false, Color.black));
					staffroll_sprite_.sprite = null;
				}
				main_state_ = MainState.wait;
				break;
			}
			yield return null;
		}
		enumerator_state_ = null;
		end();
	}

	private IEnumerator coroutine_play()
	{
		yield return coroutineCtrl.instance.Play(coroutine_powder_effect());
		int timer = 0;
		while (timer <= 30)
		{
			timer++;
			yield return null;
		}
		staffroll_sprite_.sprite = staffroll_sprite_data_[current_index_];
		coroutineCtrl.instance.Play(coroutine_staffroll_fade(30, true));
		yield return coroutineCtrl.instance.Play(coroutine_blow());
	}

	private IEnumerator coroutine_powder_effect()
	{
		Vector2 powder_pos = new Vector2(-960f, -200f);
		Vector2 powder_interval = new Vector2(70f, 200f);
		int current_line = 0;
		while (current_line < 3)
		{
			for (int i = 0; i < 20; i++)
			{
				int rnd = Random.Range(1, 4);
				for (int j = 0; j < rnd; j++)
				{
					int pos_random_x = Random.Range(-20, 20);
					int pos_random_y = Random.Range(-100, 100);
					staffrollCtrl obj = this;
					float x = powder_pos.x;
					Vector2 sCREEN_SIZE = SCREEN_SIZE;
					int x2 = (int)(x + sCREEN_SIZE.x * 0.65f) + pos_random_x;
					float y = powder_pos.y;
					Vector2 sCREEN_SIZE2 = SCREEN_SIZE;
					obj.DropPowder(x2, (int)(y + sCREEN_SIZE2.y * 0.5f) + pos_random_y);
				}
				powder_pos.x += powder_interval.x;
				yield return null;
			}
			current_line++;
			powder_pos.y += powder_interval.y;
			powder_interval.x *= -1f;
		}
	}

	private void DropPowder(int x, int y)
	{
		Sprite sprite = powder_sprite_data_;
		Rect sourceRect = new Rect(0f, 0f, 1f, 1f);
		Rect screenRect = new Rect((float)x + sprite.rect.width * -0.5f, (float)y + sprite.rect.height * -0.5f, sprite.rect.width, sprite.rect.height);
		GL.PushMatrix();
		GL.LoadPixelMatrix(0f, render_texture_.width, render_texture_.height, 0f);
		Graphics.SetRenderTarget(render_texture_);
		Graphics.DrawTexture(screenRect, sprite.texture, sourceRect, 0, 0, 0, 0, Color.white, render_material_);
		Graphics.SetRenderTarget(null);
		GL.PopMatrix();
	}

	private IEnumerator coroutine_staffroll_fade(int fade_time, bool fade_type)
	{
		Color fade_speed = new Color(0f, 0f, 0f, 1f / (float)fade_time);
		fade_speed.a = ((!fade_type) ? (0f - fade_speed.a) : fade_speed.a);
		for (int timer = 0; timer < fade_time; timer++)
		{
			staffroll_sprite_.color += fade_speed;
			if (staffroll_sprite_.color.a < 0f || staffroll_sprite_.color.a > 1f)
			{
				break;
			}
			yield return null;
		}
		staffroll_sprite_.color = new Color(1f, 1f, 1f, (!fade_type) ? 0f : 1f);
	}

	private IEnumerator coroutine_blow()
	{
		for (int i = 0; i < 30; i++)
		{
			float t = (float)i / 30f;
			render_image_.material.color = new Color(1f, 1f, 1f, 1f - t);
			yield return null;
		}
		render_image_.enabled = false;
		yield return null;
		Graphics.SetRenderTarget(render_texture_);
		GL.Begin(4);
		GL.Clear(false, true, Color.clear);
		GL.End();
		Graphics.SetRenderTarget(null);
		render_image_.material.color = Color.white;
		render_image_.enabled = true;
		yield return null;
	}

	private Mesh CreateQuadMesh(Vector2 size, Rect uv_rect)
	{
		Mesh mesh = new Mesh();
		Vector2 vector = size * 0.5f;
		mesh.vertices = new Vector3[4]
		{
			new Vector3(0f - vector.x, 0f - vector.y, 0f),
			new Vector3(vector.x, 0f - vector.y, 0f),
			new Vector3(0f - vector.x, vector.y, 0f),
			new Vector3(vector.x, vector.y, 0f)
		};
		mesh.uv = new Vector2[4]
		{
			new Vector2(uv_rect.x, uv_rect.y),
			new Vector2(uv_rect.x + uv_rect.width, uv_rect.y),
			new Vector2(uv_rect.x, uv_rect.y + uv_rect.height),
			new Vector2(uv_rect.x + uv_rect.width, uv_rect.y + uv_rect.height)
		};
		mesh.triangles = new int[6] { 0, 1, 2, 1, 2, 3 };
		return mesh;
	}

	private void load()
	{
		staffroll_sprite_data_ = new Sprite[staffroll_list_.Length];
		AssetBundle assetBundle;
		for (int i = 0; i < staffroll_sprite_data_.Length; i++)
		{
			if (staffroll_list_[i].no != -1)
			{
				string path = staffroll_list_[i].path;
				string file = staffroll_list_[i].file;
				assetBundle = AssetBundleCtrl.instance.load(path, file);
				staffroll_sprite_data_[i] = assetBundle.LoadAllAssets<Sprite>()[0];
			}
			else
			{
				staffroll_sprite_data_[i] = null;
			}
		}
		string in_path = "/GS1/3D/eff/";
		string in_name = "eff021";
		assetBundle = AssetBundleCtrl.instance.load(in_path, in_name);
		powder_sprite_data_ = assetBundle.LoadAllAssets<Sprite>()[0];
		string in_path2 = "/GS1/minigame/";
		string in_name2 = "frame05";
		assetBundle = AssetBundleCtrl.instance.load(in_path2, in_name2);
		background_sprite_.sprite = assetBundle.LoadAllAssets<Sprite>()[0];
		render_material_ = new Material(Shader.Find("Sprites/Default"));
		render_texture_ = new RenderTexture(2048, 2048, 0);
		render_image_.material.mainTexture = render_texture_;
		render_image_.material.color = Color.white;
		GL.PushMatrix();
		Graphics.SetRenderTarget(render_texture_);
		GL.Begin(4);
		GL.Clear(false, true, Color.clear);
		GL.End();
		GL.PopMatrix();
		Vector2 sCREEN_SIZE = SCREEN_SIZE;
		float y = 1f - sCREEN_SIZE.y / (float)render_texture_.height;
		Vector2 sCREEN_SIZE2 = SCREEN_SIZE;
		float width = sCREEN_SIZE2.x / (float)render_texture_.width;
		Vector2 sCREEN_SIZE3 = SCREEN_SIZE;
		Rect uv_rect = new Rect(0f, y, width, sCREEN_SIZE3.y / (float)render_texture_.height);
		render_image_mesh_ = CreateQuadMesh(SCREEN_SIZE, uv_rect);
		render_image_.GetComponent<MeshFilter>().mesh = render_image_mesh_;
	}
}
