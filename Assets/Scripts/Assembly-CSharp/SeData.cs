using UnityEngine;

[CreateAssetMenu(fileName = "Se", menuName = "Scriptable/Se")]
public class SeData : ScriptableObject
{
	[SerializeField]
public string anm_name;

	[SerializeField]
public string se_name;

	[SerializeField]
public int sequence_num;

	public string AnmName
	{
		get
		{
			return anm_name;
		}
	}

	public string SeName
	{
		get
		{
			return se_name;
		}
	}

	public int SequenceNum
	{
		get
		{
			return sequence_num;
		}
	}
}
