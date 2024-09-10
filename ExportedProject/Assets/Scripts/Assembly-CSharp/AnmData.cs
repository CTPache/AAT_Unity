using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Anm", menuName = "Scriptable/Anm")]
public class AnmData : ScriptableObject
{
	[Serializable]
	public class Sequence
	{
		public enum LoopCode
		{
			Loop = 255,
			End = 254,
			EndAndDelete = 253
		}

		[SerializeField]
		private int use_sprite_group;

		[SerializeField]
		private int keep_frame;

		[SerializeField]
		private int attribute;

		[SerializeField]
		private Vector2 coord;

		public int UseSpriteGroup
		{
			get
			{
				return use_sprite_group;
			}
		}

		public int KeepFrame
		{
			get
			{
				return keep_frame;
			}
		}

		public int Attribute
		{
			get
			{
				return attribute;
			}
		}

		public Vector2 Coord
		{
			get
			{
				return coord;
			}
		}
	}

	[Serializable]
	public class SpriteGroup
	{
		[Serializable]
		public class Sprite
		{
			[SerializeField]
			private int u;

			[SerializeField]
			private int v;

			[SerializeField]
			private int w;

			[SerializeField]
			private int h;

			[SerializeField]
			private int offset_x;

			[SerializeField]
			private int offset_y;

			[HideInInspector]
			[SerializeField]
			private int x;

			[HideInInspector]
			[SerializeField]
			private int y;

			[InspectorReadonly]
			[SerializeField]
			private Vector2 uv;

			[InspectorReadonly]
			[SerializeField]
			private Vector2 wh;

			[InspectorReadonly]
			[SerializeField]
			private Vector2 offset;

			[HideInInspector]
			[SerializeField]
			private Vector2 xy;

			[InspectorReadonly]
			[SerializeField]
			private float rot;

			[SerializeField]
			private bool value_fixed;

			public int U
			{
				get
				{
					return u;
				}
			}

			public int V
			{
				get
				{
					return v;
				}
			}

			public int W
			{
				get
				{
					return w;
				}
			}

			public int H
			{
				get
				{
					return h;
				}
			}

			public int OffsetX
			{
				get
				{
					return offset_x;
				}
			}

			public int OffsetY
			{
				get
				{
					return offset_y;
				}
			}

			public int X
			{
				get
				{
					return x;
				}
			}

			public int Y
			{
				get
				{
					return y;
				}
			}

			public bool flipThis
			{
				get
				{
					return x < 0;
				}
			}
		}

		[SerializeField]
		private List<Sprite> sprites;

		private const int CenterTexture = 864;

		public List<Sprite> Sprites
		{
			get
			{
				return sprites;
			}
		}

		public int WidthOfGroup
		{
			get
			{
				int num = sprites.Min((Sprite spr) => spr.OffsetX);
				int num2 = sprites.Max((Sprite spr) => spr.OffsetX + spr.W);
				return num2 - num;
			}
		}

		public int HeightOfGroup
		{
			get
			{
				int num = sprites.Min((Sprite spr) => spr.OffsetY);
				int num2 = sprites.Max((Sprite spr) => spr.OffsetY + spr.H);
				return num2 - num;
			}
		}

		public Vector3 OffsetCoord
		{
			get
			{
				int num = sprites.Min((Sprite spr) => spr.OffsetX);
				int num2 = sprites.Max((Sprite spr) => spr.OffsetX + spr.W);
				int num3 = (num + num2) / 2 - 864;
				int num4 = sprites.Min((Sprite spr) => spr.OffsetY);
				int num5 = sprites.Max((Sprite spr) => spr.OffsetY + spr.H);
				int num6 = (num4 + num5) / 2 - 864;
				return new Vector3(num3, -num6, 0f);
			}
		}
	}

	public List<Sequence> sequences;

	public List<SpriteGroup> sprite_groups;

	public Sequence.LoopCode LoopCode
	{
		get
		{
			return (Sequence.LoopCode)sequences.Last().KeepFrame;
		}
	}

	public int LoopCodeFrameIndex
	{
		get
		{
			return sequences.Count - 1;
		}
	}

	public SpriteGroup SpriteGroupOfSequence(int sequenceIndex)
	{
		return sprite_groups[sequences[sequenceIndex].UseSpriteGroup];
	}
}
