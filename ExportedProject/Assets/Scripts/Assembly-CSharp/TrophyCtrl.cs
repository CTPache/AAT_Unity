using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrophyCtrl
{
	private class MessageInfo
	{
		public readonly TitleId title;

		public readonly byte scenario;

		public readonly ushort mes_no;

		public MessageInfo(TitleId title, byte scenario, uint mes_no)
		{
			this.title = title;
			this.scenario = scenario;
			this.mes_no = (ushort)mes_no;
		}
	}

	private class TrophyMessageFlagInfo
	{
		public readonly int trophy;

		public readonly MessageInfo[] message_info;

		public TrophyMessageFlagInfo(int trophy, TitleId title, byte scenario, uint mes_no)
		{
			this.trophy = trophy;
			message_info = new MessageInfo[1]
			{
				new MessageInfo(title, scenario, mes_no)
			};
		}

		public TrophyMessageFlagInfo(int trophy, MessageInfo[] message_info)
		{
			this.trophy = trophy;
			this.message_info = message_info;
		}

		public bool HasMessageInfo(TitleId title, byte scenario, uint mes_no)
		{
			for (int i = 0; i < message_info.Length; i++)
			{
				MessageInfo messageInfo = message_info[i];
				if (messageInfo.title == title && messageInfo.scenario == scenario && messageInfo.mes_no == mes_no)
				{
					return true;
				}
			}
			return false;
		}
	}

	private static bool[] trophy_ = new bool[31];

	private static BitArray message_flag_ = new BitArray(96);

	private static TrophyMessageFlagInfo[] trophy_info = new TrophyMessageFlagInfo[67]
	{
		new TrophyMessageFlagInfo(15, TitleId.GS1, 0, scenario.SC0_00050),
		new TrophyMessageFlagInfo(16, TitleId.GS1, 13, scenario.SC3_03500),
		new TrophyMessageFlagInfo(16, TitleId.GS1, 13, scenario.SC3_03510),
		new TrophyMessageFlagInfo(16, TitleId.GS1, 13, scenario.SC3_03520),
		new TrophyMessageFlagInfo(17, TitleId.GS2, 0, scenario_GS2.SC0_00380),
		new TrophyMessageFlagInfo(18, TitleId.GS2, 2, scenario_GS2.SC1_01930),
		new TrophyMessageFlagInfo(19, TitleId.GS2, 8, 144u),
		new TrophyMessageFlagInfo(20, TitleId.GS2, 16, scenario_GS2.SC3_19820),
		new TrophyMessageFlagInfo(21, TitleId.GS3, 1, scenario_GS3.SC0_1_31120),
		new TrophyMessageFlagInfo(22, new MessageInfo[2]
		{
			new MessageInfo(TitleId.GS3, 2, scenario_GS3.SC1_0_57830),
			new MessageInfo(TitleId.GS3, 4, 200u)
		}),
		new TrophyMessageFlagInfo(22, TitleId.GS3, 4, 274u),
		new TrophyMessageFlagInfo(22, TitleId.GS3, 4, 279u),
		new TrophyMessageFlagInfo(22, TitleId.GS3, 4, 278u),
		new TrophyMessageFlagInfo(23, TitleId.GS3, 7, 225u),
		new TrophyMessageFlagInfo(23, new MessageInfo[7]
		{
			new MessageInfo(TitleId.GS3, 7, 240u),
			new MessageInfo(TitleId.GS3, 7, 241u),
			new MessageInfo(TitleId.GS3, 7, 243u),
			new MessageInfo(TitleId.GS3, 7, 249u),
			new MessageInfo(TitleId.GS3, 7, 274u),
			new MessageInfo(TitleId.GS3, 9, scenario_GS3.SC2_2_44010),
			new MessageInfo(TitleId.GS3, 10, scenario_GS3.SC2_3_45010)
		}),
		new TrophyMessageFlagInfo(23, TitleId.GS3, 7, 225u),
		new TrophyMessageFlagInfo(23, new MessageInfo[1]
		{
			new MessageInfo(TitleId.GS3, 7, 238u)
		}),
		new TrophyMessageFlagInfo(23, TitleId.GS3, 7, 239u),
		new TrophyMessageFlagInfo(23, new MessageInfo[5]
		{
			new MessageInfo(TitleId.GS3, 7, 239u),
			new MessageInfo(TitleId.GS3, 7, 248u),
			new MessageInfo(TitleId.GS3, 7, 283u),
			new MessageInfo(TitleId.GS3, 9, scenario_GS3.SC2_2_42770),
			new MessageInfo(TitleId.GS3, 9, scenario_GS3.SC2_2_42880)
		}),
		new TrophyMessageFlagInfo(23, TitleId.GS3, 7, 240u),
		new TrophyMessageFlagInfo(23, TitleId.GS3, 7, 226u),
		new TrophyMessageFlagInfo(23, new MessageInfo[6]
		{
			new MessageInfo(TitleId.GS3, 7, 246u),
			new MessageInfo(TitleId.GS3, 7, 247u),
			new MessageInfo(TitleId.GS3, 7, 272u),
			new MessageInfo(TitleId.GS3, 9, scenario_GS3.SC2_2_42740),
			new MessageInfo(TitleId.GS3, 9, scenario_GS3.SC2_2_42780),
			new MessageInfo(TitleId.GS3, 9, scenario_GS3.SC2_2_42870)
		}),
		new TrophyMessageFlagInfo(23, new MessageInfo[2]
		{
			new MessageInfo(TitleId.GS3, 7, 248u),
			new MessageInfo(TitleId.GS3, 9, scenario_GS3.SC2_2_42770)
		}),
		new TrophyMessageFlagInfo(23, new MessageInfo[2]
		{
			new MessageInfo(TitleId.GS3, 7, 251u),
			new MessageInfo(TitleId.GS3, 9, scenario_GS3.SC2_2_42830)
		}),
		new TrophyMessageFlagInfo(23, new MessageInfo[2]
		{
			new MessageInfo(TitleId.GS3, 7, 251u),
			new MessageInfo(TitleId.GS3, 9, scenario_GS3.SC2_2_42830)
		}),
		new TrophyMessageFlagInfo(23, new MessageInfo[1]
		{
			new MessageInfo(TitleId.GS3, 7, 266u)
		}),
		new TrophyMessageFlagInfo(23, new MessageInfo[2]
		{
			new MessageInfo(TitleId.GS3, 7, 280u),
			new MessageInfo(TitleId.GS3, 10, scenario_GS3.SC2_3_44720)
		}),
		new TrophyMessageFlagInfo(23, TitleId.GS3, 7, 279u),
		new TrophyMessageFlagInfo(23, TitleId.GS3, 7, 283u),
		new TrophyMessageFlagInfo(23, TitleId.GS3, 7, 283u),
		new TrophyMessageFlagInfo(23, TitleId.GS3, 7, 282u),
		new TrophyMessageFlagInfo(23, TitleId.GS3, 7, 288u),
		new TrophyMessageFlagInfo(23, new MessageInfo[1]
		{
			new MessageInfo(TitleId.GS3, 7, 287u)
		}),
		new TrophyMessageFlagInfo(23, new MessageInfo[2]
		{
			new MessageInfo(TitleId.GS3, 9, scenario_GS3.SC2_2_42790),
			new MessageInfo(TitleId.GS3, 10, scenario_GS3.SC2_3_45060)
		}),
		new TrophyMessageFlagInfo(23, new MessageInfo[0]),
		new TrophyMessageFlagInfo(23, new MessageInfo[0]),
		new TrophyMessageFlagInfo(23, TitleId.GS3, 10, scenario_GS3.SC2_3_44760),
		new TrophyMessageFlagInfo(23, new MessageInfo[0]),
		new TrophyMessageFlagInfo(24, TitleId.GS3, 9, scenario_GS3.SC2_2_43830),
		new TrophyMessageFlagInfo(25, TitleId.GS3, 15, scenario_GS3.SC4_0_48970),
		new TrophyMessageFlagInfo(25, TitleId.GS3, 15, scenario_GS3.SC4_0_55170),
		new TrophyMessageFlagInfo(27, TitleId.GS2, 21, scenario_GS2.SC3_BAD_END),
		new TrophyMessageFlagInfo(28, new MessageInfo[2]
		{
			new MessageInfo(TitleId.GS1, 5, scenario.SC2_12080),
			new MessageInfo(TitleId.GS1, 7, scenario.SC2_13770)
		}),
		new TrophyMessageFlagInfo(28, new MessageInfo[2]
		{
			new MessageInfo(TitleId.GS2, 8, 312u),
			new MessageInfo(TitleId.GS2, 11, 329u)
		}),
		new TrophyMessageFlagInfo(28, new MessageInfo[2]
		{
			new MessageInfo(TitleId.GS3, 2, 318u),
			new MessageInfo(TitleId.GS3, 4, scenario_GS3.SC1_2_36740)
		}),
		new TrophyMessageFlagInfo(28, TitleId.GS3, 19, 325u),
		new TrophyMessageFlagInfo(28, new MessageInfo[3]
		{
			new MessageInfo(TitleId.GS1, 18, scenario.SC4_63830),
			new MessageInfo(TitleId.GS1, 22, scenario.SC4_62940),
			new MessageInfo(TitleId.GS1, 29, scenario.SC4_67280)
		}),
		new TrophyMessageFlagInfo(29, new MessageInfo[1]
		{
			new MessageInfo(TitleId.GS1, 18, scenario.SC4_60270)
		}),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 18, scenario.SC4_60270),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 18, scenario.SC4_60330),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 18, scenario.SC4_60300),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 18, scenario.SC4_60310),
		new TrophyMessageFlagInfo(29, new MessageInfo[1]
		{
			new MessageInfo(TitleId.GS1, 20, 168u)
		}),
		new TrophyMessageFlagInfo(29, new MessageInfo[0]),
		new TrophyMessageFlagInfo(29, new MessageInfo[0]),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 19, 151u),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 19, 152u),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 19, 170u),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 19, 170u),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 20, 155u),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 20, 157u),
		new TrophyMessageFlagInfo(29, new MessageInfo[2]
		{
			new MessageInfo(TitleId.GS1, 20, 168u),
			new MessageInfo(TitleId.GS1, 20, 170u)
		}),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 20, 170u),
		new TrophyMessageFlagInfo(29, new MessageInfo[2]
		{
			new MessageInfo(TitleId.GS1, 20, 181u),
			new MessageInfo(TitleId.GS1, 20, 183u)
		}),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 22, scenario.SC4_62550),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 22, scenario.SC4_62610),
		new TrophyMessageFlagInfo(29, TitleId.GS1, 29, scenario.SC4_67400)
	};

	public static readonly string[] message_flag_name = new string[67]
	{
		"帰らせていただきます", "秘密コンプ・ミサイル", "秘密コンプ・ツリザオ", "秘密コンプ・金属探知機", "よけいなおせわッス", "ジャネットさん", "ロバート・Ｂ・ナルホドー", "ミラクル仮面", "夜道にはご用心", "オバチャンの痕跡①",
		"オバチャンの痕跡②", "オバチャンの痕跡③", "オバチャンの痕跡④", "ホンドボーのフランス語教室\u3000ボンジュウル", "ホンドボーのフランス語教室\u3000ウィ", "ホンドボーのフランス語教室\u3000ビアンヴニュ", "ホンドボーのフランス語教室\u3000トレ・ビアン", "ホンドボーのフランス語教室\u3000エ・ビアン", "ホンドボーのフランス語教室\u3000モン・デュ", "ホンドボーのフランス語教室\u3000メ・ビアン・シュール",
		"ホンドボーのフランス語教室\u3000ジュ・ヴザン・プリ", "ホンドボーのフランス語教室\u3000オー・ラ・ラ", "ホンドボーのフランス語教室\u3000セ・ラ・ヴィ", "ホンドボーのフランス語教室\u3000フェリシタシオン", "ホンドボーのフランス語教室\u3000メ・ケスク・セ", "ホンドボーのフランス語教室\u3000ヴォロンティエ", "ホンドボーのフランス語教室\u3000プルコワ", "ホンドボーのフランス語教室\u3000サ・ヴァ", "ホンドボーのフランス語教室\u3000ユイ・マンソンジュ", "ホンドボーのフランス語教室\u3000シルヴ・プレ",
		"ホンドボーのフランス語教室\u3000オ・ルヴォア", "ホンドボーのフランス語教室\u3000ケスチョン", "ホンドボーのフランス語教室\u3000クワ", "ホンドボーのフランス語教室\u3000ジュ・スイ・デゾレ", "ホンドボーのフランス語教室\u3000グラン・ミロワール", "ホンドボーのフランス語教室\u3000コム", "ホンドボーのフランス語教室\u3000メルシ・ビエン", "ホンドボーのフランス語教室\u3000ジュ・ヴ・デュマン・パルドン", "うらみのお稲荷さん", "古き厄介な友人\u3000①弁護士バッジ",
		"古き厄介な友人\u3000②万国旗", "バッドエンディング", "はしごと脚立\u3000①真宵", "はしごと脚立\u3000②真宵", "はしごと脚立\u3000③真宵", "はしごと脚立\u3000④イトノコ", "はしごと脚立\u3000⑤茜", "ベン・トーマスター\u3000とりそぼろ", "ベン・トーマスター\u3000ノリ", "ベン・トーマスター\u3000自家製タクアン",
		"ベン・トーマスター\u3000２段重ね", "ベン・トーマスター\u3000くさや", "ベン・トーマスター\u3000キャビア", "ベン・トーマスター\u3000ふりかけ", "ベン・トーマスター\u3000ゴマ", "ベン・トーマスター\u3000ソース", "ベン・トーマスター\u3000初恋", "ベン・トーマスター\u3000青ノリ", "ベン・トーマスター\u3000ケチャップ", "ベン・トーマスター\u3000スイカ",
		"ベン・トーマスター\u3000ミカンのカワ", "ベン・トーマスター\u3000輪ゴム", "ベン・トーマスター\u3000スペシャル", "ベン・トーマスター\u3000シオカラ", "ベン・トーマスター\u3000サシミ", "ベン・トーマスター\u3000ステーキ", "ベン・トーマスター\u3000シンタマ"
	};

	public const int GS1_SC0_CLEAR = 1;

	public const int GS1_SC1_CLEAR = 2;

	public const int GS1_SC2_CLEAR = 3;

	public const int GS1_SC3_CLEAR = 4;

	public const int GS1_SC4_CLEAR = 5;

	public const int GS2_SC0_CLEAR = 6;

	public const int GS2_SC1_CLEAR = 7;

	public const int GS2_SC2_CLEAR = 8;

	public const int GS2_SC3_CLEAR = 9;

	public const int GS3_SC0_CLEAR = 10;

	public const int GS3_SC1_CLEAR = 11;

	public const int GS3_SC2_CLEAR = 12;

	public const int GS3_SC3_CLEAR = 13;

	public const int GS3_SC4_CLEAR = 14;

	public const int GO_HOME = 15;

	public const int WEAPON_COMPLETE = 16;

	public const int UNNECESSARY_CARE = 17;

	public const int JANET = 18;

	public const int ROBERT_B_NARUHODO = 19;

	public const int MIRACLE_MASK = 20;

	public const int NIGHT_ROAD = 21;

	public const int OBACHAN_TRACE = 22;

	public const int FRENCH = 23;

	public const int INARI_SUSHI = 24;

	public const int YAHARI = 25;

	public const int HELLOW_GS = 26;

	public const int BAD_END = 27;

	public const int LADDER_STEPLADDER = 28;

	public const int LUNCHBOX_MASTER = 29;

	public const int SCENARIO_ALL_CLEAR = 30;

	public const int TROPHY_ALL_COMPLETE = 31;

	public static bool[] trophy
	{
		get
		{
			return trophy_;
		}
	}

	public static BitArray message_flag
	{
		get
		{
			return message_flag_;
		}
	}

	public static bool disable_check_trophy_by_mes_no { get; set; }

	public static void init(bool[] in_trophy)
	{
		for (int i = 0; i < trophy_.Length; i++)
		{
			if (i >= in_trophy.Length)
			{
				trophy_[i] = false;
				continue;
			}
			if (in_trophy[i])
			{
				Debug.Log("Trophy Num : [" + i + "] is already get");
			}
			trophy_[i] = in_trophy[i];
		}
	}

	public static void reset()
	{
		for (int i = 0; i < trophy_.Length; i++)
		{
			trophy_[i] = false;
		}
	}

	public static void reset_message_flag()
	{
		message_flag_.SetAll(false);
	}

	public static void set_trophy(int trophy_id, bool flag)
	{
		trophy_[trophy_id] = flag;
	}

	public static void set_tropthy(int in_id)
	{
		if (!trophy_[in_id])
		{
			Debug.Log("set_trophy(" + in_id + ")");
			trophy_[in_id] = true;
			SteamCtrl.SetAchievement(in_id);
		}
	}

	public static void set_flag(int flag)
	{
		if (flag < trophy_info.Length)
		{
			message_flag_[flag] = true;
			check_trophy_message_flag(trophy_info[flag].trophy);
		}
	}

	public static void check_trophy_by_mes_no()
	{
		if (disable_check_trophy_by_mes_no || !GSStatic.message_work_.enable_message_trophy)
		{
			return;
		}
		GSStatic.message_work_.enable_message_trophy = false;
		TitleId title = GSStatic.global_work_.title;
		byte b = GSStatic.global_work_.scenario;
		ushort now_no = GSStatic.message_work_.now_no;
		HashSet<int> hashSet = new HashSet<int>();
		for (int i = 0; i < trophy_info.Length; i++)
		{
			if (trophy_info[i].HasMessageInfo(title, b, now_no))
			{
				message_flag_[i] = true;
				hashSet.Add(trophy_info[i].trophy);
			}
		}
		if (hashSet.Count <= 0)
		{
			return;
		}
		foreach (int item in hashSet)
		{
			check_trophy_message_flag(item);
		}
	}

	private static void check_trophy_message_flag(int trophy_id)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < trophy_info.Length; i++)
		{
			if (trophy_info[i].trophy == trophy_id)
			{
				num++;
				if (message_flag_[i])
				{
					num2++;
				}
			}
		}
		if (num2 >= num)
		{
			set_tropthy(trophy_id);
		}
	}

	public static void check_trophy_scenario_clear(bool ending)
	{
		TitleId title = GSStatic.global_work_.title;
		byte b = GSStatic.global_work_.scenario;
		switch (title)
		{
		case TitleId.GS1:
			switch (b)
			{
			case 1:
				set_tropthy(1);
				break;
			case 5:
				set_tropthy(2);
				break;
			case 11:
				set_tropthy(3);
				break;
			case 17:
				set_tropthy(4);
				break;
			case 34:
				if (ending)
				{
					set_tropthy(5);
				}
				break;
			}
			break;
		case TitleId.GS2:
			switch (b)
			{
			case 2:
				set_tropthy(6);
				break;
			case 8:
				set_tropthy(7);
				break;
			case 14:
				set_tropthy(8);
				break;
			case 21:
				if (ending)
				{
					set_tropthy(9);
				}
				break;
			}
			break;
		case TitleId.GS3:
			switch (b)
			{
			case 2:
				set_tropthy(10);
				break;
			case 7:
				set_tropthy(11);
				break;
			case 12:
				set_tropthy(12);
				break;
			case 14:
				set_tropthy(13);
				break;
			case 22:
				if (ending)
				{
					set_tropthy(14);
				}
				break;
			}
			break;
		}
		int[] array = new int[14]
		{
			1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
			11, 12, 13, 14
		};
		bool flag = true;
		for (int i = 0; i < array.Length; i++)
		{
			if (!trophy_[array[i]])
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			set_tropthy(30);
		}
	}

	public static void MessageFlagCopyToIntArray(int[] array)
	{
		message_flag_.CopyTo(array, 0);
	}

	public static void MessageFlagCopyFromIntArray(int[] array)
	{
		if (array == null)
		{
			array = new int[3];
		}
		else if (array.Length != 3)
		{
			int[] array2 = new int[3];
			Array.Copy(array, array2, (array.Length >= 3) ? 3 : array.Length);
			array = array2;
		}
		message_flag_ = new BitArray(array);
	}
}
