using System;
using System.Collections;
using System.Collections.Generic;
using SaveStruct;
using UnityEngine;
using UnityEngine.Rendering;

public class AnimationObject : MonoBehaviour
{
	private struct CycleDataToSave
	{
		public bool sequenceDataNotLoadedYet;

		public int sequence;

		public int framesFromStarted;

		public int framesInSequence;
	}

	private struct MonochromeDataToSave
	{
		public bool fadeIn;

		public ushort time;

		public ushort speed;
	}

	private class AnimSpriteData
	{
		public SpriteRenderer sprite_renderer;

		public Transform transform;

		public GameObject gameObject;

		public SpriteMask mask;

		public bool enabled
		{
			get
			{
				return sprite_renderer.enabled;
			}
			set
			{
				sprite_renderer.enabled = value;
			}
		}

		public Sprite sprite
		{
			get
			{
				return sprite_renderer.sprite;
			}
			set
			{
				sprite_renderer.sprite = value;
			}
		}

		public Color color
		{
			get
			{
				return sprite_renderer.color;
			}
			set
			{
				sprite_renderer.color = value;
			}
		}

		public Vector2 size
		{
			get
			{
				return sprite_renderer.size;
			}
			set
			{
				sprite_renderer.size = value;
			}
		}

		public bool flipX
		{
			get
			{
				return sprite_renderer.flipX;
			}
			set
			{
				sprite_renderer.flipX = value;
			}
		}

		public int sortingOrder
		{
			get
			{
				return sprite_renderer.sortingOrder;
			}
			set
			{
				sprite_renderer.sortingOrder = value;
			}
		}

		public Material material
		{
			get
			{
				return sprite_renderer.material;
			}
			set
			{
				sprite_renderer.material = value;
			}
		}

		public void GetPropertyBlock(MaterialPropertyBlock mpb)
		{
			sprite_renderer.GetPropertyBlock(mpb);
		}

		public void SetPropertyBlock(MaterialPropertyBlock mpb)
		{
			sprite_renderer.SetPropertyBlock(mpb);
		}
	}

	[SerializeField]
	private string playingAnimationName_;

	[SerializeField]
	private SpriteRenderer spriteOriginal_;

	[SerializeField]
	private SortingGroup rootSortingGroup_;

	private List<AnimSpriteData> spriteInstances_ = new List<AnimSpriteData>();

	private float alpha_ = 1f;

	private Color color_ = Color.white;

	private IEnumerator playingCycle_;

	private IEnumerator fadeEnumerator;

	private bool in_centor;

	private Vector3 set_local_centor_pos = Vector3.zero;

	private bool is_fade;

	private bool is_fadeIn;

	[SerializeField]
	private Material default_;

	[SerializeField]
	private Material grayscale_;

	[SerializeField]
	private Material sepia_;

	[SerializeField]
	private Material black_;

	private IEnumerator spefEnumerator;

	private int CharacterID_;

	private ushort sw_ = ushort.MaxValue;

	private bool is_animation_continue_;

	private bool isDefaultInitialized;

	private float defaultZ_;

	private bool sprite_mask_ = true;

	private CycleDataToSave cycleDataToSave;

	private MonochromeDataToSave monochromeDataToSave;

	private TitleId CurrentTitle
	{
		get
		{
			return GSStatic.global_work_.title;
		}
	}

	private AnimationSystem AnimationSystem
	{
		get
		{
			return AnimationSystem.Instance;
		}
	}

	private AnimationCutHolder SourceHolder
	{
		get
		{
			return AnimationSystem.Holder;
		}
	}

	public string PlayingAnimationName
	{
		get
		{
			return playingAnimationName_;
		}
	}

	public AnimationRenderTarget RenderTarget { get; private set; }

	public float Alpha
	{
		get
		{
			return alpha_;
		}
		set
		{
			foreach (AnimSpriteData item in spriteInstances_)
			{
				Color color = item.color;
				color.a = value;
				item.color = color;
			}
			alpha_ = value;
		}
	}

	public Color ObjColor
	{
		get
		{
			return color_;
		}
		set
		{
			foreach (AnimSpriteData item in spriteInstances_)
			{
				item.color = value;
			}
			color_ = value;
		}
	}

