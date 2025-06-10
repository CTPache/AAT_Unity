using System;
using System.Collections.Generic;
using InControl;
using UnityEngine;
using UnityEngine.UI;

public class padCtrl : MonoBehaviour
{
	public enum StickType
	{
		Left = 0,
		Up = 1,
		Right = 2,
		Down = 3
	}

	public enum Stick
	{
		Pressed = 0,
		Down = 1,
		Up = 2
	}

	private static padCtrl instance_ = null;

	private bool[][] stick_l_ = new bool[4][]
	{
		new bool[3],
		new bool[3],
		new bool[3],
		new bool[3]
	};

	private bool[][] stick_r_ = new bool[4][]
	{
		new bool[3],
		new bool[3],
		new bool[3],
		new bool[3]
	};

	private bool[] trigger_left = new bool[3];

	private bool[] trigger_right = new bool[3];

	[SerializeField]
public NintendoSDKPad nintendo_pad_;

	[SerializeField]
public MenuTest debug_menu_;

	[SerializeField]
public Text text_;

	private Vector2 axis_pos_L_ = Vector2.zero;

	private Vector2 axis_pos_R_ = Vector2.zero;

	public const float stick_react_ = 0.3f;

	public bool off_;

	public bool wheel_repeat;

	public float wheel_val_;

	public float old_wheel_val_;

	private bool first_wheel_input;

	private bool first_left_stick_input;

	private bool first_right_stick_input;

	private int wait_time_ = 10;

	private int key_wait_;

	private const string input_key_vertical = "Vertical";

	private const string input_key_horizontal = "Horizontal";

	private const string input_key_vertical_r = "VerticalR";

	private const string input_key_horizontal_r = "HorizontalR";

	private const string input_key_R2 = "R2";

	private const string input_key_L2 = "L2";

	public static List<KeyCode> configuration_key_list_ = new List<KeyCode>
	{
		KeyCode.Return,
		KeyCode.Backspace,
		KeyCode.Tab,
		KeyCode.Clear,
		KeyCode.Escape,
		KeyCode.Space,
		KeyCode.Insert,
		KeyCode.Home,
		KeyCode.End,
		KeyCode.PageUp,
		KeyCode.PageDown,
		KeyCode.F1,
		KeyCode.F2,
		KeyCode.F3,
		KeyCode.F4,
		KeyCode.F5,
		KeyCode.F6,
		KeyCode.F7,
		KeyCode.F8,
		KeyCode.F9,
		KeyCode.F10,
		KeyCode.F11,
		KeyCode.F12,
		KeyCode.F13,
		KeyCode.F14,
		KeyCode.F15,
		KeyCode.A,
		KeyCode.B,
		KeyCode.C,
		KeyCode.D,
		KeyCode.E,
		KeyCode.F,
		KeyCode.G,
		KeyCode.H,
		KeyCode.I,
		KeyCode.J,
		KeyCode.K,
		KeyCode.L,
		KeyCode.M,
		KeyCode.N,
		KeyCode.O,
		KeyCode.P,
		KeyCode.Q,
		KeyCode.R,
		KeyCode.S,
		KeyCode.T,
		KeyCode.U,
		KeyCode.V,
		KeyCode.W,
		KeyCode.X,
		KeyCode.Y,
		KeyCode.Z,
		KeyCode.RightShift,
		KeyCode.LeftShift,
		KeyCode.RightControl,
		KeyCode.LeftControl,
		KeyCode.RightAlt,
		KeyCode.LeftAlt,
		KeyCode.Keypad0,
		KeyCode.Keypad1,
		KeyCode.Keypad2,
		KeyCode.Keypad3,
		KeyCode.Keypad4,
		KeyCode.Keypad5,
		KeyCode.Keypad6,
		KeyCode.Keypad7,
		KeyCode.Keypad8,
		KeyCode.Keypad9,
		KeyCode.UpArrow,
		KeyCode.DownArrow,
		KeyCode.RightArrow,
		KeyCode.LeftArrow,
		KeyCode.Alpha0,
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8,
		KeyCode.Alpha9
	};

