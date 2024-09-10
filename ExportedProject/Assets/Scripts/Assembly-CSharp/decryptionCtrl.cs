using System.IO;
using System.Security.Cryptography;
using System.Text;
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
		byte[] array = File.ReadAllBytes(in_path);
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		rijndaelManaged.KeySize = 128;
		rijndaelManaged.BlockSize = 128;
		string password = "u8DurGE2";
		string s = "6BBGizHE";
		byte[] bytes = Encoding.UTF8.GetBytes(s);
		Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, bytes);
		rfc2898DeriveBytes.IterationCount = 1000;
		rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
		rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
		ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
		byte[] result = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
		cryptoTransform.Dispose();
		return result;
	}
}
