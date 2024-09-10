using System.Collections;
using UnityEngine;

public class forcedRecordCtrl : MonoBehaviour
{
	public static forcedRecordCtrl instance { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	public void forcedTukituke()
	{
		StartCoroutine(CoroutineForcedTukituke());
	}

	private IEnumerator CoroutineForcedTukituke()
	{
		GlobalWork global_work = GSStatic.global_work_;
		messageBoardCtrl.instance.board(false, false);
		fadeCtrl.instance.play(3u, 1u, 4u);
		Balloon.PlayTakeThat();
		while (objMoveCtrl.instance.is_play)
		{
			yield return null;
		}
		objMoveCtrl.instance.stop(4);
		messageBoardCtrl.instance.board(true, false);
		global_work.status_flag &= 4294966527u;
		global_work.r_bk.Set(4, 1, 0, 0);
		MessageWork message_work = MessageSystem.GetActiveMessageWork();
		int mujyun_id = recordListCtrl.instance.selectNoteIDToMujyunID(message_work.now_no);
		messageBoardCtrl.instance.board(true, false);
		if (mujyun_id != -1)
		{
			MUJYUN_CK_DATA[] mujyunCkData = GSScenario.GetMujyunCkData();
			uint jump = mujyunCkData[mujyun_id].jump;
			advCtrl.instance.message_system_.SetMessage(jump);
		}
		else
		{
			uint num = message_work.now_no;
			if ((message_work.status & MessageSystem.Status.NEXT_PULS) != 0)
			{
				num++;
			}
			else
			{
				uint sYS_M = scenario.SYS_M0100;
				uint sYS_M2 = scenario.SYS_M0110;
				uint sYS_M3 = scenario.SYS_M0120;
				uint sYS_M4 = scenario.SYS_M0130;
				switch (global_work.title)
				{
				case TitleId.GS1:
					sYS_M = scenario.SYS_M0100;
					sYS_M2 = scenario.SYS_M0110;
					sYS_M3 = scenario.SYS_M0120;
					sYS_M4 = scenario.SYS_M0130;
					break;
				case TitleId.GS2:
					sYS_M = scenario_GS2.SYS_M0100;
					sYS_M2 = scenario_GS2.SYS_M0110;
					sYS_M3 = scenario_GS2.SYS_M0120;
					sYS_M4 = scenario_GS2.SYS_M0130;
					break;
				case TitleId.GS3:
					sYS_M = scenario_GS3.SYS_M0100;
					sYS_M2 = scenario_GS3.SYS_M0110;
					sYS_M3 = scenario_GS3.SYS_M0120;
					sYS_M4 = scenario_GS3.SYS_M0130;
					break;
				}
				switch (Random.Range(0, 4))
				{
				case 0:
					num = sYS_M;
					break;
				case 1:
					num = sYS_M2;
					break;
				case 2:
					num = sYS_M3;
					break;
				case 3:
					num = sYS_M4;
					break;
				}
				MessageSystem.SetActiveMessageWindow(WindowType.SUB);
			}
			advCtrl.instance.message_system_.SetMessage(num);
		}
		global_work.r.CopyFrom(ref global_work.r_bk);
	}
}
