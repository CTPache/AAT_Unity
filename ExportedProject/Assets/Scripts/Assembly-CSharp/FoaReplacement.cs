using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Foa", menuName = "Scriptable/FOA")]
public class FoaReplacement : ScriptableObject
{
	[Serializable]
	public class ReplacedAnimationNames
	{
		public string Key;

		public string InJapanese;

		public string InEnglish;
	}

	[SerializeField]
	private List<ReplacedAnimationNames> replaces;

	public string Replace(string key)
	{
		foreach (ReplacedAnimationNames replace in replaces)
		{
			if (replace.Key == key)
			{
				switch (GSStatic.global_work_.language)
				{
				case Language.JAPAN:
					return replace.InJapanese;
				case Language.USA:
					return replace.InEnglish;
				default:
					return replace.InJapanese;
				}
			}
		}
		return key;
	}
}
