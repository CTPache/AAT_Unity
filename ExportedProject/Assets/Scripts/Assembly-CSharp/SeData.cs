using UnityEngine;

[CreateAssetMenu(fileName = "Se", menuName = "Scriptable/Se")]
public class SeData : ScriptableObject
{
	[SerializeField]
	private string anm_name;

	[SerializeField]
	private string se_name;

	[SerializeField]
	private int sequence_num;

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
