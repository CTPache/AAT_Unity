using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class BoolButtonAttribute : PropertyAttribute
{
	public string label_;

	public string method_name_;

	public BoolButtonAttribute(string label, string method_name)
	{
		label_ = label;
		method_name_ = method_name;
	}
}
