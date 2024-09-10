using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evidenceObjectManager : MonoBehaviour
{
	[SerializeField]
	private Material dev_coll_maetrial_;

	[SerializeField]
	private Material dev_coll_maetrial_nuke;

	[SerializeField]
	private Material ret_coll_maetrial_;

	[SerializeField]
	private Transform operete_trans_;

	[SerializeField]
	private Transform model_parent_;

	[SerializeField]
	private AnimationCurve curve_;

	private List<AssetBundleCtrl.AssetBundleData> asset_list_ = new List<AssetBundleCtrl.AssetBundleData>();

	private List<StModelSet> evidences_ = new List<StModelSet>();

	private List<StModelSet> add_model_list_ = new List<StModelSet>();

	private float scale_ratio_ = 1f;

	private Vector3 request_pos_ = Vector3.zero;

	private Vector3 request_angle_ = Vector3.zero;

	private float request_scale_ = 1f;

	private bool busy_request_;

	public const string MODEL_OBJ_MARK_HEADER = "_!!_";

	public float scale_ratio
	{
		get
		{
			return scale_ratio_;
		}
	}

	public bool busy_request
	{
		get
		{
			return busy_request_;
		}
	}

	public void LoadEvidenceModel(polyData in_poly)
	{
		Release();
		evidences_.Add(LoadAndInstantiate(LoadAssetBundle(in_poly), in_poly));
		for (polyData subPolyData = polyDataCtrl.instance.GetSubPolyData(in_poly); subPolyData != null; subPolyData = polyDataCtrl.instance.GetSubPolyData(subPolyData))
		{
			StModelSet item = LoadAndInstantiate(LoadAssetBundle(subPolyData), subPolyData);
			item.active = false;
			evidences_.Add(item);
		}
		SetPosition(0f, 0f);
		SetScaleRatio(1f);
		SetRotate(0f, 0f);
	}

	public StModelSet AddLoadEventModel(int in_table_id)
	{
		polyData in_poly = polyDataCtrl.instance.GetPolyData(in_table_id);
		StModelSet stModelSet = LoadAndInstantiate(LoadAssetBundle(in_poly), in_poly);
		stModelSet.active = false;
		add_model_list_.Add(stModelSet);
		return stModelSet;
	}

	private AssetBundle LoadAssetBundle(polyData in_poly)
	{
		foreach (AssetBundleCtrl.AssetBundleData item in asset_list_)
		{
			if (item.path_ == "/GS1/science/" && item.name_ == in_poly.common_name)
			{
				return item.bundle_;
			}
		}
		AssetBundleCtrl.AssetBundleData assetBundleData = new AssetBundleCtrl.AssetBundleData();
		assetBundleData.path_ = "/GS1/science/";
		assetBundleData.name_ = in_poly.common_name;
		string in_path = Application.streamingAssetsPath + "/GS1/science/" + in_poly.common_name + ".unity3d";
		byte[] binary = decryptionCtrl.instance.load(in_path);
		assetBundleData.bundle_ = AssetBundle.LoadFromMemory(binary);
		asset_list_.Add(assetBundleData);
		return assetBundleData.bundle_;
	}

	private void AddChildMeshCollider(Transform target_object, string[] obj_names)
	{
		foreach (Transform item in target_object)
		{
			foreach (string text in obj_names)
			{
				if (item.name == text)
				{
					item.gameObject.AddComponent<MeshCollider>();
				}
			}
			AddChildMeshCollider(item, obj_names);
		}
	}

	private StModelSet LoadAndInstantiate(AssetBundle in_asset_bundle, polyData in_poly)
	{
		GameObject gameObject = LoadAsset(in_asset_bundle, in_poly.prefab_name);
		if (in_poly.col_obj_names != null)
		{
			AddChildMeshCollider(gameObject.transform, in_poly.col_obj_names);
		}
		GameObject gameObject2 = null;
		if (!string.IsNullOrEmpty(in_poly.hit_prefab_name))
		{
			gameObject2 = LoadAsset(in_asset_bundle, in_poly.hit_prefab_name);
			MeshRenderer[] componentsInChildren = gameObject2.transform.GetComponentsInChildren<MeshRenderer>(true);
			foreach (MeshRenderer meshRenderer in componentsInChildren)
			{
				meshRenderer.gameObject.AddComponent<MeshCollider>();
				if (scienceInvestigationCtrl.instance.debug_show_area)
				{
					if (meshRenderer.gameObject.name.Contains("nuki") || meshRenderer.gameObject.name.Contains("nuke"))
					{
						meshRenderer.material = dev_coll_maetrial_nuke;
					}
					else
					{
						meshRenderer.material = dev_coll_maetrial_;
					}
				}
			}
		}
		return new StModelSet(gameObject, gameObject2, polyDataCtrl.instance.GetObjId(in_poly));
	}

	public void SetDebugColliderCollor()
	{
		MeshCollider[] componentsInChildren = model_parent_.GetComponentsInChildren<MeshCollider>(true);
		foreach (MeshCollider meshCollider in componentsInChildren)
		{
			MeshRenderer component = meshCollider.GetComponent<MeshRenderer>();
			if (!scienceInvestigationCtrl.instance.debug_show_area)
			{
				component.material = ret_coll_maetrial_;
			}
			else if (component.gameObject.name.Contains("nuki") || component.gameObject.name.Contains("nuke"))
			{
				component.material = dev_coll_maetrial_nuke;
			}
			else
			{
				component.material = dev_coll_maetrial_;
			}
		}
	}

	private GameObject LoadAsset(AssetBundle in_asset_bundle, string in_name)
	{
		GameObject original = in_asset_bundle.LoadAsset<GameObject>(in_name);
		GameObject gameObject = Object.Instantiate(original);
		gameObject.name = in_name;
		gameObject.layer = model_parent_.gameObject.layer;
		Vector3 localScale = gameObject.transform.localScale;
		gameObject.transform.parent = model_parent_;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = localScale;
		Utility.SetLayerAll(gameObject.transform, gameObject.layer);
		return gameObject;
	}

	public void Release()
	{
		foreach (AssetBundleCtrl.AssetBundleData item in asset_list_)
		{
			if (item.bundle_ != null)
			{
				item.bundle_.Unload(true);
			}
		}
		asset_list_.Clear();
		foreach (StModelSet item2 in evidences_)
		{
			item2.Destroy();
		}
		evidences_.Clear();
		foreach (StModelSet item3 in add_model_list_)
		{
			item3.Destroy();
		}
		add_model_list_.Clear();
	}

	public GameObject GetObject(string in_prefab_name)
	{
		return evidences_.Find((StModelSet p) => p.name == in_prefab_name).model_;
	}

	public int GetCurrentObjId()
	{
		return evidences_.Find((StModelSet p) => p.active).obj_id;
	}

	public void ChangeModel(string in_prefab_name)
	{
		for (int i = 0; i < evidences_.Count; i++)
		{
			StModelSet stModelSet = evidences_[i];
			stModelSet.active = evidences_[i].name == in_prefab_name;
		}
	}

	public void SetPosition(float in_pos_x, float in_pos_y)
	{
		operete_trans_.localPosition = new Vector3(in_pos_x, in_pos_y, 0f);
	}

	public void SetRotate(float in_angle_x, float in_angle_y)
	{
		operete_trans_.rotation = Quaternion.Euler(in_angle_y, in_angle_x, 0f);
	}

	public void SetScaleRatio(float in_scale_ratio)
	{
		operete_trans_.localScale = Vector3.one * in_scale_ratio;
		scale_ratio_ = in_scale_ratio;
	}

	private void AddRotate(float in_angle_x, float in_angle_y, float in_angle_z)
	{
		Vector3 zero = Vector3.zero;
		zero.x += in_angle_y;
		zero.y += in_angle_x;
		zero.z += in_angle_z;
		operete_trans_.Rotate(zero, Space.World);
	}

	public void AddVerticalRotate(float in_angle)
	{
		AddRotate(0f, in_angle, 0f);
	}

	public void AddHorizontalRotate(float in_angle)
	{
		AddRotate(in_angle, 0f, 0f);
	}

	public void AddRoll_Z_Rotate(float in_angle)
	{
		AddRotate(0f, 0f, in_angle);
	}

	public void AddScaleRatio(float in_scale_ratio)
	{
		SetScaleRatio(scale_ratio_ + in_scale_ratio);
	}

	public Coroutine RequestTo(Vector3 in_pos, Vector3 in_angle, float in_scale_ratio, float in_time)
	{
		request_pos_ = in_pos;
		request_angle_ = in_angle;
		request_scale_ = in_scale_ratio;
		busy_request_ = true;
		return StartCoroutine(RequestCoroutine(in_time));
	}

	private IEnumerator RequestCoroutine(float in_time)
	{
		float ratio2 = 0f;
		float now = 0f;
		float last_scale = scale_ratio_;
		Vector3 last_pos = operete_trans_.position;
		Vector3 last_euler = operete_trans_.eulerAngles;
		if (in_time > 0f)
		{
			while (now <= in_time)
			{
				now += Time.deltaTime;
				ratio2 = now / in_time;
				ratio2 = ((!(((!(ratio2 < 0f)) ? ratio2 : 0f) > 1f)) ? ratio2 : 1f);
				float value = curve_.Evaluate(ratio2);
				LerpParams(last_pos, last_euler, last_scale, value);
				yield return 0;
			}
		}
		LerpParams(last_pos, last_euler, last_scale, 1f);
		busy_request_ = false;
		yield return 0;
	}

	private void LerpParams(Vector3 last_pos, Vector3 last_euler, float last_scale, float in_time)
	{
		float x = Mathf.LerpAngle(last_pos.x, request_pos_.x, in_time);
		float y = Mathf.LerpAngle(last_pos.y, request_pos_.y, in_time);
		float z = Mathf.LerpAngle(last_pos.z, request_pos_.z, in_time);
		operete_trans_.position = new Vector3(x, y, z);
		operete_trans_.localRotation = Quaternion.Slerp(Quaternion.Euler(last_euler), Quaternion.Euler(request_angle_), in_time);
		float scaleRatio = Mathf.Lerp(last_scale, request_scale_, in_time);
		SetScaleRatio(scaleRatio);
	}
}
