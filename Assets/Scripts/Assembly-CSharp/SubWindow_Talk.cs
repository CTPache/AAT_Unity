using UnityEngine;

public static class SubWindow_Talk
{
    static SubWindow_Talk()
    {
    }

    public static bool msg_lock_dsp(ushort mess)
    {
        for (ushort num = 0; num < GSStatic.global_work_.lock_max; num++)
        {
            if (GSStatic.global_work_.lockdat[num] == mess)
            {
                return true;
            }
        }
        return false;
    }

    public static void Proc(SubWindow sub_window)
    {
        Routine currentRoutine = sub_window.GetCurrentRoutine();
        Routine3d[] routine_3d = currentRoutine.routine_3d;
        TALK_DATA tALK_DATA = null;
        for (int i = 0; i < GSStatic.talk_work_.talk_data_.Length && GSStatic.talk_work_.talk_data_[i].room != 255; i++)
        {
            tALK_DATA = GSStatic.talk_work_.talk_data_[i];
            if (tALK_DATA.room == GSStatic.global_work_.Room && tALK_DATA.pl_id == AnimationSystem.Instance.IdlingCharacterMasked && tALK_DATA.sw == 1)
            {
                break;
            }
        }
        switch (currentRoutine.r.no_1)
        {
            case 0:
                talk_init(sub_window, tALK_DATA);
                break;
            case 1:
                talk_enter(sub_window, tALK_DATA);
                break;
            case 2:
                talk_main(sub_window, tALK_DATA);
                break;
            case 3:
                talk_accepted_exit(sub_window, tALK_DATA);
                break;
            case 4:
                talk_cancel_exit(sub_window, tALK_DATA);
                break;
            case 5:
                talk_enter_from_inventory(sub_window, tALK_DATA);
                break;
            case 6:
                talk_enter_from_short_message(sub_window, tALK_DATA);
                break;
            case 7:
                talk_exit_to_inventory(sub_window, tALK_DATA);
                break;
        }
        for (int j = 0; j < currentRoutine.r.no_3; j++)
        {
            if (routine_3d[j].disp_off == 0)
            {
            }
            if (currentRoutine.r.no_1 != 2 || !GSFlag.Check(2u, tALK_DATA.flag[j]) || msg_lock_dsp((ushort)tALK_DATA.mess[j]))
            {
            }
        }
    }

