using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SaveStruct
{
    [Serializable]
    public struct SaveSlotData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1000)]
        public SaveData[] save_data_;

        public static void New(out SaveSlotData slot_data)
        {
            slot_data = default(SaveSlotData);
            slot_data.save_data_ = new SaveData[Language.languages.Count * 10];
        }

        public void CopyFrom(global::SaveData[] src)
        {
            for (int i = 0; i < src.Length; i++)
            {
                save_data_[i].CopyFrom(ref src[i]);
            }
            for (int j = 0; j < Language.languages.Count; j++)
            {
                int languageSlotNum = GSUtility.GetLanguageSlotNum(j, GSStatic.save_slot_language_);
                if (languageSlotNum > 0)
                {
                    Debug.Log(Language.languages.Count+" languages are present.");
                    var a = GSStatic.save_data[j];
                    save_data_[languageSlotNum].CopyFrom(ref GSStatic.save_data[j]);
                }
            }
        }

        public void CopyTo(global::SaveData[] dest)
        {
            if (save_data_ == null)
            {
                save_data_ = new SaveData[Language.languages.Count * 10];
            }
            for (int i = 0; i < dest.Length; i++)
            {
                save_data_[i].CopyTo(ref dest[i]);
            }
            for (int j = 0; j < Language.languages.Count; j++)
            {
                int languageSlotNum = GSUtility.GetLanguageSlotNum(j, Language.languages[GSStatic.option_work.language_type]);
                save_data_[languageSlotNum].CopyTo(ref GSStatic.save_data[j]);
            }
        }
    }
}
