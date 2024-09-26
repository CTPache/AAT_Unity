using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class objMoveCtrl : MonoBehaviour
{
    [StructLayout(LayoutKind.Explicit)]
    private struct POS_W
    {
        [FieldOffset(0)]
        public short l;

        [FieldOffset(0)]
        public hl b;
    }

    private struct hl
    {
        public sbyte l;

        public byte h;
    }

    private class obj_data
    {
        public int pos_x;

        public int pos_y;

        public string path;

        public string name;

        public obj_data(int in_pos_x, int in_pos_y, string in_path, string in_name)
        {
            pos_x = in_pos_x;
            pos_y = in_pos_y;
            path = in_path;
            name = in_name;
        }
    }

    public enum Gs1ObjMoveFoa
    {
        OBJ_ETC00B = 1,
        OBJ_ETC00A = 2,
        OBJ_ETC_00A_2 = 3,
        OBJ_ETC_012 = 4,
        OBJ_SPR_200 = 5,
        OBJ_SPR_201 = 6,
        OBJ_SPR_205_0 = 7,
        OBJ_SPR_205_1 = 8,
        OBJ_SPR_205_2 = 9,
        OBJ_ETC_00B_2 = 10,
        OBJ_ETC_00A_3 = 11,
        OBJ_ETC_00B0 = 12
    }

    public enum Gs2ObjMoveFoa
    {
        ETC_00B = 1,
        ETC_00A = 2,
        ETC_00A_2 = 3,
        ETC_012 = 4,
        ETC_00B_2 = 5,
        ETC_00A_3 = 6,
        ETC_00B_3 = 7,
        ETC_00A_4 = 8,
        ETC_00B0 = 9
    }

    public enum Gs3ObjMoveFoa
    {
        ETC_00B = 1,
        ETC_00A = 2,
        ETC_00A_2 = 3,
        ETC_012 = 4,
        ETC_00B_2 = 5,
        ETC_00A_3 = 6,
        ETC_00B_3 = 7,
        ETC_00A_4 = 8,
        ETC_00B0 = 9
    }

    private enum obj_index
    {
        balloon = 0,
        pine_1 = 0,
        pine_2 = 1,
        akudaikan = 2,
        tonosaman = 3,
        plant_1 = 4,
        plant_2 = 5
    }

    [SerializeField]
    private AssetBundleSprite[] obj_;

    private IEnumerator enumerator_balloon_;

    private uint random_seed_ = 3626417289u;

    private bool is_play_;

    private const int SPD_AKUDAIKAAN = 30;

    private const int SPD_TONOSAMAN = 60;

    private const int SPD_PINE = 15;

    private const int SPD_PLANT = 150;

    private const int OBJ_MOVE_OFFSET = 1;

    private List<obj_data> obj_data_list = new List<obj_data>
    {
        new obj_data(0, 0, "/GS1/etc/", "etc00b"),
        new obj_data(0, 0, "/GS1/etc/", "etc00a"),
        new obj_data(0, 0, "/GS1/etc/", "etc00a"),
        new obj_data(0, 0, "/GS1/etc/", "etc012"),
        new obj_data(-480, -245, "/GS1/etc/", "spr200"),
        new obj_data(768, -110, "/GS1/etc/", "spr201"),
        new obj_data(-840, -325, "/GS1/etc/", "spr205"),
        new obj_data(60, -325, "/GS1/etc/", "spr205"),
        new obj_data(-420, 0, "/GS1/BG/", "bg04a"),
        new obj_data(0, 0, "/GS1/etc/", "etc00b"),
        new obj_data(0, 0, "/GS1/etc/", "etc00a"),
        new obj_data(0, 0, "/GS1/etc/", "etc00b")
    };

    private List<obj_data> obj_data_list_gs2 = new List<obj_data>
    {
        new obj_data(0, 0, "/GS1/etc/", "etc00b"),
        new obj_data(0, 0, "/GS1/etc/", "etc00a"),
        new obj_data(0, 0, "/GS1/etc/", "etc00a"),
        new obj_data(0, 0, "/GS1/etc/", "etc012"),
        new obj_data(0, 0, "/GS1/etc/", "etc00b"),
        new obj_data(0, 0, "/GS1/etc/", "etc00a"),
        new obj_data(0, 0, "/GS1/etc/", "etc00b"),
        new obj_data(0, 0, "/GS1/etc/", "etc00a"),
        new obj_data(0, 0, "/GS1/etc/", "etc00b")
    };

    private List<obj_data> obj_data_list_gs3 = new List<obj_data>
    {
        new obj_data(0, 0, "/GS1/etc/", "etc00b"),
        new obj_data(0, 0, "/GS1/etc/", "etc00a"),
        new obj_data(0, 0, "/GS1/etc/", "etc00a"),
        new obj_data(0, 0, "/GS1/etc/", "etc012"),
        new obj_data(0, 0, "/GS1/etc/", "etc00b"),
        new obj_data(0, 0, "/GS1/etc/", "etc00a"),
        new obj_data(0, 0, "/GS1/etc/", "etc00b"),
        new obj_data(0, 0, "/GS1/etc/", "etc00a"),
        new obj_data(0, 0, "/GS1/etc/", "etc00b")
    };

    public static objMoveCtrl instance { get; private set; }

    public bool is_play
    {
        get
        {
            return is_play_;
        }
    }

    private static TitleId CurrentTitleId
    {
        get
        {
            return GSStatic.global_work_.title;
        }
    }

    public static int OBJ_MOVE_MAX
    {
        get
        {
            switch (CurrentTitleId)
            {
                default:
                    return Enum.GetNames(typeof(Gs1ObjMoveFoa)).Length;
                case TitleId.GS2:
                    return Enum.GetNames(typeof(Gs2ObjMoveFoa)).Length;
                case TitleId.GS3:
                    return Enum.GetNames(typeof(Gs3ObjMoveFoa)).Length;
            }
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void play(int in_obj_id)
    {
        stop(in_obj_id);
        if (IsBalloon(in_obj_id))
        {
            enumerator_balloon_ = ObjEtc00B(in_obj_id);
            coroutineCtrl.instance.Play(enumerator_balloon_);
        }
        else if (CurrentTitleId == TitleId.GS1)
        {
            if (in_obj_id == 9)
            {
                coroutineCtrl.instance.Play(ObjSpr205_2(in_obj_id));
            }
            else
            {
                coroutineCtrl.instance.Play(ObjSpr200(in_obj_id));
            }
        }
    }

    public void stop(int in_obj_id)
    {
        if (IsBalloon(in_obj_id))
        {
            if (enumerator_balloon_ != null)
            {
                coroutineCtrl.instance.Stop(enumerator_balloon_);
                enumerator_balloon_ = null;
                obj_[0].active = false;
                is_play_ = false;
            }
            return;
        }
        switch ((Gs1ObjMoveFoa)in_obj_id)
        {
            case Gs1ObjMoveFoa.OBJ_SPR_200:
                obj_[2].active = false;
                break;
            case Gs1ObjMoveFoa.OBJ_SPR_201:
                obj_[3].active = false;
                break;
            case Gs1ObjMoveFoa.OBJ_SPR_205_0:
                obj_[0].active = false;
                break;
            case Gs1ObjMoveFoa.OBJ_SPR_205_1:
                obj_[1].active = false;
                break;
            case Gs1ObjMoveFoa.OBJ_SPR_205_2:
                obj_[4].active = false;
                obj_[5].active = false;
                break;
        }
    }

    public void stopAll()
    {
        if (enumerator_balloon_ != null)
        {
            coroutineCtrl.instance.Stop(enumerator_balloon_);
            enumerator_balloon_ = null;
        }
        AssetBundleSprite[] array = obj_;
        foreach (AssetBundleSprite assetBundleSprite in array)
        {
            assetBundleSprite.active = false;
        }
        is_play_ = false;
    }

    private IEnumerator ObjEtc00B(int in_obj_id)
    {
        is_play_ = true;
        AssetBundleSprite obj = obj_[0];
        obj_data data;
        switch (CurrentTitleId)
        {
            default:
                data = obj_data_list[in_obj_id - 1];
                break;
            case TitleId.GS2:
                data = obj_data_list_gs2[in_obj_id - 1];
                break;
            case TitleId.GS3:
                data = obj_data_list_gs3[in_obj_id - 1];
                break;
        }
        string language = string.Empty;
        if (Language.langFallback[GSStatic.global_work_.language] == "USA")
        {
            language = "u";
        }
        obj.load(data.path, data.name + language, false, true);
        obj.transform.localPosition = Vector3.zero;
        obj.active = true;
        int time2 = 0;
        int dx2 = 0;
        int dy2 = 0;
        while (time2 < 30)
        {
            if (time2 % 3 == 0)
            {
                dx2 = (int)(((Rnd() & 3) + 1) * 11);
                dy2 = (int)(((Rnd() & 7) - 3) * 11);
                obj.transform.localPosition = new Vector3(dx2, dy2, 0f);
            }
            time2++;
            yield return null;
        }
        obj.transform.localPosition = Vector3.zero;
        time2 = 0;
        while (time2 < 30)
        {
            time2++;
            yield return null;
        }
        obj.active = false;
        is_play_ = false;
        enumerator_balloon_ = null;
    }

    private IEnumerator ObjSpr200(int in_obj_id)
    {
        AssetBundleSprite obj = null;
        Vector3 move = Vector3.zero;
        switch ((Gs1ObjMoveFoa)in_obj_id)
        {
            case Gs1ObjMoveFoa.OBJ_SPR_200:
                obj = obj_[2];
                move.x += 30f;
                break;
            case Gs1ObjMoveFoa.OBJ_SPR_201:
                obj = obj_[3];
                move.x += -60f;
                break;
            case Gs1ObjMoveFoa.OBJ_SPR_205_0:
                obj = obj_[0];
                move.x += 15f;
                break;
            case Gs1ObjMoveFoa.OBJ_SPR_205_1:
                obj = obj_[1];
                move.x += 15f;
                break;
        }
        obj_data data = obj_data_list[in_obj_id - 1];
        obj.load(data.path, data.name);
        obj.active = true;
        obj.transform.localPosition = new Vector3(obj_data_list[in_obj_id - 1].pos_x, obj_data_list[in_obj_id - 1].pos_y, 0f);
        move.x /= 20f;
        do
        {
            obj.transform.localPosition += move;
            yield return null;
        }
        while (!GSStatic.bg_save_data.bg_black);
        obj.active = false;
        do
        {
            yield return null;
        }
        while (GSStatic.bg_save_data.bg_black);
        obj.active = true;
        do
        {
            obj.transform.localPosition += move;
            yield return null;
        }
        while (!GSStatic.bg_save_data.bg_black);
        obj.active = false;
        obj.transform.localPosition = Vector3.zero;
    }

    private IEnumerator ObjSpr205_2(int in_obj_id)
    {
        AssetBundleSprite obj_0 = obj_[4];
        AssetBundleSprite obj_1 = obj_[5];
        obj_data data = obj_data_list[in_obj_id - 1];
        obj_0.load(data.path, data.name);
        obj_1.load(data.path, data.name);
        obj_0.active = true;
        obj_1.active = true;
        obj_0.transform.localPosition = new Vector3(obj_data_list[in_obj_id - 1].pos_x, obj_data_list[in_obj_id - 1].pos_y, 0f);
        obj_1.transform.localPosition = new Vector3(obj_data_list[in_obj_id - 1].pos_x + 1920, obj_data_list[in_obj_id - 1].pos_y, 0f);
        Vector3 move = Vector3.zero;
        move.x += 150f;
        move.x = move.x * -1f / 20f;
        do
        {
            obj_0.transform.localPosition += move;
            obj_1.transform.localPosition += move;
            plantLoop(obj_0);
            plantLoop(obj_1);
            yield return null;
        }
        while (!GSStatic.bg_save_data.bg_black);
        obj_0.active = false;
        obj_1.active = false;
        do
        {
            yield return null;
        }
        while (GSStatic.bg_save_data.bg_black);
        obj_0.active = true;
        obj_1.active = true;
        do
        {
            obj_0.transform.localPosition += move;
            obj_1.transform.localPosition += move;
            plantLoop(obj_0);
            plantLoop(obj_1);
            yield return null;
        }
        while (!GSStatic.bg_save_data.bg_black);
        obj_0.active = false;
        obj_1.active = false;
        obj_0.transform.localPosition = Vector3.zero;
        obj_1.transform.localPosition = Vector3.zero;
    }

    private void plantLoop(AssetBundleSprite obj)
    {
        if (obj.transform.localPosition.x < -1920f)
        {
            Vector3 localPosition = obj.transform.localPosition;
            localPosition.x *= -1f;
            obj.transform.localPosition = localPosition;
        }
    }

    private uint Rnd()
    {
        POS_W pOS_W = default(POS_W);
        POS_W pOS_W2 = default(POS_W);
        pOS_W.l = (short)random_seed_;
        pOS_W2.l = (short)(pOS_W.l * 3);
        //ref hl b = ref pOS_W.b;
        //b.l += (sbyte)pOS_W2.b.h;
        pOS_W.b.l += (sbyte)pOS_W2.b.h;
        pOS_W.b.h = pOS_W2.b.h;
        random_seed_ = (uint)pOS_W.l;
        return (uint)pOS_W.b.l;
    }

    public bool IsBalloon(int targetFoa)
    {
        switch (CurrentTitleId)
        {
            case TitleId.GS1:
                return targetFoa < 5 || 9 < targetFoa;
            case TitleId.GS2:
                return 1 <= targetFoa && targetFoa <= 9;
            case TitleId.GS3:
                return 1 <= targetFoa && targetFoa <= 9;
            default:
                return false;
        }
    }
}
