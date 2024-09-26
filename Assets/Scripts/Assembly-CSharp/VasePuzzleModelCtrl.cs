using System;
using System.Collections;
using UnityEngine;

public class VasePuzzleModelCtrl : MonoBehaviour
{
	private AssetBundle l_vase_bundle;

	[SerializeField]
	private Transform vase_base_;

	private GameObject[] pieces_list_;

	private GameObject[] vase_list_;

	private GameObject[] flashing_list_;

	private IEnumerator vase_animation_coroutine_;

	private int current_vase_index_;

	public static VasePuzzleModelCtrl instance { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	public float GetAngleZ()
	{
		return vase_base_.localEulerAngles.z;
	}

	public void BaseRotate(float l_animation_angle)
	{
		vase_base_.localEulerAngles = new Vector3(0f, l_animation_angle, 0f);
	}

	public void init(PiecesStatus[] in_pieces_status)
	{
		l_vase_bundle = AssetBundleCtrl.instance.load("/GS1/science/", "itm05a0");
		pieces_list_ = new GameObject[in_pieces_status.Length];
		for (int i = 0; i < pieces_list_.Length; i++)
		{
			debugLogger.instance.Log("Puzzle", "load AssetBundle > name[" + in_pieces_status[i].model_name + "]");
			pieces_list_[i] = UnityEngine.Object.Instantiate(l_vase_bundle.LoadAsset<GameObject>(in_pieces_status[i].model_name));
			pieces_list_[i].transform.parent = base.transform;
			pieces_list_[i].name = in_pieces_status[i].model_name;
			_setLayer(pieces_list_[i], base.gameObject.layer);
			pieces_list_[i].transform.localPosition = Vector3.zero;
			pieces_list_[i].transform.eulerAngles = Vector3.zero;
			pieces_list_[i].transform.localScale = Vector3.one;
		}
		string[] array = new string[10] { "itm05a3", "itm0590", "itm0591", "itm0592", "itm0593", "itm0594", "itm0595", "itm0596", "itm05a2", "itm05a0" };
		vase_list_ = new GameObject[array.Length];
		for (int j = 0; j < vase_list_.Length; j++)
		{
			string text = array[j];
			string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
			switch (lang)
			{
			case "USA":
			case "FRANCE":
			case "GERMAN":
				text += "u";
				break;
			case "KOREA":
				text += "k";
				break;
			}
			debugLogger.instance.Log("Puzzle", "load AssetBundle > name[" + array[j] + "]");
			vase_list_[j] = UnityEngine.Object.Instantiate(l_vase_bundle.LoadAsset<GameObject>(text));
			vase_list_[j].transform.parent = vase_base_;
			vase_list_[j].name = text;
			_setLayer(vase_list_[j], base.gameObject.layer);
			vase_list_[j].transform.localPosition = Vector3.zero;
			vase_list_[j].transform.eulerAngles = Vector3.zero;
			vase_list_[j].transform.localScale = new Vector3(1f, 1f, 1f);
		}
		string[] array2 = new string[9] { "flashing_1", "flashing_2", "flashing_3", "flashing_4", "flashing_5", "flashing_6", "flashing_7", "flashing_8", "flashing_9" };
		flashing_list_ = new GameObject[array2.Length];
		for (int k = 0; k < flashing_list_.Length; k++)
		{
			debugLogger.instance.Log("Puzzle", "load AssetBundle > name[" + array2[k] + "]");
			flashing_list_[k] = UnityEngine.Object.Instantiate(l_vase_bundle.LoadAsset<GameObject>(array2[k]));
			flashing_list_[k].transform.parent = vase_list_[k].transform;
			flashing_list_[k].name = array2[k];
			_setLayer(vase_list_[k], base.gameObject.layer);
			flashing_list_[k].transform.localPosition = Vector3.zero;
			flashing_list_[k].transform.eulerAngles = Vector3.zero;
			flashing_list_[k].transform.localScale = new Vector3(1f, 1f, 1f);
			VasePuzzleFlashingAnimation vasePuzzleFlashingAnimation = flashing_list_[k].AddComponent<VasePuzzleFlashingAnimation>();
			vasePuzzleFlashingAnimation.renderer_ = _getChildRenderer(flashing_list_[k]);
			_setAllObjectRenderQueue(flashing_list_[k], 2100);
		}
	}

	public void exit()
	{
		if (l_vase_bundle != null)
		{
			l_vase_bundle.Unload(true);
		}
		l_vase_bundle = null;
		GameObject[] array = pieces_list_;
		foreach (GameObject obj in array)
		{
			UnityEngine.Object.Destroy(obj);
		}
		pieces_list_ = null;
		GameObject[] array2 = vase_list_;
		foreach (GameObject obj2 in array2)
		{
			UnityEngine.Object.Destroy(obj2);
		}
		vase_list_ = null;
		GameObject[] array3 = flashing_list_;
		foreach (GameObject obj3 in array3)
		{
			UnityEngine.Object.Destroy(obj3);
		}
		AssetBundleCtrl.instance.remove("/GS1/science/", "itm05a0");
		flashing_list_ = null;
	}

	public void setPiecesStatus(PiecesStatus[] in_pieces_status, int in_current_index)
	{
		for (int i = 0; i < pieces_list_.Length; i++)
		{
			if (in_current_index == i)
			{
				pieces_list_[i].SetActive(true);
				Debug.Log("x:0.0f y:" + in_pieces_status[i].front_rotate_y + " z:" + in_pieces_status[i].angle_id * 90);
				Transform transform = pieces_list_[i].transform;
				float front_rotate_y = in_pieces_status[i].front_rotate_y;
				float angle = in_pieces_status[i].angle_id * 90;
				Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
				transform.rotation = quaternion * Quaternion.Euler(0f, front_rotate_y, 0f);
				transform.localPosition = transform.up * (0f - in_pieces_status[i].offset_y);
				transform.localPosition = new Vector3(transform.localPosition.x + 3f, transform.localPosition.y + 1f, transform.localPosition.z + 1f);
			}
			else
			{
				pieces_list_[i].SetActive(false);
			}
		}
	}

	public void setVaseStatus(int in_pattern_id)
	{
		if (vase_list_.Length <= in_pattern_id)
		{
			Debug.LogWarning("vase_list_.Length(" + vase_list_.Length + ") <= in_pattern_id(" + in_pattern_id + ")");
		}
		current_vase_index_ = 0;
		for (int i = 0; i < vase_list_.Length; i++)
		{
			if (i == in_pattern_id)
			{
				vase_list_[i].SetActive(true);
				current_vase_index_ = i;
			}
			else
			{
				vase_list_[i].SetActive(false);
			}
		}
	}

	public void setFlashing(bool in_enables)
	{
		GameObject[] array = flashing_list_;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(in_enables);
		}
	}

