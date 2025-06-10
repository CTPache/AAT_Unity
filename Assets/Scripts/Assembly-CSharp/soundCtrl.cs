using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundCtrl : MonoBehaviour
{
    private enum BGMFadeState
    {
        FADE_NONE = 0,
        FADE_IN = 1,
        FADE_OUT = 2
    }

    [Serializable]
    public class AssetBundleClip
    {
        public List<AudioClip> clip_list_ = new List<AudioClip>();

        public AssetBundle asset_bundle_;

        public void load(string in_path, string in_name, bool force)
        {
            AssetBundle assetBundle = AssetBundleCtrl.instance.load(in_path, in_name, false, -1, force);
            clip_list_.AddRange(assetBundle.LoadAllAssets<AudioClip>());
        }

        public AudioClip clip(string in_name)
        {
            foreach (AudioClip item in clip_list_)
            {
                if (item.name == in_name)
                {
                    return item;
                }
            }
            return null;
        }

        public void end()
        {
            clip_list_.Clear();
        }
    }

    private static soundCtrl instance_;

    private int play_bgm_no = 254;

    private int stop_bgm_no = 254;

    private int[] loop_se_no = new int[10] { 268435455, 268435455, 268435455, 268435455, 268435455, 268435455, 268435455, 268435455, 268435455, 268435455 };

    private int play_bgm_audio_index;

    private float current_volume = 1f;

    private BGMFadeState is_bgm_fade_;

    private bool[] is_pause_bgm = new bool[3];

    private int max_volume = 256;

    public float option_set_bgm_rate = 1f;

    public float option_set_se_rate = 1f;

    [SerializeField]
public List<AudioSource> list_SE_ = new List<AudioSource>();

    [SerializeField]
public List<AudioSource> list_BGM_ = new List<AudioSource>();

    private List<AssetBundleClip> asset_SE_ = new List<AssetBundleClip>();

    private List<AssetBundleClip> asset_BGM_ = new List<AssetBundleClip>();

    public byte[] GS1_Talk_se_data_tbl = new byte[55]
    {
        0, 0, 0, 0, 1, 1, 1, 1, 0, 0,
        0, 0, 0, 0, 0, 1, 1, 1, 0, 0,
        0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
        0, 1, 0, 0, 0, 0, 0, 0, 1, 1,
        1, 1, 0, 0, 1, 1, 0, 0, 0, 0,
        1, 0, 0, 0, 0
    };

    public byte[] GS2_Talk_se_data_tbl = new byte[54]
    {
        0, 0, 1, 0, 1, 1, 1, 0, 0, 0,
        0, 0, 0, 0, 0, 1, 0, 1, 1, 1,
        1, 1, 0, 0, 1, 1, 1, 1, 1, 0,
        0, 0, 0, 1, 1, 0, 1, 0, 0, 1,
        0, 1, 0, 0, 1, 0, 0, 1, 1, 0,
        0, 0, 0, 0
    };

    public byte[] GS3_Talk_se_data_tbl = new byte[68]
    {
        0, 0, 1, 0, 1, 0, 0, 1, 0, 0,
        0, 1, 0, 0, 1, 0, 1, 0, 1, 0,
        0, 1, 1, 0, 1, 1, 0, 0, 0, 0,
        1, 0, 1, 1, 1, 1, 0, 1, 0, 0,
        1, 1, 0, 1, 1, 0, 1, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 1, 1, 0, 0,
        0, 0, 0, 1, 1, 1, 0, 1
    };

    private IEnumerator enumerator_fade_BGM_;

    public const ushort BGM000 = 0;

    public const ushort BGM001 = 1;

    public const ushort BGM002 = 2;

    public const ushort BGM003 = 3;

    public const ushort BGM004 = 4;

    public const ushort BGM005 = 5;

    public const ushort BGM006 = 6;

    public const ushort BGM007 = 7;

    public const ushort BGM008 = 8;

    public const ushort BGM009 = 9;

    public const ushort BGM010 = 10;

    public const ushort BGM011 = 11;

    public const ushort BGM012 = 12;

    public const ushort BGM013 = 13;

    public const ushort BGM014 = 14;

    public const ushort BGM015 = 15;

    public const ushort BGM016 = 16;

    public const ushort BGM017 = 17;

    public const ushort BGM018 = 18;

    public const ushort BGM019 = 19;

    public const ushort BGM020 = 20;

    public const ushort BGM021 = 21;

    public const ushort BGM022 = 22;

    public const ushort BGM023 = 23;

    public const ushort BGM024 = 24;

    public const ushort BGM025 = 25;

    public const ushort BGM026 = 26;

    public const ushort BGM027 = 27;

    public const ushort BGM028 = 28;

    public const ushort BGM029 = 29;

    public const ushort BGM030 = 30;

    public const ushort BGM031 = 31;

    public const ushort BGM032 = 32;

    public const ushort BGM033 = 33;

    public const ushort BGM034 = 34;

    public const ushort BGM035 = 35;

    public const ushort BGM036 = 36;

    public const ushort BGM037 = 37;

    public const ushort BGM038 = 38;

    public const ushort BGM039 = 39;

    public const ushort BGM040 = 40;

    public const ushort BGM041 = 41;

    public const ushort BGM050 = 200;

    public const ushort BGM051 = 201;

    public const ushort BGM052 = 202;

    public const ushort BGM053 = 203;

    public const ushort BGM054 = 204;

    public const ushort BGM055 = 205;

    public const ushort BGM056 = 206;

    public const ushort BGM057 = 207;

    public const ushort BGM058 = 208;

    public const ushort BGM059 = 209;

    public const ushort BGM060 = 210;

    public const ushort BGM061 = 211;

    public const ushort BGM062 = 212;

    public const ushort BGM063 = 213;

    public const ushort BGM064 = 214;

    public const ushort BGM065 = 215;

    public const ushort BGM066 = 216;

    public const ushort BGM067 = 217;

    public const ushort BGM068 = 218;

    public const ushort BGM069 = 219;

    public const ushort BGM070 = 220;

    public const ushort BGM071 = 221;

    public const ushort BGM072 = 222;

    public const ushort BGM073 = 223;

    public const ushort BGM074 = 224;

    public const ushort BGM075 = 225;

    public const ushort BGM076 = 226;

    public const ushort BGM077 = 227;

    public const ushort BGM078 = 228;

    public const ushort BGM079 = 229;

    public const ushort BGM080 = 230;

    public const ushort BGM081 = 231;

    public const ushort BGM082 = 232;

    public const ushort BGM083 = 233;

    public const ushort BGM084 = 234;

    public const ushort BGM085 = 235;

    public const ushort BGM086 = 236;

    public const ushort BGM087 = 237;

    public const ushort BGM088 = 238;

    public const ushort BGM089 = 239;

    public const ushort BGM090 = 240;

    public const ushort BGM091 = 241;

    public const ushort BGM092 = 242;

    public const ushort BGM093 = 243;

    public const ushort BGM094 = 244;

    public const ushort BGM095 = 245;

    public const ushort BGM096 = 246;

    public const ushort BGM097 = 247;

    public const ushort BGM098 = 248;

    public const ushort BGM099 = 249;

    public const ushort BGM100 = 300;

    public const ushort BGM101 = 301;

    public const ushort BGM102 = 302;

    public const ushort BGM103 = 303;

    public const ushort BGM104 = 304;

    public const ushort BGM105 = 305;

    public const ushort BGM106 = 306;

    public const ushort BGM107 = 307;

    public const ushort BGM108 = 308;

    public const ushort BGM109 = 309;

    public const ushort BGM110 = 310;

    public const ushort BGM111 = 311;

    public const ushort BGM112 = 312;

    public const ushort BGM113 = 313;

    public const ushort BGM114 = 314;

    public const ushort BGM115 = 315;

    public const ushort BGM116 = 316;

    public const ushort BGM117 = 317;

    public const ushort BGM118 = 318;

    public const ushort BGM119 = 319;

    public const ushort BGM120 = 320;

    public const ushort BGM121 = 321;

    public const ushort BGM122 = 322;

    public const ushort BGM123 = 323;

    public const ushort BGM124 = 324;

    public const ushort BGM125 = 325;

    public const ushort BGM126 = 326;

    public const ushort BGM127 = 327;

    public const ushort BGM128 = 328;

    public const ushort BGM129 = 329;

    public const ushort BGM130 = 330;

    public const ushort BGM131 = 331;

    public const ushort BGM132 = 332;

    public const ushort BGM133 = 333;

    public const ushort BGM134 = 334;

    public const ushort BGM135 = 335;

    public const ushort BGM136 = 336;

    public const ushort BGM137 = 337;

    public const ushort BGM138 = 338;

    public const ushort BGM139 = 339;

    public const ushort BGM140 = 340;

    public const ushort BGM141 = 341;

    public const ushort BGM142 = 342;

    public const ushort BGM143 = 343;

    public const ushort BGM144 = 344;

    public const ushort BGM145 = 345;

    public const ushort BGM146 = 346;

    public const ushort BGM147 = 347;

    public const ushort BGM148 = 348;

    public const ushort BGM149 = 349;

    public const ushort BGM150 = 380;

    public const ushort BGM151 = 381;

    public const ushort BGM152 = 382;

    public const ushort BGM153 = 383;

    public const ushort BGM154 = 384;

    public const ushort BGM155 = 385;

    public const ushort BGM156 = 386;

    public const ushort BGM200 = 400;

    public const ushort BGM_CREDIT = 600;

    private IEnumerator enumerator_fade_SE_;

    public const ushort SE_OFFSET = 42;

    public const ushort SE000 = 42;

    public const ushort SE001 = 43;

    public const ushort SE002 = 44;

    public const ushort SE003 = 45;

    public const ushort SE004 = 46;

    public const ushort SE005 = 47;

    public const ushort SE006 = 48;

    public const ushort SE007 = 49;

    public const ushort SE008 = 50;

    public const ushort SE009 = 51;

    public const ushort SE00A = 52;

    public const ushort SE00B = 53;

    public const ushort SE00C = 54;

    public const ushort SE00D = 55;

    public const ushort SE00E = 56;

    public const ushort SE00F = 57;

    public const ushort SE010 = 58;

    public const ushort SE011 = 59;

    public const ushort SE012 = 60;

    public const ushort SE013 = 61;

    public const ushort SE014 = 62;

    public const ushort SE015 = 63;

    public const ushort SE016 = 64;

    public const ushort SE017 = 65;

    public const ushort SE018 = 66;

    public const ushort SE019 = 67;

    public const ushort SE01A = 68;

    public const ushort SE01B = 69;

    public const ushort SE01C = 70;

    public const ushort SE01D = 71;

    public const ushort SE01E = 72;

    public const ushort SE01F = 73;

    public const ushort SE020 = 74;

    public const ushort SE021 = 75;

    public const ushort SE022 = 76;

    public const ushort SE023 = 77;

    public const ushort SE024 = 78;

    public const ushort SE025 = 79;

    public const ushort SE026 = 80;

    public const ushort SE027 = 81;

    public const ushort SE028 = 82;

    public const ushort SE029 = 83;

    public const ushort SE02A = 84;

    public const ushort SE02B = 85;

    public const ushort SE02C = 86;

    public const ushort SE02D = 87;

    public const ushort SE02E = 88;

    public const ushort SE02F = 89;

    public const ushort SE030 = 90;

    public const ushort SE031 = 91;

    public const ushort SE032 = 92;

    public const ushort SE033 = 93;

    public const ushort SE034 = 94;

    public const ushort SE035 = 95;

    public const ushort SE036 = 96;

    public const ushort SE037 = 97;

    public const ushort SE038 = 98;

    public const ushort SE039 = 99;

    public const ushort SE03A = 100;

    public const ushort SE03B = 101;

    public const ushort SE03C = 102;

    public const ushort SE03D = 103;

    public const ushort SE03E = 104;

    public const ushort SE03F = 105;

    public const ushort SE040 = 106;

    public const ushort SE041 = 107;

    public const ushort SE042 = 108;

    public const ushort SE043 = 109;

    public const ushort SE044 = 110;

    public const ushort SE045 = 111;

    public const ushort SE046 = 112;

    public const ushort SE047 = 113;

    public const ushort SE048 = 114;

    public const ushort SE049 = 115;

    public const ushort SE04A = 116;

    public const ushort SE04B = 117;

    public const ushort SE04C = 118;

    public const ushort SE04D = 119;

    public const ushort SE04E = 120;

    public const ushort SE04F = 121;

    public const ushort SE050 = 122;

    public const ushort SE051 = 123;

    public const ushort SE052 = 124;

    public const ushort SE053 = 125;

    public const ushort SE054 = 126;

    public const ushort SE055 = 127;

    public const ushort SE056 = 128;

    public const ushort SE057 = 129;

    public const ushort SE058 = 130;

    public const ushort SE059 = 131;

    public const ushort SE05A = 132;

    public const ushort SE05B = 133;

    public const ushort SE05C = 134;

    public const ushort SE05D = 135;

    public const ushort SE05E = 136;

    public const ushort SE05F = 137;

    public const ushort SE060 = 138;

    public const ushort SE061 = 139;

    public const ushort SE062 = 140;

    public const ushort SE063 = 141;

    public const ushort SE064 = 142;

    public const ushort SE065 = 143;

    public const ushort SE066 = 144;

    public const ushort SE067 = 145;

    public const ushort SE068 = 146;

    public const ushort SE069 = 147;

    public const ushort SE06A = 148;

    public const ushort SE06B = 149;

    public const ushort SE06C = 150;

    public const ushort SE06D = 151;

    public const ushort SE06E = 152;

    public const ushort SE06F = 153;

    public const ushort SE070 = 154;

    public const ushort SE071 = 155;

    public const ushort SE072 = 156;

    public const ushort SE073 = 157;

    public const ushort SE074 = 158;

    public const ushort SE075 = 159;

    public const ushort SE076 = 160;

    public const ushort SE077 = 161;

    public const ushort SE078 = 162;

    public const ushort SE079 = 163;

    public const ushort SE07A = 164;

    public const ushort SE07B = 165;

    public const ushort SE07C = 166;

    public const ushort SE07D = 167;

    public const ushort SE07E = 168;

    public const ushort SE07F = 169;

    public const ushort SE080 = 170;

    public const ushort SE081 = 171;

    public const ushort SE082 = 172;

    public const ushort SE083 = 173;

    public const ushort SE084 = 174;

    public const ushort SE085 = 175;

    public const ushort SE086 = 176;

    public const ushort SE087 = 177;

    public const ushort SE088 = 178;

    public const ushort SE089 = 179;

    public const ushort SE08A = 180;

    public const ushort SE08B = 181;

    public const ushort SE08C = 182;

    public const ushort SE08D = 183;

    public const ushort SE08E = 184;

    public const ushort SE08F = 185;

    public const ushort SE090 = 350;

    public const ushort SE091 = 351;

    public const ushort SE092 = 352;

    public const ushort SE093 = 353;

    public const ushort SE094 = 354;

    public const ushort SE095 = 355;

    public const ushort SE096 = 356;

    public const ushort SE097 = 357;

    public const ushort SE098 = 358;

    public const ushort SE099 = 359;

    public const ushort SE09A = 360;

    public const ushort SE09B = 361;

    public const ushort SE09C = 362;

    public const ushort SE09D = 363;

    public const ushort SE09E = 364;

    public const ushort SE09F = 365;

    public const ushort SE0A0 = 366;

    public const ushort SE0A1 = 367;

    public const ushort SE0A2 = 368;

    public const ushort SE0A3 = 369;

    public const ushort SE0A4 = 370;

    public const ushort SE0A5 = 371;

    public const ushort SE0A6 = 372;

    public const ushort SE0A7 = 373;

    public const ushort SE0A8 = 374;

    public const ushort SE0A9 = 375;

    public const ushort SE0AA = 376;

    public const ushort SE0AB = 377;

    public const ushort SE_CURSOL = 42;

    public const ushort SE_DECIDE = 43;

    public const ushort SE_CANCEL = 44;

    public const ushort SE_ENTER = 47;

    public const ushort SE_NOTE_OPEN = 49;

    public const ushort SE100 = 500;

    public const ushort SE101 = 501;

    public const ushort SE102 = 502;

    public const ushort SE103 = 503;

    public const ushort SE104 = 504;

    public const ushort SE105 = 505;

    public const ushort SE106 = 506;

    public const ushort SE107 = 507;

    public const ushort SE108 = 508;

    public const ushort SE109 = 509;

    public const ushort SE10A = 510;

    public const ushort SE10B = 511;

    public const ushort SE10C = 512;

    public const ushort SE10D = 513;

    public const ushort GS1_SE0B6 = 406;

    public const ushort GS1_SE0B7 = 407;

    public const ushort GS1_SE0B8 = 408;

    public const ushort GS1_SE0B9 = 409;

    public const ushort GS1_SE0BA = 410;

    public const ushort GS1_SE0BB = 411;

    public const ushort GS1_SE0BC = 412;

    public const ushort GS1_SE0BD = 413;

    public const ushort GS1_SE0BE = 414;

    public const ushort GS1_SE0BF = 415;

    public const ushort GS1_SE0C0 = 416;

    public const ushort GS1_SE0C1 = 417;

    public const ushort GS1_SE0C2 = 418;

    public const ushort GS1_SE0C3 = 419;

    public const ushort GS1_SE0C4 = 420;

    public const ushort GS1_SE0C5 = 421;

    public const ushort GS1_SE0C6 = 422;

    public const ushort GS1_SE0C7 = 423;

    public const ushort GS1_SE0C8 = 424;

    public const ushort GS1_SE0C9 = 425;

    public const ushort GS1_SE0CA = 426;

    public const ushort GS1_SE0CB = 427;

    public const ushort GS1_SE0CC = 428;

    public const ushort GS1_SE0CD = 429;

    public const ushort GS1_SE0CE = 430;

    public const ushort GS1_SE0CF = 431;

    public const ushort GS1_SE0D0 = 432;

    public const ushort GS1_SE0D1 = 433;

    public const ushort GS1_SE0D2 = 434;

    public const ushort GS1_SE0D3 = 435;

    public const ushort GS1_SE0D4 = 436;

    public const ushort GS1_SE0D5 = 437;

    public const ushort GS1_SE0D6 = 438;

    public const ushort GS1_SE0D7 = 439;

    public const ushort GS1_SE0D8 = 440;

    public const ushort GS1_SE0D9 = 441;

    public const ushort GS1_SE0DA = 442;

    public const ushort GS1_SE0DB = 443;

    public const ushort GS1_SE0DC = 444;

    public const ushort GS1_SE0DD = 445;

    public const ushort GS1_SE0DE = 446;

    public const ushort GS1_SE0DF = 447;

    public const ushort GS1_SE0E0 = 448;

    public const ushort GS1_SE0E1 = 449;

    public const ushort GS1_SE0E2 = 450;

    public const ushort GS1_SE0E3 = 451;

    public const ushort GS1_SE0E4 = 452;

    public const ushort GS1_SE0E5 = 453;

    public const ushort GS1_SE0E6 = 454;

    public const ushort GS1_SE0E7 = 455;

    public const ushort GS1_SE0E8 = 456;

    public const ushort GS1_SE0E9 = 457;

    public const ushort GS1_SE0EA = 458;

    public const ushort GS1_SE0EB = 459;

    public const ushort GS1_SE0EC = 460;

    public const ushort GS1_SE0ED = 461;

    public const ushort GS1_SE0EE = 462;

    public const ushort GS1_SE0EF = 463;

    public const ushort GS1_SE0F0 = 464;

    public const ushort GS1_SE0F1 = 465;

    public const ushort GS1_SE0F2 = 466;

    public const ushort GS1_SE0F3 = 467;

    public const ushort GS1_SE0F4 = 468;

    public const ushort GS1_SE0F5 = 469;

    public const ushort GS1_SE0F6 = 470;

    public const ushort GS1_SE0F7 = 471;

    public const ushort GS1_SE0F8 = 472;

    public const ushort GS1_SE0F9 = 473;

    public const ushort GS1_SE0FA = 474;

    public const ushort GS1_SE0FB = 475;

    public const ushort GS1_SE0FC = 476;

    public const ushort GS1_SE0FD = 477;

    public const ushort GS1_SE0FE = 478;

    public const ushort GS1_SE0FF = 479;

    public const ushort GS3_SE0AC = 378;

    public const ushort GS3_SE0AD = 379;

    public const ushort GS3_SE0AE = 380;

    public const ushort GS3_SE0AF = 381;

    public const ushort GS3_SE0B0 = 382;

    public const ushort GS3_SE0B1 = 383;

    public const ushort GS3_SE0B2 = 384;

    public const ushort GS3_SE0B3 = 385;

    public const ushort GS3_SE0B4 = 386;

    public const ushort GS3_SE0B5 = 387;

    public const ushort GS3_SE0B6 = 388;

    public const ushort GS3_SE0B7 = 389;

    public const ushort GS3_SE0B8 = 390;

    public const ushort GS3_SE0B9 = 391;

    public const ushort GS3_SE0BA = 392;

    public const ushort GS3_SE0BB = 393;

    public const ushort GS3_SE0BC = 394;

    public const ushort GS3_SE0BD = 395;

    public const ushort GS3_SE0BE = 396;

    public const ushort GS3_SE0BF = 397;

    public const ushort GS3_SE0C0 = 398;

    public const ushort GS3_SE0C1 = 399;

    public const ushort GS3_SE0C2 = 400;

    public const ushort GS3_SE0C3 = 401;

    public const ushort GS3_SE0C4 = 402;

    public const ushort GS3_SE0C5 = 403;

    public const ushort GS3_SE0C6 = 404;

    public const ushort GS3_SE0C7 = 405;

    public const ushort GS3_SE0C8 = 406;

    public const ushort GS3_SE0C9 = 407;

    public const ushort GS3_SE0CA = 408;

    public const ushort GS3_SE0CB = 409;

    public const ushort GS3_SE0CC = 410;

    public const ushort GS3_SE0CD = 411;

    public const ushort GS3_SE0CE = 412;

    public const ushort GS3_SE0CF = 413;

    public static soundCtrl instance
    {
        get
        {
            return instance_;
        }
    }

    public int maxVolume
    {
        get
        {
            return max_volume;
        }
        set
        {
            max_volume = value;
        }
    }

    public int playBgmNo
    {
        get
        {
            return play_bgm_no;
        }
        set
        {
            play_bgm_no = value;
        }
    }

    public int stopBgmNo
    {
        get
        {
            return stop_bgm_no;
        }
        set
        {
            stop_bgm_no = value;
        }
    }

    public bool[] pause_bgm
    {
        get
        {
            return is_pause_bgm;
        }
        set
        {
            is_pause_bgm = value;
        }
    }

    public int[] se_no
    {
        get
        {
            return loop_se_no;
        }
    }

    public short bgm_volume
    {
        get
        {
            return (short)(list_BGM_[play_bgm_audio_index].volume * (float)max_volume);
        }
    }

    public byte[] GetTalkSEDataTable
    {
        get
        {
            switch (GSStatic.global_work_.title)
            {
                case TitleId.GS1:
                    return GS1_Talk_se_data_tbl;
                case TitleId.GS2:
                    return GS2_Talk_se_data_tbl;
                default:
                    return GS3_Talk_se_data_tbl;
            }
        }
    }

    private void Awake()
    {
        if (instance_ != null)
        {
            Debug.LogError(GetType().Name + " already exists.");
        }
        else
        {
            instance_ = this;
        }
    }

    public void init()
    {
        play_bgm_no = 254;
        stop_bgm_no = 254;
        loop_se_no[0] = 268435455;
        loop_se_no[0] = 268435455;
        loop_se_no[0] = 268435455;
        loop_se_no[0] = 268435455;
        loop_se_no[0] = 268435455;
        loop_se_no[0] = 268435455;
        loop_se_no[0] = 268435455;
        loop_se_no[0] = 268435455;
        loop_se_no[0] = 268435455;
        loop_se_no[0] = 268435455;
        play_bgm_audio_index = 0;
        current_volume = 1f;
        is_bgm_fade_ = BGMFadeState.FADE_NONE;
        is_pause_bgm[0] = false;
        is_pause_bgm[0] = false;
        is_pause_bgm[0] = false;
        max_volume = 256;
    }

    public void end()
    {
        AllStopBGM();
        AllStopSE();
        foreach (AssetBundleClip item in asset_SE_)
        {
            item.end();
        }
        foreach (AssetBundleClip item2 in asset_BGM_)
        {
            item2.end();
        }
        asset_SE_.Clear();
        asset_BGM_.Clear();
    }

    private AudioClip GetClipSE(int in_se_no, bool force = false)
    {
        AudioClip audioClip = null;
        soundData soundData2 = ((GSStatic.global_work_.title != TitleId.GS3 || in_se_no != 400) ? soundDataCtrl.get_sound_data(in_se_no) : soundDataCtrl.get_sound_data("se0c2"));
        if (soundData2 == null)
        {
            Debug.Log("GetClipSE Error!! " + in_se_no);
            return null;
        }
        AssetBundleClip clip = null;
        foreach (AssetBundleClip item in asset_SE_)
        {
            audioClip = ((GSStatic.global_work_.language != "JAPAN" && GSStatic.global_work_.language != "USA") ? item.clip(ReplaceLanguage.GetFileName(soundData2.path, soundData2.name)) : item.clip(soundData2.name));
            if (audioClip != null)
            {
                // Debug.Log("GetClipSE(" + in_se_no + ")");
                if (!force)
                    return audioClip;
                audioClip.UnloadAudioData();
                clip = item;
            }
        }

        asset_SE_.Remove(clip);
        AssetBundleClip assetBundleClip = new AssetBundleClip();
        assetBundleClip.load(soundData2.path, soundData2.name, force);
        asset_SE_.Add(assetBundleClip);
        if (GSStatic.global_work_.language == "JAPAN" || GSStatic.global_work_.language == "USA")
        {
            return assetBundleClip.clip(soundData2.name);
        }
        return assetBundleClip.clip(ReplaceLanguage.GetFileName(soundData2.path, soundData2.name));
    }

    private AudioClip GetClipBGM(int in_bgm_no)
    {
        AudioClip audioClip = null;
        soundData soundData2 = soundDataCtrl.get_sound_data(in_bgm_no);
        foreach (AssetBundleClip item in asset_BGM_)
        {
            audioClip = item.clip(soundData2.name);
            if (audioClip != null)
            {
                return audioClip;
            }
        }
        AssetBundleClip assetBundleClip = new AssetBundleClip();
        assetBundleClip.load(soundData2.path, soundData2.name, false);
        asset_BGM_.Add(assetBundleClip);
        return assetBundleClip.clip(soundData2.name);
    }

    public int GetSeNumber(string se_name)
    {
        return soundDataCtrl.get_sound_data(se_name).no;
    }

    public AudioClip GetAudioClipSE(int in_se_no, bool force = false)
    {
        int language = Language.languages.IndexOf(Language.langFallback[GSStatic.global_work_.language].ToUpper());
        switch (in_se_no)
        {
            case 81:
                in_se_no = (int)soundDataCtrl.se_change_tbl[0, language];
                break;
            case 71:
                in_se_no = (int)soundDataCtrl.se_change_tbl[1, language];
                break;
            case 55:
                in_se_no = (int)soundDataCtrl.se_change_tbl[2, language];
                break;
            case 56:
                in_se_no = (int)soundDataCtrl.se_change_tbl[3, language];
                break;
            case 367:
                in_se_no = (int)soundDataCtrl.se_change_tbl[4, language];
                break;
            case 368:
                in_se_no = (int)soundDataCtrl.se_change_tbl[5, language];
                break;
            case 57:
                in_se_no = (int)soundDataCtrl.se_change_tbl[6, language];
                break;
            case 369:
                in_se_no = (int)soundDataCtrl.se_change_tbl[7, language];
                break;
            case 370:
                in_se_no = (int)soundDataCtrl.se_change_tbl[8, language];
                break;
            case 371:
                in_se_no = (int)soundDataCtrl.se_change_tbl[9, language];
                break;
            case 150:
                in_se_no = (int)soundDataCtrl.se_change_tbl[10, language];
                break;
            case 372:
                in_se_no = (int)soundDataCtrl.se_change_tbl[11, language];
                break;
            case 65:
                in_se_no = (int)soundDataCtrl.se_change_tbl[12, language];
                break;
        }
        AudioClip clipSE = GetClipSE(in_se_no, force);
        return clipSE;
    }

    public void PlaySE(int in_se_no, bool in_multi = true, bool force = false)
    {
        AudioClip clipSE = GetAudioClipSE(in_se_no, force);
        if (clipSE == null)
        {
            Debug.LogWarning("----soundCtrl.PlaySE in_se_no:" + in_se_no);
            return;
        }
        if (GSStatic.global_work_.title == TitleId.GS1)
        {
            if (in_se_no == 91)
            {
                StopSE(90);
            }
            if (in_se_no == 92)
            {
                StopSE(in_se_no);
            }
        }
        soundData soundData2 = ((GSStatic.global_work_.title != TitleId.GS3 || in_se_no != 400) ? soundDataCtrl.get_sound_data(in_se_no) : soundDataCtrl.get_sound_data("se0c2"));
        if (!in_multi)
        {
            foreach (AudioSource item in list_SE_)
            {
                if (item.isPlaying && item.clip.name == soundData2.name)
                {
                    return;
                }
            }
        }
        if (soundData2.loop)
        {
            AudioSource audioSource = list_SE_.Find((AudioSource _) => _.isPlaying && _.loop);
            if (audioSource != null)
            {
                audioSource.Stop();
                loop_se_no[0] = 268435455;
            }
            if (enumerator_fade_SE_ != null)
            {
                coroutineCtrl.instance.Stop(enumerator_fade_SE_);
                enumerator_fade_SE_ = null;
            }
        }
        foreach (AudioSource item2 in list_SE_)
        {
            if (!item2.isPlaying)
            {
                item2.clip = clipSE;
                item2.loop = soundData2.loop;
                item2.volume = 1f * option_set_se_rate;
                item2.Play();
                if (soundData2.loop)
                {
                    loop_se_no[0] = in_se_no;
                }
                break;
            }
        }
    }

    public void PlayBGM(int in_bgm_no, int in_time = 0, bool in_multi = false)
    {
        bool flag = false;
        if (in_bgm_no == 255)
        {
            if (play_bgm_no == 254)
            {
                return;
            }
            flag = true;
            in_bgm_no = play_bgm_no;
        }
        AudioClip clipBGM = GetClipBGM(in_bgm_no);
        if (clipBGM == null)
        {
            Debug.LogWarning("----soundCtrl.PlayBGM in_bgm_no:" + in_bgm_no);
            return;
        }
        soundData soundData2 = soundDataCtrl.get_sound_data(in_bgm_no);
        if (!in_multi && list_BGM_[play_bgm_audio_index].isPlaying && play_bgm_no != 254 && in_bgm_no != play_bgm_no)
        {
            soundData soundData3 = soundDataCtrl.get_sound_data(play_bgm_no);
            StopBGM(soundData3.name);
        }
        for (int i = 0; i < 1; i++)
        {
            if (list_BGM_[i].isPlaying)
            {
                if (flag)
                {
                    if (list_BGM_[i].volume == current_volume * option_set_bgm_rate && is_bgm_fade_ == BGMFadeState.FADE_NONE)
                    {
                        list_BGM_[i].volume = 0f;
                        playFadeBGM(0f, current_volume * option_set_bgm_rate, in_time);
                        break;
                    }
                    if (enumerator_fade_BGM_ != null && is_bgm_fade_ == BGMFadeState.FADE_OUT)
                    {
                        coroutineCtrl.instance.Stop(enumerator_fade_BGM_);
                        enumerator_fade_BGM_ = null;
                        is_bgm_fade_ = BGMFadeState.FADE_NONE;
                        playFadeBGM(list_BGM_[i].volume, current_volume * option_set_bgm_rate, in_time);
                        break;
                    }
                    if (in_time == 0)
                    {
                        list_BGM_[i].Stop();
                        is_pause_bgm[i] = false;
                        list_BGM_[i].clip = clipBGM;
                        list_BGM_[i].loop = soundData2.loop;
                        list_BGM_[i].Play();
                        play_bgm_no = in_bgm_no;
                        play_bgm_audio_index = 0;
                        list_BGM_[i].volume = current_volume * option_set_bgm_rate;
                        break;
                    }
                    if (in_time > 0)
                    {
                        if (GSStatic.global_work_.title == TitleId.GS2 && GSStatic.global_work_.scenario == 21 && GSStatic.message_work_.now_no == scenario_GS2.SC3_27200)
                        {
                            is_pause_bgm[i] = false;
                            playFadeBGM(list_BGM_[i].volume, current_volume * option_set_bgm_rate, in_time);
                        }
                        break;
                    }
                }
                else
                {
                    if (in_bgm_no != play_bgm_no)
                    {
                        continue;
                    }
                    if (enumerator_fade_BGM_ != null && is_bgm_fade_ != 0)
                    {
                        coroutineCtrl.instance.Stop(enumerator_fade_BGM_);
                        enumerator_fade_BGM_ = null;
                        is_bgm_fade_ = BGMFadeState.FADE_NONE;
                        if (in_time > 0)
                        {
                            playFadeBGM(list_BGM_[i].volume, current_volume * option_set_bgm_rate, in_time);
                            break;
                        }
                        list_BGM_[i].Stop();
                        list_BGM_[i].Play();
                        list_BGM_[i].volume = current_volume * option_set_bgm_rate;
                    }
                    else
                    {
                        list_BGM_[i].volume = current_volume * option_set_bgm_rate;
                    }
                    break;
                }
                continue;
            }
            if (flag && is_pause_bgm[i])
            {
                is_pause_bgm[i] = false;
                list_BGM_[i].UnPause();
            }
            else
            {
                list_BGM_[i].Stop();
                is_pause_bgm[i] = false;
                list_BGM_[i].clip = clipBGM;
                list_BGM_[i].loop = soundData2.loop;
                list_BGM_[i].Play();
            }
            play_bgm_no = in_bgm_no;
            play_bgm_audio_index = 0;
            if (in_time > 0)
            {
                list_BGM_[i].volume = 0f;
                playFadeBGM(0f, current_volume * option_set_bgm_rate, in_time);
            }
            else
            {
                list_BGM_[i].volume = current_volume * option_set_bgm_rate;
            }
            break;
        }
    }

    public void ReplayBGM()
    {
        if (!list_BGM_[play_bgm_audio_index].isPlaying)
        {
            is_pause_bgm[play_bgm_audio_index] = false;
            list_BGM_[play_bgm_audio_index].UnPause();
        }
    }

    public void PauseBGM()
    {
        for (int i = 0; i < list_BGM_.Count; i++)
        {
            AudioSource audioSource = list_BGM_[i];
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
                is_pause_bgm[i] = true;
            }
        }
    }

    public void ReplayBGM(int in_bgm_no)
    {
        AudioSource audioSource = list_BGM_[in_bgm_no];
        if (!audioSource.isPlaying)
        {
            audioSource.UnPause();
            is_pause_bgm[in_bgm_no] = false;
        }
    }

    public void PauseBGM(int in_bgm_no)
    {
        AudioSource audioSource = list_BGM_[in_bgm_no];
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            is_pause_bgm[in_bgm_no] = true;
        }
    }

    public void VolumeChangeBGM(int in_volume, int in_time)
    {
        if (in_volume < 0)
        {
            in_volume = 0;
        }
        else if (in_volume > max_volume)
        {
            in_volume = max_volume;
        }
        GSStatic.global_work_.bgm_vol = (short)in_volume;
        if (enumerator_fade_BGM_ != null)
        {
            if (GSStatic.global_work_.title == TitleId.GS3 && GSStatic.global_work_.scenario == 20 && GSStatic.message_work_.now_no == scenario_GS3.SC4_3_55600)
            {
                return;
            }
            coroutineCtrl.instance.Stop(enumerator_fade_BGM_);
            enumerator_fade_BGM_ = null;
            is_bgm_fade_ = BGMFadeState.FADE_NONE;
        }
        if (in_time > 0)
        {
            float volume = list_BGM_[play_bgm_audio_index].volume;
            float end_volume = (float)in_volume / 256f * option_set_bgm_rate;
            playFadeBGM(volume, end_volume, in_time, false);
        }
        else
        {
            list_BGM_[play_bgm_audio_index].volume = (float)in_volume / 256f * option_set_bgm_rate;
        }
    }

    public void FadeOutBGM(int in_time)
    {
        GlobalWork global_work_ = GSStatic.global_work_;
        if (global_work_.bgm_vol != max_volume)
        {
            switch (global_work_.title)
            {
                case TitleId.GS1:
                    if (global_work_.scenario == 18 && GSStatic.message_work_.now_no == scenario.SC4_60510)
                    {
                        global_work_.bgm_vol = (short)max_volume;
                    }
                    break;
            }
        }
        if (list_BGM_[play_bgm_audio_index].isPlaying)
        {
            if (enumerator_fade_BGM_ != null)
            {
                coroutineCtrl.instance.Stop(enumerator_fade_BGM_);
                enumerator_fade_BGM_ = null;
                is_bgm_fade_ = BGMFadeState.FADE_NONE;
                playFadeBGM(list_BGM_[play_bgm_audio_index].volume * option_set_bgm_rate, 0f, in_time);
            }
            else
            {
                playFadeBGM(current_volume * option_set_bgm_rate, 0f, in_time);
            }
        }
        else
        {
            is_pause_bgm[play_bgm_audio_index] = true;
        }
    }

    private void playFadeBGM(float start_volume, float end_volume, int in_time, bool in_ispause = true)
    {
        enumerator_fade_BGM_ = CoroutineFadeBGM(start_volume, end_volume, in_time, play_bgm_audio_index, in_ispause);
        coroutineCtrl.instance.Play(enumerator_fade_BGM_);
    }

    private void stopFadeBGM()
    {
        if (enumerator_fade_BGM_ != null)
        {
            coroutineCtrl.instance.Stop(enumerator_fade_BGM_);
            enumerator_fade_BGM_ = null;
            is_bgm_fade_ = BGMFadeState.FADE_NONE;
        }
    }

    private IEnumerator CoroutineFadeBGM(float start_volume, float end_volume, int in_time, int audio_index, bool in_ispause = true)
    {
        if (start_volume < end_volume)
        {
            is_bgm_fade_ = BGMFadeState.FADE_IN;
        }
        else
        {
            is_bgm_fade_ = BGMFadeState.FADE_OUT;
        }
        float volume = start_volume;
        float speed = (end_volume - start_volume) / (float)in_time;
        while (true)
        {
            in_time--;
            if (in_time < 0)
            {
                break;
            }
            list_BGM_[audio_index].volume = volume;
            volume += speed;
            yield return null;
        }
        if (in_time > 0)
        {
            list_BGM_[audio_index].volume = volume;
        }
        else
        {
            list_BGM_[audio_index].volume = end_volume;
            if (end_volume <= 0f && in_ispause)
            {
                list_BGM_[audio_index].Pause();
                is_pause_bgm[audio_index] = true;
            }
        }
        is_bgm_fade_ = BGMFadeState.FADE_NONE;
        enumerator_fade_BGM_ = null;
    }

    public void StopSE(int in_se_no)
    {
        soundData soundData2 = soundDataCtrl.get_sound_data(in_se_no);
        loop_se_no[0] = 268435455;
        foreach (AudioSource item in list_SE_)
        {
            if (!item.isPlaying || !(item.clip.name == soundData2.name))
            {
                continue;
            }
            item.Stop();
            item.loop = false;
            break;
        }
    }

    public void FadeOutSE(int in_se_no, int fade_time)
    {
        string text = soundDataCtrl.get_sound_data(in_se_no).name;
        foreach (AudioSource item in list_SE_)
        {
            if (!item.isPlaying || !(item.clip.name == text))
            {
                continue;
            }
            bool loop = soundDataCtrl.get_sound_data(in_se_no).loop;
            if (GSStatic.global_work_.title == TitleId.GS3 && (GSStatic.global_work_.scenario == 10 || GSStatic.global_work_.scenario == 13 || GSStatic.global_work_.scenario == 21) && loop)
            {
                if (enumerator_fade_SE_ != null)
                {
                    coroutineCtrl.instance.Stop(enumerator_fade_SE_);
                    enumerator_fade_SE_ = null;
                }
                enumerator_fade_SE_ = FadeOutLoopSE(item, fade_time);
                coroutineCtrl.instance.Play(enumerator_fade_SE_);
            }
            else
            {
                coroutineCtrl.instance.Play(FadeOutSE(item, fade_time));
            }
        }
        if (loop_se_no[0] == in_se_no)
        {
            loop_se_no[0] = 268435455;
        }
    }

    private IEnumerator FadeOutSE(AudioSource audio, int fade_time)
    {
        float change_per_frame = audio.volume / (float)fade_time;
        for (int left = fade_time; left > 0; left--)
        {
            audio.volume -= change_per_frame;
            yield return null;
        }
        audio.Stop();
        audio.loop = false;
    }

    private IEnumerator FadeOutLoopSE(AudioSource audio, int fade_time)
    {
        float change_per_frame = audio.volume / (float)fade_time;
        for (int left = fade_time; left > 0; left--)
        {
            audio.volume -= change_per_frame;
            yield return null;
        }
        audio.Stop();
        audio.loop = false;
        enumerator_fade_SE_ = null;
    }

    public void StopBGM(int in_bgm_no)
    {
        soundData soundData2 = soundDataCtrl.get_sound_data(in_bgm_no);
        foreach (AudioSource item in list_BGM_)
        {
            if (item.clip == null || !item.isPlaying || !(item.clip.name == soundData2.name))
            {
                continue;
            }
            if (enumerator_fade_BGM_ != null)
            {
                coroutineCtrl.instance.Stop(enumerator_fade_BGM_);
                enumerator_fade_BGM_ = null;
                is_bgm_fade_ = BGMFadeState.FADE_NONE;
            }
            item.Stop();
            stop_bgm_no = play_bgm_no;
            play_bgm_no = 254;
            break;
        }
    }

    public void StopBGM(string name)
    {
        foreach (AudioSource item in list_BGM_)
        {
            if (item.clip == null || !item.isPlaying || !(item.clip.name == name))
            {
                continue;
            }
            if (enumerator_fade_BGM_ != null)
            {
                coroutineCtrl.instance.Stop(enumerator_fade_BGM_);
                enumerator_fade_BGM_ = null;
                is_bgm_fade_ = BGMFadeState.FADE_NONE;
            }
            item.Stop();
            stop_bgm_no = play_bgm_no;
            play_bgm_no = 254;
            break;
        }
    }

    public void AllStopSE()
    {
        foreach (AudioSource item in list_SE_)
        {
            item.Stop();
            item.loop = false;
        }
        loop_se_no[0] = 268435455;
    }

    public void AllStopBGM()
    {
        if (enumerator_fade_BGM_ != null)
        {
            coroutineCtrl.instance.Stop(enumerator_fade_BGM_);
            enumerator_fade_BGM_ = null;
            is_bgm_fade_ = BGMFadeState.FADE_NONE;
        }
        foreach (AudioSource item in list_BGM_)
        {
            item.Stop();
        }
        stop_bgm_no = play_bgm_no;
        play_bgm_no = 254;
        play_bgm_audio_index = 0;
    }

    public void AllStop()
    {
        AllStopSE();
        AllStopBGM();
    }

    public void SetLoopSEID(int id)
    {
        loop_se_no[0] = id;
    }

    public bool IsPlayingSE(int in_se_no)
    {
        AudioClip clipSE = GetClipSE(in_se_no);
        foreach (AudioSource item in list_SE_)
        {
            if (item.clip == clipSE)
            {
                return item.isPlaying;
            }
        }
        return false;
    }

    public float GetAudioClipLength(int in_se_no)
    {
        AudioClip clipSE = GetClipSE(in_se_no);
        return clipSE.length;
    }
}
