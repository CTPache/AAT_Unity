using System.Collections.Generic;
using UnityEngine;

public class debugLogger : MonoBehaviour
{
	private static debugLogger instance_;

	[SerializeField]
	private List<string> tag_list_ = new List<string>();

	public static debugLogger instance
	{
		get
		{
			return instance_;
		}
	}

	public void Log(string in_tag, string in_msg)
	{
		foreach (string item in tag_list_)
		{
			if (in_tag == item)
			{
				Debug.Log(in_tag + ":" + in_msg);
				break;
			}
		}
	}

	private void Awake()
	{
		instance_ = this;
	}
}
