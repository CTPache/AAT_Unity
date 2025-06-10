using System.Collections.Generic;
using UnityEngine;

public class MovieCollisionPlayer : MonoBehaviour, IRectHolder
{
	[SerializeField]
public List<RectTransform> collision_rects;

	[SerializeField]
public MovieController frame_source;

	private List<MovieCollision> collision_datas = new List<MovieCollision>();

	public int previousFrame = -1;

	private List<RectTransform> rects_for_serve = new List<RectTransform>();

	public IEnumerable<RectTransform> Rects
	{
		get
		{
			return rects_for_serve;
		}
	}

	public void Play(List<MovieCollision> collisions)
	{
		base.enabled = true;
		collision_datas = collisions;
	}

	public void Stop()
	{
		base.enabled = false;
	}

	public void CollisionUpdate()
	{
		if (!base.enabled)
		{
			return;
		}
		int currentFrame = frame_source.Frame;
		if (previousFrame == currentFrame)
		{
			return;
		}
		rects_for_serve.Clear();
		previousFrame = currentFrame;
		for (int i = 0; i < collision_datas.Count; i++)
		{
			MovieCollision.FrameData frameData = collision_datas[i].frames.Find((MovieCollision.FrameData f) => f.frameNo == currentFrame - 1);
			collision_rects[i].gameObject.SetActive(frameData != null);
			if (!collision_rects[i].gameObject.activeSelf)
			{
				rects_for_serve.Add(null);
				continue;
			}
			float num = 5.625f;
			collision_rects[i].localPosition = new Vector3(240f + (float)(int)frameData.rect.x * num - 960f, 540f - (float)(int)frameData.rect.y * 5.625f);
			collision_rects[i].sizeDelta = new Vector3((float)(int)frameData.rect.w * 5.625f, (float)(int)frameData.rect.h * 5.625f);
			rects_for_serve.Add(collision_rects[i]);
		}
	}
}
