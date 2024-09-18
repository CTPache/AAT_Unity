using System;
using System.Collections.Generic;
using UnityEngine;

public class GS2_OpObjCtrl
{
	public class Obj
	{
		public int intervals_;

		public int tmp0_;

		public int rno_;

		public int cnt_;

		public uint id_;

		public int subId_;

		public int yuragi_cnt_;

		public int yuragi_add_;

		public Vector3 pos_;

		public Vector3 speed_;

		public Transform animationTransform_;

		public Action<Obj, Transform> updateTransform_;

		public void Init(uint id, AnimationObject animationObject, int intervals, Action<Obj, Transform> updateTransform)
		{
			id_ = id;
			animationTransform_ = animationObject.transform;
			intervals_ = intervals;
			updateTransform_ = updateTransform;
		}

		public void Process()
		{
			if ((intervals_ == 0 || Time.frameCount % intervals_ == 1) && updateTransform_ != null)
			{
				updateTransform_(this, animationTransform_);
			}
		}
	}

	private static GS2_OpObjCtrl instance_;

	private const float HANA_SPEED_X_BASE = -1f;

	private const float HANA_SPEED_Y_BASE = 1f;

	private int spotlight_command_status_;

	private ushort bg2_scale_cnt_;

	private AssetBundleSprite tonosaman_sprite_;

	private Transform tonosaman_Root_;

	private AssetBundleSprite tonosaman_mask_sprite_;

	private List<Obj> objList_ = new List<Obj>();

	private const float CANVAS_WIDTH = 1920f;

	private const float CANVAS_HEIGHT = 1080f;

	private const float CONVERT_X = 7.5f;

	private const float CONVERT_Y = 5.625f;

	private const float SPOTLIGHT_SPEED_X = 9.5f;

	private const float SPOTLIGHT_SPEED_Y = 2f;

	private const float SPOTLIGHT_FOCUS_X = 115f;

	private const float SPOTLIGHT_FOCUS_Y = 40f;

	private const float START_POS_X_L = -64f;

	private const float START_POS_X_R = 320f;

	private readonly Dictionary<uint, int> multipleAnimationNameTable_ = new Dictionary<uint, int>
	{
		{ 117u, 117 },
		{ 118u, 118 }
	};

	private readonly ushort[] sin_cos_table_ = new ushort[320]
	{
		0, 6, 12, 18, 25, 31, 37, 43, 49, 56,
		62, 68, 74, 80, 86, 92, 97, 103, 109, 115,
		120, 126, 131, 136, 142, 147, 152, 157, 162, 167,
		171, 176, 181, 185, 189, 193, 197, 201, 205, 209,
		212, 216, 219, 222, 225, 228, 231, 234, 236, 238,
		241, 243, 244, 246, 248, 249, 251, 252, 253, 254,
		254, 255, 255, 255, 256, 255, 255, 255, 254, 254,
		253, 252, 251, 249, 248, 246, 244, 243, 241, 238,
		236, 234, 231, 228, 225, 222, 219, 216, 212, 209,
		205, 201, 197, 193, 189, 185, 181, 176, 171, 167,
		162, 157, 152, 147, 142, 136, 131, 126, 120, 115,
		109, 103, 97, 92, 86, 80, 74, 68, 62, 56,
		49, 43, 37, 31, 25, 18, 12, 6, 0, 65530,
		65524, 65518, 65511, 65505, 65499, 65493, 65487, 65480, 65474, 65468,
		65462, 65456, 65450, 65444, 65439, 65433, 65427, 65421, 65416, 65410,
		65405, 65400, 65394, 65389, 65384, 65379, 65374, 65369, 65365, 65360,
		65355, 65351, 65347, 65343, 65339, 65335, 65331, 65327, 65324, 65320,
		65317, 65314, 65311, 65308, 65305, 65302, 65300, 65298, 65295, 65293,
		65292, 65290, 65288, 65287, 65285, 65284, 65283, 65282, 65282, 65281,
		65281, 65281, 65280, 65281, 65281, 65281, 65282, 65282, 65283, 65284,
		65285, 65287, 65288, 65290, 65292, 65293, 65295, 65298, 65300, 65302,
		65305, 65308, 65311, 65314, 65317, 65320, 65324, 65327, 65331, 65335,
		65339, 65343, 65347, 65351, 65355, 65360, 65365, 65369, 65374, 65379,
		65384, 65389, 65394, 65400, 65405, 65410, 65416, 65421, 65427, 65433,
		65439, 65444, 65450, 65456, 65462, 65468, 65474, 65480, 65487, 65493,
		65499, 65505, 65511, 65518, 65524, 65530, 0, 6, 12, 18,
		25, 31, 37, 43, 49, 56, 62, 68, 74, 80,
		86, 92, 97, 103, 109, 115, 120, 126, 131, 136,
		142, 147, 152, 157, 162, 167, 171, 176, 181, 185,
		189, 193, 197, 201, 205, 209, 212, 216, 219, 222,
		225, 228, 231, 234, 236, 238, 241, 243, 244, 246,
		248, 249, 251, 252, 253, 254, 254, 255, 255, 255
	};

