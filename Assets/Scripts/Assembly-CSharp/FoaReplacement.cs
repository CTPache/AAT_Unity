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

		public string InFrance;

		public string InGerman;

		public string InKorea;

		public string InChina_s;

		public string InChina_t;
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
				case Language.FRANCE:
					return replace.InFrance;
				case Language.GERMAN:
					return replace.InGerman;
				case Language.KOREA:
					return replace.InKorea;
				case Language.CHINA_S:
					return replace.InChina_s;
				case Language.CHINA_T:
					return replace.InChina_t;
				default:
					return replace.InEnglish;
				}
			}
		}
		return key;
	}
}
