using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SummaryResource
{
	public class CutBuilder
	{
		public class DrawInfo
		{
			private int attribute;

			public AnmData.SpriteGroup DrawSprites { get; set; }

			public bool UseAlphaBlend { get; set; }

			public int Attribute
			{
				get
				{
					return attribute;
				}
				set
				{
					attribute = value;
					FlipX = (Attribute & 1) != 0;
				}
			}

			public bool FlipX { get; private set; }
		}

		public const int CutSideLength = 1728;

		public const int StretchedLength = 2048;

		public List<Texture2D> BuildCuts(Texture2D spriteSource, AnmData anm)
		{
			return (from num in Enumerable.Range(0, anm.sprite_groups.Count)
				select BuildCut(spriteSource, anm, num)).ToList();
		}

		public List<Texture2D> BuildCutsFaster(Texture2D spriteSource, AnmData anm)
		{
			return (from num in Enumerable.Range(0, anm.sprite_groups.Count)
				select BuildCutFaster(spriteSource, anm, num, false)).ToList();
		}

		public List<Texture2D> BuildCutsIgnoreAlpha(Texture2D spriteSource, AnmData anm)
		{
			return (from num in Enumerable.Range(0, anm.sprite_groups.Count)
				select BuildCutFaster(spriteSource, anm, num, true)).ToList();
		}

		private bool IsNeedStretch(IEnumerable<AnmData.SpriteGroup.Sprite> spriteGroup)
		{
			bool flag = false;
			flag |= spriteGroup.Min((AnmData.SpriteGroup.Sprite spr) => spr.OffsetX) < 0;
			flag |= spriteGroup.Max((AnmData.SpriteGroup.Sprite spr) => spr.OffsetX + spr.W) >= 1728;
			flag |= spriteGroup.Min((AnmData.SpriteGroup.Sprite spr) => spr.OffsetY) < 0;
			return flag | (spriteGroup.Max((AnmData.SpriteGroup.Sprite spr) => spr.OffsetY + spr.H) >= 1728);
		}

		private Texture2D CreateNewTexture(bool needStretch)
		{
			int num = ((!needStretch) ? 1728 : 2048);
			Texture2D texture2D = new Texture2D(num, num, TextureFormat.RGBA32, false);
			Color32[] pixels = Enumerable.Repeat(new Color32(0, 0, 0, 0), num * num).ToArray();
			texture2D.SetPixels32(pixels);
			return texture2D;
		}

		private Texture2D CreateNewTextureSmarter(AnmData.SpriteGroup group)
		{
			int widthOfGroup = group.WidthOfGroup;
			int heightOfGroup = group.HeightOfGroup;
			Texture2D texture2D = new Texture2D(widthOfGroup, heightOfGroup, TextureFormat.RGBA32, false);
			Color32[] pixels = Enumerable.Repeat(new Color32(0, 0, 0, 0), widthOfGroup * heightOfGroup).ToArray();
			texture2D.SetPixels32(pixels);
			return texture2D;
		}

		private Color[] ExtractPart(Texture2D source, AnmData.SpriteGroup.Sprite sprite)
		{
			return source.GetPixels(sprite.U, source.height - sprite.V - sprite.H, sprite.W, sprite.H);
		}

		private Color32[] ExtractPart(Color32[] source, int sourceWidth, AnmData.SpriteGroup.Sprite sprite, bool flipX)
		{
			Color32[] array = new Color32[sprite.W * sprite.H];
			int u = sprite.U;
			int num = source.Length / sourceWidth - sprite.V - sprite.H;
			if (!flipX)
			{
				for (int i = 0; i < sprite.H; i++)
				{
					Array.Copy(source, (num + i) * sourceWidth + u, array, i * sprite.W, sprite.W);
				}
			}
			else
			{
				Color32[] array2 = new Color32[sprite.W];
				for (int j = 0; j < sprite.H; j++)
				{
					Array.Copy(source, (num + j) * sourceWidth + u, array2, 0, sprite.W);
					Array.Reverse(array2);
					Array.Copy(array2, 0, array, j * sprite.W, sprite.W);
				}
			}
			return array;
		}

		public Texture2D BuildCutFaster(Texture2D spriteSource, AnmData anm, int targetCutIndex, bool useAlphaBlend)
		{
			Texture2D texture2D = CreateNewTextureSmarter(anm.sprite_groups[targetCutIndex]);
			int num = anm.sprite_groups[targetCutIndex].Sprites.Min((AnmData.SpriteGroup.Sprite spr) => spr.OffsetX);
			int num2 = anm.sprite_groups[targetCutIndex].Sprites.Min((AnmData.SpriteGroup.Sprite spr) => spr.OffsetY);
			foreach (AnmData.SpriteGroup.Sprite sprite in anm.sprite_groups[targetCutIndex].Sprites)
			{
				Color[] array = ExtractPart(spriteSource, sprite);
				if (useAlphaBlend)
				{
					Color[] pixels = texture2D.GetPixels(sprite.OffsetX - num, texture2D.height - sprite.OffsetY + num2 - sprite.H, sprite.W, sprite.H);
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = PremultipliedAlphaBlend(array[i], pixels[i]);
					}
				}
				texture2D.SetPixels(sprite.OffsetX - num, texture2D.height - sprite.OffsetY + num2 - sprite.H, sprite.W, sprite.H, array);
			}
			texture2D.Apply(false, true);
			return texture2D;
		}

		private Color PremultipliedAlphaBlend(Color over, Color based)
		{
			Color result = over;
			result.r += based.r * (1f - over.a);
			result.g += based.g * (1f - over.a);
			result.b += based.b * (1f - over.a);
			result.a += based.a * (1f - over.a);
			return result;
		}

		public Texture2D BuildCut(Texture2D spriteSource, AnmData anm, int targetCutIndex)
		{
			bool flag = IsNeedStretch(anm.sprite_groups[targetCutIndex].Sprites);
			Texture2D texture2D = CreateNewTexture(flag);
			int num = (flag ? 160 : 0);
			foreach (AnmData.SpriteGroup.Sprite sprite in anm.sprite_groups[targetCutIndex].Sprites)
			{
				Color[] colors = ExtractPart(spriteSource, sprite);
				texture2D.SetPixels(sprite.OffsetX + num, texture2D.height - sprite.OffsetY - sprite.H + num, sprite.W, sprite.H, colors);
			}
			texture2D.Apply(false, true);
			return texture2D;
		}

		public void DrawCut(Color32[] spriteSource, int sourceTextureWidth, Texture2D destination, DrawInfo drawInfo)
		{
			int num = (destination.width - 1728) / 2;
			int num2 = destination.height - (destination.height - 1728) / 2;
			foreach (AnmData.SpriteGroup.Sprite sprite in drawInfo.DrawSprites.Sprites)
			{
				Color32[] array = ExtractPart(spriteSource, sourceTextureWidth, sprite, drawInfo.FlipX);
				int num3 = num + sprite.OffsetX;
				if (drawInfo.FlipX)
				{
					num3 = destination.width - num3 - sprite.W;
				}
				if (drawInfo.UseAlphaBlend)
				{
					Color[] pixels = destination.GetPixels(num3, num2 - sprite.OffsetY - sprite.H, sprite.W, sprite.H);
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = PremultipliedAlphaBlend(array[i], pixels[i]);
					}
				}
				destination.SetPixels32(num3, num2 - sprite.OffsetY - sprite.H, sprite.W, sprite.H, array);
			}
		}

		public void ClearCut(Texture2D destination, DrawInfo clearInfo)
		{
			List<AnmData.SpriteGroup.Sprite> sprites = clearInfo.DrawSprites.Sprites;
			int num = (destination.width - 1728) / 2;
			int num2 = destination.height - (destination.height - 1728) / 2;
			int num3 = sprites.Max((AnmData.SpriteGroup.Sprite spr) => spr.W * spr.H);
			Color32[] colors = new Color32[num3];
			foreach (AnmData.SpriteGroup.Sprite item in sprites)
			{
				int num4 = num + item.OffsetX;
				if (clearInfo.FlipX)
				{
					num4 = destination.width - num4 - item.W;
				}
				destination.SetPixels32(num4, num2 - item.OffsetY - item.H, item.W, item.H, colors);
			}
		}

		public Rect DrawArea(Texture2D destination, AnmData anm)
		{
			int num = (destination.width - 1728) / 2;
			int num2 = destination.height - (destination.height - 1728) / 2;
			int num3 = int.MaxValue;
			int num4 = 0;
			int num5 = int.MaxValue;
			int num6 = 0;
			for (int i = 0; i < anm.sequences.Count; i++)
			{
				bool flag = (anm.sequences[i].Attribute & 1) == 0;
				List<AnmData.SpriteGroup.Sprite> sprites = anm.SpriteGroupOfSequence(i).Sprites;
				int num7 = sprites.Min((AnmData.SpriteGroup.Sprite spr) => spr.OffsetX);
				int num8 = sprites.Max((AnmData.SpriteGroup.Sprite spr) => spr.OffsetX + spr.W);
				if (flag)
				{
					int num9 = num8 - num7;
					num8 = 1728 - num7;
					num7 = num8 - num9;
				}
				num3 = Math.Min(num3, num7);
				num4 = Math.Max(num4, num8);
				int val = sprites.Min((AnmData.SpriteGroup.Sprite spr) => spr.OffsetY);
				int val2 = sprites.Max((AnmData.SpriteGroup.Sprite spr) => spr.OffsetY + spr.H);
				num5 = Math.Min(num5, val);
				num6 = Math.Max(num6, val2);
			}
			return new Rect(num + num3, num2 - num5, num4 - num3, num6 - num5);
		}

		public List<Texture2D> BuildMasks(int outputWidth, int outputHeight, AnmData anm)
		{
			return (from num in Enumerable.Range(0, anm.sprite_groups.Count)
				select BuildMask(outputWidth, outputHeight, anm, num)).ToList();
		}

		public Texture2D BuildMask(int outputWidth, int outputHeight, AnmData anm, int targetCutIndex)
		{
			Texture2D texture2D = new Texture2D(1728, 1728);
			Color black = Color.black;
			black.a = 0.4f;
			Color[] pixels = Enumerable.Repeat(black, texture2D.width * texture2D.height).ToArray();
			texture2D.SetPixels(pixels);
			foreach (AnmData.SpriteGroup.Sprite sprite in anm.sprite_groups[targetCutIndex].Sprites)
			{
				Color clear = Color.clear;
				Color[] colors = Enumerable.Repeat(clear, sprite.W * sprite.H).ToArray();
				texture2D.SetPixels(sprite.U, texture2D.height - sprite.V - sprite.H, sprite.W, sprite.H, colors);
			}
			return texture2D;
		}
	}
}