	public bool isFade
	{
		get
		{
			return is_fade;
		}
	}

	public bool isFadeIn
	{
		get
		{
			return is_fadeIn;
		}
	}

	private int fadeFrame { get; set; }

	public bool IsPlaying
	{
		get
		{
			return playingCycle_ != null;
		}
	}

	public bool Exists { get; private set; }

	public int FrameCountFromStarted { get; private set; }

	public int BgNumber { get; private set; }

	public int BeFlag { get; set; }

	public bool HasHReverse
	{
		get
		{
			return (BeFlag & 1) != 0;
		}
	}

	public bool HasMove
	{
		get
		{
			return (BeFlag & int.MinValue) != 0;
		}
	}

	public int CharacterID
	{
		get
		{
			return CharacterID_;
		}
		set
		{
			LastCharacterID = CharacterID_;
			CharacterID_ = value;
		}
	}

	public int LastCharacterID { get; set; }

	public int ObjectFOA { get; set; }

	public float DefaultZ
	{
		get
		{
			return defaultZ_;
		}
		set
		{
			if (!isDefaultInitialized)
			{
				defaultZ_ = value;
			}
		}
	}

	public float Z
	{
		get
		{
			return base.transform.localPosition.z;
		}
		set
		{
			Transform transform = base.transform;
			Vector3 localPosition = transform.localPosition;
			transform.localPosition = new Vector3(localPosition.x, localPosition.y, value);
		}
	}

	public bool sprite_mask
	{
		get
		{
			return sprite_mask_;
		}
		set
		{
			if (sprite_mask_ != value)
			{
				sprite_mask_ = value;
				for (int i = 0; i < spriteInstances_.Count; i++)
				{
					spriteInstances_[i].mask.enabled = sprite_mask_;
				}
			}
		}
	}

	public AnimationObjectSave StatusToSave
	{
		get
		{
			AnimationObjectSave result = default(AnimationObjectSave);
			result.exists = Exists;
			result.x = base.transform.localPosition.x;
			result.y = base.transform.localPosition.y;
			result.z = base.transform.localPosition.z;
			result.foa = ObjectFOA;
			result.characterID = CharacterID;
			result.beFlag = BeFlag;
			result.sequence = cycleDataToSave.sequence;
			result.framesFromStarted = cycleDataToSave.framesFromStarted;
			result.framesInSequence = cycleDataToSave.framesInSequence;
			result.isFading = isFade;
			result.isFadeIn = isFadeIn;
			result.fadeFrame = fadeFrame;
			result.alpha = Alpha;
			result.monochrome_sw = sw_;
			result.monochrome_time = monochromeDataToSave.time;
			result.monochrome_speed = monochromeDataToSave.speed;
			result.monochrome_fadein = monochromeDataToSave.fadeIn;
			result.buffer = new byte[80];
			return result;
		}
	}

	public void SetPriority(AnimationRenderTarget target)
	{
		RenderTarget = target;
		rootSortingGroup_.sortingLayerID = target.ObjectSortingLayer;
		base.gameObject.layer = target.ObjectLayer;
		foreach (AnimSpriteData item in spriteInstances_)
		{
			item.gameObject.layer = target.ObjectLayer;
		}
	}

	public void LoadStatus(AnimationObjectSave toLoad)
	{
		base.transform.localPosition = new Vector3(toLoad.x, toLoad.y, toLoad.z);
		BeFlag = toLoad.beFlag;
		if (((uint)BeFlag & 0x8000000u) != 0)
		{
			base.gameObject.SetActive(false);
		}
		if (playingCycle_ != null)
		{
			cycleDataToSave.sequenceDataNotLoadedYet = true;
			cycleDataToSave.sequence = toLoad.sequence;
			cycleDataToSave.framesFromStarted = toLoad.framesFromStarted;
			cycleDataToSave.framesInSequence = toLoad.framesInSequence;
			playingCycle_.MoveNext();
		}
		if (toLoad.isFading)
		{
			Fade(toLoad.isFadeIn, toLoad.fadeFrame);
		}
		else
		{
			Alpha = toLoad.alpha;
		}
		PlayMonochrome(toLoad.monochrome_time, toLoad.monochrome_speed, toLoad.monochrome_sw, toLoad.monochrome_fadein);
	}

