using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MovieController : MonoBehaviour
{
	public enum PlayState
	{
		Paused = 0,
		Playback = 1,
		Rewinding = 2,
		FastForwarding = 3
	}

	[SerializeField]
	private RawImage screen;

	[SerializeField]
	private Camera use_camera_;

	private AssetBundle bundle;

	private bool isPausing;

	private const float playback_speed = 1f;

	private const float rewind_speed = 6f;

	private const float fast_speed = 6f;

	private float now_speed;

	public IEnumerator play_enumerator;

	public Coroutine play_coroutine;

	public bool auto_play;

	public bool loop_play = true;

	public bool is_play;

	private int frame_rate = 4;

	private float speed_rate = 1f;

	private int start_frame = 1;

	private int end_frame = 710;

	private int continue_frame;

	public int auto_state;

	public bool auto_unload = true;

	public bool one_step;

	private Dictionary<int, Texture2D> sprite_list_ = new Dictionary<int, Texture2D>();

	private List<Vector3> noise_pos = new List<Vector3>
	{
		new Vector3(0f, 510f, -20f),
		new Vector3(0f, 68f, -20f),
		new Vector3(0f, 34f, -20f),
		new Vector3(0f, 0f, -20f),
		new Vector3(0f, -510f, -20f)
	};

	public int Frame { get; private set; }

	public PlayState State { get; private set; }

	public bool video_pause { get; set; }

	private Dictionary<int, Texture2D> sprite_list
	{
		get
		{
			return sprite_list_;
		}
	}

	public long Cinema_get_frame()
	{
		return Frame;
	}

	public bool Cinema_check_end()
	{
		return is_play;
	}

	public void InitData(bool auto)
	{
		Frame = 0;
		State = ((!auto) ? PlayState.Playback : State);
		isPausing = auto && isPausing;
		auto_play = (auto ? true : false);
		loop_play = !auto;
		is_play = false;
		frame_rate = 2;
		speed_rate = 1f;
		start_frame = 1;
		end_frame = 1420;
		auto_state = 0;
		one_step = false;
	}

	public void end()
	{
		Stop();
	}

	public void SetSandStorm()
	{
		InitData(true);
		screen.rectTransform.sizeDelta = new Vector2(1440f, 1080f);
		use_camera_.clearFlags = CameraClearFlags.Color;
		AssetBundle assetBundle = AssetBundleCtrl.instance.load("/GS1/moviescriptable/", "videonoise");
		ConfrontWithMovie.instance.SetScreenActivate();
		auto_play = true;
		now_speed = 1f;
		frame_rate = 1;
		isPausing = false;
		play_enumerator = PlaySandStorm(assetBundle);
		coroutineCtrl.instance.Play(play_enumerator);
	}

	public void SetVideo()
	{
		InitData(true);
		screen.rectTransform.sizeDelta = new Vector2(1800f, 1125f);
		use_camera_.clearFlags = CameraClearFlags.Depth;
		AssetBundle taiho_bundle = AssetBundleCtrl.instance.load("/GS1/moviescriptable/", "film01_confront", false);
		ConfrontWithMovie.instance.SetScreenActivate();
		auto_play = true;
		now_speed = 1f;
		frame_rate = 1;
		play_enumerator = PlayTaihokun(taiho_bundle);
		coroutineCtrl.instance.Play(play_enumerator);
	}

	public void Play(AssetBundle bundle)
	{
		screen.rectTransform.sizeDelta = new Vector2(1440f, 1080f);
		use_camera_.clearFlags = CameraClearFlags.Color;
		frame_rate = 2;
		play_enumerator = PlaySecurityCamera(bundle);
		coroutineCtrl.instance.Play(play_enumerator);
		State = PlayState.Playback;
		isPausing = false;
		video_pause = false;
	}

	public void Stop()
	{
		Cinema.cinema_end();
		State = PlayState.Paused;
		if (play_enumerator != null)
		{
			coroutineCtrl.instance.Stop(play_enumerator);
		}
		is_play = false;
		play_enumerator = null;
	}

	public void Clear()
	{
		screen.texture = null;
		AssetBundleCtrl.instance.remove("/GS1/moviescriptable/", "videonoise");
		AssetBundleCtrl.instance.remove("/GS1/moviescriptable/", "film01_confront");
		sprite_list.Clear();
	}

	public void SetAutoPlayStatus(int type)
	{
		auto_state = type;
		isPausing = false;
		long num = (long)type & 0xFL;
		if (num >= 1 && num <= 5)
		{
			switch (num - 1)
			{
			case 0L:
				goto IL_0060;
			case 1L:
				goto IL_007e;
			case 2L:
				goto IL_00b6;
			case 3L:
				goto IL_00c2;
			case 4L:
				goto IL_00e0;
			}
		}
		if (num != 16384 && num == 32768)
		{
			loop_play = true;
		}
		goto IL_0129;
		IL_00c2:
		now_speed = 1f * speed_rate;
		State = PlayState.Rewinding;
		goto IL_0129;
		IL_00e0:
		if (play_enumerator != null)
		{
			coroutineCtrl.instance.Stop(play_enumerator);
			is_play = false;
			play_enumerator = null;
		}
		Cinema.cinema_end();
		goto IL_0129;
		IL_0060:
		now_speed = 1f * speed_rate;
		State = PlayState.Playback;
		goto IL_0129;
		IL_007e:
		is_play = false;
		if (bundle != null)
		{
			bundle.Unload(true);
		}
		ConfrontWithMovie.instance.CheckEndPlay();
		Cinema.cinema_end();
		goto IL_0129;
		IL_0129:
		if (((ulong)type & 0x8000uL) != 0)
		{
			loop_play = true;
		}
		if (((ulong)type & 0x4000uL) != 0)
		{
			auto_unload = false;
		}
		if (((ulong)type & 0x2000uL) == 0)
		{
			return;
		}
		one_step = true;
		if (State == PlayState.Rewinding)
		{
			continue_frame = Frame - 1;
			if (continue_frame < start_frame)
			{
				continue_frame = start_frame;
			}
		}
		else if (State == PlayState.Playback)
		{
			continue_frame = Frame + 1;
			if (continue_frame > end_frame)
			{
				continue_frame = end_frame;
			}
		}
		return;
		IL_00b6:
		isPausing = true;
		goto IL_0129;
	}

	public void SetAutoPlayFrame(int frame)
	{
		continue_frame = frame;
	}

	public void SetAutoPlaySpeed(int percent)
	{
		speed_rate = percent / 100;
	}

	public void SetAutoPlayStartFrame(int frame)
	{
		start_frame = frame;
	}

	public void SetAutoPlayEndFrame(int frame)
	{
		if (frame != 0)
		{
			end_frame = ((frame < 1420) ? frame : 1420);
		}
	}

	public IEnumerator PlaySecurityCamera(AssetBundle bundle)
	{
		ConfrontWithMovie instance = ConfrontWithMovie.instance;
		is_play = true;
		int idx2 = start_frame;
		float time = 0f;
		if (auto_play)
		{
			if (continue_frame != 0 && continue_frame < end_frame)
			{
				idx2 = continue_frame;
				continue_frame = 0;
			}
			else if (continue_frame != 0)
			{
				end_frame = continue_frame;
			}
		}
		screen.gameObject.SetActive(true);
		int sprite_num2 = 0;
		sprite_num2 = 1420;
		while (true)
		{
			if (isPausing)
			{
				yield return null;
				continue;
			}
			if (continue_frame != 0)
			{
				idx2 = continue_frame;
				continue_frame = 0;
			}
			time += now_speed;
			yield return null;
			if ((float)frame_rate < time || one_step)
			{
				time = 0f;
				if (State == PlayState.Rewinding)
				{
					idx2--;
				}
				else
				{
					idx2++;
					if (video_pause && sprite_num2 < idx2)
					{
						idx2 = sprite_num2;
					}
				}
				if (auto_play)
				{
					if (idx2 > end_frame && State != PlayState.Rewinding)
					{
						if (!loop_play)
						{
							break;
						}
						idx2 = start_frame;
					}
					else if (idx2 < start_frame && State == PlayState.Rewinding)
					{
						if (!loop_play)
						{
							break;
						}
						idx2 = end_frame;
					}
				}
				if (sprite_num2 < idx2)
				{
					idx2 = 1;
					break;
				}
				if (idx2 < 1)
				{
					idx2 = 1;
				}
			}
			string name2 = string.Empty;
			name2 = idx2.ToString("D5");
			screen.texture = bundle.LoadAsset(name2) as Texture2D;
			Frame = idx2;
			GSStatic.cinema_work_.frame_set = (short)idx2;
			if (one_step)
			{
				one_step = false;
				SetAutoPlayStatus(3);
			}
			instance.collision_player.CollisionUpdate();
		}
		is_play = false;
		if (bundle != null && auto_unload)
		{
			bundle.Unload(true);
			bundle = null;
			screen.texture = null;
		}
		play_enumerator = null;
	}

	public IEnumerator PlaySandStorm(AssetBundle bundle)
	{
		is_play = true;
		int idx = start_frame;
		float time = 0f;
		if (auto_play)
		{
			if (continue_frame != 0 && continue_frame < end_frame)
			{
				idx = continue_frame;
				continue_frame = 0;
			}
			else if (continue_frame != 0)
			{
				end_frame = continue_frame;
			}
			yield return null;
			if (State == PlayState.Rewinding)
			{
				continue_frame = idx;
				idx = end_frame;
				end_frame = continue_frame;
				start_frame = continue_frame;
				continue_frame = 0;
			}
		}
		screen.gameObject.SetActive(true);
		List<string> sprite_names = (from name in bundle.GetAllAssetNames()
			orderby name
			select name).ToList();
		sprite_list.Clear();
		while (true)
		{
			if (isPausing)
			{
				yield return null;
				continue;
			}
			if (!sprite_list.ContainsKey(idx))
			{
				sprite_list.Add(idx, bundle.LoadAsset<Texture2D>(sprite_names[idx]));
			}
			screen.texture = sprite_list[idx];
			time += now_speed;
			yield return null;
			if ((float)frame_rate < time)
			{
				time = 0f;
				if (auto_play)
				{
					if (idx >= end_frame && State != PlayState.Rewinding)
					{
						if (!loop_play)
						{
							break;
						}
						idx = start_frame;
					}
					else if (idx <= start_frame && State == PlayState.Rewinding)
					{
						if (!loop_play)
						{
							break;
						}
						idx = end_frame;
					}
				}
				idx = ((State != PlayState.Rewinding) ? (idx + 1) : (idx - 1));
				if (sprite_names.Count - 1 < idx)
				{
					idx = 0;
				}
				else if (idx < 0)
				{
					idx = 0;
				}
			}
			Frame = idx;
		}
		is_play = false;
	}

	public IEnumerator PlayTaihokun(AssetBundle taiho_bundle)
	{
		ConfrontWithMovie instance = ConfrontWithMovie.instance;
		is_play = true;
		int idx = start_frame;
		float time = 0f;
		if (auto_play)
		{
			if (continue_frame != 0 && continue_frame < end_frame)
			{
				idx = continue_frame;
				continue_frame = 0;
			}
			else if (continue_frame != 0)
			{
				end_frame = continue_frame;
			}
			yield return null;
			if (State == PlayState.Rewinding)
			{
				continue_frame = idx;
				idx = end_frame;
				end_frame = continue_frame;
				start_frame = continue_frame;
				continue_frame = 0;
			}
		}
		screen.gameObject.SetActive(true);
		List<string> sprite_names = (from name in taiho_bundle.GetAllAssetNames()
			orderby name
			select name).ToList();
		sprite_list.Clear();
		while (true)
		{
			if (isPausing)
			{
				yield return null;
				continue;
			}
			if (!sprite_list.ContainsKey(idx))
			{
				sprite_list.Add(idx, taiho_bundle.LoadAsset<Texture2D>(sprite_names[idx]));
			}
			screen.texture = sprite_list[idx];
			time += now_speed;
			yield return null;
			if ((float)frame_rate < time)
			{
				time = 0f;
				if (auto_play)
				{
					if (idx >= end_frame && State != PlayState.Rewinding)
					{
						if (!loop_play)
						{
							break;
						}
						idx = start_frame;
					}
					else if (idx <= start_frame && State == PlayState.Rewinding)
					{
						if (!loop_play)
						{
							break;
						}
						idx = end_frame;
					}
				}
				idx = ((State != PlayState.Rewinding) ? (idx + 1) : (idx - 1));
				if (sprite_names.Count - 1 < idx)
				{
					if (!auto_play || ConfrontWithMovie.instance.IsDetailing)
					{
						float frame = 0f;
						float wait = 2f;
						while (true)
						{
							frame += Time.deltaTime;
							if (frame > wait)
							{
								break;
							}
							yield return null;
						}
					}
					idx = 0;
					if (!loop_play && !ConfrontWithMovie.instance.IsDetailing)
					{
						break;
					}
				}
				else if (idx < 0)
				{
					idx = 0;
				}
			}
			Frame = idx;
		}
		is_play = false;
	}

	public IEnumerator NoiseAnimation()
	{
		List<KeyValuePair<GameObject, RawImage>> noise_list = ConfrontWithMovie.instance.noise_list;
		foreach (KeyValuePair<GameObject, RawImage> item in noise_list)
		{
			item.Key.SetActive(true);
		}
		int count = 0;
		while (true)
		{
			Vector3 pos = noise_list[count].Key.transform.localPosition;
			noise_list[count].Key.transform.localPosition = new Vector3(pos.x, pos.y - 30f, pos.z);
			noise_list[count].Value.uvRect = new Rect(Random.Range(0f, 1f), 0f, 1f, 1f);
			if (noise_list[count].Key.transform.localPosition.y <= -680f)
			{
				noise_list[count].Key.transform.localPosition = new Vector3(0f, 680f, -20f);
			}
			count++;
			if (noise_list.Count <= count)
			{
				count = 0;
			}
			yield return null;
		}
	}

	public void StopAllObject()
	{
		ConfrontWithMovie instance = ConfrontWithMovie.instance;
		foreach (KeyValuePair<GameObject, RawImage> item in instance.sprite_list)
		{
			item.Value.enabled = true;
		}
		StopNoise();
	}

	public void StopNoise()
	{
		int num = 0;
		foreach (KeyValuePair<GameObject, RawImage> item in ConfrontWithMovie.instance.noise_list)
		{
			item.Key.transform.localPosition = noise_pos[num];
			item.Key.SetActive(false);
			num++;
		}
	}

	public void ResetScreenTexture()
	{
		screen.texture = null;
	}
}
