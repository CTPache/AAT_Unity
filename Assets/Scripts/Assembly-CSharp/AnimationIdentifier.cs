using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationIdentifier : MonoBehaviour
{
	private static AnimationIdentifier instance_;

	private List<AnimationDictionary> dictionary_instances;

	private List<FoaReplacement> replacement_instances;

	private bool isRelationDictionaryLoaded;

	private bool isReplacementDictionaryLoaded;

	private bool isInitializingStarted;

	private List<int> characterFoaCounts;

	private List<int> objectFoaCounts;

	private int Gs1DictionaryCount;

	private int Gs2DictionaryCount;

	public static AnimationIdentifier instance
	{
		get
		{
			return instance_;
		}
	}

	public bool isInitialized
	{
		get
		{
			return isRelationDictionaryLoaded & isReplacementDictionaryLoaded;
		}
	}

	private TitleId CurrentTitleId
	{
		get
		{
			return GSStatic.global_work_.title;
		}
	}

	public int CharacterFoaCount
	{
		get
		{
			return characterFoaCounts[(int)CurrentTitleId];
		}
	}

	public int ObjectFoaCount
	{
		get
		{
			return objectFoaCounts[(int)CurrentTitleId];
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
		characterFoaCounts = new List<int>
		{
			Enum.GetNames(typeof(FOA.GS1_FOA)).Length,
			Enum.GetNames(typeof(FOA.GS2_FOA)).Length,
			Enum.GetNames(typeof(FOA.GS3_FOA)).Length
		};
		objectFoaCounts = new List<int>
		{
			Enum.GetNames(typeof(FOA.GS1_OBJ_FOA)).Length,
			Enum.GetNames(typeof(FOA.GS2_OBJ_FOA)).Length,
			Enum.GetNames(typeof(FOA.GS3_OBJ_FOA)).Length
		};
	}

	public string IdToAnimationName(int titleVersion, int characterID, int animationFOA)
	{
		if (animationFOA < CharacterFoaCount)
		{
			switch (CurrentTitleId)
			{
			case TitleId.GS1:
			{
				FoaReplacement foaReplacement3 = replacement_instances[0];
				FOA.GS1_FOA gS1_FOA = (FOA.GS1_FOA)animationFOA;
				return foaReplacement3.Replace(gS1_FOA.ToString());
			}
			case TitleId.GS2:
			{
				FoaReplacement foaReplacement2 = replacement_instances[2];
				FOA.GS2_FOA gS2_FOA = (FOA.GS2_FOA)animationFOA;
				return foaReplacement2.Replace(gS2_FOA.ToString());
			}
			case TitleId.GS3:
			{
				FoaReplacement foaReplacement = replacement_instances[4];
				FOA.GS3_FOA gS3_FOA = (FOA.GS3_FOA)animationFOA;
				return foaReplacement.Replace(gS3_FOA.ToString());
			}
			}
		}
		else if (animationFOA < CharacterFoaCount + ObjectFoaCount)
		{
			int num = animationFOA - CharacterFoaCount;
			switch (CurrentTitleId)
			{
			case TitleId.GS1:
			{
				FoaReplacement foaReplacement6 = replacement_instances[1];
				FOA.GS1_OBJ_FOA gS1_OBJ_FOA = (FOA.GS1_OBJ_FOA)num;
				return foaReplacement6.Replace(gS1_OBJ_FOA.ToString());
			}
			case TitleId.GS2:
			{
				FoaReplacement foaReplacement5 = replacement_instances[3];
				FOA.GS2_OBJ_FOA gS2_OBJ_FOA = (FOA.GS2_OBJ_FOA)num;
				return foaReplacement5.Replace(gS2_OBJ_FOA.ToString());
			}
			case TitleId.GS3:
			{
				FoaReplacement foaReplacement4 = replacement_instances[5];
				FOA.GS3_OBJ_FOA gS3_OBJ_FOA = (FOA.GS3_OBJ_FOA)num;
				return foaReplacement4.Replace(gS3_OBJ_FOA.ToString());
			}
			}
		}
		return string.Empty;
	}

	public string AnimationNameToBundlePath(string animation_name)
	{
		int num = 0;
		switch (CurrentTitleId)
		{
		case TitleId.GS2:
			num = Gs1DictionaryCount;
			break;
		case TitleId.GS3:
			num = Gs1DictionaryCount + Gs2DictionaryCount;
			break;
		}
		for (int i = num; i < dictionary_instances.Count; i++)
		{
			AnimationDictionary animationDictionary = dictionary_instances[i];
			foreach (AnimationDictionary.TextureAnmNameRelation relation in animationDictionary.Relations)
			{
				if (relation.Values.Contains(animation_name))
				{
					string text = "/GS" + (int)(CurrentTitleId + 1);
					return Application.streamingAssetsPath + string.Format(text + "/TextureAnmRelations/{0}/{1}.unity3d", animationDictionary.name, relation.Key);
				}
			}
		}
		return string.Empty;
	}

	public void LoadDictionary()
	{
		if (!isInitializingStarted)
		{
			isInitializingStarted = true;
			dictionary_instances = new List<AnimationDictionary>();
			coroutineCtrl.instance.Play(LoadDictionaryFlow());
			replacement_instances = new List<FoaReplacement>();
			coroutineCtrl.instance.Play(LoadReplacementDictionaryFlow());
		}
	}

	private IEnumerator LoadDictionaryFlow()
	{
		List<string> gs1Dictionaries = new List<string>
		{
			Application.streamingAssetsPath + "/GS1/AnimationDictionaries/characters.unity3d",
			Application.streamingAssetsPath + "/GS1/AnimationDictionaries/sprites.unity3d",
			Application.streamingAssetsPath + "/GS1/AnimationDictionaries/courts.unity3d",
			Application.streamingAssetsPath + "/GS1/AnimationDictionaries/etcs.unity3d"
		};
		Gs1DictionaryCount = gs1Dictionaries.Count;
		List<string> gs2Dictionaries = new List<string>
		{
			Application.streamingAssetsPath + "/GS2/AnimationDictionaries/characters.unity3d",
			Application.streamingAssetsPath + "/GS2/AnimationDictionaries/sprites.unity3d",
			Application.streamingAssetsPath + "/GS2/AnimationDictionaries/courts.unity3d",
			Application.streamingAssetsPath + "/GS2/AnimationDictionaries/etcs.unity3d",
			Application.streamingAssetsPath + "/GS2/AnimationDictionaries/psycolock.unity3d",
			Application.streamingAssetsPath + "/GS2/AnimationDictionaries/ita.unity3d",
			Application.streamingAssetsPath + "/GS2/AnimationDictionaries/itb.unity3d"
		};
		Gs2DictionaryCount = gs2Dictionaries.Count;
		List<string> gs3Dictionaries = new List<string>
		{
			Application.streamingAssetsPath + "/GS3/AnimationDictionaries/characters.unity3d",
			Application.streamingAssetsPath + "/GS3/AnimationDictionaries/sprites.unity3d",
			Application.streamingAssetsPath + "/GS3/AnimationDictionaries/courts.unity3d",
			Application.streamingAssetsPath + "/GS3/AnimationDictionaries/etcs.unity3d",
			Application.streamingAssetsPath + "/GS3/AnimationDictionaries/psycolock.unity3d",
			Application.streamingAssetsPath + "/GS3/AnimationDictionaries/ita.unity3d",
			Application.streamingAssetsPath + "/GS3/AnimationDictionaries/itb.unity3d"
		};
		List<string> loadTargets = new List<string>();
		loadTargets.AddRange(gs1Dictionaries);
		loadTargets.AddRange(gs2Dictionaries);
		loadTargets.AddRange(gs3Dictionaries);
		foreach (string targetPath in loadTargets)
		{
			byte[] data = decryptionCtrl.instance.load(targetPath);
			AssetBundleCreateRequest load_process = AssetBundle.LoadFromMemoryAsync(data);
			yield return load_process;
			AssetBundleRequest load_proc2 = load_process.assetBundle.LoadAllAssetsAsync();
			yield return load_proc2;
			dictionary_instances.Add(load_proc2.asset as AnimationDictionary);
			load_process.assetBundle.Unload(false);
		}
		isRelationDictionaryLoaded = true;
	}

	private IEnumerator LoadReplacementDictionaryFlow()
	{
		List<string> loadTargets = new List<string>
		{
			Application.streamingAssetsPath + "/GS1/AnimationDictionaries/foareplacementcharacters.unity3d",
			Application.streamingAssetsPath + "/GS1/AnimationDictionaries/foareplacementobjects.unity3d",
			Application.streamingAssetsPath + "/GS2/AnimationDictionaries/foareplacementcharacters.unity3d",
			Application.streamingAssetsPath + "/GS2/AnimationDictionaries/foareplacementobjects.unity3d",
			Application.streamingAssetsPath + "/GS3/AnimationDictionaries/foareplacementcharacters.unity3d",
			Application.streamingAssetsPath + "/GS3/AnimationDictionaries/foareplacementobjects.unity3d"
		};
		foreach (string targetPath in loadTargets)
		{
			byte[] data = decryptionCtrl.instance.load(targetPath);
			AssetBundleCreateRequest load_process = AssetBundle.LoadFromMemoryAsync(data);
			yield return load_process;
			AssetBundleRequest load_proc2 = load_process.assetBundle.LoadAllAssetsAsync();
			yield return load_proc2;
			replacement_instances.Add(load_proc2.asset as FoaReplacement);
			load_process.assetBundle.Unload(false);
		}
		isReplacementDictionaryLoaded = true;
	}
}