	private void Awake()
	{
		RenderTarget = AnimationRenderTarget.Default;
	}

	public void Stop()
	{
		Stop(false);
	}

	public void Stop(bool idClear)
	{
		if (playingCycle_ != null)
		{
			coroutineCtrl.instance.Stop(playingCycle_);
			(playingCycle_ as IDisposable).Dispose();
		}
		ClearCycleData();
		if (idClear)
		{
			BeFlag = 0;
			CharacterID = 0;
			ObjectFOA = 0;
			Alpha = 1f;
			base.gameObject.SetActive(true);
			ResetSpef();
		}
	}

	private void ClearCycleData()
	{
		DisableAllSprites();
		Exists = false;
		playingAnimationName_ = string.Empty;
		cycleDataToSave.sequence = 0;
		cycleDataToSave.framesFromStarted = 0;
		cycleDataToSave.framesInSequence = 0;
	}

	public void Play(string animationName, bool resetAlpha)
	{
		if (LastCharacterID == 0)
		{
			Alpha = 1f;
			if (fadeEnumerator != null && is_fadeIn)
			{
				Alpha = 0f;
			}
		}
		Stop(false);
		in_centor = false;
		BgNumber = bgCtrl.instance.bg_no;
		FrameCountFromStarted = 0;
		foreach (Transform item in base.transform)
		{
			item.gameObject.SetActive(true);
		}
		if (is_fade || resetAlpha)
		{
		}
		base.transform.localScale = Vector3.one;
		playingCycle_ = PlayCycle(animationName);
		coroutineCtrl.instance.Play(playingCycle_);
	}

	public void HoldAndSetAsLastSequence()
	{
		if (IsPlaying || !Exists)
		{
			if (IsPlaying)
			{
				coroutineCtrl.instance.Stop(playingCycle_);
				(playingCycle_ as IDisposable).Dispose();
				playingCycle_ = null;
			}
			AnimationCutHolder.AnimationSource source = SourceHolder.GetSource(playingAnimationName_);
			AnmData anmData = source.Animations[playingAnimationName_];
			if (source == null || anmData == null)
			{
				Exists = false;
				playingAnimationName_ = string.Empty;
				Debug.LogError("Settin last sequence was failed. " + playingAnimationName_);
			}
			else
			{
				SetSpritesAs(playingAnimationName_, source, anmData.sequences[anmData.sequences.Count - 2]);
			}
		}
	}

