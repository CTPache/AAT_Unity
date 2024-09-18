using System.Collections;
using UnityEngine;

public class WindowListener : MonoBehaviour
{
	public static WindowListener instance { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	public void Start()
	{
		WindowAPI.Init();
		StartCoroutine(stateCoroutine());
	}

	private IEnumerator stateCoroutine()
	{
		while (!WindowAPI.is_close)
		{
			yield return null;
		}
		Window.Exit();
	}

	public void OnDestroy()
	{
		WindowAPI.Term();
	}
}
