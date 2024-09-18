using System.IO;
using UnityEngine;

public class decryptionCtrl : MonoBehaviour
{
	private static decryptionCtrl instance_;

	public static decryptionCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public byte[] load(string in_path)
	{
		return File.ReadAllBytes(in_path);
	}

	static decryptionCtrl()
	{
	}
}
