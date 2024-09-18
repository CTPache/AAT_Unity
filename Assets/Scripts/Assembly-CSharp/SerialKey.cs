using UnityEngine;
using UnityEngine.UI;

public class SerialKey : MonoBehaviour
{
	public Text text;

	public static SerialKey instance { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	public void SetSerialKey()
	{
	}

	private bool Decode(string src, out string dst)
	{
		dst = string.Empty;
		return true;
	}

	public bool CheckKey()
	{
		return true;
	}
}