	private IEnumerator PlayCycle(string animationName)
	{
		bool previous_be_reverse = HasHReverse;
		base.transform.rotation = ((!previous_be_reverse) ? Quaternion.identity : Quaternion.Euler(0f, -180f, 0f));
		playingAnimationName_ = animationName;
		Exists = true;
		bool applyDeletion = false;
		try
		{
			AnimationCutHolder.AnimationSource animationSource = SourceHolder.GetSource(animationName);
			if (animationSource == null)
			{
				yield break;
			}
			AnmData own_animation = animationSource.Animations[animationName];
			SourceHolder.UpdateHistory(animationName);
			soundCtrl sound_ctrl = soundCtrl.instance;
			Dictionary<int, string> se_dic = SourceHolder.GetSeData(animationName);
			Dictionary<int, int> effect_dic = SourceHolder.GetEffectData(animationName);
			while (true)
			{
				for (int i = 0; i < own_animation.sequences.Count; i++)
				{
					cycleDataToSave.sequence = i;
					if (se_dic != null && GSStatic.global_work_.r.no_0 != 8 && GSStatic.global_work_.r.no_0 != 17 && se_dic.ContainsKey(i))
					{
						sound_ctrl.PlaySE(sound_ctrl.GetSeNumber(se_dic[i]));
					}
					if (effect_dic != null && effect_dic.ContainsKey(i))
					{
						SetEffect(effect_dic[i]);
					}
					if (i == own_animation.LoopCodeFrameIndex)
					{
						break;
					}
					int frameInSequence = 0;
					AnmData.Sequence currentSequence = own_animation.sequences[i];
					SetSpritesAs(animationName, animationSource, currentSequence);
					while (frameInSequence < currentSequence.KeepFrame)
					{
						if (previous_be_reverse != HasHReverse)
						{
							previous_be_reverse = !previous_be_reverse;
							base.transform.rotation = ((!previous_be_reverse) ? Quaternion.identity : Quaternion.Euler(0f, -180f, 0f));
						}
						yield return null;
						if (AnimationSystem.Instance.pause)
						{
							continue;
						}
						if (cycleDataToSave.sequenceDataNotLoadedYet)
						{
							cycleDataToSave.sequenceDataNotLoadedYet = false;
							i = cycleDataToSave.sequence;
							if (i == own_animation.LoopCodeFrameIndex)
							{
								i--;
							}
							currentSequence = own_animation.sequences[i];
							SetSpritesAs(animationName, animationSource, currentSequence);
							frameInSequence = cycleDataToSave.framesInSequence;
							FrameCountFromStarted = cycleDataToSave.framesFromStarted;
							yield return null;
						}
						if ((GSStatic.global_work_.r.no_0 == 4 || GSStatic.global_work_.r.no_0 == 7 || GSStatic.global_work_.r.no_0 == 6) && CharacterID != 0 && lifeGaugeCtrl.instance.is_lifegauge_moving_dam())
						{
							if ((GSStatic.global_work_.title == TitleId.GS2 && GSStatic.global_work_.scenario == 6 && animationName == FOA.GS2_FOA.pl0c006.ToString()) || (GSStatic.global_work_.title == TitleId.GS2 && animationName == FOA.GS2_FOA.pl0030d.ToString()) || (GSStatic.global_work_.title == TitleId.GS3 && animationName == FOA.GS3_FOA.pl0030d.ToString()))
							{
								frameInSequence++;
								FrameCountFromStarted++;
							}
						}
						else
						{
							frameInSequence++;
							FrameCountFromStarted++;
						}
						cycleDataToSave.framesInSequence = frameInSequence;
						cycleDataToSave.framesFromStarted = FrameCountFromStarted;
						if (HasMove)
						{
							continue;
						}
						yield break;
					}
				}
				switch (own_animation.LoopCode)
				{
				case AnmData.Sequence.LoopCode.End:
					yield break;
				case AnmData.Sequence.LoopCode.EndAndDelete:
					applyDeletion = true;
					yield break;
				}
			}
		}
		finally
		{
			if (applyDeletion)
			{
				ClearCycleData();
				BeFlag = 0;
				CharacterID = 0;
				ObjectFOA = 0;
				Alpha = 1f;
				base.gameObject.SetActive(true);
				ResetSpef();
			}
			if (CurrentTitle == TitleId.GS3)
			{
				if (is_animation_continue_)
				{
					is_animation_continue_ = false;
				}
				else
				{
					playingCycle_ = null;
				}
			}
			else
			{
				playingCycle_ = null;
			}
		}
	}

