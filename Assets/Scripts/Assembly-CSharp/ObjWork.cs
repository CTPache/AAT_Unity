using System;
using SaveStruct;
using UnityEngine;

[Serializable]
public class ObjWork
{
	public ushort foa;

	public ushort idlingFOA;

	public byte h_num;

	public AnimationSystemSave system_data;

	public AnimationObjectSave[] objects_data;

	public void init()
	{
		foa = 0;
		idlingFOA = 0;
		h_num = 0;
		system_data.character_id = 0;
		system_data.idling_foa = 0;
		system_data.talking_foa = 0;
		system_data.playing_foa = 0;
		if (system_data.buffer != null)
		{
			for (int i = 0; i < system_data.buffer.Length; i++)
			{
				system_data.buffer[i] = 0;
			}
		}
		else
		{
			Debug.Log("system_data.buffer null");
		}
		if (objects_data != null)
		{
			for (int j = 0; j < objects_data.Length; j++)
			{
				objects_data[j].exists = false;
				objects_data[j].x = 0f;
				objects_data[j].y = 0f;
				objects_data[j].z = 0f;
				objects_data[j].foa = 0;
				objects_data[j].characterID = 0;
				objects_data[j].beFlag = 0;
				objects_data[j].sequence = 0;
				objects_data[j].framesFromStarted = 0;
				objects_data[j].framesInSequence = 0;
				objects_data[j].isFading = false;
				objects_data[j].isFadeIn = false;
				objects_data[j].fadeFrame = 0;
				objects_data[j].alpha = 0f;
				objects_data[j].monochrome_fadein = false;
				objects_data[j].monochrome_sw = 0;
				objects_data[j].monochrome_time = 0;
				objects_data[j].monochrome_speed = 0;
				if (objects_data[j].buffer != null)
				{
					for (int k = 0; k < objects_data[j].buffer.Length; k++)
					{
						objects_data[j].buffer[k] = 0;
					}
				}
				else
				{
					Debug.Log("objects_data[i].buffer null");
				}
			}
		}
		else
		{
			Debug.Log("objects_data null");
		}
	}
}
