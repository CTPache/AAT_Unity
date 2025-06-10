using System;
using System.Collections.Generic;
using UnityEngine;

public class Evaluator : MonoBehaviour
{
	[SerializeField]
public AnimationCurve curve;

	public List<UnityEngine.Object> receivers;

	[NonSerialized]
	public float current;

	private float time_at_started;

	private float current_position;

	public void Initialize()
	{
		current = 0f;
		time_at_started = 0f;
		current_position = 0f;
		base.enabled = false;
	}

	private void OnEnable()
	{
		time_at_started = Time.time - current_position;
	}

	private void FixedUpdate()
	{
		Process();
	}

	private void Process()
	{
		current_position = Time.time - time_at_started;
		current = curve.Evaluate(current_position);
		foreach (IEvaluateReceiver receiver in receivers)
		{
			receiver.SetEvaluatedValue(current);
		}
	}
}
