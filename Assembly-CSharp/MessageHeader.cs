using System;
using System.Collections.Generic;
using System.Text;

public class MessageHeader
{
	private Dictionary<string, ushort> dictionary_;

	public MessageHeader(byte[] bytes)
	{
		Encoding aSCII = Encoding.ASCII;
		dictionary_ = new Dictionary<string, ushort>();
		int i = 0;
		while (i < bytes.Length)
		{
			int num = i;
			for (; i < bytes.Length && bytes[i] != 0; i++)
			{
			}
			string @string = aSCII.GetString(bytes, num, i - num);
			i++;
			if (i + 2 <= bytes.Length)
			{
				ushort value = BitConverter.ToUInt16(bytes, i);
				i += 2;
				if (!dictionary_.ContainsKey(@string))
				{
					dictionary_.Add(@string, value);
				}
			}
		}
	}

	public ushort GetMessageNo(string message_name)
	{
		ushort value;
		if (!dictionary_.TryGetValue(message_name, out value))
		{
			return ushort.MaxValue;
		}
		return value;
	}
}