	public static GS2_OpObjCtrl instance
	{
		get
		{
			if (instance_ == null)
			{
				instance_ = new GS2_OpObjCtrl();
			}
			return instance_;
		}
	}

	public void CreateObj_GS2(AnimationObject animationObject, uint id)
	{
		if (animationObject == null)
		{
			return;
		}
		if (id == 90 && 8 <= GSStatic.global_work_.scenario && GSStatic.global_work_.scenario <= 13)
		{
			animationObject.BeFlag |= 8;
			Create(id, animationObject, new Vector3(0f, 0f, animationObject.transform.localPosition.z), 0, delegate(Obj o, Transform t)
			{
				if ((long)bgCtrl.instance.bg_no == 69)
				{
					float bgPosX = GetBgPosX();
					if (bgPosX > 140f)
					{
						if ((int)bgPosX % 10 == 0)
						{
							t.localPosition += new Vector3(0f, -5.625f, 0f);
						}
					}
					else if (bgPosX < 120f && (int)bgPosX % 6 == 0)
					{
						t.localPosition += new Vector3(0f, 5.625f, 0f);
					}
					if (!bgCtrl.instance.is_scroll)
					{
						t.localPosition += new Vector3(0f, -5.625f, 0f);
					}
				}
			});
		}
		if (id == 81)
		{
			Create(id, animationObject, new Vector3(0f, 0f, animationObject.transform.localPosition.z), 0, delegate(Obj o, Transform t)
			{
				if ((long)bgCtrl.instance.bg_no == 35)
				{
					if (o.tmp0_ % 40 > 12 && o.tmp0_ % 40 <= 24)
					{
						if (o.tmp0_ % 6 < 3)
						{
							t.localPosition -= new Vector3(7.5f, 0f, 0f);
							bgCtrl.instance.bg_pos_x += 7.5f;
						}
						else
						{
							t.localPosition += new Vector3(7.5f, 0f, 0f);
							bgCtrl.instance.bg_pos_x -= 7.5f;
						}
					}
					o.tmp0_++;
				}
			});
		}
		if ((long)bgCtrl.instance.bg_no == 26)
		{
			switch (id)
			{
			case 71u:
				Create(id, animationObject, new Vector3(477f, -27f, animationObject.transform.localPosition.z), 10, delegate(Obj o, Transform t)
				{
					if (t.localPosition.x > 0f)
					{
						t.localPosition += new Vector3(-6.8142858f, 0f, 0f);
					}
				});
				break;
			case 72u:
				Create(id, animationObject, new Vector3(0f, 0f, animationObject.transform.localPosition.z), 7, delegate(Obj o, Transform t)
				{
					t.localPosition += new Vector3(0f, -2f, 0f);
				});
				break;
			case 73u:
				Create(id, animationObject, new Vector3(-264f, -156f, animationObject.transform.localPosition.z), 10, delegate(Obj o, Transform t)
				{
					if (t.localPosition.x < 0f)
					{
						t.localPosition += new Vector3(3.7714286f, 0f, 0f);
					}
					else
					{
						t.localPosition = new Vector3(0f, t.localPosition.y, t.localPosition.z);
					}
					if (t.localPosition.y < 0f)
					{
						t.localPosition += new Vector3(0f, 2.2285714f, 0f);
					}
				});
				break;
			case 76u:
				Create(id, animationObject, new Vector3(0f, -27f, animationObject.transform.localPosition.z), 7, delegate(Obj o, Transform t)
				{
					t.localPosition += new Vector3(0f, -2f, 0f);
				});
				break;
			case 74u:
			case 75u:
				break;
			}
		}
		else if ((long)bgCtrl.instance.bg_no == 36)
		{
			switch (id)
			{
			case 82u:
				animationObject.transform.localPosition = new Vector3(0f, -900f, 0f);
				break;
			case 83u:
				animationObject.transform.localPosition = new Vector3(0f, -900f, 0f);
				break;
			}
		}
		else if ((long)bgCtrl.instance.bg_no == 67)
		{
			switch (id)
			{
			case 87u:
			case 88u:
				animationObject.transform.localPosition += new Vector3(0f, 0f, 1f);
				break;
			case 89u:
			{
				animationObject.BeFlag |= 8;
				Obj obj = Create(id, animationObject, new Vector3(0f, 0f, animationObject.transform.localPosition.z + 1f), 0, delegate(Obj o, Transform t)
				{
					AnimationObject animationObject2 = AnimationSystem.Instance.FindObject(1, 0, 86);
					if (animationObject2 != null)
					{
						if (o.tmp0_ > 0)
						{
							o.tmp0_--;
							if (o.tmp0_ % 3 == 0)
							{
								t.localPosition += new Vector3(0f, (float)(o.tmp0_ / 3) * 5.625f, 0f);
							}
						}
					}
					else if (o.tmp0_ < 36)
					{
						o.tmp0_++;
						if (o.tmp0_ % 2 == 0)
						{
							t.localPosition += new Vector3(0f, -5.625f, 0f);
						}
					}
					else if (o.tmp0_ < 66)
					{
						o.tmp0_++;
						if (o.tmp0_ % 2 == 0)
						{
							t.localPosition += new Vector3(0f, 5.625f, 0f);
						}
					}
					else if (o.tmp0_ < 86)
					{
						o.tmp0_++;
						if (o.tmp0_ % 2 == 0)
						{
							t.localPosition += new Vector3(0f, -5.625f, 0f);
						}
					}
					else
					{
						float bgPosY = GetBgPosY();
						if (bgPosY > 136f)
						{
							t.localPosition += new Vector3(0f, 5.625f, 0f);
						}
						else if (137f - bgPosY / 6f < 3f)
						{
							float y2 = (137f - bgPosY) / 6f * 5.625f;
							t.localPosition += new Vector3(0f, y2, 0f);
						}
						else
						{
							t.localPosition += new Vector3(0f, 16.875f, 0f);
						}
					}
				});
				obj.tmp0_ = 24;
				break;
			}
			}
		}
		else if ((long)bgCtrl.instance.bg_no == 77)
		{
			if (id != 19)
			{
				return;
			}
			Create(id, animationObject, animationObject.transform.localPosition, 0, delegate(Obj o, Transform t)
			{
				if (GSFlag.Check(0u, scenario_GS2.SCF_21_MAX) && t.localPosition.y < ConvertPosY(-136f))
				{
					o.tmp0_++;
					float x = 0f;
					float y = 0f;
					int tmp0_ = o.tmp0_;
					if (tmp0_ < 36)
					{
						if (tmp0_ % 3 == 0)
						{
							x = 7.5f;
						}
						y = 5.625f;
					}
					else if (tmp0_ < 76)
					{
						if (tmp0_ % 6 == 0)
						{
							x = 7.5f;
						}
						if (tmp0_ % 4 == 0)
						{
							y = 5.625f;
						}
					}
					else if (tmp0_ < 86)
					{
						if (tmp0_ % 4 == 0)
						{
							x = 7.5f;
						}
						if (tmp0_ % 3 == 0)
						{
							y = 5.625f;
						}
					}
					else if (tmp0_ < 101)
					{
						if (tmp0_ % 3 == 0)
						{
							x = 7.5f;
						}
						if (tmp0_ % 2 == 0)
						{
							y = 5.625f;
						}
					}
					else
					{
						if (tmp0_ % 5 == 0)
						{
							x = 7.5f;
						}
						y = 5.625f;
					}
					t.localPosition += new Vector3(x, y, 0f);
				}
			});
		}
		else if (bgCtrl.instance.bg_no == 65535)
		{
			switch (id)
			{
			case 95u:
				Create(id, animationObject, animationObject.transform.localPosition, 0, delegate(Obj o, Transform t)
				{
					if ((long)bgCtrl.instance.bg_no == 120)
					{
						t.localPosition += new Vector3(0f, 5.625f, 0f);
					}
				});
				break;
			case 97u:
				Create(id, animationObject, animationObject.transform.localPosition + Vector3.forward, 2, delegate(Obj o, Transform t)
				{
					if ((long)bgCtrl.instance.bg_no == 120)
					{
						t.localPosition += new Vector3(0f, 5.625f, 0f);
					}
				});
				break;
			case 119u:
			case 120u:
			case 121u:
			case 122u:
			case 123u:
				Create(id, animationObject, new Vector3(-96f, -985f, animationObject.transform.localPosition.z), 0, delegate(Obj o, Transform t)
				{
					if ((long)bgCtrl.instance.bg_no == 120)
					{
						t.localPosition += new Vector3(0f, 5.625f, 0f);
					}
				});
				break;
			}
		}
		else if ((long)bgCtrl.instance.bg_no == 120)
		{
			switch (id)
			{
			case 117u:
				animationObject.transform.localScale = Vector3.one * 2f;
				Create(id, animationObject, new Vector3(-64f, UnityEngine.Random.Range(0, 192), animationObject.transform.localPosition.z), 0, delegate(Obj o, Transform t)
				{
					spotlight_main(o, t, 0);
				});
				animationObject.Alpha = 0.7f;
				break;
			case 118u:
				animationObject.transform.localScale = Vector3.one * 2f;
				Create(id, animationObject, new Vector3(320f, UnityEngine.Random.Range(0, 192), animationObject.transform.localPosition.z), 0, delegate(Obj o, Transform t)
				{
					spotlight_main(o, t, 1);
				});
				animationObject.Alpha = 0.7f;
				break;
			}
		}
		else if ((long)bgCtrl.instance.bg_no == 121)
		{
			switch (id)
			{
			case 95u:
				Create(id, animationObject, new Vector3(0f, animationObject.transform.localPosition.y + 2160f, -30f), 0, delegate(Obj o, Transform t)
				{
					if ((long)bgCtrl.instance.bg_no == 122)
					{
						if (t.localPosition.y > 0f)
						{
							t.localPosition = new Vector3(0f, animationObject.transform.localPosition.y - 90f, -30f);
						}
						else
						{
							t.localPosition = new Vector3(0f, 0f, -30f);
						}
					}
				});
				break;
			case 97u:
				Create(id, animationObject, new Vector3(0f, 1080f, -29f), 2, delegate(Obj o, Transform t)
				{
					if ((long)bgCtrl.instance.bg_no == 122)
					{
						if (t.localPosition.y > 0f)
						{
							t.localPosition = new Vector3(0f, animationObject.transform.localPosition.y - 90f, -29f);
						}
						else
						{
							t.localPosition = new Vector3(0f, 0f, -29f);
						}
					}
				});
				break;
			case 119u:
			case 120u:
			case 121u:
			case 122u:
			case 123u:
				Create(id, animationObject, new Vector3(-96f, 1188f, animationObject.transform.localPosition.z), 0, delegate(Obj o, Transform t)
				{
					if ((long)bgCtrl.instance.bg_no == 122)
					{
						t.localPosition = new Vector3(t.localPosition.x, 1188f + bgCtrl.instance.body.transform.localPosition.y, t.localPosition.z);
					}
				});
				break;
			}
		}
		else if ((long)bgCtrl.instance.bg_no == 122)
		{
			if (id == 98)
			{
				animationObject.transform.localPosition += new Vector3(0f, 0f, -1f);
			}
		}
		else if ((long)bgCtrl.instance.bg_no == 97 && id == 124)
		{
			Create(id, animationObject, new Vector3(180f, -130f, animationObject.transform.localPosition.z), 30, delegate(Obj o, Transform t)
			{
				t.localPosition += new Vector3(-6f, 0f, 0f);
			});
		}
	}

