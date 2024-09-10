using UnityEngine;

public class AnimationRenderTarget
{
	private static AnimationRenderTarget behind_bg;

	private static AnimationRenderTarget like_bg;

	private static AnimationRenderTarget default_depth;

	private static AnimationRenderTarget front_of_desk;

	private static AnimationRenderTarget front_of_subbg;

	private static AnimationRenderTarget front_of_message_window;

	public bool UseAnotherRenderTexture { get; private set; }

	public CameraClearFlags CanvasClearFlag { get; private set; }

	public int CanvasDepth { get; private set; }

	public int ObjectSortingLayer { get; private set; }

	public int ObjectLayer { get; private set; }

	public static AnimationRenderTarget BehingBg
	{
		get
		{
			return behind_bg ?? (behind_bg = new AnimationRenderTarget(true, CameraClearFlags.Color, 8, "Default"));
		}
	}

	public static AnimationRenderTarget LikeBg
	{
		get
		{
			return like_bg ?? (like_bg = new AnimationRenderTarget(false, CameraClearFlags.Color, 8, "AnimationBGLike"));
		}
	}

	public static AnimationRenderTarget Default
	{
		get
		{
			return default_depth ?? (default_depth = new AnimationRenderTarget(false, CameraClearFlags.Color, 8, "Default"));
		}
	}

	public static AnimationRenderTarget FrontOfDesk
	{
		get
		{
			return front_of_desk ?? (front_of_desk = new AnimationRenderTarget(false, CameraClearFlags.Depth, 12, "FrontOfDesk"));
		}
	}

	public static AnimationRenderTarget FrontOfSubBG
	{
		get
		{
			return front_of_subbg ?? (front_of_subbg = new AnimationRenderTarget(true, CameraClearFlags.Depth, 12, "FrontOfBG"));
		}
	}

	public static AnimationRenderTarget FrontOfMessageWindow
	{
		get
		{
			return front_of_message_window ?? (front_of_message_window = new AnimationRenderTarget(true, CameraClearFlags.Depth, 80, "FrontOfMessageWindow"));
		}
	}

	private AnimationRenderTarget(bool useAnotherRenderTexture, CameraClearFlags canvasClearFlag, int canvasDepth, string objectSortingLayer)
	{
		UseAnotherRenderTexture = useAnotherRenderTexture;
		CanvasClearFlag = canvasClearFlag;
		CanvasDepth = canvasDepth;
		ObjectSortingLayer = SortingLayer.NameToID(objectSortingLayer);
		ObjectLayer = LayerMask.NameToLayer((!useAnotherRenderTexture) ? "Animation" : "irregular_Animation");
	}
}
