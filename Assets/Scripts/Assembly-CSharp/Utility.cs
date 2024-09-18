using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

public class Utility
{
	private const int time_diff = 32400;

	public static T Min<T>(T a, T b) where T : IComparable
	{
		return (a.CompareTo(b) < 0) ? a : b;
	}

	public static T Max<T>(T a, T b) where T : IComparable
	{
		return (a.CompareTo(b) < 0) ? b : a;
	}

	public static void SetLayerAllChildren(Transform trans, int layer)
	{
		foreach (Transform tran in trans)
		{
			tran.gameObject.layer = layer;
			SetLayerAllChildren(tran, layer);
		}
	}

	public static void SetLayerChildrens(Transform trans, int layer)
	{
		trans.gameObject.layer = layer;
		foreach (Transform tran in trans)
		{
			tran.gameObject.layer = layer;
		}
	}

	public static void SetLayerAll(Transform trans, int layer)
	{
		trans.gameObject.layer = layer;
		foreach (Transform tran in trans)
		{
			SetLayerAll(tran, layer);
		}
	}

	public static Transform GetChildTrans(Transform in_trans, string in_name)
	{
		if (in_trans.name == in_name)
		{
			return in_trans;
		}
		foreach (Transform in_tran in in_trans)
		{
			Transform childTrans = GetChildTrans(in_tran, in_name);
			if (childTrans != null)
			{
				return childTrans;
			}
		}
		return null;
	}

	public static Transform GetChildTransContainsName(Transform in_trans, string in_name)
	{
		if (in_trans.name.Contains(in_name))
		{
			return in_trans;
		}
		foreach (Transform in_tran in in_trans)
		{
			Transform childTransContainsName = GetChildTransContainsName(in_tran, in_name);
			if (childTransContainsName != null)
			{
				return childTransContainsName;
			}
		}
		return null;
	}