	private void SetSpritesAs(string animationName, AnimationCutHolder.AnimationSource animationSource, AnmData.Sequence sequence)
	{
		AnmData anmData = animationSource.Animations[animationName];
		bool flag = (sequence.Attribute & 1) != 0;
		DisableAllSprites();
		AnimationCutHolder.AnimationSource source = SourceHolder.GetSource(animationName);
		List<AnmData.SpriteGroup.Sprite> sprites = anmData.sprite_groups[sequence.UseSpriteGroup].Sprites;
		CreateSpriteAsNeed(sprites.Count);
		ResortChildOrder();
		int num = 0;
		for (int i = 0; i < sequence.UseSpriteGroup; i++)
		{
			num += anmData.sprite_groups[i].Sprites.Count;
		}
		for (int j = 0; j < sprites.Count; j++)
		{
			AnimSpriteData animSpriteData = spriteInstances_[j];
			animSpriteData.enabled = true;
			AnmData.SpriteGroup.Sprite sprite = sprites[j];
			if (source.Sprites[animationName][num + j] == null)
			{
				Rect rect = new Rect(sprite.U, source.Tex.height - sprite.V - sprite.H, sprite.W, sprite.H);
				source.Sprites[animationName][num + j] = Sprite.Create(source.Tex, rect, new Vector2(0f, 1f), 1f, 1u, SpriteMeshType.FullRect);
			}
			animSpriteData.sprite = source.Sprites[animationName][num + j];
			animSpriteData.mask.sprite = animSpriteData.sprite;
			animSpriteData.mask.enabled = sprite_mask_;
			int num2 = -864 + sprite.OffsetX;
			if (!in_centor)
			{
				animSpriteData.transform.localPosition = new Vector3((!flag) ? num2 : (-num2), 864 - sprite.OffsetY);
			}
			else
			{
				animSpriteData.transform.localPosition = set_local_centor_pos;
			}
			animSpriteData.size = new Vector2(sprite.W, sprite.H);
			animSpriteData.transform.localScale = new Vector3((!(flag | sprite.flipThis)) ? 1 : (-1), 1f, 1f);
			if (sprite.flipThis)
			{
				animSpriteData.transform.localPosition += new Vector3(sprite.W, 0f);
			}
		}
	}