	public static long[][] default_key_type_ = new long[31][]
	{
		new long[2],
		new long[2] { 13L, 0L },
		new long[2] { 8L, 0L },
		new long[2] { 101L, 0L },
		new long[2] { 106L, 0L },
		new long[2],
		new long[2],
		new long[2] { 113L, 0L },
		new long[2] { 9L, 0L },
		new long[2],
		new long[2],
		new long[2] { 27L, 0L },
		new long[2],
		new long[2] { 276L, 0L },
		new long[2] { 273L, 0L },
		new long[2] { 275L, 0L },
		new long[2] { 274L, 0L },
		new long[2],
		new long[2],
		new long[2],
		new long[2],
		new long[2] { 98L, 0L },
		new long[2] { 104L, 0L },
		new long[2] { 109L, 0L },
		new long[2] { 110L, 0L },
		new long[2],
		new long[2],
		new long[2],
		new long[2],
		new long[2] { 114L, 0L },
		new long[2] { 0L, 127L }
	};

	public long[][] key_type_ = new long[31][]
	{
		new long[2],
		new long[2] { 13L, 0L },
		new long[2] { 8L, 0L },
		new long[2] { 101L, 0L },
		new long[2] { 106L, 0L },
		new long[2],
		new long[2],
		new long[2] { 113L, 0L },
		new long[2] { 9L, 0L },
		new long[2],
		new long[2],
		new long[2] { 27L, 0L },
		new long[2],
		new long[2] { 276L, 0L },
		new long[2] { 273L, 0L },
		new long[2] { 275L, 0L },
		new long[2] { 274L, 0L },
		new long[2],
		new long[2],
		new long[2],
		new long[2],
		new long[2] { 98L, 0L },
		new long[2] { 104L, 0L },
		new long[2] { 109L, 0L },
		new long[2] { 110L, 0L },
		new long[2],
		new long[2],
		new long[2],
		new long[2],
		new long[2] { 114L, 0L },
		new long[2] { 0L, 127L }
	};

	public static padCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public NintendoSDKPad nintendo_pad
	{
		get
		{
			return nintendo_pad_;
		}
	}

	public Vector3 touch_pos
	{
		get
		{
			return Input.mousePosition;
		}
	}

	public Text text
	{
		get
		{
			return text_;
		}
	}

	public Vector2 axis_pos_L
	{
		get
		{
			return axis_pos_L_;
		}
	}

