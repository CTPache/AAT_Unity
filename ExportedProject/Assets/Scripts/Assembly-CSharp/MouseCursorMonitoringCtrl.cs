using System;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorMonitoringCtrl : MonoBehaviour
{
	private Vector2 preve_ = default(Vector2);

	private float elapsed_time_;

	private bool callback_done_;

	private List<Action<bool>> hide_cursor_time_over_callback_list_ = new List<Action<bool>>();

	private Rect window_rect_ = default(Rect);

	public float hide_cursor_time { get; set; }

	public void RegisterHideCursorTimeOverCallBack(Action<bool> callback)
	{
		hide_cursor_time_over_callback_list_.Add(callback);
	}

	private void Start()
	{
		preve_ = padCtrl.instance.InputMousePosition();
		window_rect_ = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	private void Update()
	{
		Vector2 vector = padCtrl.instance.InputMousePosition();
		window_rect_ = new Rect(0f, 0f, Screen.width, Screen.height);
		if (preve_ == vector && !callback_done_ && window_rect_.Contains(preve_))
		{
			elapsed_time_ += Time.deltaTime;
			if (elapsed_time_ >= hide_cursor_time)
			{
				foreach (Action<bool> item in hide_cursor_time_over_callback_list_)
				{
					item(false);
				}
				callback_done_ = true;
			}
		}
		else if (preve_ != vector && callback_done_ && callback_done_)
		{
			callback_done_ = false;
			elapsed_time_ = 0f;
			foreach (Action<bool> item2 in hide_cursor_time_over_callback_list_)
			{
				item2(true);
			}
		}
		preve_ = vector;
	}

	private void OnDisable()
	{
		for (int i = 0; i < hide_cursor_time_over_callback_list_.Count; i++)
		{
			hide_cursor_time_over_callback_list_[i] = null;
		}
	}
}