	public Vector3 GetCentorPos()
	{
		using (List<AnimSpriteData>.Enumerator enumerator = spriteInstances_.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				AnimSpriteData current = enumerator.Current;
				Vector3 position = new Vector3(current.transform.position.x + current.size.x / 2f, current.transform.position.y - current.size.y / 2f, current.transform.position.z);
				return base.transform.parent.InverseTransformPoint(position);
			}
		}
		return Vector3.zero;
	}

	public void SetCentor(Vector3 centor_pos)
	{
		Vector3 position = Vector3.zero;
		using (List<AnimSpriteData>.Enumerator enumerator = spriteInstances_.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				AnimSpriteData current = enumerator.Current;
				if (current.enabled)
				{
					position = current.transform.position;
				}
			}
		}
		base.transform.localPosition = centor_pos;
		foreach (AnimSpriteData item in spriteInstances_)
		{
			if (item.enabled)
			{
				item.transform.position = position;
				set_local_centor_pos = item.transform.localPosition;
				continue;
			}
			break;
		}
		in_centor = true;
	}

	private void DisableAllSprites()
	{
		foreach (AnimSpriteData item in spriteInstances_)
		{
			item.sprite = null;
			item.enabled = false;
			item.mask.sprite = null;
			item.mask.enabled = false;
		}
	}

	private void CreateSpriteAsNeed(int totalRequired)
	{
		Material currentMaterial = GetCurrentMaterial();
		float value = ((!(currentMaterial != null)) ? 0f : GetSpefVolume());
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		for (int i = spriteInstances_.Count; i < totalRequired; i++)
		{
			SpriteRenderer spriteRenderer = UnityEngine.Object.Instantiate(spriteOriginal_);
			spriteRenderer.transform.SetParent(base.transform);
			spriteRenderer.transform.localScale = Vector3.one;
			spriteRenderer.transform.localRotation = Quaternion.identity;
			spriteRenderer.gameObject.layer = RenderTarget.ObjectLayer;
			SpriteMask component = spriteRenderer.GetComponent<SpriteMask>();
			if (currentMaterial != null)
			{
				spriteRenderer.material = currentMaterial;
				spriteRenderer.GetPropertyBlock(materialPropertyBlock);
				materialPropertyBlock.SetFloat(spefCtrl.instance.volumePropetyId, value);
				spriteRenderer.SetPropertyBlock(materialPropertyBlock);
			}
			AnimSpriteData animSpriteData = new AnimSpriteData();
			animSpriteData.sprite_renderer = spriteRenderer;
			animSpriteData.gameObject = spriteRenderer.gameObject;
			animSpriteData.transform = spriteRenderer.transform;
			animSpriteData.mask = component;
			AnimSpriteData item = animSpriteData;
			spriteInstances_.Add(item);
		}
	}

	private void ResortChildOrder()
	{
		for (int i = 0; i < spriteInstances_.Count; i++)
		{
			spriteInstances_[i].sortingOrder = (i - spriteInstances_.Count) * 2;
			spriteInstances_[i].mask.frontSortingOrder = spriteInstances_[i].sortingOrder - 1;
			spriteInstances_[i].mask.backSortingOrder = -300;
		}
		rootSortingGroup_.sortingOrder = 1;
		rootSortingGroup_.sortingOrder = 0;
		Alpha = Alpha;
	}

	private void SetEffect(int effect_type)
	{
		switch (effect_type)
		{
		case 1:
			if (optionCtrl.instance.is_shake)
			{
				ScreenCtrl.instance.Quake(30, 1u);
			}
			break;
		case 2:
			if (fadeCtrl.instance.status == fadeCtrl.Status.NO_FADE)
			{
				fadeCtrl.instance.play(3u, 1u, 8u, 31u);
			}
			break;
		}
		if (CurrentTitle == TitleId.GS3)
		{
			switch (effect_type)
			{
			case 3:
				AnimationSystem.Instance.PlayTalkCharacter(382);
				break;
			case 4:
				AnimationSystem.Instance.PlayTalkCharacter(528);
				break;
			case 5:
				AnimationSystem.Instance.PlayTalkCharacter(535);
				break;
			case 6:
				AnimationSystem.Instance.PlayTalkCharacter(521);
				break;
			case 7:
				AnimationSystem.Instance.PlayTalkCharacter(536);
				break;
			}
			is_animation_continue_ = true;
		}
	}

	public void Fade(bool fadeIn, int fadeFrame)
	{
		if (fadeEnumerator != null)
		{
			coroutineCtrl.instance.Stop(fadeEnumerator);
			fadeEnumerator = null;
		}
		fadeEnumerator = FadeCoroutine(fadeIn, fadeFrame);
		coroutineCtrl.instance.Play(fadeEnumerator);
	}

	private IEnumerator FadeCoroutine(bool fadeIn, int fadeFrame)
	{
		this.fadeFrame = fadeFrame;
		is_fade = true;
		is_fadeIn = fadeIn;
		float startAlpha = ((!fadeIn) ? 1f : 0f);
		float endAlpha = 1f - startAlpha;
		for (int frame = 1; frame < fadeFrame; frame++)
		{
			if (Exists)
			{
				Alpha = startAlpha + (endAlpha - startAlpha) * (float)frame / (float)fadeFrame;
			}
			if (AnimationSystem.IsPlayingAcro)
			{
				TraceAcroBirdFade();
			}
			yield return null;
		}
		if (Exists)
		{
			Alpha = endAlpha;
		}
		BeFlag &= -33554433;
		if (((uint)BeFlag & 4u) != 0)
		{
			if (((uint)BeFlag & 0x4000000u) != 0)
			{
				if (Exists)
				{
					BeFlag &= -536870913;
					BeFlag |= 134217728;
					base.gameObject.SetActive(false);
					Alpha = 1f;
				}
			}
			else
			{
				Stop(true);
			}
		}
		if (AnimationSystem.IsPlayingAcro)
		{
			TraceAcroBirdFade();
		}
		fadeEnumerator = null;
		is_fade = false;
		this.fadeFrame = 0;
		fadeCtrl.instance.status = fadeCtrl.Status.NO_FADE;
	}

	private void TraceAcroBirdFade()
	{
		foreach (AnimationObject playingAcroBird in AnimationSystem.PlayingAcroBirds)
		{
			playingAcroBird.Alpha = Alpha;
			if (((uint)BeFlag & 0x8000000u) != 0)
			{
				playingAcroBird.BeFlag |= 134217728;
				playingAcroBird.gameObject.SetActive(false);
			}
			else
			{
				playingAcroBird.BeFlag &= -134217729;
				playingAcroBird.gameObject.SetActive(true);
			}
		}
	}

	public void ResetPositionByNameFlag(int name_flag)
	{
		float x = 0f;
		if (((ulong)name_flag & 0x8000uL) != 0)
		{
			x = bgCtrl.instance.body.transform.localPosition.x;
		}
		else if (((ulong)name_flag & 0x4000uL) != 0)
		{
			x = bgCtrl.instance.body.transform.localPosition.x + 1920f;
		}
		base.transform.localPosition = new Vector3(x, 0f, DefaultZ);
	}

	public void PlayMonochrome(ushort time, ushort speed, ushort sw, bool fadeIn)
	{
		sw_ = sw;
		monochromeDataToSave.speed = speed;
		monochromeDataToSave.fadeIn = fadeIn;
		monochromeDataToSave.time = time;
		if (spefEnumerator != null)
		{
			coroutineCtrl.instance.Stop(spefEnumerator);
			spefEnumerator = null;
		}
		BeFlag &= -1025;
		switch (sw)
		{
		case 0:
			SetSpefMaterial(grayscale_);
			if (fadeIn)
			{
				BeFlag |= 1024;
			}
			break;
		case 1:
			SetSpefMaterial(default_);
			SetSpefVolume(0f);
			break;
		case 2:
			SetSpefMaterial(grayscale_);
			SetSpefVolume(1f);
			BeFlag |= 1024;
			return;
		case 3:
			SetSpefMaterial(black_);
			break;
		case 6:
			SetSpefMaterial(sepia_);
			break;
		default:
			SetSpefMaterial(default_);
			return;
		}
		if (fadeIn)
		{
			spefEnumerator = SetSpef(time, speed);
		}
		else
		{
			spefEnumerator = SetSpefRet(time, speed);
		}
		coroutineCtrl.instance.Play(spefEnumerator);
	}

	private IEnumerator SetSpef(ushort time, ushort speed)
	{
		ushort timer = 0;
		float volume2 = ((speed < 32) ? ((float)(int)speed / 32f) : 1f);
		SetSpefVolume(volume2);
		do
		{
			ushort num;
			timer = (num = (ushort)(timer + 1));
			if (num >= time)
			{
				timer = 0;
				speed = (num = (ushort)(speed + 1));
				volume2 = (float)(int)num / 32f;
				if (speed >= 32)
				{
					volume2 = 1f;
				}
				SetSpefVolume(volume2);
			}
			yield return null;
		}
		while (speed < 32);
		spefEnumerator = null;
	}

	private IEnumerator SetSpefRet(ushort time, ushort speed)
	{
		ushort timer = 0;
		float volume2 = ((speed > 0) ? ((float)(int)speed / 32f) : 0f);
		SetSpefVolume(volume2);
		do
		{
			ushort num;
			timer = (num = (ushort)(timer + 1));
			if (num >= time)
			{
				timer = 0;
				speed = (num = (ushort)(speed - 1));
				volume2 = (float)(int)num / 32f;
				if ((short)speed <= 0)
				{
					volume2 = 0f;
				}
				SetSpefVolume(volume2);
			}
			yield return null;
		}
		while ((short)speed > 0);
		spefEnumerator = null;
	}

	private void SetSpefMaterial(Material material)
	{
		foreach (AnimSpriteData item in spriteInstances_)
		{
			item.material = material;
		}
	}

	public void SetSpefVolume(float volume)
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		foreach (AnimSpriteData item in spriteInstances_)
		{
			item.GetPropertyBlock(materialPropertyBlock);
			materialPropertyBlock.SetFloat(spefCtrl.instance.volumePropetyId, volume);
			item.SetPropertyBlock(materialPropertyBlock);
		}
	}

	public void ResetSpef()
	{
		SetSpefMaterial(default_);
		sw_ = ushort.MaxValue;
		monochromeDataToSave = default(MonochromeDataToSave);
	}

	public bool IsMonochrom()
	{
		if (spriteInstances_.Count > 0)
		{
			return Exists && (BeFlag & 0x400) != 0;
		}
		return false;
	}

	private Material GetCurrentMaterial()
	{
		switch (sw_)
		{
		case 0:
			return grayscale_;
		case 3:
			return black_;
		case 6:
			return sepia_;
		default:
			return null;
		}
	}

	private float GetSpefVolume()
	{
		float result = 0f;
		if (spriteInstances_.Count != 0)
		{
			MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
			spriteInstances_[0].GetPropertyBlock(materialPropertyBlock);
			result = materialPropertyBlock.GetFloat(spefCtrl.instance.volumePropetyId);
		}
		return result;
	}
}
