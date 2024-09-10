using System.Collections.Generic;
using UnityEngine;

public class FrameCtrl : MonoBehaviour
{
	[SerializeField]
	private List<RectTransform> fram_list_ = new List<RectTransform>();

	private void Start()
	{
		fram_list_[0].sizeDelta = new Vector2(Screen.width, 1080f);
		fram_list_[1].sizeDelta = new Vector2(Screen.width, 1080f);
		fram_list_[2].sizeDelta = new Vector2(1920f, Screen.height);
		fram_list_[3].sizeDelta = new Vector2(1920f, Screen.height);
	}
}
