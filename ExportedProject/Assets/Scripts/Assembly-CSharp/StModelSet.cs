using UnityEngine;

public struct StModelSet
{
	public readonly GameObject model_;

	public readonly GameObject hit_;

	public readonly int obj_id_;

	public string name
	{
		get
		{
			return model_.name;
		}
	}

	public int obj_id
	{
		get
		{
			return obj_id_;
		}
	}

	public Transform transform
	{
		get
		{
			return model_.transform;
		}
	}

	public bool active
	{
		get
		{
			return model_.activeSelf;
		}
		set
		{
			SetActive(value);
		}
	}

	public StModelSet(GameObject in_model, GameObject in_hit, int in_obj_id)
	{
		model_ = in_model;
		hit_ = in_hit;
		obj_id_ = in_obj_id;
	}

	public void Destroy()
	{
		Object.Destroy(model_);
		if (hit_ != null)
		{
			Object.Destroy(hit_);
		}
	}

	private void SetActive(bool active)
	{
		model_.SetActive(active);
		if (hit_ != null)
		{
			hit_.SetActive(active);
		}
	}
}
