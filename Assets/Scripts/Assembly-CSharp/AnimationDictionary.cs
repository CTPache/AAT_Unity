using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDictionary : ScriptableObject
{
	[Serializable]
	public class TextureAnmNameRelation
	{
		[SerializeField]
public string key;

		[SerializeField]
public List<string> values;

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
public List<TextureAnmNameRelation> relations;

	public List<TextureAnmNameRelation> Relations
	{
		get
		{
			return relations;
		}
	}
}
