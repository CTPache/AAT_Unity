using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct OptionWork
	{
		public ushort bgm_value;

		public ushort se_value;

		public ushort skip_type;

		public ushort shake_type;

		public ushort vibe_type;

		public ushort window_type;

		public ushort language_type;

		public byte window_mode;

		public uint resolution_w;

		public uint resolution_h;

		public byte vsync;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
		public ushort[] key_config;

		public static void New(out OptionWork option_work)
		{
			option_work = default(OptionWork);
			option_work.key_config = new ushort[30];
			for (int i = 0; i < 30; i++)
			{
				option_work.key_config[i] = (ushort)padCtrl.instance.key_type_[i][0];
			}
		}

		public void CopyFrom(global::OptionWork src)
		{
			bgm_value = src.bgm_value;
			se_value = src.se_value;
			skip_type = src.skip_type;
			shake_type = src.shake_type;
			vibe_type = src.vibe_type;
			window_type = src.window_type;
			language_type = src.language_type;
			window_mode = src.window_mode;
			resolution_w = src.resolution_w;
			resolution_h = src.resolution_h;
			vsync = src.vsync;
			for (int i = 0; i < 30; i++)
			{
				src.key_config[i] = (ushort)padCtrl.instance.key_type_[i][0];
			}
			Array.Copy(src.key_config, key_config, src.key_config.Length);
		}

		public void CopyTo(global::OptionWork dest)
		{
			dest.bgm_value = bgm_value;
			dest.se_value = se_value;
			dest.skip_type = skip_type;
			dest.shake_type = shake_type;
			dest.vibe_type = vibe_type;
			dest.window_type = window_type;
			dest.language_type = language_type;
			dest.window_mode = window_mode;
			dest.resolution_w = resolution_w;
			dest.resolution_h = resolution_h;
			dest.vsync = vsync;
			Array.Copy(key_config, dest.key_config, key_config.Length);
			for (int i = 0; i < 30; i++)
			{
				padCtrl.instance.key_type_[i][0] = (int)dest.key_config[i];
			}
		}
	}
}
