using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugMemory : MonoBehaviour
{
	[SerializeField]
public Text text_;

	private float CPU;

	private float GPU;

	private void Update()
	{
		GPU = 1f / Time.deltaTime;
	}

	private void FixedUpdate()
	{
		CPU = 1f / Time.deltaTime;
		Process();
	}

	private void Process()
	{
		string empty = string.Empty;
		long totalMemory = GC.GetTotalMemory(false);
		empty += string.Format("{0:f1}({1:f1})fps\n", CPU, GPU);
		empty += string.Format("sys_mem:{0:f1} mb\n", totalMemory / 1048576);
		text_.text = empty;
	}
}
