using System.Collections.Generic;
using DebugMenu.uGUI;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class MenuTest : MonoBehaviour
{
	[SerializeField]
public Button button_;

	[SerializeField]
public DebugMenu.uGUI.Menu menu_;

	[SerializeField]
public NintendoSDKPad nintendo_pad_;

	[SerializeField]
public List<guideCtrl> l_guide_ = new List<guideCtrl>();

	[SerializeField]
public List<keyGuideCtrl> l_key_guide_ = new List<keyGuideCtrl>();

	[SerializeField]
public IndicateRevision revision_;

	[SerializeField]
public Text input_hisotry_type_;

	[SerializeField]
public Text show_save_status_;

	[SerializeField]
public GameObject debug_memory_;

	public static bool dont_open_;

	public static int debug_title_id = -1;

	public static int debug_story_id = -1;

	public static int debug_scenario_id = -1;

	public static bool debug_show_area;

	private int mdt_index;

	private bool show_key_history_;

	private static bool debug_show_save_status_ = false;

	private static List<string> list_save_status_str_ = new List<string>();

	private bool debug_show_memory_ = true;

	private bool asset_bundle_data_ = true;

	private int debug_rest_point_ = 10;

	private int debug_life_point_ = 10;

	private bool debug_no_damage_;

	private bool debug_instant_death_;

	private List<string> input_history_list_ = new List<string>();

	private List<string> midst_input_history_list_ = new List<string>();

	private List<string> frame_input_history = new List<string>();

	private Dictionary<KeyType, string> debug_key_word_map_ = new Dictionary<KeyType, string>();

	public DebugMenu.uGUI.Menu menu
	{
		get
		{
			return menu_;
		}
	}

	private void InputHistory()
	{
		for (int i = 0; i < midst_input_history_list_.Count; i++)
		{
			if (midst_input_history_list_[i] == "[")
			{
				int num = i + 1;
				if (midst_input_history_list_.Count > num && midst_input_history_list_[num] == "]")
				{
					midst_input_history_list_.RemoveAt(i);
					midst_input_history_list_.RemoveAt(i);
				}
			}
		}
		if (frame_input_history.Count > 0)
		{
			frame_input_history.Insert(0, "[");
			frame_input_history.Add("]");
			midst_input_history_list_.AddRange(frame_input_history);
		}
		input_hisotry_type_.text = string.Join(" ", midst_input_history_list_.ToArray());
	}

	private bool IsMenuOpenWithPad()
	{
		return false;
	}

	public void OnButtonClick()
	{
	}

	private void MenuOpen()
	{
	}

	private void MenuClose()
	{
	}

	public static bool DebugAdvStart()
	{
		return false;
	}
}
