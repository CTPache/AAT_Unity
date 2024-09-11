using System;
using System.Collections.Generic;
using System.Text;

public class ConvertLineData
{
	public struct Data
	{
		public ushort id;

		public string[] text;
	}

	private Data[] data_;

	public Data[] data
	{
		get
		{
			return data_;
		}
	}

	public ConvertLineData(byte[] bytes, Language language)
	{
		StringBuilder stringBuilder = new StringBuilder();
		List<Data> list = new List<Data>();
		int num = 0;
		while (num + 2 <= bytes.Length)
		{
			Data item = default(Data);
			List<string> list2 = new List<string>();
			item.id = BitConverter.ToUInt16(bytes, num);
			num += 2;
			bool flag = true;
			while (flag && num + 2 <= bytes.Length)
			{
				char c;
				for (; num + 2 <= bytes.Length; stringBuilder.Append(c))
				{
					c = BitConverter.ToChar(bytes, num);
					num += 2;
					switch (c)
					{
					case '\0':
						flag = false;
						break;
					default:
						continue;
					case ',':
						break;
					}
					break;
				}
				string text;
				switch (language)
				{
				case Language.JAPAN:
					text = stringBuilder.ToString();
					break;
				default:
					text = MessageSystem.EnToHalf(stringBuilder.ToString(), language);
					break;
				}
				text = text.Replace('φ', ' ');
				list2.Add(text);
				stringBuilder.Length = 0;
			}
			item.text = list2.ToArray();
			list.Add(item);
		}
		data_ = list.ToArray();
	}

	public string GetText(ushort id, int line)
	{
		for (int i = 0; i < data_.Length; i++)
		{
			if (data_[i].id == id && line < data_[i].text.Length)
			{
				return data_[i].text[line];
			}
		}
		return string.Empty;
	}

	public string[] GetTexts(ushort id)
	{
		string[] result = new string[data_.Length];
		for (int i = 0; i < data_.Length; i++)
		{
			if (data_[i].id == id)
			{
				return data_[i].text;
			}
		}
		return result;
	}
}