	public static int intParse(string in_str)
	{
		int result = 0;
		try
		{
			result = int.Parse(in_str);
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		return result;
	}

	public static float floatParse(string in_str)
	{
		float result = 0f;
		try
		{
			result = float.Parse(in_str);
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		return result;
	}

	public static bool boolParse(string in_str)
	{
		bool result = true;
		try
		{
			result = bool.Parse(in_str);
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		return result;
	}

	public static bool EnumTryParse<T>(string in_str, out T result) where T : IConvertible
	{
		result = default(T);
		if (!typeof(T).IsEnum)
		{
			return false;
		}
		Array values = Enum.GetValues(typeof(T));
		foreach (object item in values)
		{
			if (in_str == item.ToString())
			{
				result = (T)item;
				return true;
			}
		}
		return false;
	}

	public static bool IsFirstVectorCloser(Vector3 criterion, Vector3 first, Vector3 second)
	{
		return (first - criterion).sqrMagnitude < (second - criterion).sqrMagnitude;
	}

	public static void FindSetShader(Transform trans)
	{
	}

	public static string GetRelativePass(Transform transform, Transform target)
	{
		if (target.parent == null || target.parent == transform)
		{
			return string.Empty;
		}
		return GetRelativePass(transform, target.parent) + "/" + target.parent.name;
	}

	public static string BBCode_ColoredText(string text, string color_hexaDecimal)
	{
		return string.Format("[{0}]{1}[-]", color_hexaDecimal, text);
	}

	public static List<string> SplitByString(string original, string separator)
	{
		string[] collection = original.Split(new string[1] { separator }, StringSplitOptions.None);
		return new List<string>(collection);
	}

	public static List<string> SplitByLength(string original, int count)
	{
		List<string> list = new List<string>();
		for (int i = 0; i * count < original.Length; i++)
		{
			list.Add(original.Substring(i * count, count));
		}
		return list;
	}

	public static string ToCsvStr<T>(List<T> source)
	{
		List<string> list = new List<string>(source.Count);
		foreach (T item in source)
		{
			list.Add(item.ToString());
		}
		return string.Join(",", list.ToArray());
	}

	public static string ToCsvStr<T>(List<T> source, Func<T, string> to_string)
	{
		List<string> list = new List<string>(source.Count);
		foreach (T item in source)
		{
			list.Add(to_string(item));
		}
		return string.Join(",", list.ToArray());
	}

	public static List<T> CSVToList<T>(string csv_str, Func<string, T> convert)
	{
		string[] source = csv_str.Split(',');
		return source.Select(convert).ToList();
	}

	public static Color32 ByteToColor(List<byte> list)
	{
		return new Color32(list[0], list[1], list[2], byte.MaxValue);
	}

	public static Color CSVToColor(string csv_color)
	{
		return ByteToColor(CSVToList(csv_color, byte.Parse));
	}

	public static int CountUnit(int num_total, int num_unit)
	{
		int num = num_total / num_unit;
		int num2 = num_total - num_unit * num;
		return num + ((num2 != 0) ? 1 : 0);
	}

	public static T GetComponentInGroup<T>(IEnumerable<GameObject> target_group) where T : Component
	{
		foreach (GameObject item in target_group)
		{
			T component = item.GetComponent<T>();
			if (component != null)
			{
				return component;
			}
		}
		return (T)null;
	}

	public static T GetComponentInGroup<T, U>(IEnumerable<U> target_group) where T : Component where U : Component
	{
		foreach (U item in target_group)
		{
			U current = item;
			T component = current.GetComponent<T>();
			if (component != null)
			{
				return component;
			}
		}
		return (T)null;
	}

	public static List<T> GetComponentsInGroup<T>(IEnumerable<GameObject> target_group) where T : Component
	{
		List<T> list = new List<T>();
		foreach (GameObject item in target_group)
		{
			T component = item.GetComponent<T>();
			if (component != null)
			{
				list.Add(component);
			}
		}
		return list;
	}

	public static List<T> GetComponentsInGroup<T, U>(IEnumerable<U> target_group) where T : Component where U : Component
	{
		List<T> list = new List<T>();
		foreach (U item in target_group)
		{
			U current = item;
			T component = current.GetComponent<T>();
			if (component != null)
			{
				list.Add(component);
			}
		}
		return list;
	}

	public static void ObjectToFormated(StringBuilder output, object target, bool recursive)
	{
		ObjectToFormated(output, target, recursive, 0);
	}

	private static void ObjectToFormated(StringBuilder output, object target, bool recursive, int nest_level)
	{
		string text = ((nest_level <= 0) ? string.Empty : Enumerable.Repeat(" ", nest_level).Aggregate((string s1, string s2) => s1 + s2));
		string value = text + " ";
		Type type2 = target.GetType();
		output.Append(text);
		output.Append(type2.Name);
		output.Append(Environment.NewLine);
		FieldInfo[] fields = target.GetType().GetFields();
		foreach (FieldInfo fieldInfo in fields)
		{
			output.Append(value);
			output.Append(fieldInfo.Name);
			output.Append(" : ");
			object value2 = fieldInfo.GetValue(target);
			if (value2 is string)
			{
				output.Append(value2);
			}
			else if (value2 is IEnumerable)
			{
				output.Append(Environment.NewLine);
				if (recursive && !value2.GetType().GetGenericArguments().All((Type type) => type.IsValueType))
				{
					foreach (object item in value2 as IEnumerable)
					{
						ObjectToFormated(output, item, recursive, nest_level + 2);
					}
				}
				else
				{
					int num = 0;
					foreach (object item2 in value2 as IEnumerable)
					{
						output.Append(" [" + num++ + "]" + item2.ToString());
						output.Append(Environment.NewLine);
					}
				}
			}
			else if (!value2.GetType().IsValueType && recursive)
			{
				ObjectToFormated(output, value2, recursive, nest_level + 1);
			}
			else
			{
				output.Append(value2);
			}
			output.Append(Environment.NewLine);
		}
	}

	public static DateTime UnixToDateTime(long unix_time, bool add_time_diff)
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unix_time);
		return (!add_time_diff) ? dateTime : dateTime.AddSeconds(32400.0);
	}
}
