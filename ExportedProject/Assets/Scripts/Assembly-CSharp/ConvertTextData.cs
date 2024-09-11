using System;
using System.Collections.Generic;
using System.Text;

public class ConvertTextData
{
	private struct Data
	{
		public ushort id;

		public string text;
	}

	private Data[] data_;

	public ConvertTextData(byte[] bytes, Language language)
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
			case Language.JAPAN:
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
