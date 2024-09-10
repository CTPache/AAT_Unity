using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mcol", menuName = "Scriptable/MovieCollision")]
public class MovieCollision : ScriptableObject
{
	[Serializable]
	public class Header
	{
		public uint formatId;

		public uint formatVersion;

		public uint startFrame;

		public uint endFrame;
	}

	[Serializable]
	public class FrameData
	{
		public uint frameNo;

		public ShortRect rect;

		public byte[] data;
	}

	[Serializable]
	public class ShortRect
	{
		public ushort x;

		public ushort y;

		public ushort w;

		public ushort h;
	}

	public Header header;

	public List<FrameData> frames;
}
