using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ConvertTextData
{
	private struct Data
	{
		public ushort id;

		public string text;
	}

	private Data[] data_;

	public ConvertTextData(byte[] bytes, string language)
	{
		StringBuilder stringBuilder = new StringBuilder();
		List<Data> list = new List<Data>();
		int num = 0;
		while (num + 2 <= bytes.Length)
		{
			Data item = new Data
			{
				id = BitConverter.ToUInt16(bytes, num)
			};
			num += 2;
			while (num + 2 <= bytes.Length)
			{
				char c = BitConverter.ToChar(bytes, num);
				num += 2;
				if (c != 0)
				{
					stringBuilder.Append(c);
					continue;
				}
				break;
			}
			string text;
			switch (language)
			{
			case "JAPAN":
				text = stringBuilder.ToString();
				break;
			default:
				text = MessageSystem.EnToHalf(stringBuilder.ToString(), language);
				break;
			}
			text = text.Replace('φ', ' ');
			item.text = text;
			stringBuilder.Length = 0;
			list.Add(item);
			//Debug.Log(text);
		}
		data_ = list.ToArray();
	}

	public string GetText(ushort id)
	{
		for (int i = 0; i < data_.Length; i++)
		{
			if (data_[i].id == id)
			{
				return data_[i].text;
			}
		}
		return string.Empty;
	}
}
