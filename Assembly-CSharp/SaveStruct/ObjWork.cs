using System;
using System.Runtime.InteropServices;

namespace SaveStruct
{
	[Serializable]
	public struct ObjWork
	{
		public const int ObjectsDataArraySize = 25;

		public ushort foa;

		public ushort idlingFOA;

		public byte h_num;

		public AnimationSystemSave system_data;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
		public AnimationObjectSave[] objects_data;

		public ChouState chou_state;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public ChouWork[] chou_work;

		private const int YobiBufferSize = 120;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 120)]
		public byte[] yobi_buffer;

		public static void New(out ObjWork obj_work)
		{
			obj_work = default(ObjWork);
		}

		public void CopyFrom()
		{
			foa = GSStatic.obj_work_[1].foa;
			idlingFOA = GSStatic.obj_work_[1].idlingFOA;
			h_num = GSStatic.obj_work_[1].h_num;
			AnimationSystem instance = AnimationSystem.Instance;
			system_data = instance.SystemDataToSave;
			objects_data = instance.ObjectStatusToSave;
			chou_state = default(ChouState);
			chou_state.choustateBK = GSStatic.message_work_.choustateBK;
			chou_state.choustate = GSStatic.message_work_.choustate;
			chou_state.chou_no = GSStatic.message_work_.chou_no;
			chou_state.chou_st = GSStatic.message_work_.chou_st;
			chou_state.chou_cnt = GSStatic.message_work_.chou_cnt;
			chou_state.buffer = new byte[12];
			chou_work = new ChouWork[3];
			for (int i = 0; i < 3; i++)
			{
				chou_work[i] = default(ChouWork);
				global::ChouWork chouWork = GSStatic.message_work_.chou[i];
				chou_work[i].x = chouWork.x;
				chou_work[i].y = chouWork.y;
				chou_work[i].flg = chouWork.flg;
				chou_work[i].num = chouWork.num;
				chou_work[i].work16 = chouWork.work16;
				chou_work[i].E = chouWork.E;
				chou_work[i].ax = chouWork.ax;
				chou_work[i].ay = chouWork.ay;
				chou_work[i].dx = chouWork.dx;
				chou_work[i].dy = chouWork.dy;
				chou_work[i].work = new byte[4];
				Array.Copy(chouWork.work, chou_work[i].work, 4);
				chou_work[i].buffer = new byte[8];
			}
			yobi_buffer = new byte[120];
		}

		public void CopyTo(global::ObjWork dest)
		{
			dest.foa = foa;
			dest.idlingFOA = idlingFOA;
			dest.h_num = h_num;
			dest.system_data = system_data;
			dest.objects_data = objects_data;
			GSStatic.message_work_.choustateBK = chou_state.choustateBK;
			GSStatic.message_work_.choustate = chou_state.choustate;
			GSStatic.message_work_.chou_no = chou_state.chou_no;
			GSStatic.message_work_.chou_st = chou_state.chou_st;
			GSStatic.message_work_.chou_cnt = chou_state.chou_cnt;
			for (int i = 0; i < 3; i++)
			{
				ChouWork chouWork = chou_work[i];
				global::ChouWork chouWork2 = GSStatic.message_work_.chou[i];
				chouWork2.x = chouWork.x;
				chouWork2.y = chouWork.y;
				chouWork2.flg = chouWork.flg;
				chouWork2.num = chouWork.num;
				chouWork2.work16 = chouWork.work16;
				chouWork2.E = chouWork.E;
				chouWork2.ax = chouWork.ax;
				chouWork2.ay = chouWork.ay;
				chouWork2.dx = chouWork.dx;
				chouWork2.dy = chouWork.dy;
				chouWork2.work = new byte[4];
				Array.Copy(chouWork.work, chouWork2.work, 4);
			}
		}
	}
}
