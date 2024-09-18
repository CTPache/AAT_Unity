using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coroutineCtrl : MonoBehaviour
{
	[Serializable]
	public class coroutineData
	{
		public IEnumerator self;

		public bool first = true;

		public bool is_end;
	}

	public class UpDateWait
	{
		public bool is_update = true;
	}

	private static coroutineCtrl instance_;

	public List<int> remove_list_ = new List<int>();

	public List<coroutineData> coroutine_list_ = new List<coroutineData>();

	private bool is_update;

	private float up_date_time;

	public static coroutineCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	private void Update()
	{
		is_update = true;
		up_date_time = Time.time;
	}

	private void FixedUpdate()
	{
		float num = Time.time - up_date_time;
		if (num < 1f / 30f)
		{
			is_update = true;
		}
		Process();
		is_update = false;
	}

	private void Process()
	{
		if (remove_list_.Count > 0)
		{
			remove_list_.Sort((int a, int b) => b - a);
			foreach (int item in remove_list_)
			{
				coroutine_list_.RemoveAt(item);
			}
		}
		remove_list_.Clear();
		for (int num = coroutine_list_.Count - 1; num >= 0; num--)
		{
			PlayCoroutine(coroutine_list_[num], true);
		}
	}

	private bool PlayCoroutine(coroutineData in_data, bool in_not = false)
	{
		bool result = false;
		if (in_not && in_data.first)
		{
			in_data.first = false;
			return result;
		}
		object current = in_data.self.Current;
		if (current == null)
		{
			if (!is_update)
			{
				return result;
			}
		}
		else if (!(current is WaitForSeconds))
		{
			if (current is UpDateWait)
			{
				if (!is_update)
				{
					return result;
				}
			}
			else if (current is coroutineData)
			{
				coroutineData coroutineData = current as coroutineData;
				if (!coroutineData.is_end)
				{
					return result;
				}
			}
		}
		if (!in_data.self.MoveNext())
		{
			in_data.is_end = true;
			for (int i = 0; i < coroutine_list_.Count; i++)
			{
				if (in_data.self.GetHashCode() == coroutine_list_[i].self.GetHashCode())
				{
					if (!remove_list_.Contains(i))
					{
						remove_list_.Add(i);
						break;
					}
					result = true;
				}
			}
		}
		return result;
	}

	public coroutineData Play(IEnumerator in_self)
	{
		coroutineData coroutineData = null;
		coroutineData = Data(in_self);
		coroutine_list_.Add(coroutineData);
		PlayCoroutine(coroutineData);
		return coroutineData;
	}

	public coroutineData Data(IEnumerator in_self)
	{
		coroutineData coroutineData = new coroutineData();
		coroutineData.self = in_self;
		coroutineData.first = true;
		coroutineData.is_end = false;
		return coroutineData;
	}

	public void Stop(IEnumerator in_enumerator)
	{
		AddStop(in_enumerator);
	}

	public void AddStop(IEnumerator in_enumerator)
	{
		for (int i = 0; i < coroutine_list_.Count; i++)
		{
			if (coroutine_list_[i].self.GetHashCode() == in_enumerator.GetHashCode() && !remove_list_.Contains(i))
			{
				remove_list_.Add(i);
			}
		}
	}
}
