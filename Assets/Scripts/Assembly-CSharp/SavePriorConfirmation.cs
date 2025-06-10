using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SavePriorConfirmation : MonoBehaviour
{
	[SerializeField]
public Text[] confirmation_text_;

	[SerializeField]
public AssetBundleSprite message_window_;

	[SerializeField]
public AssetBundleSprite bg_;

	[SerializeField]
public titleSelectPlate select_plate_;

	[SerializeField]
public GameObject body_;

	private IEnumerator enumerator_confirmation_;

	public static SavePriorConfirmation instance { get; private set; }

	public bool is_end { get; private set; }

	public bool is_save { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	public void Load()
	{
		message_window_.load("/menu/common/", "talk_bg");
		bg_.load("/GS1/BG/", "bg043");
	}

	public void Init()
	{
		Load();
		select_plate_.mainTitleInit(new string[2]
		{
			TextDataCtrl.GetText(TextDataCtrl.TitleTextID.YES),
			TextDataCtrl.GetText(TextDataCtrl.TitleTextID.NO)
		}, "select_button", 0);
		confirmation_text_[0].text = TextDataCtrl.GetText(TextDataCtrl.SaveTextID.CLEAR_SAVE);
		confirmation_text_[1].text = TextDataCtrl.GetText(TextDataCtrl.SaveTextID.CLEAR_SAVE, 1);
		body_.SetActive(true);
	}

	public void End()
	{
		if (enumerator_confirmation_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_confirmation_);
			enumerator_confirmation_ = null;
		}
		select_plate_.End();
		is_end = true;
		body_.SetActive(false);
	}

	public void OpenConfirmation()
	{
		Init();
		if (enumerator_confirmation_ != null)
		{
			coroutineCtrl.instance.Stop(enumerator_confirmation_);
		}
		enumerator_confirmation_ = CoroutineConfirmation();
		coroutineCtrl.instance.Play(CoroutineConfirmation());
	}

	private IEnumerator CoroutineConfirmation()
	{
		is_end = false;
		select_plate_.body_active = true;
		select_plate_.playCursor(0);
		yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play(0.5f, true));
		while (true)
		{
			if (select_plate_.is_end)
			{
				if (select_plate_.cursor_no == 0)
				{
					is_save = true;
				}
				else
				{
					is_save = false;
				}
				break;
			}
			if (!select_plate_.is_play_animation && padCtrl.instance.GetKeyDown(KeyType.B))
			{
				select_plate_.stopCursor();
				select_plate_.body_active = false;
				soundCtrl.instance.PlaySE(44);
				is_save = false;
				break;
			}
			yield return null;
		}
		yield return coroutineCtrl.instance.Play(fadeCtrl.instance.play(0.5f, false));
		enumerator_confirmation_ = null;
		is_end = true;
		body_.SetActive(false);
	}
}
