using System.Collections.Generic;
using UnityEngine;

public class cutInCtrl : MonoBehaviour
{
	private static cutInCtrl instance_;

	[SerializeField]
public List<AssetBundleSprite> sprite_list_;

	[SerializeField]
public GameObject body_;

	public static cutInCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public AssetBundleSprite cut_in
	{
		get
		{
			return sprite_list_[0];
		}
	}

	public bool body_active
	{
		get
		{
			return body_.activeSelf;
		}
		set
		{
			body_.SetActive(value);
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public void cutIn(int in_face_no)
	{
		body_.transform.localPosition = Vector3.zero;
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			if (in_face_no == 0 || in_face_no != 1)
			{
				cut_in.load("/GS1/BG/", "bg040");
			}
			else
			{
				cut_in.load("/GS1/BG/", "bg041");
			}
		}
		else if (GSStatic.global_work_.title == TitleId.GS2)
		{
			switch (in_face_no)
			{
			default:
				cut_in.load("/GS2/BG/", "bg040");
				break;
			case 1:
				cut_in.load("/GS2/BG/", "bg041");
				break;
			case 2:
				cut_in.load("/GS2/BG/", "bg013");
				break;
			}
		}
		else if (GSStatic.global_work_.title == TitleId.GS3)
		{
			switch (in_face_no)
			{
			default:
				cut_in.load("/GS3/BG/", "bg00b");
				break;
			case 1:
				cut_in.load("/GS3/BG/", "bg00e");
				break;
			case 2:
				cut_in.load("/GS3/BG/", "bg00f");
				break;
			case 3:
				cut_in.load("/GS3/BG/", "bg00c");
				break;
			case 4:
				cut_in.load("/GS3/BG/", "bg00d");
				break;
			}
		}
		body_active = true;
	}

	public void cutOut()
	{
		body_active = false;
	}

	public void SetBodyPosition(int pos_x)
	{
		body_.transform.localPosition = new Vector3((float)(-pos_x) * 6.75f, 0f);
	}
}