	public Vector2 axis_pos_R
	{
		get
		{
			return axis_pos_R_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	private void FixedUpdate()
	{
		Process();
	}

	private void Process()
	{
		if (key_wait_ > 0)
		{
			key_wait_--;
		}
		if (InputCtrl.instance.GetKey(KeyCode.Numlock))
		{
			Input.ResetInputAxes();
		}
		if (InputAnyKeyPad())
		{
			keyGuideBase.UpdateKeyPadGuid(true);
		}
		else if (InputAnyKeyBord())
		{
			keyGuideBase.UpdateKeyPadGuid(false);
		}
		InputAnyLeftStick();
		InputAnyRightStick();
		StickUpdate();
		if (GetLeftStick(KeyType.StickL_Left))
		{
			if (!stick_l_[0][0])
			{
				stick_l_[0][1] = true;
			}
			else
			{
				stick_l_[0][1] = false;
			}
			stick_l_[0][0] = true;
		}
		else
		{
			if (stick_l_[0][0])
			{
				stick_l_[0][2] = true;
			}
			else
			{
				stick_l_[0][2] = false;
			}
			stick_l_[0][0] = false;
			stick_l_[0][1] = false;
		}
		if (GetLeftStick(KeyType.StickL_Up))
		{
			if (!stick_l_[1][0])
			{
				stick_l_[1][1] = true;
			}
			else
			{
				stick_l_[1][1] = false;
			}
			stick_l_[1][0] = true;
		}
		else
		{
			if (stick_l_[1][0])
			{
				stick_l_[1][2] = true;
			}
			else
			{
				stick_l_[1][2] = false;
			}
			stick_l_[1][0] = false;
			stick_l_[1][1] = false;
		}
		if (GetLeftStick(KeyType.StickL_Right))
		{
			if (!stick_l_[2][0])
			{
				stick_l_[2][1] = true;
			}
			else
			{
				stick_l_[2][1] = false;
			}
			stick_l_[2][0] = true;
		}
		else
		{
			if (stick_l_[2][0])
			{
				stick_l_[2][2] = true;
			}
			else
			{
				stick_l_[2][2] = false;
			}
			stick_l_[2][0] = false;
			stick_l_[2][1] = false;
		}
		if (GetLeftStick(KeyType.StickL_Down))
		{
			if (!stick_l_[3][0])
			{
				stick_l_[3][1] = true;
			}
			else
			{
				stick_l_[3][1] = false;
			}
			stick_l_[3][0] = true;
		}
		else
		{
			if (stick_l_[3][0])
			{
				stick_l_[3][2] = true;
			}
			else
			{
				stick_l_[3][2] = false;
			}
			stick_l_[3][0] = false;
			stick_l_[3][1] = false;
		}
		if (GetRightStick(KeyType.StickR_Left))
		{
			if (!stick_r_[0][0])
			{
				stick_r_[0][1] = true;
			}
			else
			{
				stick_r_[0][1] = false;
			}
			stick_r_[0][0] = true;
		}
		else
		{
			if (stick_r_[0][0])
			{
				stick_r_[0][2] = true;
			}
			else
			{
				stick_r_[0][2] = false;
			}
			stick_r_[0][0] = false;
			stick_r_[0][1] = false;
		}
		if (GetRightStick(KeyType.StickR_Up))
		{
			if (!stick_r_[1][0])
			{
				stick_r_[1][1] = true;
			}
			else
			{
				stick_r_[1][1] = false;
			}
			stick_r_[1][0] = true;
		}
		else
		{
			if (stick_r_[1][0])
			{
				stick_r_[1][2] = true;
			}
			else
			{
				stick_r_[1][2] = false;
			}
			stick_r_[1][0] = false;
			stick_r_[1][1] = false;
		}
		if (GetRightStick(KeyType.StickR_Right))
		{
			if (!stick_r_[2][0])
			{
				stick_r_[2][1] = true;
			}
			else
			{
				stick_r_[2][1] = false;
			}
			stick_r_[2][0] = true;
		}
		else
		{
			if (stick_r_[2][0])
			{
				stick_r_[2][2] = true;
			}
			else
			{
				stick_r_[2][2] = false;
			}
			stick_r_[2][0] = false;
			stick_r_[2][1] = false;
		}
		if (GetRightStick(KeyType.StickR_Down))
		{
			if (!stick_r_[3][0])
			{
				stick_r_[3][1] = true;
			}
			else
			{
				stick_r_[3][1] = false;
			}
			stick_r_[3][0] = true;
		}
		else
		{
			if (stick_r_[3][0])
			{
				stick_r_[3][2] = true;
			}
			else
			{
				stick_r_[3][2] = false;
			}
			stick_r_[3][0] = false;
			stick_r_[3][1] = false;
		}
		if (GetTriggerRight(KeyType.ZR))
		{
			if (!trigger_right[0])
			{
				trigger_right[1] = true;
			}
			else
			{
				trigger_right[1] = false;
			}
			trigger_right[0] = true;
		}
		else
		{
			if (trigger_right[0])
			{
				trigger_right[2] = true;
			}
			else
			{
				trigger_right[2] = false;
			}
			trigger_right[0] = false;
			trigger_right[1] = false;
		}
		if (GetTriggerLeft(KeyType.ZL))
		{
			if (!trigger_left[0])
			{
				trigger_left[1] = true;
			}
			else
			{
				trigger_left[1] = false;
			}
			trigger_left[0] = true;
			return;
		}
		if (trigger_left[0])
		{
			trigger_left[2] = true;
		}
		else
		{
			trigger_left[2] = false;
		}
		trigger_left[0] = false;
		trigger_left[1] = false;
	}

	private void StickUpdate()
	{
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (activeDevice.IsAttached && !WindowAPI.is_mini)
		{
			axis_pos_L_ = new Vector2(activeDevice.LeftStick.X, activeDevice.LeftStick.Y);
			axis_pos_R_ = new Vector2(activeDevice.RightStick.X, activeDevice.RightStick.Y);
		}
	}

	public bool GetLeftStick(KeyType type)
	{
		bool result = false;
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (activeDevice.IsAttached && !WindowAPI.is_mini)
		{
			if (type == KeyType.StickL_Left && activeDevice.LeftStick.X < -0.3f)
			{
				result = true;
			}
			if (type == KeyType.StickL_Right && activeDevice.LeftStick.X > 0.3f)
			{
				result = true;
			}
			if (type == KeyType.StickL_Up && activeDevice.LeftStick.Y > 0.3f)
			{
				result = true;
			}
			if (type == KeyType.StickL_Down && activeDevice.LeftStick.Y < -0.3f)
			{
				result = true;
			}
		}
		return result;
	}

	public bool GetRightStick(KeyType type)
	{
		bool result = false;
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (activeDevice.IsAttached && !WindowAPI.is_mini)
		{
			if (type == KeyType.StickR_Left && activeDevice.RightStick.X < -0.3f)
			{
				result = true;
			}
			if (type == KeyType.StickR_Right && activeDevice.RightStick.X > 0.3f)
			{
				result = true;
			}
			if (type == KeyType.StickR_Up && activeDevice.RightStick.Y > 0.3f)
			{
				result = true;
			}
			if (type == KeyType.StickR_Down && activeDevice.RightStick.Y < -0.3f)
			{
				result = true;
			}
		}
		return result;
	}

	public bool GetTriggerRight(KeyType type)
	{
		bool result = false;
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (activeDevice.IsAttached && !WindowAPI.is_mini && type == KeyType.ZR && (float)activeDevice.RightTrigger > 0.3f)
		{
			result = true;
		}
		return result;
	}

	public bool GetTriggerLeft(KeyType type)
	{
		bool result = false;
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (activeDevice.IsAttached && !WindowAPI.is_mini && type == KeyType.ZL && (float)activeDevice.LeftTrigger > 0.3f)
		{
			result = true;
		}
		return result;
	}

	private bool GetKeyTypeTrigger(KeyType type, int mode)
	{
		switch (type)
		{
		case KeyType.ZL:
			if (trigger_left[mode])
			{
				return true;
			}
			break;
		case KeyType.ZR:
			if (trigger_right[mode])
			{
				return true;
			}
			break;
		}
		return false;
	}

	public bool GetStick(KeyType type, int mode, int ext)
	{
		bool result = false;
		if (type == KeyType.StickL_Left)
		{
			if (stick_l_[0][mode])
			{
				result = true;
			}
		}
		else if (type == KeyType.StickL_Up)
		{
			if (stick_l_[1][mode])
			{
				result = true;
			}
		}
		else if (type == KeyType.StickL_Right)
		{
			if (stick_l_[2][mode])
			{
				result = true;
			}
		}
		else if (type == KeyType.StickL_Down)
		{
			if (stick_l_[3][mode])
			{
				result = true;
			}
		}
		else if (type == KeyType.StickR_Left)
		{
			if (stick_r_[0][mode])
			{
				result = true;
			}
		}
		else if (type == KeyType.StickR_Up)
		{
			if (stick_r_[1][mode])
			{
				result = true;
			}
		}
		else if (type == KeyType.StickR_Right)
		{
			if (stick_r_[2][mode])
			{
				result = true;
			}
		}
		else if (type == KeyType.StickR_Down)
		{
			if (stick_r_[3][mode])
			{
				result = true;
			}
		}
		else if (type == KeyType.ZL)
		{
			if (trigger_left[mode])
			{
				result = true;
			}
		}
		else if (type == KeyType.ZR)
		{
			if (trigger_right[mode])
			{
				result = true;
			}
		}
		else if (type == KeyType.L && ext == 2)
		{
			if (trigger_left[mode])
			{
				result = true;
			}
		}
		else if (type == KeyType.R && ext == 2 && trigger_right[mode])
		{
			result = true;
		}
		return result;
	}

	public bool GetKey(KeyType type, int ext = 2, bool is_debug = true, KeyType is_controller_type = KeyType.None)
	{
		if (is_debug && debug_menu_.menu.IsOpen())
		{
			return false;
		}
		if (type == KeyType.None)
		{
			return false;
		}
		if ((int)type >= key_type_.Length)
		{
			Debug.Log("key_type error!! " + type);
			return false;
		}
		bool result = false;
		if (TouchSystem.GetTouched(type))
		{
			return true;
		}
		if (is_controller_type == KeyType.None)
		{
			if (GameingControllerChange_GetKey(type))
			{
				return true;
			}
			if (GetStick(type, 0, ext))
			{
				return true;
			}
		}
		else
		{
			if (GameingControllerChange_GetKey(is_controller_type))
			{
				return true;
			}
			if (GetStick(is_controller_type, 0, ext))
			{
				return true;
			}
		}
		for (int i = 0; i < ext; i++)
		{
			if (key_type_[(int)type][i] < 65536 && key_type_[(int)type][i] != 0 && ((int)key_type_[(int)type][i] != 306 || !InputCtrl.instance.GetKey(KeyCode.AltGr)) && InputCtrl.instance.GetKey((KeyCode)key_type_[(int)type][i]))
			{
				return true;
			}
		}
		return result;
	}

	public bool GetKeyDown(KeyType type, int ext = 2, bool is_debug = true, KeyType is_controller_type = KeyType.None)
	{
		if (key_wait_ > 0)
		{
			return false;
		}
		if (is_debug && debug_menu_.menu.IsOpen())
		{
			return false;
		}
		if (type == KeyType.None)
		{
			return false;
		}
		if ((int)type >= key_type_.Length)
		{
			Debug.Log("key_type error!! " + type);
			return false;
		}
		bool result = false;
		if (TouchSystem.GetDown(type))
		{
			return true;
		}
		if (TouchSystem.GetTouchDownQuestioning(type))
		{
			return true;
		}
		if (TouchSystem.GetTouch(type))
		{
			return true;
		}
		if (type == KeyType.B && InputGetMouseButtonDown(1))
		{
			return true;
		}
		if (is_controller_type == KeyType.None)
		{
			if (GameingControllerChange_GetKeyDown(type))
			{
				return true;
			}
			if (GetStick(type, 1, ext))
			{
				if (type == KeyType.StickL_Left || type == KeyType.StickL_Up || type == KeyType.StickL_Right || type == KeyType.StickL_Down || type == KeyType.StickR_Left || type == KeyType.StickR_Up || type == KeyType.StickR_Right || type == KeyType.StickR_Down)
				{
					key_wait_ = wait_time_;
				}
				return true;
			}
		}
		else
		{
			if (GameingControllerChange_GetKeyDown(is_controller_type))
			{
				return true;
			}
			if (GetStick(is_controller_type, 1, ext))
			{
				if (is_controller_type == KeyType.StickL_Left || is_controller_type == KeyType.StickL_Up || is_controller_type == KeyType.StickL_Right || is_controller_type == KeyType.StickL_Down || is_controller_type == KeyType.StickR_Left || is_controller_type == KeyType.StickR_Up || is_controller_type == KeyType.StickR_Right || is_controller_type == KeyType.StickR_Down)
				{
					key_wait_ = wait_time_;
				}
				return true;
			}
		}
		for (int i = 0; i < ext; i++)
		{
			if (type == KeyType.None)
			{
				continue;
			}
			long num = key_type_[(int)type][i];
			if (num < 65536 && ((int)num != 306 || !InputCtrl.instance.GetKeyDown(KeyCode.AltGr)) && InputCtrl.instance.GetKeyDown((KeyCode)num))
			{
				if ((int)num == 273 || (int)num == 274 || (int)num == 275 || (int)num == 276)
				{
					key_wait_ = wait_time_;
				}
				return true;
			}
		}
		return result;
	}

	public bool GetKeyUp(KeyType type, int ext = 2, bool is_debug = true, KeyType is_controller_type = KeyType.None)
	{
		if (is_debug && debug_menu_.menu.IsOpen())
		{
			return false;
		}
		if (type == KeyType.None)
		{
			return false;
		}
		if ((int)type >= key_type_.Length)
		{
			Debug.Log("key_type error!! " + type);
			return false;
		}
		bool result = false;
		if (is_controller_type == KeyType.None)
		{
			if (GameingControllerChange_GetKeyUp(type))
			{
				return true;
			}
			if (GetStick(type, 2, ext))
			{
				return true;
			}
		}
		else
		{
			if (GameingControllerChange_GetKeyUp(is_controller_type))
			{
				return true;
			}
			if (GetStick(is_controller_type, 2, ext))
			{
				return true;
			}
		}
		for (int i = 0; i < ext; i++)
		{
			if (key_type_[(int)type][i] < 65536 && key_type_[(int)type][i] != 0 && ((int)key_type_[(int)type][i] != 306 || !InputCtrl.instance.GetKeyUp(KeyCode.AltGr)) && InputCtrl.instance.GetKeyUp((KeyCode)key_type_[(int)type][i]))
			{
				return true;
			}
		}
		return result;
	}

	public void vibration_play()
	{
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (activeDevice.IsAttached)
		{
			activeDevice.Vibrate(1f);
		}
	}

	public void vibration_stop()
	{
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (activeDevice.IsAttached)
		{
			activeDevice.StopVibration();
		}
	}

	private bool GameingControllerChange_GetKeyUp(KeyType _type)
	{
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (activeDevice.IsAttached && !WindowAPI.is_mini)
		{
			if (_type == KeyType.Left && activeDevice.DPadLeft.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.Right && activeDevice.DPadRight.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.Up && activeDevice.DPadUp.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.Down && activeDevice.DPadDown.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.A && activeDevice.Action1.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.B && activeDevice.Action2.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.X && activeDevice.Action4.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.Y && activeDevice.Action3.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.StickL && activeDevice.LeftStickButton.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.StickR && activeDevice.RightStickButton.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.L && activeDevice.LeftBumper.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.R && activeDevice.RightBumper.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.ZL && activeDevice.LeftTrigger.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.ZR && activeDevice.RightTrigger.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.Start && activeDevice.GetControl(InputControlType.Start).WasPressed)
			{
				return true;
			}
			if (_type == KeyType.Start && activeDevice.GetControl(InputControlType.Options).WasPressed)
			{
				return true;
			}
		}
		return false;
	}

	private bool GameingControllerChange_GetKeyDown(KeyType _type)
	{
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (activeDevice.IsAttached && !WindowAPI.is_mini)
		{
			if (_type == KeyType.Left && activeDevice.DPadLeft.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.Right && activeDevice.DPadRight.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.Up && activeDevice.DPadUp.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.Down && activeDevice.DPadDown.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.A && activeDevice.Action1.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.B && activeDevice.Action2.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.X && activeDevice.Action4.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.Y && activeDevice.Action3.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.StickL && activeDevice.LeftStickButton.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.StickR && activeDevice.RightStickButton.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.L && activeDevice.LeftBumper.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.R && activeDevice.RightBumper.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.ZL && activeDevice.LeftTrigger.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.ZR && activeDevice.RightTrigger.WasPressed)
			{
				return true;
			}
			if (_type == KeyType.Start && activeDevice.GetControl(InputControlType.Start).WasPressed)
			{
				return true;
			}
			if (_type == KeyType.Start && activeDevice.GetControl(InputControlType.Options).WasPressed)
			{
				return true;
			}
		}
		return false;
	}

	private bool GameingControllerChange_GetKey(KeyType _type)
	{
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (activeDevice.IsAttached && !WindowAPI.is_mini)
		{
			if (_type == KeyType.Left && activeDevice.DPadLeft.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.Right && activeDevice.DPadRight.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.Up && activeDevice.DPadUp.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.Down && activeDevice.DPadDown.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.A && activeDevice.Action1.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.B && activeDevice.Action2.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.X && activeDevice.Action4.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.Y && activeDevice.Action3.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.StickL && activeDevice.LeftStickButton.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.StickR && activeDevice.RightStickButton.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.L && activeDevice.LeftBumper.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.R && activeDevice.RightBumper.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.ZL && activeDevice.LeftTrigger.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.ZR && activeDevice.RightTrigger.IsPressed)
			{
				return true;
			}
			if (_type == KeyType.Start && activeDevice.GetControl(InputControlType.Start).IsPressed)
			{
				return true;
			}
			if (_type == KeyType.Start && activeDevice.GetControl(InputControlType.Options).IsPressed)
			{
				return true;
			}
		}
		return false;
	}

	public bool InputAnyKeyPad()
	{
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (!activeDevice.IsAttached)
		{
			return false;
		}
		if (WindowAPI.is_mini)
		{
			return false;
		}
		if (activeDevice.AnyButtonWasPressed)
		{
			return true;
		}
		if (activeDevice.LeftTrigger.WasPressed || activeDevice.RightTrigger.WasPressed || activeDevice.LeftBumper.WasPressed || activeDevice.RightBumper.WasPressed)
		{
			return true;
		}
		if (activeDevice.DPadLeft.WasPressed)
		{
			return true;
		}
		if (activeDevice.DPadRight.WasPressed)
		{
			return true;
		}
		if (activeDevice.DPadUp.WasPressed)
		{
			return true;
		}
		if (activeDevice.DPadDown.WasPressed)
		{
			return true;
		}
		if (activeDevice.GetControl(InputControlType.Start).WasPressed)
		{
			return true;
		}
		if (activeDevice.GetControl(InputControlType.Options).WasPressed)
		{
			return true;
		}
		if (activeDevice.GetControl(InputControlType.Back).WasPressed)
		{
			return true;
		}
		if (activeDevice.GetControl(InputControlType.Share).WasPressed)
		{
			return true;
		}
		if (activeDevice.GetControl(InputControlType.TouchPadButton).WasPressed)
		{
			return true;
		}
		if (activeDevice.GetControl(InputControlType.LeftStickButton).WasPressed)
		{
			return true;
		}
		if (activeDevice.GetControl(InputControlType.RightStickButton).WasPressed)
		{
			return true;
		}
		return false;
	}

	public bool InputAnyKeyBord()
	{
		for (int i = 0; i < configuration_key_list_.Count; i++)
		{
			if (InputCtrl.instance.GetKeyDown(configuration_key_list_[i]))
			{
				return true;
			}
		}
		if (InputCtrl.instance.GetKeyDown(KeyCode.Return) || InputCtrl.instance.GetKeyDown(KeyCode.Backspace))
		{
			return true;
		}
		if (InputCtrl.instance.GetKeyDown(KeyCode.Mouse0) || InputCtrl.instance.GetKeyDown(KeyCode.Mouse1))
		{
			return true;
		}
		if (GetWheelMoveUp() || GetWheelMoveDown())
		{
			if (!first_wheel_input)
			{
				first_wheel_input = true;
				return true;
			}
			return false;
		}
		first_wheel_input = false;
		return false;
	}

	private void InputAnyLeftStick()
	{
		InputDevice activeDevice = InputManager.ActiveDevice;
		bool flag = false;
		if (activeDevice.IsAttached && !WindowAPI.is_mini)
		{
			if (activeDevice.LeftStick.X < -0.3f)
			{
				flag = true;
			}
			if (activeDevice.LeftStick.X > 0.3f)
			{
				flag = true;
			}
			if (activeDevice.LeftStick.Y > 0.3f)
			{
				flag = true;
			}
			if (activeDevice.LeftStick.Y < -0.3f)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			first_left_stick_input = false;
		}
		else if (!first_left_stick_input)
		{
			keyGuideBase.UpdateKeyPadGuid(true);
			first_left_stick_input = true;
		}
	}

	private void InputAnyRightStick()
	{
		InputDevice activeDevice = InputManager.ActiveDevice;
		bool flag = false;
		if (activeDevice.IsAttached && !WindowAPI.is_mini)
		{
			if (activeDevice.RightStick.X < -0.3f)
			{
				flag = true;
			}
			if (activeDevice.RightStick.X > 0.3f)
			{
				flag = true;
			}
			if (activeDevice.RightStick.Y > 0.3f)
			{
				flag = true;
			}
			if (activeDevice.RightStick.Y < -0.3f)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			first_right_stick_input = false;
		}
		else if (!first_right_stick_input)
		{
			keyGuideBase.UpdateKeyPadGuid(true);
			first_right_stick_input = true;
		}
	}

	public bool SetKeyEnable(KeyCode set_code)
	{
		if (!configuration_key_list_.Contains(set_code))
		{
			return false;
		}
		return true;
	}

	public bool SetKeyType(KeyType type, KeyCode set_code)
	{
		long num = (long)type;
		long num2 = (long)set_code;
		long num3 = key_type_[num][0];
		if (num3 != num2)
		{
			for (int i = 0; i < Enum.GetValues(typeof(KeyType)).Length; i++)
			{
				if (i != num && key_type_[i][0] == num2)
				{
					key_type_[i][0] = num3;
				}
			}
			key_type_[num][0] = num2;
		}
		return true;
	}

	public KeyCode GetKeyCode(KeyType type)
	{
		long num = (long)type;
		return (KeyCode)key_type_[num][0];
	}

	public KeyCode GetDefaultKeyCode(KeyType type)
	{
		long num = (long)type;
		return (KeyCode)default_key_type_[num][0];
	}

	public void SetKeyTypeRegion()
	{
		if (PS4DetectEnterButton.IsEnterButtonAssignCircle())
		{
			long num = key_type_[1][0];
			if (num != 331)
			{
				key_type_[1][0] = key_type_[2][0];
				key_type_[2][0] = num;
			}
		}
	}

	public bool GetWheelMoveUp()
	{
		return GetWheelVal() > 0f;
	}

	public bool GetWheelMoveDown()
	{
		return GetWheelVal() < 0f;
	}

	public void WheelMoveValUpdate()
	{
		old_wheel_val_ = GetWheelVal();
	}

	public bool IsNextMove()
	{
		return old_wheel_val_ == 0f;
	}

	private float GetWheelVal()
	{
		return Input.GetAxisRaw("Mouse ScrollWheel");
	}

	public bool InControlInputWasPressedAction1()
	{
		if (WindowAPI.is_mini)
		{
			return false;
		}
		return InputManager.ActiveDevice.Action1.WasPressed;
	}

	public bool InputAnyKey()
	{
		if (WindowAPI.is_mini)
		{
			return false;
		}
		return Input.anyKey;
	}

	public float InputGetAxis(string _axis_name)
	{
		if (WindowAPI.is_mini)
		{
			return 0f;
		}
		return Input.GetAxis(_axis_name);
	}

	public float InputGetAxisRaw(string _axis_name)
	{
		if (WindowAPI.is_mini)
		{
			return 0f;
		}
		return Input.GetAxisRaw(_axis_name);
	}

	public bool InputGetButtonDown(string _button_name)
	{
		if (WindowAPI.is_mini)
		{
			return false;
		}
		return Input.GetButtonDown(_button_name);
	}

	public bool InputGetKey(KeyCode _keycode)
	{
		if (WindowAPI.is_mini)
		{
			return false;
		}
		return InputCtrl.instance.GetKey(_keycode);
	}

	public bool InputGetKeyUp(KeyCode _keycode)
	{
		if (WindowAPI.is_mini)
		{
			return false;
		}
		return InputCtrl.instance.GetKeyUp(_keycode);
	}

	public bool InputGetKeyDown(KeyCode _keycode)
	{
		if (WindowAPI.is_mini)
		{
			return false;
		}
		return InputCtrl.instance.GetKeyDown(_keycode);
	}

	public bool InputGetMouseButton(int _button)
	{
		if (WindowAPI.is_mini)
		{
			return false;
		}
		return InputCtrl.instance.GetKey((_button != 0) ? KeyCode.Mouse1 : KeyCode.Mouse0);
	}

	public bool InputGetMouseButtonUp(int _button)
	{
		if (WindowAPI.is_mini)
		{
			return false;
		}
		return InputCtrl.instance.GetKeyUp((_button != 0) ? KeyCode.Mouse1 : KeyCode.Mouse0);
	}

	public bool InputGetMouseButtonDown(int _button)
	{
		if (WindowAPI.is_mini)
		{
			return false;
		}
		return InputCtrl.instance.GetKeyDown((_button != 0) ? KeyCode.Mouse1 : KeyCode.Mouse0);
	}

	public UnityEngine.Touch InputGetTouch(int _index)
	{
		return Input.GetTouch(_index);
	}

	public Vector3 InputMousePosition()
	{
		if (WindowAPI.is_mini)
		{
			return Vector3.zero;
		}
		return Input.mousePosition;
	}

	public void InputResetInputAxis()
	{
		Input.ResetInputAxes();
	}

	public int InputTouchCount()
	{
		return Input.touchCount;
	}
}