	public Obj Create(uint id, AnimationObject animationObject, Vector3 initPos, int intervals, Action<Obj, Transform> updateTransform)
	{
		animationObject.transform.localPosition = initPos;
		return Create(id, animationObject, intervals, updateTransform);
	}

	public Obj Create(uint id, AnimationObject animationObject, int intervals, Action<Obj, Transform> updateTransform)
	{
		Obj obj = new Obj();
		obj.Init(id, animationObject, intervals, updateTransform);
		objList_.Add(obj);
		return obj;
	}

	public void Remove(int id)
	{
		Obj obj = objList_.Find((Obj o) => o.id_ == id);
		if (obj != null)
		{
			objList_.Remove(obj);
		}
	}

	public Obj Find(int id)
	{
		return objList_.Find((Obj o) => o.id_ == id);
	}

	public void ClearList()
	{
		objList_.Clear();
	}

	public void Process()
	{
		for (int i = 0; i < objList_.Count; i++)
		{
			Obj obj = objList_[i];
			obj.Process();
		}
	}

	public int SearchMultipleAnimationName(uint id)
	{
		if (multipleAnimationNameTable_.ContainsKey(id))
		{
			return multipleAnimationNameTable_[id];
		}
		return 0;
	}

	private void spotlight_main(Obj obj, Transform trans, int id)
	{
		spotlight_move(obj, trans, id);
	}

