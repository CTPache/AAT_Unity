using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDictionary : ScriptableObject
{
	[Serializable]
	public class TextureAnmNameRelation
	{
		[SerializeField]
		private string key;

		[SerializeField]
		private List<string> values;

		public string Key
		{
			get
			{
				return key;
			}
		}

		public List<string> Values
		{
			get
			{
				return values;
			}
		}
	}

	[SerializeField]
	private List<TextureAnmNameRelation> relations;

	public List<TextureAnmNameRelation> Relations
	{
		get
		{
			return relations;
		}
	}
}
