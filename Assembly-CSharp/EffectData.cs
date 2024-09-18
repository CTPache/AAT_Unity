using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Scriptable/Effect")]
public class EffectData : ScriptableObject
{
	public enum EFFECT_TYPE
	{
		QUAKE = 1,
		FLASH = 2,
		CONTINUE = 3,
		CONTINUE2 = 4,
		CONTINUE3 = 5,
		CONTINUE4 = 6,
		CONTINUE5 = 7
	}

	[SerializeField]
	private string anm_name;

	[SerializeField]
	private EFFECT_TYPE effect_type;

	[SerializeField]
	private int sequence_num;

	public string AnmName
	{
		get
		{
			return anm_name;
		}
	}

	public EFFECT_TYPE EffectType
	{
		get
		{
			return effect_type;
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
