using System.Collections.Generic;
using UnityEngine;

public class DIconController : MonoBehaviour
{
	private static DIconController instance_;

	private List<string> iconset = new List<string> { "etc04e", "etc04f", "etc050" };

	private List<string> iconset_U = new List<string> { "etc04e", "etc04fu", "etc050u" };

	[SerializeField]
	private Transform dicon_trs_;

	[SerializeField]
	private AssetBundleSprite asset_budle_sprite_;

	public const ushort RNO_DICON_NO_DISP = 0;

	public const ushort RND_DICON_MOVE_IN = 1;

	public const ushort RNO_DICON_MOVE_CENTER = 2;

	public const ushort RNO_DICON_MOVE_OUT = 3;

	public int Dicon_pos_x = 400;

	public ushort Dicon_rno_0;

	public byte Dicon_id;

	public static DIconController instance
	{
		get
		{
			return instance_;
		}
	}

	private void Awake()
	{
		instance_ = this;
	}

	public void Dicon_disp_main()
	{
		if (Dicon_rno_0 == 0)
		{
			return;
		}
		Dicon_disp(0);
		switch (Dicon_rno_0)
		{
		case 1:
			Dicon_disp(1);
			Dicon_pos_x -= 16;
			if (Dicon_pos_x < 0)
			{
				Dicon_pos_x = 0;
				Dicon_rno_0 = 2;
			}
			break;
		case 2:
			Dicon_disp(1);
			break;
		case 3:
			Dicon_disp(1);
			Dicon_pos_x -= 16;
			if (Dicon_pos_x < -400)
			{
				Dicon_pos_x = -400;
				Dicon_rno_0 = 0;
				Terminate();
			}
			break;
		}
		dicon_trs_.localPosition = new Vector3((float)Dicon_pos_x * 5.45f, 0f, -2700f);
	}

	public void Dicon_disp(ushort flag)
	{
		if (flag == 1)
		{
			asset_budle_sprite_.sprite_renderer_.enabled = true;
		}
		else
		{
			asset_budle_sprite_.sprite_renderer_.enabled = false;
		}
	}

	public void Terminate()
	{
		asset_budle_sprite_.end();
		asset_budle_sprite_.remove();
		asset_budle_sprite_.sprite_renderer_.sprite = null;
	}

	public void Dicon_set()
	{
		asset_budle_sprite_.end();
		asset_budle_sprite_.remove();
		asset_budle_sprite_.sprite_renderer_.sprite = null;
		if (GSStatic.global_work_.language == Language.USA)
		{
			asset_budle_sprite_.load("/GS1/etc/", iconset_U[Dicon_id]);
		}
		else
		{
			asset_budle_sprite_.load("/GS1/etc/", iconset[Dicon_id]);
		}
	}

	private void RestoreDicon()
	{
		if (Dicon_rno_0 != 0)
		{
			Dicon_set();
		}
	}
}