	private void spotlight_move(Obj obj, Transform trans, int id)
	{
		switch (obj.rno_)
		{
		case 0:
			if (id == 0)
			{
				obj.pos_ = new Vector3(-64f, UnityEngine.Random.Range(0, 192), 0f);
				obj.speed_ = new Vector3(9.5f, -2f + (float)UnityEngine.Random.Range(0, 16) * 2f / 8f, 0f);
			}
			else
			{
				obj.pos_ = new Vector3(320f, UnityEngine.Random.Range(0, 128), 0f);
				obj.speed_ = new Vector3(-9.5f, -2f + (float)UnityEngine.Random.Range(0, 16) * 2f / 8f, 0f);
			}
			if ((long)bgCtrl.instance.bg_no == 120)
			{
				obj.pos_ += new Vector3(0f, 192f + GetBgPosY(), 0f);
			}
			obj.rno_++;
			break;
		case 1:
			obj.pos_ += obj.speed_;
			if ((long)bgCtrl.instance.bg_no == 120)
			{
				obj.pos_ += new Vector3(0f, -1f, 0f);
			}
			if (id == 0)
			{
				if (obj.pos_.x >= 320f)
				{
					obj.rno_++;
					obj.cnt_ = UnityEngine.Random.Range(0, 256);
				}
			}
			else if (obj.pos_.x <= -64f)
			{
				obj.rno_++;
				obj.cnt_ = UnityEngine.Random.Range(0, 256);
			}
			break;
		case 2:
			if (spotlight_command_status_ == 1)
			{
				obj.rno_ = 3;
			}
			obj.pos_ = new Vector3(320f, obj.pos_.y, obj.pos_.z);
			if (obj.cnt_-- < 0)
			{
				obj.rno_ = 0;
			}
			break;
		case 3:
			if (is_all_spotlight_ready_to_focus())
			{
				if (id == 0)
				{
					obj.pos_ = new Vector3(-64f, -96 + UnityEngine.Random.Range(0, 80), obj.pos_.z);
				}
				else
				{
					obj.pos_ = new Vector3(320f, -48 + UnityEngine.Random.Range(0, 96), obj.pos_.z);
				}
				obj.speed_ = new Vector3(115f - obj.pos_.x, 40f - obj.pos_.y, 0f) / 96f;
				obj.rno_++;
				obj.cnt_ = 0;
			}
			break;
		case 4:
			obj.cnt_++;
			obj.pos_ += obj.speed_;
			if (obj.cnt_ >= 96)
			{
				obj.pos_ = new Vector3(115f, 40f, obj.pos_.z);
				obj.rno_++;
			}
			break;
		case 5:
			if ((long)bgCtrl.instance.bg_no == 122)
			{
				obj.animationTransform_.localScale = Vector3.one;
				obj.pos_ = new Vector3(obj.pos_.x, 232f - GetBgPosY(), obj.pos_.z);
			}
			break;
		}
		UpdateTransform(obj, trans);
	}