	public void UnionVase(int in_pieces_id, bool in_success)
	{
		debugLogger.instance.Log("Puzzle", "Union success flag:" + in_success);
		if (in_success)
		{
			vase_animation_coroutine_ = _unionSuccessVase(pieces_list_[in_pieces_id]);
			coroutineCtrl.instance.Play(vase_animation_coroutine_);
		}
		else
		{
			vase_animation_coroutine_ = _unionFailureVase(pieces_list_[in_pieces_id]);
			coroutineCtrl.instance.Play(vase_animation_coroutine_);
		}
	}

	public void rotateVase(float in_start_wait, int in_angle, float in_speed)
	{
		debugLogger.instance.Log("Puzzle", "rotate:" + in_angle);
		vase_animation_coroutine_ = _rotateVase(in_start_wait, in_angle, in_speed);
		coroutineCtrl.instance.Play(vase_animation_coroutine_);
	}

	public bool isRotateEnd()
	{
		return vase_animation_coroutine_ == null;
	}

	private void _setLayer(GameObject in_parent, int in_layer)
	{
		in_parent.layer = in_layer;
		foreach (Transform item in in_parent.transform)
		{
			_setLayer(item.gameObject, in_layer);
		}
	}

	private IEnumerator _unionSuccessVase(GameObject in_pieces_object)
	{
		Vector3 l_start = in_pieces_object.transform.position;
		Vector3 l_ctrl = new Vector3(l_start.x, l_start.y + 1f, l_start.z);
		Vector3 l_end = vase_base_.transform.position;
		float l_time = 0f;
		Transform l_pieces = in_pieces_object.transform;
		while (l_time <= 1f)
		{
			l_time += Time.deltaTime * 2f;
			l_pieces.position = _getPosition(l_time, l_start, l_ctrl, l_end);
			yield return null;
		}
		Material in_material = new Material(Shader.Find("Sprites/Default"));
		_ChangeAllObjectMaterial(in_pieces_object, in_material);
		_ChangeAllObjectMaterial(vase_list_[current_vase_index_], in_material);
		soundCtrl.instance.PlaySE(428);
		float time = 0f;
		float wait = 0.5f;
		while (true)
		{
			time += Time.deltaTime;
			if (time > wait)
			{
				break;
			}
			yield return null;
		}
		Material in_material2 = new Material(Shader.Find("Unlit/Texture"));
		_ChangeAllObjectMaterial(in_pieces_object, in_material2);
		_ChangeAllObjectMaterial(vase_list_[current_vase_index_], in_material2);
		vase_animation_coroutine_ = null;
	}

	private IEnumerator _unionFailureVase(GameObject in_pieces_object)
	{
		Vector3 l_start = in_pieces_object.transform.position;
		Vector3 l_ctrl = new Vector3(l_start.x, l_start.y + 1f, l_start.z);
		Vector3 l_end = vase_base_.transform.position;
		float l_time = 0f;
		Transform l_pieces = in_pieces_object.transform;
		while (l_time <= 0.8f)
		{
			l_time += Time.deltaTime * 2f;
			l_pieces.position = _getPosition(l_time, l_start, l_ctrl, l_end);
			yield return null;
		}
		while (0f <= l_time)
		{
			l_time -= Time.deltaTime * 2f;
			l_pieces.position = _getPosition(l_time, l_start, l_ctrl, l_end);
			yield return null;
		}
		float time = 0f;
		float wait = 0.5f;
		while (true)
		{
			time += Time.deltaTime;
			if (time > wait)
			{
				break;
			}
			yield return null;
		}
		vase_animation_coroutine_ = null;
	}

