using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugMemory : MonoBehaviour
{
	[SerializeField]
	private Text text_;

	private void Update()
	{
		string empty = string.Empty;
		long totalMemory = GC.GetTotalMemory(false);
		float num = 1f / Time.deltaTime;
		empty += string.Format("{0:f1}:fps\n", num, totalMemory);
		empty += string.Format("sys_mem:{0:f1} mb\n", totalMemory / 1048576);
		text_.text = empty;
	}
}
