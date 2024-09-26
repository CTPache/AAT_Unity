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
                string lang = Language.langFallback[GSStatic.global_work_.language].ToUpper();
                switch (lang)
                {
				case "JAPAN":
					return replace.InJapanese;
				case "USA":
					return replace.InEnglish;
				case "FRANCE":
					return replace.InFrance;
				case "GERMAN":
					return replace.InGerman;
				case "KOREA":
					return replace.InKorea;
				case "CHINA_S":
					return replace.InChina_s;
				case "CHINA_T":
					return replace.InChina_t;
				default:
					return replace.InEnglish;
				}
			}
		}
		return key;
	}
}