	public void spotlight_move_focus()
	{
		spotlight_command_status_ = 1;
	}

	private bool is_all_spotlight_ready_to_focus()
	{
		for (int i = 0; i < 2; i++)
		{
			Obj obj = Find(117 + i);
			if (obj != null && obj.rno_ < 3)
			{
				return false;
			}
		}
		return true;
	}

	public bool sc3_opening_tonosaman_update()
	{
		if (bg2_scale_cnt_ == 0)
		{
			sc3_opening_tonosaman_up_bg_start();
			return true;
		}
		if (bg2_scale_cnt_ < 32768)
		{
			sc3_opening_tonosaman_up_bg_main();
			if (bg2_scale_cnt_ > 32)
			{
				return true;
			}
			bg2_scale_cnt_ = 32768;
			return true;
		}
		sc3_opening_tonosaman_up_bg_end();
		return false;
	}

	private void sc3_opening_tonosaman_up_bg_start()
	{
		bg2_scale_cnt_ = 224;
		GameObject gameObject = new GameObject("tonosaman_mask");
		gameObject.transform.parent = AnimationSystem.Instance.instance_parent;
		SpriteRenderer sprite_renderer_ = gameObject.AddComponent<SpriteRenderer>();
		tonosaman_mask_sprite_ = new AssetBundleSprite();
		tonosaman_mask_sprite_.sprite_renderer_ = sprite_renderer_;
		tonosaman_mask_sprite_.load("/menu/common/", "mask");
		tonosaman_mask_sprite_.transform.localScale = new Vector3(1920f, 1080f, 1f);
		tonosaman_mask_sprite_.sprite_renderer_.sortingOrder = 0;
		tonosaman_mask_sprite_.sprite_renderer_.color = Color.white;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.layer = AnimationSystem.Instance.instance_parent.gameObject.layer;
		GameObject gameObject2 = new GameObject("tonosaman_Root");
		tonosaman_Root_ = gameObject2.transform;
		tonosaman_Root_.parent = AnimationSystem.Instance.instance_parent;
		tonosaman_Root_.localPosition = new Vector3(0f, 230f, -30f);
		gameObject2.layer = gameObject2.transform.parent.gameObject.layer;
		tonosaman_Root_.transform.localScale = new Vector3(0f, 0f, 1f);
		GameObject gameObject3 = new GameObject("tonosaman_sprite");
		gameObject3.transform.parent = AnimationSystem.Instance.instance_parent;
		gameObject3.transform.parent = tonosaman_Root_;
		gameObject3.transform.localPosition = new Vector3(-90f, -230f, 0f);
		gameObject3.layer = gameObject2.transform.parent.gameObject.layer;
		SpriteRenderer sprite_renderer_2 = gameObject3.AddComponent<SpriteRenderer>();
		tonosaman_sprite_ = new AssetBundleSprite();
		tonosaman_sprite_.sprite_renderer_ = sprite_renderer_2;
		tonosaman_sprite_.load("/GS2/BG/", "bg40e");
		tonosaman_sprite_.transform.localScale = new Vector3(1f, 1f, 1f);
		tonosaman_sprite_.sprite_renderer_.sortingOrder = 10;
		sc3_opening_tonosaman_up_bg_main();
	}

