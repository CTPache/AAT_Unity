using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationCameraManager : MonoBehaviour
{
	private static AnimationCameraManager instance_;

	[SerializeField]
	private Camera root_camera_;

	[SerializeField]
	private RenderTexture root_texture_;

	[SerializeField]
	private Camera irregular_depth_camera_;

	[SerializeField]
	private RenderTexture irregular_depth_texture_;

	[SerializeField]
	private RawImage irregular_image_;

	[SerializeField]
	private Camera canvas_camera_;

	[SerializeField]
	private Camera main_camera_;

	[SerializeField]
	private Transform quake_targets_;

	[SerializeField]
	private Transform quake_targets_mapicon_;

	[SerializeField]
	private SpriteRenderer fade_targets_;

	[SerializeField]
	private List<SpriteRenderer> parts_sprite_;

	[SerializeField]
	private Transform instance_parent_;

	[SerializeField]
	private GameObject demo_;

	[SerializeField]
	private SpriteRenderer ending_table_;

	[SerializeField]
	private List<SpriteRenderer> front_sprites_ = new List<SpriteRenderer>();

	private AnimationRenderTarget previous_render_target;

	public static AnimationCameraManager Instance
	{
		get
		{
			return instance_;
		}
	}

	private AnimationSystem System
	{
		get
		{
			return AnimationSystem.Instance;
		}
	}

	public RawImage irregular_image
	{
		get
		{
			return irregular_image_;
		}
		set
		{
			irregular_image_ = value;
		}
	}

	public Camera canvas_camera
	{
		get
		{
			return canvas_camera_;
		}
		set
		{
			canvas_camera_ = value;
		}
	}

	public Camera main_camera
	{
		get
		{
			return main_camera_;
		}
		set
		{
			main_camera_ = value;
		}
	}

	public Transform quake_targets
	{
		get
		{
			return quake_targets_;
		}
	}

	public Transform quake_targets_mapicon
	{
		get
		{
			return quake_targets_mapicon_;
		}
	}

	public SpriteRenderer fade_targets
	{
		get
		{
			return fade_targets_;
		}
	}

	public List<SpriteRenderer> parts_sprite
	{
		get
		{
			return parts_sprite_;
		}
	}

	public Transform instance_parent
	{
		get
		{
			return instance_parent_;
		}
	}

	public GameObject demo
	{
		get
		{
			return demo_;
		}
	}

	public SpriteRenderer ending_table
	{
		get
		{
			return ending_table_;
		}
	}

	public List<SpriteRenderer> front_sprites
	{
		get
		{
			return front_sprites_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
		root_camera_.targetTexture = root_texture_;
		irregular_depth_camera_.targetTexture = irregular_depth_texture_;
	}

	public void OnDisable()
	{
		Debug.Log("AnimationCameraManager.OnDisable");
		root_camera_.targetTexture = null;
		irregular_depth_camera_.targetTexture = null;
	}

	private void LateUpdate()
	{
		AnimationRenderTarget animationRenderTarget = AnimationRenderTarget.Default;
		foreach (AnimationObject item in System.AllAnimationObject)
		{
			if (!item.Exists || !item.enabled || item.RenderTarget == AnimationRenderTarget.Default)
			{
				continue;
			}
			animationRenderTarget = item.RenderTarget;
			break;
		}
		if (previous_render_target != animationRenderTarget)
		{
			Camera camera = irregular_depth_camera_;
			bool useAnotherRenderTexture = animationRenderTarget.UseAnotherRenderTexture;
			canvas_camera.enabled = useAnotherRenderTexture;
			useAnotherRenderTexture = useAnotherRenderTexture;
			irregular_image.enabled = useAnotherRenderTexture;
			camera.enabled = useAnotherRenderTexture;
			canvas_camera.clearFlags = animationRenderTarget.CanvasClearFlag;
			canvas_camera.depth = animationRenderTarget.CanvasDepth;
			if (canvas_camera.clearFlags == CameraClearFlags.Color && canvas_camera.depth < 10f && animationRenderTarget.UseAnotherRenderTexture)
			{
				main_camera.clearFlags = CameraClearFlags.Depth;
			}
			else
			{
				main_camera.clearFlags = CameraClearFlags.Color;
			}
			previous_render_target = animationRenderTarget;
		}
	}
}
