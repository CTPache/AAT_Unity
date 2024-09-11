using System;
using System.Collections.Generic;
using UnityEngine;

public class InternationalFilesObject : ScriptableObject
{
	[Serializable]
	public class InternationalFile
	{
		public string key;

		public LanguageFileName value;
	}

	[SerializeField]
	public List<InternationalFile> file_names = new List<InternationalFile>();
}