	private void sc3_opening_tonosaman_up_bg_main()
	{
		bg2_scale_cnt_ -= 14;
		tonosaman_Root_.localScale += new Vector3(0.3f, 0.3f, 0f);
	}

	private void sc3_opening_tonosaman_up_bg_end()
	{
		tonosaman_mask_sprite_.end();
		tonosaman_mask_sprite_.remove();
		tonosaman_sprite_.end();
		tonosaman_sprite_.remove();
		UnityEngine.Object.Destroy(tonosaman_mask_sprite_.obj);
		UnityEngine.Object.Destroy(tonosaman_sprite_.obj);
		UnityEngine.Object.Destroy(tonosaman_Root_.gameObject);
		tonosaman_mask_sprite_ = null;
		tonosaman_sprite_ = null;
		bg2_scale_cnt_ = 0;
		spotlight_command_status_ = 0;
	}

	private void HanaObj_set_start_pos(Obj o, int no)
	{
		o.pos_ = new Vector3(UnityEngine.Random.Range(0, 256) + 64, -16f, 0f);
		o.speed_ = new Vector3(-1f + (float)UnityEngine.Random.Range(0, 16) / 256f, 1f + (float)UnityEngine.Random.Range(0, 24) / 256f, 0f);
		if (no >= 8)
		{
			o.speed_ /= 2f;
		}
		o.yuragi_cnt_ = UnityEngine.Random.Range(0, 256);
		o.yuragi_add_ = UnityEngine.Random.Range(0, 3);
		o.animationTransform_.localPosition = new Vector3(ConvertPosX(o.pos_.x), ConvertPosY(o.pos_.y), o.animationTransform_.localPosition.z);
	}

