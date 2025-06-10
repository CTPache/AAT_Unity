using System;
using UnityEngine;

[Serializable]
public struct R
{
	[SerializeField]
public byte no_0_;

	[SerializeField]
public byte no_1_;

	[SerializeField]
public byte no_2_;

	[SerializeField]
public byte no_3_;

	public byte no_0
	{
		get
		{
			return no_0_;
		}
		set
		{
			no_0_ = value;
		}
	}

	public byte no_1
	{
		get
		{
			return no_1_;
		}
		set
		{
			no_1_ = value;
		}
	}

	public byte no_2
	{
		get
		{
			return no_2_;
		}
		set
		{
			no_2_ = value;
		}
	}

	public byte no_3
	{
		get
		{
			return no_3_;
		}
		set
		{
			no_3_ = value;
		}
	}

	public void Set(byte no_0, byte no_1, byte no_2, byte no_3)
	{
		this.no_0 = no_0;
		this.no_1 = no_1;
		this.no_2 = no_2;
		this.no_3 = no_3;
	}

	public void CopyFrom(ref R src)
	{
		no_0 = src.no_0;
		no_1 = src.no_1;
		no_2 = src.no_2;
		no_3 = src.no_3;
	}

	public void init()
	{
		no_0 = 0;
		no_1 = 0;
		no_2 = 0;
		no_3 = 0;
	}
}
