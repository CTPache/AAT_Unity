using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
    [Serializable]
    public struct PresideData
    {
        public SystemData system_data_;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1000)]
        public GameData[] slot_list_;

        public static void New(out PresideData preside_data)
        {
            preside_data = default(PresideData);
            SystemData.New(out preside_data.system_data_);
            preside_data.slot_list_ = new GameData[1000];
            for (int i = 0; i < preside_data.slot_list_.Length; i++)
            {
                GameData.New(out preside_data.slot_list_[i]);
            }
        }

        internal void CopyToStatic()
        {
            system_data_.CopyToStatic();
            slot_list_.CopyTo(GSStatic.game_data_temp_, 0);
        }
    }
}