	private void HanaObj_init(Obj o, int no)
	{
		o.subId_ = no;
		o.cnt_ = UnityEngine.Random.Range(0, 256);
		HanaObj_set_start_pos(o, no);
	}

	private void HanaObj_end(int no)
	{
		Remove(101 + no);
		AnimationSystem.Instance.StopObject(0, 0, 101 + no);
	}

	private void HanaObj_move(Obj o, Transform trans)
	{
		if (o.cnt_ <= 0)
		{
			o.pos_ += o.speed_;
			if (Time.frameCount % 1 != 0)
			{
				o.pos_ += new Vector3(o.speed_.x * (float)(int)sin_cos_table_[(o.yuragi_cnt_ += o.yuragi_add_) % 256] / 255f, 0f, 0f);
			}
			trans.localPosition = new Vector3(ConvertPosX(o.pos_.x), ConvertPosY(o.pos_.y), trans.localPosition.z);
			if (o.pos_.y >= 202f)
			{
				HanaObj_set_start_pos(o, o.subId_);
			}
		}
		else
		{
			o.cnt_--;
		}
	}

	public void hanabira_move_start()
	{
		for (int i = 0; i < 16; i++)
		{
			AnimationObject animationObject = AnimationSystem.Instance.PlayObject(0, 0, 101 + i);
			Obj o2 = Create((uint)(101uL + (ulong)i), animationObject, animationObject.transform.localPosition, 0, delegate(Obj o, Transform t)
			{
				HanaObj_move(o, t);
			});
			HanaObj_init(o2, i);
		}
	}

	public void hanabira_move_end()
	{
		for (int i = 0; i < 16; i++)
		{
			HanaObj_end(i);
		}
	}

	private float GetBgPosX()
	{
		return (0f - bgCtrl.instance.body.transform.localPosition.x) / 1920f * 240f;
	}

	private float GetBgPosY()
	{
		return (bgCtrl.instance.body.transform.localPosition.y + 1080f) / 1080f * 192f;
	}

	private float ConvertPosX(float objX)
	{
		return objX * 7.5f - 960f;
	}

	private float ConvertPosY(float objY)
	{
		return (0f - objY) * 5.625f + 540f;
	}

	private void UpdateTransform(Obj o, Transform trans)
	{
		trans.localPosition = new Vector3(ConvertPosX(o.pos_.x), ConvertPosY(o.pos_.y), trans.localPosition.z);
	}
}
