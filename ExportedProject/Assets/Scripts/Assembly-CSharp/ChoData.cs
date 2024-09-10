using System;
using System.Collections.Generic;
using System.Text;

public class ChoData
{
	private struct Data
	{
		public ushort id;

		public string text;
	}

	private Data[] data_;

	public ChoData(byte[] bytes)
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
			item.text = stringBuilder.ToString();
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
