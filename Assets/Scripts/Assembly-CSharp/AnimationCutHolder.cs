using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AnimationCutHolder : MonoBehaviour
{
	public class AnimationSource
	{
		public List<Sprite> all_;

		public Texture2D Tex { get; set; }

		public Dictionary<string, List<Sprite>> Sprites { get; set; }

		public int TextureWidth
		{
			get
			{
				return Tex.width;
			}
		}

		public Dictionary<string, AnmData> Animations { get; set; }

		public string BundleName { get; set; }

		public AnimationSource()
		{
			Sprites = new Dictionary<string, List<Sprite>>();
			Animations = new Dictionary<string, AnmData>();
		}
	}

	[SerializeField]
	private int max_cache;

	private Dictionary<string, string> animation_name_to_bundle_name = new Dictionary<string, string>();

	private Dictionary<string, AnimationSource> bundle_name_to_source = new Dictionary<string, AnimationSource>();

	private Dictionary<string, AssetBundle> bundle_name_to_bundle = new Dictionary<string, AssetBundle>();

	private Dictionary<string, Dictionary<int, string>> animation_name_to_se_data = new Dictionary<string, Dictionary<int, string>>();

	private Dictionary<string, Dictionary<int, int>> animation_name_to_effect_data = new Dictionary<string, Dictionary<int, int>>();

	private Dictionary<string, int> source_texture_history = new Dictionary<string, int>();

	private AnimationIdentifier animation_identifier
	{
		get
		{
			return AnimationIdentifier.instance;
		}
	}

	public bool IsSourceTextureLoaded(string targetAnimationName)
	{
		return animation_name_to_bundle_name.ContainsKey(targetAnimationName);
	}

	public AnimationSource GetSource(string animationName)
	{
		if (!animation_name_to_bundle_name.ContainsKey(animationName) && !LoadPackage(animationName))
		{
			Debug.LogError("Animation source was not found.");
			return null;
		}
		string key = animation_name_to_bundle_name[animationName];
		return bundle_name_to_source[key];
	}

	public Dictionary<int, string> GetSeData(string animationName)
	{
		if (!animation_name_to_se_data.ContainsKey(animationName))
		{
			return null;
		}
		return animation_name_to_se_data[animationName];
	}

	public Dictionary<int, int> GetEffectData(string animationName)
	{
		if (!animation_name_to_effect_data.ContainsKey(animationName))
		{
			return null;
		}
		return animation_name_to_effect_data[animationName];
	}

	private bool LoadPackage(string targetAnimationName)
	{
		string text = animation_identifier.AnimationNameToBundlePath(targetAnimationName);
		if (string.IsNullOrEmpty(text))
		{
			Debug.LogError("リソースが見つからないアニメーション = " + targetAnimationName);
			return false;
		}
		byte[] binary = decryptionCtrl.instance.load(text);
		AssetBundle assetBundle = AssetBundle.LoadFromMemory(binary);
		UnityEngine.Object[] source = assetBundle.LoadAllAssets();
		AnimationSource animationSource = new AnimationSource();
		animationSource.BundleName = Path.GetFileName(text);
		bundle_name_to_source.Add(animationSource.BundleName, animationSource);
		bundle_name_to_bundle.Add(animationSource.BundleName, assetBundle);
		animationSource.Tex = source.Single((UnityEngine.Object obj) => obj is Texture2D) as Texture2D;
		int num = 0;
		foreach (AnmData item in source.Where((UnityEngine.Object obj) => obj is AnmData))
		{
			string key = item.name.ToLower();
			animationSource.Animations.Add(key, item);
			animation_name_to_bundle_name.Add(key, animationSource.BundleName);
			animationSource.Sprites.Add(key, Enumerable.Repeat<Sprite>(null, item.sprite_groups.Sum((AnmData.SpriteGroup gp) => gp.Sprites.Count)).ToList());
			num += animationSource.Sprites[key].Count;
		}
		foreach (SeData item2 in source.Where((UnityEngine.Object data) => data is SeData))
		{
			if (!animation_name_to_se_data.ContainsKey(item2.AnmName))
			{
				animation_name_to_se_data.Add(item2.AnmName, new Dictionary<int, string>());
			}
			animation_name_to_se_data[item2.AnmName].Add(item2.SequenceNum, item2.SeName);
		}
		foreach (EffectData item3 in source.Where((UnityEngine.Object data) => data is EffectData))
		{
			if (!animation_name_to_effect_data.ContainsKey(item3.AnmName))
			{
				animation_name_to_effect_data.Add(item3.AnmName, new Dictionary<int, int>());
			}
			animation_name_to_effect_data[item3.AnmName].Add(item3.SequenceNum, (int)item3.EffectType);
		}
		source_texture_history.Add(animationSource.BundleName, 0);
		return true;
	}

	public void UpdateHistory(string lastRequestedAnimationName)
	{
		string[] array = source_texture_history.Keys.ToArray();
		foreach (string key in array)
		{
			source_texture_history[key]++;
		}
		source_texture_history[animation_name_to_bundle_name[lastRequestedAnimationName]] = 0;
	}

	public void UnlaodAll()
	{
		foreach (KeyValuePair<string, AssetBundle> item in bundle_name_to_bundle)
		{
			item.Value.Unload(true);
		}
		bundle_name_to_bundle.Clear();
		bundle_name_to_source.Clear();
		source_texture_history.Clear();
		animation_name_to_bundle_name.Clear();
		animation_name_to_se_data.Clear();
		animation_name_to_effect_data.Clear();
		GC.Collect();
	}

	private void UnloadUnreferencedSourceAssets(int numberOfDelete)
	{
		Debug.Log("----UnloadUnreferencedSourceAssets");
		List<string> source = new List<string>();
		IEnumerable<string> second = source.Select((string animationName) => animation_name_to_bundle_name[animationName]).Distinct();
		IEnumerable<string> source2 = bundle_name_to_source.Keys.Except(second);
		IEnumerable<string> enumerable = source2.OrderByDescending((string bundleName) => source_texture_history[bundleName]).Take(numberOfDelete);
		foreach (string item in enumerable)
		{
			Debug.Log("deleteName:" + item);
			source_texture_history.Remove(item);
			bundle_name_to_source.Remove(item);
			bundle_name_to_bundle[item].Unload(true);
			bundle_name_to_bundle.Remove(item);
			animation_name_to_se_data.Remove(item);
			animation_name_to_effect_data.Remove(item);
		}
		animation_name_to_bundle_name = bundle_name_to_source.SelectMany((KeyValuePair<string, AnimationSource> bundleNameSourcePair) => bundleNameSourcePair.Value.Animations.Values.Select((AnmData anm) => new
		{
			animationName = anm.name,
			bundleName = bundleNameSourcePair.Value.BundleName
		})).ToDictionary(namePair => namePair.animationName.ToLower(), namePair => namePair.bundleName);
		GC.Collect();
	}
}
