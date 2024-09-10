using System;

[Serializable]
public class FURIKO_WK
{
	public ushort total;

	public ushort status;

	public ushort RegBG2CNT;

	public ushort cnt;

	public ushort QuakeTime;

	public ushort QuakeCnt;

	public ushort FlashCnt;

	public ushort ScrollFlag;

	public ushort DispFlag;

	public ushort now_bg;

	public ushort next_bg;

	public ushort[] para = new ushort[4];

	public ushort[] sc_cnt = new ushort[3];

	public ushort[] sc_line = new ushort[3];

	public ushort[] sc_over = new ushort[3];

	public ushort[] work = new ushort[16];

	public void Clear()
	{
		total = 0;
		status = 0;
		RegBG2CNT = 0;
		cnt = 0;
		QuakeTime = 0;
		QuakeCnt = 0;
		FlashCnt = 0;
		ScrollFlag = 0;
		DispFlag = 0;
		now_bg = 0;
		next_bg = 0;
		Array.Clear(para, 0, para.Length);
		Array.Clear(sc_cnt, 0, sc_cnt.Length);
		Array.Clear(sc_line, 0, sc_line.Length);
		Array.Clear(sc_over, 0, sc_over.Length);
		Array.Clear(work, 0, work.Length);
	}

	public void init()
	{
		total = 0;
		status = 0;
		RegBG2CNT = 0;
		cnt = 0;
		QuakeTime = 0;
		QuakeCnt = 0;
		FlashCnt = 0;
		ScrollFlag = 0;
		DispFlag = 0;
		now_bg = 0;
		next_bg = 0;
		for (int i = 0; i < para.Length; i++)
		{
			para[i] = 0;
		}
		for (int j = 0; j < sc_cnt.Length; j++)
		{
			sc_cnt[j] = 0;
		}
		for (int k = 0; k < sc_line.Length; k++)
		{
			sc_line[k] = 0;
		}
		for (int l = 0; l < sc_over.Length; l++)
		{
			sc_over[l] = 0;
		}
		for (int m = 0; m < work.Length; m++)
		{
			work[m] = 0;
		}
	}
}
