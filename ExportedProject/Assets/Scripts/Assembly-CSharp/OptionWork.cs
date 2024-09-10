using System;

[Serializable]
public class OptionWork
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

	public ushort[] key_config;

	public OptionWork()
	{
		init();
	}

	public void init()
	{
		bgm_value = 3;
		se_value = 3;
		skip_type = 2;
		shake_type = 1;
		vibe_type = 1;
		window_type = 0;
		language_type = 0;
		window_mode = 0;
		resolution_w = 1280u;
		resolution_h = 720u;
		vsync = 1;
		key_config = new ushort[30];
		for (int i = 0; i < key_config.Length; i++)
		{
			key_config[i] = (ushort)padCtrl.default_key_type_[i][0];
		}
	}
}