	private IEnumerator _rotateVase(float in_start_wait, int in_angle, float in_speed)
	{
		float time2 = 0f;
		while (true)
		{
			time2 += Time.deltaTime;
			if (time2 > in_start_wait)
			{
				break;
			}
			yield return null;
		}
		float l_animation_angle = (int)vase_base_.localEulerAngles.y;
		float l_end = l_animation_angle + (float)in_angle;
		debugLogger.instance.Log("Puzzle", "start:" + l_animation_angle + " end:" + l_end);
		while (l_animation_angle != l_end)
		{
			l_animation_angle += in_speed;
			if (in_speed > 0f)
			{
				if (l_end <= l_animation_angle)
				{
					l_animation_angle = l_end;
				}
			}
			else if (l_end >= l_animation_angle)
			{
				l_animation_angle = l_end;
			}
			vase_base_.localEulerAngles = new Vector3(0f, l_animation_angle, 0f);
			yield return null;
		}
		float time = 0f;
		float wait = 0.5f;
		while (true)
		{
			time += Time.deltaTime;
			if (time > wait)
			{
				break;
			}
			yield return null;
		}
		vase_animation_coroutine_ = null;
	}

	private Vector3 _getPosition(float in_time, Vector3 in_start, Vector3 in_ctrl, Vector3 in_end)
	{
		if (1f <= in_time)
		{
			in_time = 1f;
		}
		if (in_time <= 0f)
		{
			in_time = 0f;
		}
		Vector3 zero = Vector3.zero;
		zero.x = (1f - in_time) * (1f - in_time) * in_start.x + 2f * in_time * (1f - in_time) * in_ctrl.x + in_time * in_time * in_end.x;
		zero.y = (1f - in_time) * (1f - in_time) * in_start.y + 2f * in_time * (1f - in_time) * in_ctrl.y + in_time * in_time * in_end.y;
		zero.z = (1f - in_time) * (1f - in_time) * in_start.z + 2f * in_time * (1f - in_time) * in_ctrl.z + in_time * in_time * in_end.z;
		return zero;
	}

	private MeshRenderer _getChildRenderer(GameObject in_object)
	{
		MeshRenderer component = in_object.GetComponent<MeshRenderer>();
		if (component != null)
		{
			return component;
		}
		foreach (Transform item in in_object.transform)
		{
			MeshRenderer meshRenderer = _getChildRenderer(item.gameObject);
			if (meshRenderer != null)
			{
				return meshRenderer;
			}
		}
		return null;
	}

	private void _setAllObjectRenderQueue(GameObject in_object, int in_queue)
	{
		MeshRenderer component = in_object.GetComponent<MeshRenderer>();
		if (component != null)
		{
			component.material.renderQueue = in_queue;
		}
		foreach (Transform item in in_object.transform)
		{
			_setAllObjectRenderQueue(item.gameObject, in_queue);
		}
	}

	private void _addAllObjectMaterial(GameObject in_object, Material in_material)
	{
		MeshRenderer component = in_object.GetComponent<MeshRenderer>();
		if (component != null)
		{
			_addMaterial(component, in_material);
		}
		foreach (Transform item in in_object.transform)
		{
			_addAllObjectMaterial(item.gameObject, in_material);
		}
	}

	private void _removeAllObjectMaterial(GameObject in_object)
	{
		MeshRenderer component = in_object.GetComponent<MeshRenderer>();
		if (component != null)
		{
			_removeMaterial(component);
		}
		foreach (Transform item in in_object.transform)
		{
			_removeAllObjectMaterial(item.gameObject);
		}
	}

	private void _addMaterial(MeshRenderer in_renderer, Material in_material)
	{
		int num = in_renderer.materials.Length;
		Material[] array = in_renderer.materials;
		Array.Resize(ref array, num + 1);
		array[num] = array[0];
		array[0] = in_material;
		in_renderer.materials = array;
	}

	private void _removeMaterial(MeshRenderer in_renderer)
	{
		int num = in_renderer.materials.Length;
		Material[] array = in_renderer.materials;
		array[0] = array[num - 1];
		Array.Resize(ref array, num - 1);
		in_renderer.materials = array;
	}

	private void _ChangeAllObjectMaterial(GameObject in_object, Material in_material)
	{
		MeshRenderer component = in_object.GetComponent<MeshRenderer>();
		if (component != null)
		{
			_ChangeMaterial(component, in_material);
		}
		foreach (Transform item in in_object.transform)
		{
			_ChangeAllObjectMaterial(item.gameObject, in_material);
		}
	}

	private void _ChangeMaterial(MeshRenderer in_renderer, Material in_material)
	{
		Material[] materials = in_renderer.materials;
		materials[0] = in_material;
		in_renderer.materials = materials;
	}
}
