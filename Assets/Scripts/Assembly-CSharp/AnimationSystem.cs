using System.Collections.Generic;
using System.Linq;
using SaveStruct;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
	private static AnimationSystem system_instance;

	public const float DefaultCharacterZ = -5f;

	public const float DefaultObjectZ = -30f;

	[SerializeField]
	public Transform instance_parent;

	[SerializeField]
public AnimationObject animation_object_prefab;

	[SerializeField]
public AnimationCutHolder holder_instance;

	[SerializeField]
public AnimationCameraManager camera_manager;

	public const int max_holding_animation = 18;

	[SerializeField]
public AnimationObject characters_animation_object;

	[SerializeField]
public List<AnimationObject> other_animation_objects;

	public bool is_aiga_mozaic_anim_;

	private int psy_acro_bird_id;

	private bool psy_acro_bird_reverting;

	private int idling_version;

	private int idling_character;

	private int idling_foa;

	private int talking_foa;

	private int playing_foa;

	public static AnimationSystem Instance
	{
		get
		{
			return system_instance;
		}
	}

	public bool parent_active
	{
		get
		{
			return instance_parent.gameObject.activeSelf;
		}
		set
		{
			instance_parent.gameObject.SetActive(value);
		}
	}

	public AnimationCutHolder Holder
	{
		get
		{
			return holder_instance;
		}
	}

	private AnimationIdentifier identifier_instance
	{
		get
		{
			return AnimationIdentifier.instance;
		}
	}

	public AnimationCameraManager CameraManager
	{
		get
		{
			return camera_manager;
		}
		set
		{
			camera_manager = value;
		}
	}

	private TitleId CurrentTitle
	{
		get
		{
			return GSStatic.global_work_.title;
		}
	}

	public bool pause { get; set; }

	public IEnumerable<AnimationObject> AllAnimationObject
	{
		get
		{
			yield return characters_animation_object;
			for (int i = 0; i < other_animation_objects.Count; i++)
			{
				yield return other_animation_objects[i];
			}
		}
	}

	public int IdlingCharacterMasked
	{
		get
		{
			return (int)((long)idling_character & 0x7FL);
		}
	}

	public AnimationObject CharacterAnimationObject
	{
		get
		{
			return characters_animation_object;
		}
	}

	public bool IsCharacterPlaying
	{
		get
		{
			return characters_animation_object.IsPlaying;
		}
	}

	public AnimationSystemSave SystemDataToLoad { private get; set; }

	public AnimationSystemSave SystemDataToSave
	{
		get
		{
			AnimationSystemSave result = default(AnimationSystemSave);
			result.character_id = idling_character;
			result.idling_foa = idling_foa;
			result.talking_foa = talking_foa;
			result.playing_foa = playing_foa;
			result.buffer = new byte[40];
			return result;
		}
	}

	public AnimationObjectSave[] ObjectStatusToLoad { private get; set; }

	public AnimationObjectSave[] ObjectStatusToSave
	{
		get
		{
			AnimationObjectSave[] array = new AnimationObjectSave[25];
			List<AnimationObject> list = AllAnimationObject.ToList();
			for (int i = 0; i < list.Count; i++)
			{
				array[i] = list[i].StatusToSave;
			}
			for (int j = list.Count; j < array.Length; j++)
			{
				array[j] = new AnimationObjectSave
				{
					buffer = new byte[80]
				};
			}
			return array;
		}
	}

	private List<string> DeleteByChangingOldBg
	{
		get
		{
			switch (CurrentTitle)
			{
			case TitleId.GS1:
				return FOA.GS1_DeleteByChangingBgOldNo;
			case TitleId.GS2:
				return FOA.GS2_DeleteByChangingBgOldNo;
			case TitleId.GS3:
				return FOA.GS3_DeleteByChangingBgOldNo;
			default:
				return null;
			}
		}
	}

	private List<string> DeleteByChangingCurrentBg
	{
		get
		{
			switch (CurrentTitle)
			{
			case TitleId.GS1:
				return FOA.GS1_DeleteByChangingBgNo;
			case TitleId.GS2:
				return FOA.GS2_DeleteByChangingBgNo;
			case TitleId.GS3:
				return FOA.GS3_DeleteByChangingBgNo;
			default:
				return null;
			}
		}
	}

	private List<string> DeleteByChangingBgItem
	{
		get
		{
			switch (CurrentTitle)
			{
			case TitleId.GS1:
				return FOA.GS1_DeleteByChangingBgItem;
			case TitleId.GS2:
				return FOA.GS2_DeleteByChangingBgItem;
			case TitleId.GS3:
				return FOA.GS3_DeleteByChangingBgItem;
			default:
				return null;
			}
		}
	}

	public bool IsPlayingAcro
	{
		get
		{
			return CurrentTitle == TitleId.GS2 && (long)IdlingCharacterMasked == 32;
		}
	}

	public bool NextCourtScrollTargetIsAcro
	{
		get
		{
			return CurrentTitle == TitleId.GS2 && bgCtrl.instance.is_scrolling_court && (long)bgCtrl.instance.next_characterID_in_court_scroll == 32;
		}
	}

	public IEnumerable<AnimationObject> PlayingAcroBirds
	{
		get
		{
			List<AnimationObject> list = new List<AnimationObject>();
			int num = 62;
			int num2 = 68;
			for (int i = num; i < num2; i++)
			{
				AnimationObject animationObject = Instance.FindObject(0, 0, i);
				if (animationObject != null)
				{
					list.Add(animationObject);
				}
			}
			return list;
		}
	}

	private AnimationObject AvailableObject(bool isCharacter)
	{
		return (!isCharacter) ? other_animation_objects.Find((AnimationObject obj) => !obj.Exists) : characters_animation_object;
	}

	public void PlaySavedAnimation()
	{
		AnimationSystemSave systemDataToLoad = SystemDataToLoad;
		PlayCharacter(0, systemDataToLoad.character_id, systemDataToLoad.talking_foa, systemDataToLoad.idling_foa);
		if (systemDataToLoad.talking_foa == systemDataToLoad.playing_foa)
		{
			GoTalk();
		}
		else
		{
			GoIdle();
		}
	}

	public void PlaySavedStatus()
	{
		characters_animation_object.LoadStatus(ObjectStatusToLoad[0]);
		for (int i = 0; i < other_animation_objects.Count; i++)
		{
			AnimationObjectSave toLoad = ObjectStatusToLoad[i + 1];
			if (toLoad.exists && toLoad.foa != 0)
			{
				int animationFOA = toLoad.foa + identifier_instance.CharacterFoaCount;
				string animationName = identifier_instance.IdToAnimationName(0, toLoad.characterID, animationFOA);
				PlayAnimation(other_animation_objects[i], toLoad.characterID, animationName, false, true, animationFOA, false);
				if (GSStatic.global_work_.title == TitleId.GS2)
				{
					GS2_OpObjCtrl.instance.CreateObj_GS2(other_animation_objects[i], (uint)other_animation_objects[i].ObjectFOA);
				}
			}
			other_animation_objects[i].LoadStatus(toLoad);
		}
		SystemDataToLoad = default(AnimationSystemSave);
		ObjectStatusToLoad = null;
	}

	private void Awake()
	{
		system_instance = this;
	}

	public void setup()
	{
		AnimationObject[] array = new AnimationObject[18];
		for (int i = 0; i < 18; i++)
		{
			AnimationObject animationObject = Object.Instantiate(animation_object_prefab, instance_parent);
			array[i] = animationObject;
		}
		foreach (AnimationObject item in array.Skip(1))
		{
			item.transform.localPosition = new Vector3(0f, 0f, -30f);
			item.DefaultZ = -30f;
		}
		characters_animation_object = array[0];
		characters_animation_object.transform.localPosition = new Vector3(0f, 0f, -5f);
		characters_animation_object.DefaultZ = -5f;
		other_animation_objects = array.Skip(1).ToList();
	}

	public void end()
	{
		Object.Destroy(characters_animation_object);
		characters_animation_object = null;
		for (int i = 0; i < other_animation_objects.Count; i++)
		{
			Object.Destroy(other_animation_objects[i]);
			other_animation_objects[i] = null;
		}
		other_animation_objects.Clear();
	}

	public AnimationObject RevertPsyAcroBird()
	{
		AnimationObject result = null;
		if (psy_acro_bird_id != 0)
		{
			psy_acro_bird_reverting = true;
			result = PlayObject(0, 0, psy_acro_bird_id);
			psy_acro_bird_reverting = false;
			psy_acro_bird_id = 0;
		}
		else
		{
			Debug.Log("RevertPsyAcroBird Not psy_acro_bird_id!!");
		}
		return result;
	}

	public AnimationObject PlayObject(int titleVersion, int characterID, int objectFOA)
	{
		if (CurrentTitle == TitleId.GS2 && bgCtrl.instance.bg_no_now == 254)
		{
			int num = 62;
			int num2 = 68;
			if (num <= objectFOA && objectFOA <= num2 && objectFOA != 65 && !psy_acro_bird_reverting)
			{
				psy_acro_bird_id = objectFOA;
				return null;
			}
		}
		ushort bg256_dir = bgCtrl.instance.Bg256_dir;
		ushort num3 = (ushort)(bg256_dir & 0x10u);
		AnimationObject animationObject = PlayAnimation(titleVersion, characterID, objectFOA + identifier_instance.CharacterFoaCount, false, true, false);
		if (num3 > 0)
		{
			if (GSStatic.global_work_.title == TitleId.GS1 && (long)objectFOA >= 13L)
			{
				switch (objectFOA)
				{
				default:
					animationObject.transform.localPosition -= new Vector3(1920f, 0f, 0f);
					break;
				case 28:
				case 29:
				case 30:
				case 31:
				case 32:
				case 33:
				case 34:
				case 35:
				case 36:
				case 37:
				case 38:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
					break;
				}
			}
			else if (GSStatic.global_work_.title == TitleId.GS2 && (long)objectFOA >= 10L)
			{
				switch (objectFOA)
				{
				default:
					animationObject.transform.localPosition -= new Vector3(1920f, 0f, 0f);
					break;
				case 77:
				case 78:
				case 79:
					break;
				}
			}
			else if (GSStatic.global_work_.title == TitleId.GS3 && (long)objectFOA >= 10L)
			{
				switch (objectFOA)
				{
				default:
					animationObject.transform.localPosition -= new Vector3(1920f, 0f, 0f);
					break;
				case 10:
				case 11:
				case 86:
				case 87:
				case 88:
				case 199:
				case 200:
				case 201:
				case 202:
					break;
				}
			}
		}
		if (NextCourtScrollTargetIsAcro)
		{
			foreach (AnimationObject playingAcroBird in PlayingAcroBirds)
			{
				playingAcroBird.transform.localPosition = new Vector3(bgCtrl.instance.next_posX_in_court_scroll, 0f, playingAcroBird.transform.localPosition.z);
			}
		}
		if (GSStatic.global_work_.title == TitleId.GS2 && GSStatic.global_work_.scenario == 11 && GSStatic.global_work_.Room == 14 && advCtrl.instance.sub_window_.stack_ > 0)
		{
			Routine routine = advCtrl.instance.sub_window_.routine_[advCtrl.instance.sub_window_.stack_ - 1];
			if (routine.r.no_0 == 6)
			{
				foreach (AnimationObject playingAcroBird2 in PlayingAcroBirds)
				{
					playingAcroBird2.gameObject.SetActive(false);
				}
			}
		}
		return animationObject;
	}

	public AnimationObject PlayTalkCharacter(int talkFOA)
	{
		return PlayCharacter(idling_version, idling_character, talkFOA, idling_foa);
	}

	public AnimationObject PlayCharacter(int titleVersion, int characterID, int talkFOA, int idleFOA)
	{
		idling_version = titleVersion;
		idling_character = characterID;
		talking_foa = talkFOA;
		idling_foa = idleFOA;
		playing_foa = talkFOA;
		return PlayAnimation(titleVersion, characterID, talkFOA, false, true, true);
	}

	public void GoIdle()
	{
		if (playing_foa != idling_foa)
		{
			playing_foa = idling_foa;
			AnimationObject animationObject = find_pl_option(IdlingCharacterMasked);
			int objectFOA = 0;
			if (animationObject != null)
			{
				objectFOA = animationObject.ObjectFOA;
			}
			StopCharacter(idling_version, idling_character, talking_foa);
			PlayAnimation(idling_version, idling_character, idling_foa, true, false, true);
			if (animationObject != null)
			{
				PlayObject(0, 0, objectFOA);
			}
		}
	}

	public void GoTalk()
	{
		if (playing_foa != talking_foa)
		{
			playing_foa = talking_foa;
			AnimationObject animationObject = find_pl_option(IdlingCharacterMasked);
			int objectFOA = 0;
			if (animationObject != null)
			{
				objectFOA = animationObject.ObjectFOA;
			}
			StopCharacter(idling_version, idling_character, idling_foa);
			PlayAnimation(idling_version, idling_character, talking_foa, true, false, true);
			if (animationObject != null)
			{
				PlayObject(0, 0, objectFOA);
			}
		}
	}

	private AnimationObject PlayAnimation(int titleVersion, int characterID, int animationFOA, bool dontResetPosition, bool resetAlpha, bool isCharacter)
	{
		if (animationFOA == 0)
		{
			return null;
		}
		string text = identifier_instance.IdToAnimationName(titleVersion, characterID, animationFOA);
		if (text == string.Empty)
		{
			Debug.LogError("不明なアニメーション + FOA = " + animationFOA);
			return null;
		}
		if (IsPlaying(text, characterID, animationFOA - identifier_instance.CharacterFoaCount))
		{
			return null;
		}
		return PlayAnimation(titleVersion, characterID, text, dontResetPosition, resetAlpha, animationFOA, isCharacter);
	}

	private AnimationObject PlayAnimation(int titleVersion, int characterID, string animationName, bool dontResetPosition, bool resetAlpha, int animationFOA, bool isCharacter)
	{
		AnimationObject objectToPlay = AvailableObject(isCharacter);
		return PlayAnimation(objectToPlay, characterID, animationName, dontResetPosition, resetAlpha, animationFOA, isCharacter);
	}

	private AnimationObject PlayAnimation(AnimationObject objectToPlay, int characterID, string animationName, bool dontResetPosition, bool resetAlpha, int animationFOA, bool isCharacter)
	{
		if (objectToPlay == null)
		{
			Debug.LogError("No available animation object.");
			return null;
		}
		objectToPlay.CharacterID = characterID;
		objectToPlay.ObjectFOA = ((characterID == 0) ? (animationFOA - identifier_instance.CharacterFoaCount) : animationFOA);
		if (((ulong)characterID & 0x2000uL) != 0)
		{
			objectToPlay.BeFlag |= 1;
		}
		else
		{
			objectToPlay.BeFlag &= -2;
		}
		objectToPlay.BeFlag |= int.MinValue;
		if (characterID == 0)
		{
			int num = animationFOA - identifier_instance.CharacterFoaCount;
			if ((CurrentTitle == TitleId.GS1 && (num == 164 || num == 165)) || (CurrentTitle == TitleId.GS2 && (num == 137 || num == 138)) || (CurrentTitle == TitleId.GS3 && (num == 154 || num == 155)))
			{
				objectToPlay.BeFlag &= int.MaxValue;
			}
		}
		objectToPlay.Play(animationName, resetAlpha);
		bool flag = string.Compare(animationName, 0, "pla", 0, 3) == 0 || string.Compare(animationName, 0, "plb", 0, 3) == 0;
		objectToPlay.sprite_mask = !flag;
		if (CurrentTitle == TitleId.GS3 && objectToPlay == characters_animation_object)
		{
			CtrlChinamiObj(1);
		}
		if (!dontResetPosition)
		{
			objectToPlay.ResetPositionByNameFlag(characterID);
		}
		AnimationObject animationObject = find_pl_option(characterID);
		if (animationObject != null)
		{
			animationObject.Stop(true);
		}
		SetPriorityByFOA(objectToPlay);
		if (!bgCtrl.instance.body.activeSelf)
		{
			objectToPlay.gameObject.SetActive(false);
		}
		if (characterID == 0 && ((CurrentTitle == TitleId.GS1 && animationName == FOA.GS1_OBJ_FOA.hot00.ToString()) || (CurrentTitle == TitleId.GS2 && animationName == FOA.GS2_OBJ_FOA.hot00.ToString()) || (CurrentTitle == TitleId.GS3 && animationName == FOA.GS3_OBJ_FOA.hot00.ToString()) || (CurrentTitle == TitleId.GS3 && animationName == FOA.GS3_OBJ_FOA.hot06.ToString()) || (CurrentTitle == TitleId.GS3 && animationName == FOA.GS3_OBJ_FOA.hot10.ToString())))
		{
			objectToPlay.Z -= 1f;
		}
		return objectToPlay;
	}

	public bool IsPlaying(string animationName, int characterID, int animationFOA)
	{
		return AllAnimationObject.Where((AnimationObject obj) => obj.IsPlaying).Any((AnimationObject obj) => obj.PlayingAnimationName == animationName && obj.CharacterID == characterID && obj.ObjectFOA == animationFOA);
	}

	public bool IsPlayingObject(int titleVersion, int characterID, int animationFOA)
	{
		string animationName = identifier_instance.IdToAnimationName(titleVersion, characterID, animationFOA + identifier_instance.CharacterFoaCount);
		return AllAnimationObject.Where((AnimationObject obj) => obj.IsPlaying).Any((AnimationObject obj) => obj.PlayingAnimationName == animationName);
	}

	public void StopCharacter(int titleVersion, int characterID, int stopFOA)
	{
		string animationName = identifier_instance.IdToAnimationName(titleVersion, characterID, stopFOA);
		StopCharacterByName(animationName);
	}

	public void StopObject(int titleVersion, int characterID, int stopFOA, bool idClear = false)
	{
		string animationName = identifier_instance.IdToAnimationName(titleVersion, characterID, stopFOA + identifier_instance.CharacterFoaCount);
		StopObjectByName(animationName, idClear);
	}

	private void StopCharacterByName(string animationName, bool idClear = false)
	{
		if (characters_animation_object.PlayingAnimationName == animationName)
		{
			characters_animation_object.Stop(idClear);
		}
	}

	private void StopObjectByName(string animationName, bool idClear = false)
	{
		foreach (AnimationObject other_animation_object in other_animation_objects)
		{
			if (other_animation_object.PlayingAnimationName == animationName)
			{
				other_animation_object.Stop(idClear);
			}
		}
	}

	private void StopBirdsWithAcro()
	{
		AnimationObject animationObject = FindObject(0, 0, 62);
		if (animationObject != null)
		{
			StopObject(0, 0, 62, true);
		}
		else
		{
			StopObject(0, 0, 63, true);
		}
	}

	public void StopObjects()
	{
		StopCategory(false);
	}

	public void StopCharacters()
	{
		StopCharacters(true);
	}

	public void StopCharacters(bool idClear)
	{
		if (IsPlayingAcro)
		{
			StopBirdsWithAcro();
		}
		StopCategory(true, idClear);
	}

	private void StopCategory(bool targetIsCharacter)
	{
		StopCategory(targetIsCharacter, true);
	}

	private void StopCategory(bool targetIsCharacter, bool idClear)
	{
		if (targetIsCharacter)
		{
			AnimationObject animationObject = find_pl_option(IdlingCharacterMasked);
			if (animationObject != null)
			{
				animationObject.Stop(idClear);
			}
			characters_animation_object.Stop(idClear);
			ResetSpefChara();
			idling_character = 0;
			idling_version = 0;
			idling_foa = 0;
			talking_foa = 0;
			return;
		}
		foreach (AnimationObject other_animation_object in other_animation_objects)
		{
			other_animation_object.Stop(idClear);
		}
		ResetSpefObj();
	}

	public void StopAll()
	{
		idling_character = 0;
		idling_version = 0;
		idling_foa = 0;
		talking_foa = 0;
		foreach (AnimationObject item in AllAnimationObject)
		{
			item.Stop(true);
		}
	}

	public AnimationObject FindObject(int titleVersion, int characterID, int objectFOA)
	{
		string text = identifier_instance.IdToAnimationName(titleVersion, characterID, objectFOA + identifier_instance.CharacterFoaCount);
		foreach (AnimationObject item in AllAnimationObject)
		{
			if (item.PlayingAnimationName == text)
			{
				return item;
			}
		}
		return null;
	}

	public void StopAllWithoutCalledInFrame()
	{
		foreach (AnimationObject item in AllAnimationObject.Where((AnimationObject animationObj) => animationObj.FrameCountFromStarted > 0))
		{
			item.Stop();
		}
	}

	public void StopByChangingBG()
	{
		List<string> deleteByChangingOldBg = DeleteByChangingOldBg;
		List<string> deleteByChangingCurrentBg = DeleteByChangingCurrentBg;
		foreach (AnimationObject item in AllAnimationObject)
		{
			if (deleteByChangingOldBg.Contains(item.PlayingAnimationName))
			{
				if (item.FrameCountFromStarted > 0)
				{
					item.Stop(true);
					continue;
				}
			}
			else if (deleteByChangingCurrentBg.Contains(item.PlayingAnimationName))
			{
				item.Stop(true);
				continue;
			}
			if (CurrentTitle != 0 && item.FrameCountFromStarted > 0)
			{
				int num = ((CurrentTitle != TitleId.GS2) ? 203 : 126);
				int num2 = ((CurrentTitle != TitleId.GS2) ? 204 : 127);
				string text = identifier_instance.IdToAnimationName(0, 0, num + identifier_instance.CharacterFoaCount);
				string text2 = identifier_instance.IdToAnimationName(0, 0, num2 + identifier_instance.CharacterFoaCount);
				if (item.PlayingAnimationName == text || item.PlayingAnimationName == text2)
				{
					item.Stop(true);
				}
			}
		}
	}

	public void StopByBgChanged()
	{
		int bg_no = bgCtrl.instance.bg_no;
		List<string> deleteByChangingBgItem = DeleteByChangingBgItem;
		foreach (AnimationObject item in AllAnimationObject)
		{
			if (deleteByChangingBgItem.Contains(item.PlayingAnimationName) && item.BgNumber != bg_no)
			{
				item.Stop(true);
			}
		}
	}

	public void CharFadeInit(int fadeType)
	{
	}

	public void CharFade(int fadeType, int fadeTime)
	{
		AnimationObject animationObject = null;
		AnimationObject animationObject2 = null;
		bool flag = false;
		if (((uint)fadeType & 0xFF00u) != 0)
		{
			int objectFOA = fadeType >> 8;
			fadeType &= 0xFF;
			animationObject = FindObject((int)CurrentTitle, 0, objectFOA);
		}
		else
		{
			animationObject = characters_animation_object;
			animationObject2 = find_pl_option(Instance.IdlingCharacterMasked);
		}
		if (!fadeCtrl.instance.is_end || (animationObject.isFade && ((((uint)fadeType & (true ? 1u : 0u)) != 0 && animationObject.isFadeIn) || (((uint)fadeType & 4u) != 0 && !animationObject.isFadeIn))))
		{
			return;
		}
		if (((uint)fadeType & (true ? 1u : 0u)) != 0)
		{
			flag = true;
			animationObject.BeFlag &= -201326597;
			if (animationObject.Exists)
			{
				animationObject.Alpha = 0f;
			}
			animationObject.gameObject.SetActive(true);
			if (animationObject2 != null)
			{
				if (animationObject2.Exists)
				{
					animationObject2.Alpha = 0f;
				}
				animationObject2.gameObject.SetActive(true);
			}
		}
		else
		{
			if ((fadeType & 4) == 0)
			{
				return;
			}
			flag = false;
			animationObject.BeFlag |= 4;
			if (((uint)fadeType & 0xCu) != 0)
			{
				animationObject.BeFlag |= 67108864;
			}
		}
		animationObject.Fade(flag, fadeTime * 16);
		if (animationObject2 != null)
		{
			animationObject2.Fade(flag, fadeTime * 16);
		}
	}

	public void SetFadeAigaMozaicAnim(int fadeType, int fadeTime)
	{
		AnimationObject animationObject = characters_animation_object;
		bool flag = false;
		if (((uint)fadeType & (true ? 1u : 0u)) != 0)
		{
			animationObject.Alpha = 0f;
			animationObject.gameObject.SetActive(true);
			flag = true;
		}
		else
		{
			flag = false;
		}
		animationObject.Fade(flag, fadeTime * 16);
	}

	public bool isFade(int fadeType)
	{
		AnimationObject animationObject = null;
		if (((uint)fadeType & 0xFF00u) != 0)
		{
			int objectFOA = fadeType >> 8;
			fadeType &= 0xFF;
			animationObject = FindObject((int)CurrentTitle, 0, objectFOA);
		}
		else
		{
			animationObject = characters_animation_object;
		}
		return animationObject.isFade;
	}

	public bool CheckCharFade()
	{
		return CharacterAnimationObject.Alpha == 0f || !CharacterAnimationObject.gameObject.activeSelf;
	}

	public void ChangeInstantlyAlpha(bool disp)
	{
		float alpha = 0f;
		if (disp)
		{
			alpha = 1f;
		}
		characters_animation_object.Alpha = alpha;
	}

	public void Scroll(Vector3 speed)
	{
		foreach (AnimationObject item in Instance.AllAnimationObject)
		{
			if ((item.BeFlag & 8) == 0)
			{
				item.transform.localPosition += speed;
			}
		}
	}

	public AnimationObject find_pl_option(int characterID)
	{
		if (CurrentTitle != 0)
		{
			return null;
		}
		if ((long)characterID == 50)
		{
			return other_animation_objects.FirstOrDefault((AnimationObject obj) => 39L <= (long)obj.ObjectFOA && (long)obj.ObjectFOA <= 59L);
		}
		return null;
	}

	public void ResetSpefChara()
	{
		characters_animation_object.PlayMonochrome(0, 0, 255, true);
	}

	public void ResetSpefObj()
	{
		foreach (AnimationObject other_animation_object in other_animation_objects)
		{
			other_animation_object.PlayMonochrome(0, 0, 255, true);
		}
	}

	public void Char_monochrome(ushort time, ushort speed, ushort sw, bool fadeIn)
	{
		characters_animation_object.PlayMonochrome(time, speed, sw, fadeIn);
	}

	public void OBJ_monochrome(ushort time, ushort speed, ushort sw, bool fadeIn)
	{
		if (CurrentTitle == TitleId.GS2 && GSStatic.global_work_.scenario == 12)
		{
			OBJ_monochrome2(20u, time, speed, sw, fadeIn);
			OBJ_monochrome2(21u, time, speed, sw, fadeIn);
		}
	}

	public void OBJ_monochrome2(uint id, ushort time, ushort speed, ushort sw, bool fadeIn)
	{
		AnimationObject animationObject = other_animation_objects.FirstOrDefault((AnimationObject obj) => id == obj.ObjectFOA);
		if (animationObject != null)
		{
			animationObject.PlayMonochrome(time, speed, sw, fadeIn);
		}
	}

	public bool IsCharaMonochrom()
	{
		return characters_animation_object.IsMonochrom();
	}

	public void CtrlChinamiObj(ushort flag)
	{
		MessageWork activeMessageWork = MessageSystem.GetActiveMessageWork();
		AnimationObject animationObject = characters_animation_object;
		if (flag != 0 && (animationObject == null || (long)Instance.IdlingCharacterMasked != 11))
		{
			CtrlChinamiObj(0);
			return;
		}
		switch (flag)
		{
		case 0:
		{
			for (byte b = 0; b < 3; b++)
			{
				animationObject = Instance.FindObject(2, 0, 114 + b);
				if (animationObject != null && animationObject.IsMonochrom())
				{
					Instance.OBJ_monochrome2((uint)(114 + b), 1, 1, 0, false);
				}
			}
			break;
		}
		case 1:
		{
			CtrlChinamiObj(0);
			if ((activeMessageWork.status2 & MessageSystem.Status2.MOSAIC_FLAG) != 0 && (activeMessageWork.all_work[2] & 0xF0u) != 0)
			{
				for (byte b = 0; b < 3; b++)
				{
					animationObject = Instance.FindObject(2, 0, 114 + b);
					if (animationObject != null)
					{
						Instance.OBJ_monochrome2((uint)(114 + b), 1, 31, 0, true);
					}
				}
			}
			animationObject = Instance.characters_animation_object;
			if (animationObject.IsMonochrom())
			{
				for (byte b = 0; b < 3; b++)
				{
					animationObject = Instance.FindObject(2, 0, 114 + b);
					if (animationObject != null)
					{
						Instance.OBJ_monochrome2((uint)(114 + b), 1, 31, 0, true);
					}
				}
			}
			animationObject = Instance.characters_animation_object;
			int objectFOA = animationObject.ObjectFOA;
			if ((long)objectFOA == 184)
			{
				activeMessageWork.choustate = (ushort)((activeMessageWork.choustate & 0xFF00) + 11);
			}
			else if (activeMessageWork.choustate == 6)
			{
				activeMessageWork.choustate = 9;
			}
			break;
		}
		case 4:
		{
			for (byte b = 0; b < 3; b++)
			{
				animationObject = Instance.FindObject(2, 0, 114 + b);
				if (animationObject != null)
				{
					Instance.OBJ_monochrome2((uint)(114 + b), 1, 31, 0, true);
				}
			}
			break;
		}
		}
	}

	private void SetPriorityByFOA(AnimationObject objectToPlay)
	{
		if (objectToPlay.CharacterID > 0)
		{
			if (GSStatic.global_work_.title == TitleId.GS2)
			{
				uint objectFOA = (uint)objectToPlay.ObjectFOA;
				if (objectFOA == 440)
				{
					objectToPlay.SetPriority(AnimationRenderTarget.LikeBg);
					return;
				}
			}
			else if (GSStatic.global_work_.title == TitleId.GS3)
			{
				uint objectFOA2 = (uint)objectToPlay.ObjectFOA;
				if (objectFOA2 == 590 || objectFOA2 == 591)
				{
					objectToPlay.SetPriority(AnimationRenderTarget.LikeBg);
					return;
				}
			}
			objectToPlay.SetPriority(AnimationRenderTarget.Default);
			return;
		}
		if (GSStatic.global_work_.title == TitleId.GS1)
		{
			switch ((uint)objectToPlay.ObjectFOA)
			{
			case 13u:
			case 14u:
			case 15u:
			case 16u:
			case 17u:
			case 18u:
			case 19u:
			case 60u:
			case 86u:
			case 124u:
			case 147u:
			case 148u:
			case 151u:
			case 152u:
			case 153u:
			case 154u:
			case 155u:
			case 156u:
			case 157u:
			case 158u:
			case 159u:
			case 160u:
			case 161u:
			case 162u:
			case 163u:
			case 164u:
			case 165u:
			case 166u:
			case 167u:
			case 168u:
			case 169u:
			case 170u:
			case 171u:
			case 172u:
			case 173u:
			case 174u:
			case 175u:
			case 176u:
			case 177u:
			case 178u:
			case 179u:
			case 180u:
			case 181u:
			case 182u:
			case 183u:
				objectToPlay.SetPriority(AnimationRenderTarget.LikeBg);
				return;
			case 39u:
			case 40u:
			case 41u:
			case 42u:
			case 43u:
			case 44u:
			case 45u:
			case 46u:
			case 47u:
			case 48u:
			case 49u:
			case 50u:
			case 51u:
			case 52u:
			case 53u:
			case 54u:
			case 55u:
			case 56u:
			case 57u:
			case 58u:
			case 59u:
				objectToPlay.SetPriority(AnimationRenderTarget.FrontOfDesk);
				return;
			case 20u:
			case 21u:
			case 22u:
			case 23u:
			case 24u:
			case 25u:
			case 81u:
			case 82u:
			case 83u:
			case 84u:
			case 85u:
				objectToPlay.SetPriority(AnimationRenderTarget.FrontOfMessageWindow);
				return;
			case 115u:
			case 123u:
				objectToPlay.SetPriority(AnimationRenderTarget.FrontOfMessageWindow);
				return;
			}
		}
		else if (GSStatic.global_work_.title == TitleId.GS2)
		{
			switch ((uint)objectToPlay.ObjectFOA)
			{
			case 10u:
			case 11u:
			case 12u:
			case 13u:
			case 14u:
			case 15u:
			case 19u:
			case 20u:
			case 21u:
			case 22u:
			case 26u:
			case 27u:
			case 28u:
			case 29u:
			case 30u:
			case 31u:
			case 32u:
			case 33u:
			case 34u:
			case 128u:
			case 129u:
			case 130u:
			case 131u:
			case 132u:
			case 133u:
			case 134u:
			case 135u:
			case 136u:
			case 137u:
			case 138u:
			case 139u:
			case 140u:
			case 141u:
			case 142u:
			case 143u:
			case 144u:
			case 145u:
			case 146u:
			case 147u:
			case 148u:
			case 149u:
			case 150u:
			case 151u:
			case 152u:
			case 153u:
			case 154u:
			case 155u:
			case 156u:
			case 157u:
				objectToPlay.SetPriority(AnimationRenderTarget.LikeBg);
				return;
			case 35u:
			case 36u:
			case 37u:
			case 38u:
			case 39u:
			case 40u:
			case 56u:
			case 57u:
			case 58u:
				objectToPlay.SetPriority(AnimationRenderTarget.FrontOfMessageWindow);
				return;
			}
		}
		else if (GSStatic.global_work_.title == TitleId.GS3)
		{
			switch ((uint)objectToPlay.ObjectFOA)
			{
			case 98u:
			case 147u:
			case 148u:
			case 149u:
			case 150u:
			case 151u:
			case 152u:
			case 153u:
			case 154u:
			case 155u:
			case 156u:
			case 157u:
			case 158u:
			case 159u:
			case 160u:
			case 161u:
			case 162u:
			case 163u:
			case 164u:
			case 165u:
			case 166u:
			case 167u:
			case 168u:
			case 169u:
			case 170u:
			case 171u:
			case 172u:
			case 173u:
			case 174u:
			case 175u:
			case 176u:
			case 177u:
			case 178u:
			case 179u:
			case 180u:
			case 181u:
			case 182u:
			case 183u:
			case 184u:
			case 185u:
			case 186u:
			case 187u:
			case 188u:
			case 189u:
			case 190u:
			case 191u:
			case 192u:
			case 193u:
			case 194u:
			case 195u:
			case 196u:
			case 197u:
			case 198u:
			case 207u:
				objectToPlay.SetPriority(AnimationRenderTarget.LikeBg);
				return;
			case 65u:
			case 66u:
			case 67u:
			case 68u:
			case 69u:
			case 70u:
			case 86u:
			case 87u:
			case 88u:
			case 89u:
				objectToPlay.SetPriority(AnimationRenderTarget.FrontOfMessageWindow);
				return;
			}
		}
		objectToPlay.SetPriority(AnimationRenderTarget.Default);
	}
}
