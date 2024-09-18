using Steamworks;
using UnityEngine;

public class SteamScriptBase : MonoBehaviour
{
	private static GameObject s_gameObject_;

	private static GameObject s_gameObject
	{
		get
		{
			if (s_gameObject_ == null)
			{
				s_gameObject_ = new GameObject("SteamScripts");
			}
			return s_gameObject_;
		}
	}

	protected CGameID game_id { get; private set; }

	protected virtual Rect gui_layout_area
	{
		get
		{
			return new Rect(0f, 0f, 400f, 100f);
		}
	}

	protected static T AddSteamScriptComponent<T>() where T : SteamScriptBase
	{
		return s_gameObject.AddComponent<T>();
	}

	private void Awake()
	{
		if (s_gameObject_ == null)
		{
			s_gameObject_ = base.gameObject;
		}
		if (base.transform.parent == null)
		{
			SteamManager steamManager = Object.FindObjectOfType<SteamManager>();
			if (steamManager == null)
			{
				Object.DontDestroyOnLoad(base.gameObject);
			}
			else
			{
				base.transform.parent = steamManager.transform;
			}
		}
		AwakeSteam();
	}

	private void Start()
	{
		if (SteamManager.Initialized)
		{
			game_id = new CGameID(SteamUtils.GetAppID());
			StartSteam();
		}
	}

	private void OnEnable()
	{
		OnActive();
	}

	private void OnDestroy()
	{
		OnDeactive();
	}

	protected virtual void OnActive()
	{
		if (s_gameObject_ == null)
		{
			s_gameObject_ = base.gameObject;
		}
	}

	protected virtual void OnDeactive()
	{
		if (!(s_gameObject_ != this))
		{
			s_gameObject_ = null;
		}
	}

	private void OnGUI()
	{
		if (SteamManager.Initialized)
		{
			Rect rect = gui_layout_area;
			OnGUISteam((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
		}
	}

	protected void ReStartSteam()
	{
		if (SteamManager.Initialized)
		{
			StartSteam();
		}
	}

	protected virtual void AwakeSteam()
	{
	}

	protected virtual void StartSteam()
	{
	}

	protected virtual void OnGUISteam(int x, int y, int w, int h)
	{
	}

	public virtual void Init()
	{
	}
}