    private static void talk_init(SubWindow sub_window, TALK_DATA talk_data)
    {
        TanteiWork tantei_work_ = GSStatic.tantei_work_;
        Routine currentRoutine = sub_window.GetCurrentRoutine();
        Routine3d[] routine_3d = currentRoutine.routine_3d;
        switch (currentRoutine.r.no_2)
        {
            case 0:
                {
                    sub_window.bg_return_ = 1;
                    currentRoutine.r.no_3 = 0;
                    int num = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (talk_data.tag[i] != 255)
                        {
                            if (num < 4)
                            {
                                string text = advCtrl.instance.sel_data_.GetText((ushort)talk_data.tag[i]);
                                selectPlateCtrl.instance.setText(i, text);
                                selectPlateCtrl.instance.setRead(i, GSFlag.Check(2u, talk_data.flag[i]), msg_lock_dsp((ushort)talk_data.mess[i]));
                                num++;
                            }
                            else
                            {
                                Debug.Log("Talk Num Error!! " + num);
                            }
                        }
                    }
                    if (num > 0)
                    {
                        selectPlateCtrl.instance.entryCursor(num, selectPlateCtrl.FromEntryRequest.TALK);
                    }
                    selectPlateCtrl.instance.playCursor(1);
                    coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.TANTEI_TALK));
                    for (int j = 0; j < 6; j++)
                    {
                        routine_3d[j].Clear();
                    }
                    if (GSStatic.global_work_.psy_unlock_success != 0)
                    {
                        tantei_work_.sel_place = tantei_work_.def_place;
                        GSStatic.global_work_.psy_unlock_success = 0;
                        tantei_work_.def_place = 0;
                        selectPlateCtrl.instance.SetCursorNo(tantei_work_.sel_place);
                    }
                    else if (tantei_work_.def_place != 0)
                    {
                        tantei_work_.sel_place = tantei_work_.def_place;
                        tantei_work_.def_place = 0;
                    }
                    else
                    {
                        tantei_work_.sel_place = 0;
                    }
                    sub_window.SetObjDispFlag(8);
                    currentRoutine.r.no_2++;
                    break;
                }
            case 1:
                if (sub_window.CheckObjOut())
                {
                    currentRoutine.r.no_2++;
                }
                break;
            case 2:
                currentRoutine.r.no_2 = 0;
                currentRoutine.r.no_1++;
                break;
        }
    }

    private static void talk_enter(SubWindow sub_window, TALK_DATA talk_data)
    {
        Routine currentRoutine = sub_window.GetCurrentRoutine();
        Routine3d[] routine_3d = currentRoutine.routine_3d;
        switch (currentRoutine.r.no_2)
        {
            case 0:
                currentRoutine.r.no_2++;
                break;
            case 1:
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (routine_3d[j].timer == 0)
                        {
                            routine_3d[j].disp_off = 0;
                        }
                        else
                        {
                            routine_3d[j].timer--;
                        }
                    }
                    currentRoutine.r.no_2++;
                    break;
                }
            case 2:
                if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
                {
                    sub_window.busy_ = 0u;
                    sub_window.req_ = SubWindow.Req.NONE;
                    for (int i = 0; i < currentRoutine.tp_cnt; i++)
                    {
                    }
                    currentRoutine.tp_cnt = 0;
                    currentRoutine.r.no_2 = 0;
                    currentRoutine.r.no_1++;
                }
                break;
        }
    }

    private static void talk_main(SubWindow sub_window, TALK_DATA talk_data)
    {
        GlobalWork global_work_ = GSStatic.global_work_;
        Routine currentRoutine = sub_window.GetCurrentRoutine();
        if (selectPlateCtrl.instance.select_animation_playing)
        {
            return;
        }
        if (selectPlateCtrl.instance.is_end)
        {
            if (selectPlateCtrl.instance.is_cancel)
            {
                sub_window.busy_ = 3u;
                currentRoutine.r.no_2 = 0;
                currentRoutine.r.no_1 = 4;
            }
            else
            {
                sub_window.busy_ = 3u;
                currentRoutine.r.no_1++;
                currentRoutine.r.no_2 = 0;
            }
            coroutineCtrl.instance.Play(keyGuideCtrl.instance.close());
        }
        else if (GSStatic.global_work_.r.no_0 == 11)
        {
            if (sub_window.req_ == SubWindow.Req.SAVE)
            {
                for (int i = 0; i < currentRoutine.tp_cnt; i++)
                {
                }
                currentRoutine.tp_cnt = 0;
                sub_window.busy_ = 3u;
                currentRoutine.r.Set(8, 5, 0, currentRoutine.r.no_3);
                sub_window.stack_++;
                Routine currentRoutine2 = sub_window.GetCurrentRoutine();
                currentRoutine2.r.Set(15, 0, 0, 0);
                currentRoutine2.tex_no = (byte)(currentRoutine.tex_no + currentRoutine.r.no_3);
            }
        }
        else if (padCtrl.instance.GetKeyDown(KeyType.Start))
        {
            if ((global_work_.status_flag & 0x10) == 0)
            {
                global_work_.r_bk.CopyFrom(ref global_work_.r);
                global_work_.r.Set(17, 0, 0, 0);
                soundCtrl.instance.PlaySE(49);
            }
        }
        else if (padCtrl.instance.GetKeyDown(KeyType.R) && (global_work_.status_flag & 0x10) == 0)
        {
            for (int j = 0; j < currentRoutine.tp_cnt; j++)
            {
            }
            currentRoutine.tp_cnt = 0;
            sub_window.busy_ = 3u;
            currentRoutine.r.no_1 = 7;
            currentRoutine.r.no_2 = 0;
        }
    }

    private static void talk_accepted_exit(SubWindow sub_window, TALK_DATA talk_data)
    {
        GlobalWork global_work_ = GSStatic.global_work_;
        TanteiWork tantei_work_ = GSStatic.tantei_work_;
        Routine currentRoutine = sub_window.GetCurrentRoutine();
        Routine3d[] routine_3d = currentRoutine.routine_3d;
        switch (currentRoutine.r.no_2)
        {
            case 0:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        routine_3d[i].rotate[0] = 0;
                        routine_3d[i].timer = (byte)(i * 3);
                    }
                    routine_3d[tantei_work_.sel_place].timer = 0;
                    currentRoutine.r.no_2++;
                    break;
                }
            case 1:
                currentRoutine.r.no_2++;
                break;
            case 2:
                if (!bgCtrl.instance.is_slider)
                {
                    routine_3d[tantei_work_.sel_place].y = -48;
                    currentRoutine.r.no_2++;
                }
                break;
            case 3:
                {
                    if (!GSFlag.Check(2u, talk_data.flag[selectPlateCtrl.instance.cursor_no]))
                    {
                        GSFlag.Set(2u, talk_data.flag[selectPlateCtrl.instance.cursor_no], 1u);
                    }
                    messageBoardCtrl.instance.guide_ctrl.next_guid = guideCtrl.GuideType.HOUTEI;
                    uint num = talk_data.mess[selectPlateCtrl.instance.cursor_no];
                    advCtrl.instance.message_system_.SetMessage(num);
                    set_message_window(num);
                    global_work_.r.no_2 = 3;
                    global_work_.r.no_3 = 0;
                    currentRoutine.r.Set(8, 6, 0, 0);
                    sub_window.stack_++;
                    Routine currentRoutine2 = sub_window.GetCurrentRoutine();
                    currentRoutine2.r.Set(10, 0, 0, 0);
                    currentRoutine2.tex_no = currentRoutine.tex_no;
                    break;
                }
        }
    }

    private static void talk_cancel_exit(SubWindow sub_window, TALK_DATA talk_data)
    {
        GlobalWork global_work_ = GSStatic.global_work_;
        Routine currentRoutine = sub_window.GetCurrentRoutine();
        Routine3d[] routine_3d = currentRoutine.routine_3d;
        switch (currentRoutine.r.no_2)
        {
            case 0:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        routine_3d[i].rotate[0] = ushort.MaxValue;
                        routine_3d[i].timer = (byte)(i * 3);
                    }
                    currentRoutine.r.no_2++;
                    break;
                }
            case 1:
                currentRoutine.r.no_2++;
                break;
            case 2:
                if (sub_window.bg_return_ == 1)
                {
                    sub_window.bg_return_ = 0;
                }
                currentRoutine.r.no_2++;
                break;
            case 3:
                global_work_.r.Set(5, 1, 0, 0);
                sub_window.stack_--;
                break;
        }
    }

    private static void talk_enter_from_inventory(SubWindow sub_window, TALK_DATA talk_data)
    {
        TanteiWork tantei_work_ = GSStatic.tantei_work_;
        Routine currentRoutine = sub_window.GetCurrentRoutine();
        Routine3d[] routine_3d = currentRoutine.routine_3d;
        switch (currentRoutine.r.no_2)
        {
            case 0:
                {
                    currentRoutine.r.no_3 = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        routine_3d[k].rotate[0] = 21845;
                        routine_3d[k].disp_off = 0;
                        routine_3d[k].timer = (byte)((3 - k) * 3);
                    }
                    routine_3d[4].disp_off = 0;
                    sub_window.SetObjDispFlag(8);
                    sub_window.bar_req_ = SubWindow.BarReq.TALK;
                    tantei_work_.sel_place = 0;
                    selectPlateCtrl.instance.playCursor(1);
                    currentRoutine.r.no_2++;
                    break;
                }
            case 1:
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (routine_3d[j].timer == 0)
                        {
                            if (routine_3d[j].rotate[0] - 2048 > 0)
                            {
                                routine_3d[j].rotate[0] -= 2048;
                            }
                            else
                            {
                                routine_3d[j].rotate[0] = 0;
                            }
                        }
                        else
                        {
                            routine_3d[j].timer--;
                        }
                    }
                    if (routine_3d[0].flag == 0 && sub_window.CheckObjOut())
                    {
                        routine_3d[0].flag = 1;
                    }
                    if (routine_3d[0].rotate[0] == 0 && routine_3d[0].flag == 1)
                    {
                        routine_3d[0].flag = 0;
                        currentRoutine.r.no_2++;
                    }
                    break;
                }
            case 2:
                {
                    currentRoutine.r.Set(8, 2, 0, currentRoutine.r.no_3);
                    sub_window.busy_ = 0u;
                    sub_window.req_ = SubWindow.Req.NONE;
                    for (int i = 0; i < currentRoutine.tp_cnt; i++)
                    {
                    }
                    currentRoutine.tp_cnt = 0;
                    currentRoutine.r.no_1 = 2;
                    currentRoutine.r.no_2 = 0;
                    break;
                }
        }
    }

    private static void talk_enter_from_short_message(SubWindow sub_window, TALK_DATA talk_data)
    {
        TanteiWork tantei_work_ = GSStatic.tantei_work_;
        Routine currentRoutine = sub_window.GetCurrentRoutine();
        Routine3d[] routine_3d = currentRoutine.routine_3d;
        switch (currentRoutine.r.no_2)
        {
            case 0:
                {
                    if (!sub_window.CheckObjOut())
                    {
                        break;
                    }
                    sub_window.SetObjDispFlag(8);
                    sub_window.bar_req_ = SubWindow.BarReq.TALK;
                    currentRoutine.r.no_3 = 0;
                    int num = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        if (talk_data.tag[k] != 255)
                        {
                            if (num < 4)
                            {
                                string text = advCtrl.instance.sel_data_.GetText((ushort)talk_data.tag[k]);
                                selectPlateCtrl.instance.setText(k, text);
                                selectPlateCtrl.instance.setRead(k, GSFlag.Check(2u, talk_data.flag[k]), msg_lock_dsp((ushort)talk_data.mess[k]));
                                num++;
                            }
                            else
                            {
                                Debug.Log("Talk Num Error!! " + num);
                            }
                        }
                    }
                    if (num > 0)
                    {
                        selectPlateCtrl.instance.entryCursor(num, selectPlateCtrl.FromEntryRequest.TALK);
                    }
                    selectPlateCtrl.instance.playCursor(1);
                    coroutineCtrl.instance.Play(keyGuideCtrl.instance.open(keyGuideBase.Type.TANTEI_TALK));
                    for (int l = 0; l < 4; l++)
                    {
                        routine_3d[l].rotate[0] = 21845;
                        routine_3d[l].disp_off = 0;
                        routine_3d[l].timer = (byte)((3 - l) * 3);
                        routine_3d[l].pallet = 0;
                    }
                    routine_3d[4].disp_off = 0;
                    currentRoutine.r.no_2++;
                    break;
                }
            case 1:
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (routine_3d[j].timer == 0)
                        {
                            if (routine_3d[j].rotate[0] - 2048 > 0)
                            {
                                routine_3d[j].rotate[0] -= 2048;
                            }
                            else
                            {
                                routine_3d[j].rotate[0] = 0;
                            }
                        }
                        else
                        {
                            routine_3d[j].timer--;
                        }
                    }
                    if (routine_3d[0].rotate[0] != 0)
                    {
                        break;
                    }
                    if (GSStatic.global_work_.tanchiki_talk_selp == 1)
                    {
                        if (tantei_work_.def_place == 2)
                        {
                            tantei_work_.sel_place = tantei_work_.def_place;
                        }
                        else
                        {
                            tantei_work_.sel_place = 0;
                        }
                        tantei_work_.def_place = 0;
                    }
                    else
                    {
                        tantei_work_.sel_place = 0;
                    }
                    GSStatic.global_work_.tanchiki_talk_selp = 0;
                    currentRoutine.r.no_2++;
                    break;
                }
            case 2:
                if (sub_window.CheckObjIn() && sub_window.bar_req_ == SubWindow.BarReq.NONE)
                {
                    sub_window.busy_ = 0u;
                    sub_window.req_ = SubWindow.Req.NONE;
                    for (int i = 0; i < currentRoutine.tp_cnt; i++)
                    {
                    }
                    currentRoutine.tp_cnt = 0;
                    currentRoutine.r.no_2 = 0;
                    currentRoutine.r.no_1 = 2;
                }
                break;
        }
    }

    private static void talk_exit_to_inventory(SubWindow sub_window, TALK_DATA talk_data)
    {
        GlobalWork global_work_ = GSStatic.global_work_;
        Routine currentRoutine = sub_window.GetCurrentRoutine();
        Routine3d[] routine_3d = currentRoutine.routine_3d;
        switch (currentRoutine.r.no_2)
        {
            case 0:
                {
                    for (int j = 0; j < 4; j++)
                    {
                        routine_3d[j].rotate[0] = 0;
                        routine_3d[j].timer = (byte)(j * 3);
                    }
                    routine_3d[4].rotate[0] = 0;
                    currentRoutine.r.no_2++;
                    break;
                }
            case 1:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (routine_3d[i].timer == 0)
                        {
                            if (routine_3d[i].rotate[0] + 2048 < 21845)
                            {
                                routine_3d[i].rotate[0] += 2048;
                                continue;
                            }
                            routine_3d[i].rotate[0] = 21845;
                            routine_3d[i].disp_off = 1;
                        }
                        else
                        {
                            routine_3d[i].timer--;
                        }
                    }
                    if (routine_3d[3].rotate[0] == 21845)
                    {
                        currentRoutine.r.no_2++;
                    }
                    break;
                }
            case 2:
                {
                    selectPlateCtrl.instance.stopCursor();
                    selectPlateCtrl.instance.body_active = false;
                    selectPlateCtrl.instance.is_select = false;
                    selectPlateCtrl.instance.is_talk = false;
                    global_work_.r_bk.CopyFrom(ref global_work_.r);
                    global_work_.r.Set(8, 0, 0, 0);
                    currentRoutine.r.Set(8, 5, 0, currentRoutine.r.no_3);
                    sub_window.stack_++;
                    Routine currentRoutine2 = sub_window.GetCurrentRoutine();
                    currentRoutine2.r.Set(11, 0, 0, 0);
                    currentRoutine2.tex_no = (byte)(currentRoutine.tex_no + currentRoutine.r.no_3);
                    break;
                }
        }
    }

    private static void set_message_window(uint tmp)
    {
        switch (GSStatic.global_work_.title)
        {
            case TitleId.GS1:
                GS1_set_message_window(tmp);
                break;
            case TitleId.GS2:
                GS2_set_message_window(tmp);
                break;
            case TitleId.GS3:
                GS3_set_message_window(tmp);
                break;
        }
    }

    private static void GS1_set_message_window(uint tmp)
    {
        MessageSystem.Mess_window_set(5u);
    }

    private static void GS2_set_message_window(uint tmp)
    {
        switch (GSStatic.global_work_.scenario)
        {
            case 8:
                switch (tmp)
                {
                    case 157u:
                    case 158u:
                    case 174u:
                    case 177u:
                    case 201u:
                    case 202u:
                    case 220u:
                    case 221u:
                    case 222u:
                    case 236u:
                    case 237u:
                    case 256u:
                    case 261u:
                        MessageSystem.Mess_window_set(10u);
                        break;
                    default:
                        MessageSystem.Mess_window_set(5u);
                        break;
                }
                break;
            case 11:
                switch (tmp)
                {
                    case 159u:
                    case 178u:
                    case 179u:
                    case 180u:
                    case 184u:
                    case 185u:
                    case 207u:
                    case 223u:
                    case 227u:
                    case 247u:
                    case 249u:
                    case 261u:
                        MessageSystem.Mess_window_set(10u);
                        break;
                    default:
                        MessageSystem.Mess_window_set(5u);
                        break;
                }
                break;
            case 14:
                if (tmp == 139 || tmp == 162 || tmp == 179)
                {
                    MessageSystem.Mess_window_set(10u);
                }
                else
                {
                    MessageSystem.Mess_window_set(5u);
                }
                break;
            case 18:
                if (tmp == 239)
                {
                    MessageSystem.Mess_window_set(10u);
                }
                else
                {
                    MessageSystem.Mess_window_set(5u);
                }
                break;
            case 19:
                if (tmp == 270 || tmp == 177)
                {
                    MessageSystem.Mess_window_set(10u);
                }
                else
                {
                    MessageSystem.Mess_window_set(5u);
                }
                break;
            default:
                MessageSystem.Mess_window_set(5u);
                break;
        }
    }

    private static void GS3_set_message_window(uint tmp)
    {
        switch (GSStatic.global_work_.scenario)
        {
            case 2:
                switch (tmp)
                {
                    case 281u:
                    case 307u:
                    case 344u:
                    case 345u:
                    case 348u:
                    case 368u:
                        MessageSystem.Mess_window_set(10u);
                        break;
                    default:
                        MessageSystem.Mess_window_set(5u);
                        break;
                }
                break;
            case 4:
                switch (tmp)
                {
                    case 169u:
                    case 170u:
                    case 171u:
                    case 206u:
                    case 207u:
                    case 249u:
                    case 250u:
                    case 251u:
                    case 284u:
                    case 287u:
                        MessageSystem.Mess_window_set(10u);
                        break;
                    default:
                        MessageSystem.Mess_window_set(5u);
                        break;
                }
                break;
            case 7:
                switch (tmp)
                {
                    case 153u:
                    case 155u:
                    case 238u:
                    case 307u:
                    case 335u:
                    case 336u:
                    case 337u:
                    case 338u:
                        MessageSystem.Mess_window_set(10u);
                        break;
                    default:
                        MessageSystem.Mess_window_set(5u);
                        break;
                }
                break;
            case 9:
                if (tmp == 256 || tmp == 257 || tmp == 163 || tmp == 234 || tmp == 329)
                {
                    MessageSystem.Mess_window_set(10u);
                }
                else
                {
                    MessageSystem.Mess_window_set(5u);
                }
                break;
            case 14:
                if (tmp == 147)
                {
                    MessageSystem.Mess_window_set(10u);
                }
                else
                {
                    MessageSystem.Mess_window_set(5u);
                }
                break;
            case 18:
                if (tmp == 228)
                {
                    MessageSystem.Mess_window_set(10u);
                }
                else
                {
                    MessageSystem.Mess_window_set(5u);
                }
                break;
            case 19:
                if (tmp == 237)
                {
                    MessageSystem.Mess_window_set(10u);
                }
                else
                {
                    MessageSystem.Mess_window_set(5u);
                }
                break;
            default:
                MessageSystem.Mess_window_set(5u);
                break;
        }
    }
}
