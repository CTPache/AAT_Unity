using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MovieAccessor : MonoBehaviour
{
	public enum AccessorStatus
	{
		NotInitialized = 0,
		StandBy = 1,
		Playing = 2,
		EndOfStream = 3
	}

	private VideoPlayer videoPlayer;

	[SerializeField]
public RawImage image_;

	public bool keepAsLastFrame_;

	private AssetBundle bundle;

	[SerializeField]
public MeshRenderer switch_screen_body;

	[SerializeField]
public Camera use_camera;

	public int end_frame;

	public RawImage image
	{
		get
		{
			return image_;
		}
		set
		{
			image_ = value;
		}
	}

	public static MovieAccessor Instance { get; private set; }

	public AccessorStatus Status { get; private set; }

	public float CameraDepth
	{
		get
		{
			return use_camera.depth;
		}
		set
		{
			use_camera.depth = value;
		}
	}

	private bool ChildrenActive
	{
		set
		{
			foreach (Transform item in base.transform)
			{
				item.gameObject.SetActive(value);
			}
		}
	}

	public bool SetLoop
	{
		set
		{
			if (videoPlayer != null)
			{
				videoPlayer.isLooping = value;
			}
		}
	}

	public void end()
	{
		if (Status != 0 && Status != AccessorStatus.StandBy)
		{
			Unload();
		}
	}

	public void Play(string movieName, bool keepLastFrame)
	{
		image.gameObject.SetActive(true);
		BuildComponents();
		if (Status != AccessorStatus.StandBy)
		{
			Unload();
		}
		keepAsLastFrame_ = keepLastFrame;
		use_camera.enabled = true;
		ChildrenActive = true;
		string path = ToResourcePath(movieName);
		videoPlayer.clip = LoadClip(path, movieName);
		videoPlayer.Play();
		videoPlayer.isLooping = false;
		Status = AccessorStatus.Playing;
	}

	private void FixedUpdate()
	{
		Process();
	}

	private void Process()
	{
		if (Status == AccessorStatus.Playing && !videoPlayer.isPlaying)
		{
			Status = AccessorStatus.EndOfStream;
			if (!keepAsLastFrame_)
			{
				videoPlayer.Stop();
				Unload();
			}
		}
	}

	private VideoClip LoadClip(string path, string movieName)
	{
		byte[] binary = decryptionCtrl.instance.load(path);
		bundle = AssetBundle.LoadFromMemory(binary);
		return bundle.LoadAsset<VideoClip>(movieName);
	}

	public void Unload()
	{
		if (videoPlayer != null)
		{
			videoPlayer.clip = null;
		}
		if (bundle != null)
		{
			bundle.Unload(true);
			bundle = null;
		}
		use_camera.enabled = false;
		ChildrenActive = false;
		Status = AccessorStatus.StandBy;
		image.gameObject.SetActive(false);
	}

	private string ToResourcePath(string movieName)
	{
		string text = ".unity3d";
		return Application.streamingAssetsPath + "/GS1/Movie/" + movieName + text;
	}

	public void ForcedStop()
	{
		if (videoPlayer != null)
		{
			videoPlayer.Stop();
			Unload();
		}
	}

	public void Awake()
	{
		Instance = this;
	}

	private void BuildComponents()
	{
		if (Status == AccessorStatus.NotInitialized)
		{
			videoPlayer = use_camera.gameObject.AddComponent<VideoPlayer>();
			videoPlayer.source = VideoSource.VideoClip;
			videoPlayer.playOnAwake = false;
			videoPlayer.waitForFirstFrame = false;
			videoPlayer.playbackSpeed = 1f;
			videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
			videoPlayer.isLooping = false;
			videoPlayer.targetCameraAlpha = 1f;
			videoPlayer.aspectRatio = VideoAspectRatio.FitVertically;
			videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
			Object.Destroy(switch_screen_body);
			Status = AccessorStatus.StandBy;
		}
	}
}
