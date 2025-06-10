using System.Collections.Generic;
using UnityEngine;

public class facePlateCtrl : plateCtrlBase
{
	private class faceIconData
	{
		public readonly uint id;

		public readonly string path;

		public readonly string file;

		public faceIconData(uint in_id, string in_path, string in_file)
		{
			id = in_id;
			path = in_path;
			file = in_file;
		}
	}

	private List<faceIconData> data_list_ = new List<faceIconData>
	{
		new faceIconData(0u, "/GS1/pli/", "pli1a00"),
		new faceIconData(1u, "/GS1/pli/", "pli1a01"),
		new faceIconData(2u, "/GS1/pli/", "pli1a02"),
		new faceIconData(3u, "/GS1/pli/", "pli1a03"),
		new faceIconData(4u, "/GS1/pli/", "pli1a04"),
		new faceIconData(5u, "/GS1/pli/", "pli1a05"),
		new faceIconData(6u, "/GS1/pli/", "pli1a06"),
		new faceIconData(7u, "/GS1/pli/", "pli1a07"),
		new faceIconData(9u, "/GS1/pli/", "pli1a09"),
		new faceIconData(10u, "/GS1/pli/", "pli1a0a"),
		new faceIconData(11u, "/GS1/pli/", "pli1a0b"),
		new faceIconData(12u, "/GS1/pli/", "pli0600"),
		new faceIconData(13u, "/GS1/pli/", "pli0601"),
		new faceIconData(14u, "/GS1/pli/", "pli0602"),
		new faceIconData(15u, "/GS1/pli/", "pli0603"),
		new faceIconData(16u, "/GS1/pli/", "pli0604"),
		new faceIconData(17u, "/GS1/pli/", "pli0605")
	};

	[SerializeField]
public AssetBundleSprite icon_base2_;

	public static facePlateCtrl instance { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	public override void load()
	{
		base.load();
		icon_base2_.load("/menu/common/", "evidence_base");
		icon_base2_.spriteNo(0);
	}

	public override void init()
	{
		base.init();
	}

	public override void entryItem(int in_id)
	{
		base.entryItem(in_id);
		GSStatic.message_work_.Item_face_id = (byte)base.id;
		if (in_id < data_list_.Count)
		{
			faceIconData faceIconData = data_list_.Find((faceIconData p) => p.id == in_id);
			if (faceIconData == null)
			{
				Debug.LogWarning(string.Empty);
			}
			else
			{
				loadItem(faceIconData.path, faceIconData.file);
			}
		}
	}

	public override void openItem(int in_type, float in_wait, bool immediate = false)
	{
		if (base.open_type == in_type)
		{
			immediate = true;
		}
		base.openItem(in_type, in_wait, immediate);
		GSStatic.message_work_.Item_face_open_type = (short)base.open_type;
	}

	public override void closeItem(bool immediate = false)
	{
		base.closeItem(immediate);
		GSStatic.message_work_.Item_face_open_type = -1;
	}

	public override void LoadItem()
	{
		if (GSStatic.message_work_.Item_face_open_type >= 0)
		{
			entryItem(GSStatic.message_work_.Item_face_id);
			openItem(GSStatic.message_work_.Item_face_open_type, 0f, true);
		}
	}
}
