using System;
using UnityEngine;

public class MdtData
{
	private struct Header
	{
		public ushort message_count;

		public ushort dummy;
	}

	private Header header_;

	private uint file_message_offset_;

	private uint[] message_offset_;

	private ushort[] message_data_;

	public ushort message_count
	{
		get
		{
			return header_.message_count;
		}
	}

	public MdtData(byte[] bytes)
	{
		int num = 0;
		header_.message_count = BitConverter.ToUInt16(bytes, num);
		num += 2;
		header_.dummy = BitConverter.ToUInt16(bytes, num);
		num += 2;
		file_message_offset_ = (uint)(num + header_.message_count * 4);
		message_offset_ = new uint[header_.message_count];
		for (int i = 0; i < message_offset_.Length; i++)
		{
			message_offset_[i] = BitConverter.ToUInt32(bytes, num);
			num += 4;
		}
		message_data_ = new ushort[(bytes.Length - num) / 2];
		for (int j = 0; j < message_data_.Length; j++)
		{
			message_data_[j] = BitConverter.ToUInt16(bytes, num);
			num += 2;
		}
	}

	public uint GetMessageOffset(ushort message_no)
	{
		return (message_offset_[message_no] - file_message_offset_) / 2;
	}

	public uint GetLabelMessageOffset(ushort label_no, out ushort message_no, out uint message_top)
	{
		uint num = message_offset_[label_no];
		message_no = (ushort)(num >> 16);
		message_top = GetMessageOffset(message_no);
		return message_top + (num & 0xFFFF) / 2;
	}

	public ushort GetMessage(uint index)
	{
		return message_data_[index];
	}

	public bool IsLabel(ushort message_no)
	{
		return GetMessageOffset(message_no) >= message_data_.Length;
	}

	public void MessegeOffsetLog()
	{
		string text = string.Empty;
		bool flag = false;
		for (int i = 0; i < message_offset_.Length; i++)
		{
			if (!flag && IsLabel((ushort)i))
			{
				flag = true;
			}
			int num = i + 128;
			if (!flag)
			{
				string text2 = text;
				text = text2 + "mes[" + i + "(0x" + i.ToString("x4") + ")>0x" + num.ToString("x4") + "] index=" + GetMessageOffset((ushort)i) + "\n";
			}
			else
			{
				ushort message_no;
				uint message_top;
				uint labelMessageOffset = GetLabelMessageOffset((ushort)i, out message_no, out message_top);
				string text2 = text;
				text = text2 + "label[" + i + "(0x" + i.ToString("x4") + ")] mes=" + message_no + " index=" + labelMessageOffset + " top=" + message_top + "\n";
			}
		}
		Debug.Log(text);
	}
}
