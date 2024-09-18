using System;
using System.Collections.Generic;
using UnityEngine;

public class InputCtrl : MonoBehaviour
{
	[Serializable]
	public class inputData
	{
		public KeyCode code;

		public bool input_key;

		public bool input_up;

		public bool input_down;

		public bool key;

		public bool up;

		public bool down;

		public int wait_up;

		public int wait_down;

		public inputData(KeyCode in_code, bool in_key, bool in_up, bool in_down)
		{
			code = in_code;
			input_key = false;
			input_up = false;
			input_down = false;
			key = in_key;
			up = in_up;
			down = in_down;
			wait_up = 0;
			wait_down = 0;
		}
	}

	private static InputCtrl instance_;

	public List<inputData> input_key_ = new List<inputData>
	{
		new inputData(KeyCode.None, false, false, false),
		new inputData(KeyCode.Backspace, false, false, false),
		new inputData(KeyCode.Delete, false, false, false),
		new inputData(KeyCode.Tab, false, false, false),
		new inputData(KeyCode.Clear, false, false, false),
		new inputData(KeyCode.Return, false, false, false),
		new inputData(KeyCode.Pause, false, false, false),
		new inputData(KeyCode.Escape, false, false, false),
		new inputData(KeyCode.Space, false, false, false),
		new inputData(KeyCode.Keypad0, false, false, false),
		new inputData(KeyCode.Keypad1, false, false, false),
		new inputData(KeyCode.Keypad2, false, false, false),
		new inputData(KeyCode.Keypad3, false, false, false),
		new inputData(KeyCode.Keypad4, false, false, false),
		new inputData(KeyCode.Keypad5, false, false, false),
		new inputData(KeyCode.Keypad6, false, false, false),
		new inputData(KeyCode.Keypad7, false, false, false),
		new inputData(KeyCode.Keypad8, false, false, false),
		new inputData(KeyCode.Keypad9, false, false, false),
		new inputData(KeyCode.KeypadPeriod, false, false, false),
		new inputData(KeyCode.KeypadDivide, false, false, false),
		new inputData(KeyCode.KeypadMultiply, false, false, false),
		new inputData(KeyCode.KeypadMinus, false, false, false),
		new inputData(KeyCode.KeypadPlus, false, false, false),
		new inputData(KeyCode.KeypadEnter, false, false, false),
		new inputData(KeyCode.KeypadEquals, false, false, false),
		new inputData(KeyCode.UpArrow, false, false, false),
		new inputData(KeyCode.DownArrow, false, false, false),
		new inputData(KeyCode.RightArrow, false, false, false),
		new inputData(KeyCode.LeftArrow, false, false, false),
		new inputData(KeyCode.Insert, false, false, false),
		new inputData(KeyCode.Home, false, false, false),
		new inputData(KeyCode.End, false, false, false),
		new inputData(KeyCode.PageUp, false, false, false),
		new inputData(KeyCode.PageDown, false, false, false),
		new inputData(KeyCode.F1, false, false, false),
		new inputData(KeyCode.F2, false, false, false),
		new inputData(KeyCode.F3, false, false, false),
		new inputData(KeyCode.F4, false, false, false),
		new inputData(KeyCode.F5, false, false, false),
		new inputData(KeyCode.F6, false, false, false),
		new inputData(KeyCode.F7, false, false, false),
		new inputData(KeyCode.F8, false, false, false),
		new inputData(KeyCode.F9, false, false, false),
		new inputData(KeyCode.F10, false, false, false),
		new inputData(KeyCode.F11, false, false, false),
		new inputData(KeyCode.F12, false, false, false),
		new inputData(KeyCode.F13, false, false, false),
		new inputData(KeyCode.F14, false, false, false),
		new inputData(KeyCode.F15, false, false, false),
		new inputData(KeyCode.Alpha0, false, false, false),
		new inputData(KeyCode.Alpha1, false, false, false),
		new inputData(KeyCode.Alpha2, false, false, false),
		new inputData(KeyCode.Alpha3, false, false, false),
		new inputData(KeyCode.Alpha4, false, false, false),
		new inputData(KeyCode.Alpha5, false, false, false),
		new inputData(KeyCode.Alpha6, false, false, false),
		new inputData(KeyCode.Alpha7, false, false, false),
		new inputData(KeyCode.Alpha8, false, false, false),
		new inputData(KeyCode.Alpha9, false, false, false),
		new inputData(KeyCode.Exclaim, false, false, false),
		new inputData(KeyCode.DoubleQuote, false, false, false),
		new inputData(KeyCode.Hash, false, false, false),
		new inputData(KeyCode.Dollar, false, false, false),
		new inputData(KeyCode.Ampersand, false, false, false),
		new inputData(KeyCode.Quote, false, false, false),
		new inputData(KeyCode.LeftParen, false, false, false),
		new inputData(KeyCode.RightParen, false, false, false),
		new inputData(KeyCode.Asterisk, false, false, false),
		new inputData(KeyCode.Plus, false, false, false),
		new inputData(KeyCode.Comma, false, false, false),
		new inputData(KeyCode.Minus, false, false, false),
		new inputData(KeyCode.Period, false, false, false),
		new inputData(KeyCode.Slash, false, false, false),
		new inputData(KeyCode.Colon, false, false, false),
		new inputData(KeyCode.Semicolon, false, false, false),
		new inputData(KeyCode.Less, false, false, false),
		new inputData(KeyCode.Equals, false, false, false),
		new inputData(KeyCode.Greater, false, false, false),
		new inputData(KeyCode.Question, false, false, false),
		new inputData(KeyCode.At, false, false, false),
		new inputData(KeyCode.LeftBracket, false, false, false),
		new inputData(KeyCode.Backslash, false, false, false),
		new inputData(KeyCode.RightBracket, false, false, false),
		new inputData(KeyCode.Caret, false, false, false),
		new inputData(KeyCode.Underscore, false, false, false),
		new inputData(KeyCode.BackQuote, false, false, false),
		new inputData(KeyCode.A, false, false, false),
		new inputData(KeyCode.B, false, false, false),
		new inputData(KeyCode.C, false, false, false),
		new inputData(KeyCode.D, false, false, false),
		new inputData(KeyCode.E, false, false, false),
		new inputData(KeyCode.F, false, false, false),
		new inputData(KeyCode.G, false, false, false),
		new inputData(KeyCode.H, false, false, false),
		new inputData(KeyCode.I, false, false, false),
		new inputData(KeyCode.J, false, false, false),
		new inputData(KeyCode.K, false, false, false),
		new inputData(KeyCode.L, false, false, false),
		new inputData(KeyCode.M, false, false, false),
		new inputData(KeyCode.N, false, false, false),
		new inputData(KeyCode.O, false, false, false),
		new inputData(KeyCode.P, false, false, false),
		new inputData(KeyCode.Q, false, false, false),
		new inputData(KeyCode.R, false, false, false),
		new inputData(KeyCode.S, false, false, false),
		new inputData(KeyCode.T, false, false, false),
		new inputData(KeyCode.U, false, false, false),
		new inputData(KeyCode.V, false, false, false),
		new inputData(KeyCode.W, false, false, false),
		new inputData(KeyCode.X, false, false, false),
		new inputData(KeyCode.Y, false, false, false),
		new inputData(KeyCode.Z, false, false, false),
		new inputData(KeyCode.Numlock, false, false, false),
		new inputData(KeyCode.CapsLock, false, false, false),
		new inputData(KeyCode.ScrollLock, false, false, false),
		new inputData(KeyCode.RightShift, false, false, false),
		new inputData(KeyCode.LeftShift, false, false, false),
		new inputData(KeyCode.RightControl, false, false, false),
		new inputData(KeyCode.LeftControl, false, false, false),
		new inputData(KeyCode.RightAlt, false, false, false),
		new inputData(KeyCode.LeftAlt, false, false, false),
		new inputData(KeyCode.LeftCommand, false, false, false),
		new inputData(KeyCode.LeftCommand, false, false, false),
		new inputData(KeyCode.LeftWindows, false, false, false),
		new inputData(KeyCode.RightCommand, false, false, false),
		new inputData(KeyCode.RightCommand, false, false, false),
		new inputData(KeyCode.RightWindows, false, false, false),
		new inputData(KeyCode.AltGr, false, false, false),
		new inputData(KeyCode.Help, false, false, false),
		new inputData(KeyCode.Print, false, false, false),
		new inputData(KeyCode.SysReq, false, false, false),
		new inputData(KeyCode.Break, false, false, false),
		new inputData(KeyCode.Menu, false, false, false),
		new inputData(KeyCode.Mouse0, false, false, false),
		new inputData(KeyCode.Mouse1, false, false, false),
		new inputData(KeyCode.Mouse2, false, false, false),
		new inputData(KeyCode.Mouse3, false, false, false),
		new inputData(KeyCode.Mouse4, false, false, false),
		new inputData(KeyCode.Mouse5, false, false, false),
		new inputData(KeyCode.Mouse6, false, false, false),
		new inputData(KeyCode.JoystickButton0, false, false, false),
		new inputData(KeyCode.JoystickButton1, false, false, false),
		new inputData(KeyCode.JoystickButton2, false, false, false),
		new inputData(KeyCode.JoystickButton3, false, false, false),
		new inputData(KeyCode.JoystickButton4, false, false, false),
		new inputData(KeyCode.JoystickButton5, false, false, false),
		new inputData(KeyCode.JoystickButton6, false, false, false),
		new inputData(KeyCode.JoystickButton7, false, false, false),
		new inputData(KeyCode.JoystickButton8, false, false, false),
		new inputData(KeyCode.JoystickButton9, false, false, false),
		new inputData(KeyCode.JoystickButton10, false, false, false),
		new inputData(KeyCode.JoystickButton11, false, false, false),
		new inputData(KeyCode.JoystickButton12, false, false, false),
		new inputData(KeyCode.JoystickButton13, false, false, false),
		new inputData(KeyCode.JoystickButton14, false, false, false),
		new inputData(KeyCode.JoystickButton15, false, false, false),
		new inputData(KeyCode.JoystickButton16, false, false, false),
		new inputData(KeyCode.JoystickButton17, false, false, false),
		new inputData(KeyCode.JoystickButton18, false, false, false),
		new inputData(KeyCode.JoystickButton19, false, false, false),
		new inputData(KeyCode.Joystick1Button0, false, false, false),
		new inputData(KeyCode.Joystick1Button1, false, false, false),
		new inputData(KeyCode.Joystick1Button2, false, false, false),
		new inputData(KeyCode.Joystick1Button3, false, false, false),
		new inputData(KeyCode.Joystick1Button4, false, false, false),
		new inputData(KeyCode.Joystick1Button5, false, false, false),
		new inputData(KeyCode.Joystick1Button6, false, false, false),
		new inputData(KeyCode.Joystick1Button7, false, false, false),
		new inputData(KeyCode.Joystick1Button8, false, false, false),
		new inputData(KeyCode.Joystick1Button9, false, false, false),
		new inputData(KeyCode.Joystick1Button10, false, false, false),
		new inputData(KeyCode.Joystick1Button11, false, false, false),
		new inputData(KeyCode.Joystick1Button12, false, false, false),
		new inputData(KeyCode.Joystick1Button13, false, false, false),
		new inputData(KeyCode.Joystick1Button14, false, false, false),
		new inputData(KeyCode.Joystick1Button15, false, false, false),
		new inputData(KeyCode.Joystick1Button16, false, false, false),
		new inputData(KeyCode.Joystick1Button17, false, false, false),
		new inputData(KeyCode.Joystick1Button18, false, false, false),
		new inputData(KeyCode.Joystick1Button19, false, false, false),
		new inputData(KeyCode.Joystick2Button0, false, false, false),
		new inputData(KeyCode.Joystick2Button1, false, false, false),
		new inputData(KeyCode.Joystick2Button2, false, false, false),
		new inputData(KeyCode.Joystick2Button3, false, false, false),
		new inputData(KeyCode.Joystick2Button4, false, false, false),
		new inputData(KeyCode.Joystick2Button5, false, false, false),
		new inputData(KeyCode.Joystick2Button6, false, false, false),
		new inputData(KeyCode.Joystick2Button7, false, false, false),
		new inputData(KeyCode.Joystick2Button8, false, false, false),
		new inputData(KeyCode.Joystick2Button9, false, false, false),
		new inputData(KeyCode.Joystick2Button10, false, false, false),
		new inputData(KeyCode.Joystick2Button11, false, false, false),
		new inputData(KeyCode.Joystick2Button12, false, false, false),
		new inputData(KeyCode.Joystick2Button13, false, false, false),
		new inputData(KeyCode.Joystick2Button14, false, false, false),
		new inputData(KeyCode.Joystick2Button15, false, false, false),
		new inputData(KeyCode.Joystick2Button16, false, false, false),
		new inputData(KeyCode.Joystick2Button17, false, false, false),
		new inputData(KeyCode.Joystick2Button18, false, false, false),
		new inputData(KeyCode.Joystick2Button19, false, false, false),
		new inputData(KeyCode.Joystick3Button0, false, false, false),
		new inputData(KeyCode.Joystick3Button1, false, false, false),
		new inputData(KeyCode.Joystick3Button2, false, false, false),
		new inputData(KeyCode.Joystick3Button3, false, false, false),
		new inputData(KeyCode.Joystick3Button4, false, false, false),
		new inputData(KeyCode.Joystick3Button5, false, false, false),
		new inputData(KeyCode.Joystick3Button6, false, false, false),
		new inputData(KeyCode.Joystick3Button7, false, false, false),
		new inputData(KeyCode.Joystick3Button8, false, false, false),
		new inputData(KeyCode.Joystick3Button9, false, false, false),
		new inputData(KeyCode.Joystick3Button10, false, false, false),
		new inputData(KeyCode.Joystick3Button11, false, false, false),
		new inputData(KeyCode.Joystick3Button12, false, false, false),
		new inputData(KeyCode.Joystick3Button13, false, false, false),
		new inputData(KeyCode.Joystick3Button14, false, false, false),
		new inputData(KeyCode.Joystick3Button15, false, false, false),
		new inputData(KeyCode.Joystick3Button16, false, false, false),
		new inputData(KeyCode.Joystick3Button17, false, false, false),
		new inputData(KeyCode.Joystick3Button18, false, false, false),
		new inputData(KeyCode.Joystick3Button19, false, false, false),
		new inputData(KeyCode.Joystick4Button0, false, false, false),
		new inputData(KeyCode.Joystick4Button1, false, false, false),
		new inputData(KeyCode.Joystick4Button2, false, false, false),
		new inputData(KeyCode.Joystick4Button3, false, false, false),
		new inputData(KeyCode.Joystick4Button4, false, false, false),
		new inputData(KeyCode.Joystick4Button5, false, false, false),
		new inputData(KeyCode.Joystick4Button6, false, false, false),
		new inputData(KeyCode.Joystick4Button7, false, false, false),
		new inputData(KeyCode.Joystick4Button8, false, false, false),
		new inputData(KeyCode.Joystick4Button9, false, false, false),
		new inputData(KeyCode.Joystick4Button10, false, false, false),
		new inputData(KeyCode.Joystick4Button11, false, false, false),
		new inputData(KeyCode.Joystick4Button12, false, false, false),
		new inputData(KeyCode.Joystick4Button13, false, false, false),
		new inputData(KeyCode.Joystick4Button14, false, false, false),
		new inputData(KeyCode.Joystick4Button15, false, false, false),
		new inputData(KeyCode.Joystick4Button16, false, false, false),
		new inputData(KeyCode.Joystick4Button17, false, false, false),
		new inputData(KeyCode.Joystick4Button18, false, false, false),
		new inputData(KeyCode.Joystick4Button19, false, false, false),
		new inputData(KeyCode.Joystick5Button0, false, false, false),
		new inputData(KeyCode.Joystick5Button1, false, false, false),
		new inputData(KeyCode.Joystick5Button2, false, false, false),
		new inputData(KeyCode.Joystick5Button3, false, false, false),
		new inputData(KeyCode.Joystick5Button4, false, false, false),
		new inputData(KeyCode.Joystick5Button5, false, false, false),
		new inputData(KeyCode.Joystick5Button6, false, false, false),
		new inputData(KeyCode.Joystick5Button7, false, false, false),
		new inputData(KeyCode.Joystick5Button8, false, false, false),
		new inputData(KeyCode.Joystick5Button9, false, false, false),
		new inputData(KeyCode.Joystick5Button10, false, false, false),
		new inputData(KeyCode.Joystick5Button11, false, false, false),
		new inputData(KeyCode.Joystick5Button12, false, false, false),
		new inputData(KeyCode.Joystick5Button13, false, false, false),
		new inputData(KeyCode.Joystick5Button14, false, false, false),
		new inputData(KeyCode.Joystick5Button15, false, false, false),
		new inputData(KeyCode.Joystick5Button16, false, false, false),
		new inputData(KeyCode.Joystick5Button17, false, false, false),
		new inputData(KeyCode.Joystick5Button18, false, false, false),
		new inputData(KeyCode.Joystick5Button19, false, false, false),
		new inputData(KeyCode.Joystick6Button0, false, false, false),
		new inputData(KeyCode.Joystick6Button1, false, false, false),
		new inputData(KeyCode.Joystick6Button2, false, false, false),
		new inputData(KeyCode.Joystick6Button3, false, false, false),
		new inputData(KeyCode.Joystick6Button4, false, false, false),
		new inputData(KeyCode.Joystick6Button5, false, false, false),
		new inputData(KeyCode.Joystick6Button6, false, false, false),
		new inputData(KeyCode.Joystick6Button7, false, false, false),
		new inputData(KeyCode.Joystick6Button8, false, false, false),
		new inputData(KeyCode.Joystick6Button9, false, false, false),
		new inputData(KeyCode.Joystick6Button10, false, false, false),
		new inputData(KeyCode.Joystick6Button11, false, false, false),
		new inputData(KeyCode.Joystick6Button12, false, false, false),
		new inputData(KeyCode.Joystick6Button13, false, false, false),
		new inputData(KeyCode.Joystick6Button14, false, false, false),
		new inputData(KeyCode.Joystick6Button15, false, false, false),
		new inputData(KeyCode.Joystick6Button16, false, false, false),
		new inputData(KeyCode.Joystick6Button17, false, false, false),
		new inputData(KeyCode.Joystick6Button18, false, false, false),
		new inputData(KeyCode.Joystick6Button19, false, false, false),
		new inputData(KeyCode.Joystick7Button0, false, false, false),
		new inputData(KeyCode.Joystick7Button1, false, false, false),
		new inputData(KeyCode.Joystick7Button2, false, false, false),
		new inputData(KeyCode.Joystick7Button3, false, false, false),
		new inputData(KeyCode.Joystick7Button4, false, false, false),
		new inputData(KeyCode.Joystick7Button5, false, false, false),
		new inputData(KeyCode.Joystick7Button6, false, false, false),
		new inputData(KeyCode.Joystick7Button7, false, false, false),
		new inputData(KeyCode.Joystick7Button8, false, false, false),
		new inputData(KeyCode.Joystick7Button9, false, false, false),
		new inputData(KeyCode.Joystick7Button10, false, false, false),
		new inputData(KeyCode.Joystick7Button11, false, false, false),
		new inputData(KeyCode.Joystick7Button12, false, false, false),
		new inputData(KeyCode.Joystick7Button13, false, false, false),
		new inputData(KeyCode.Joystick7Button14, false, false, false),
		new inputData(KeyCode.Joystick7Button15, false, false, false),
		new inputData(KeyCode.Joystick7Button16, false, false, false),
		new inputData(KeyCode.Joystick7Button17, false, false, false),
		new inputData(KeyCode.Joystick7Button18, false, false, false),
		new inputData(KeyCode.Joystick7Button19, false, false, false),
		new inputData(KeyCode.Joystick8Button0, false, false, false),
		new inputData(KeyCode.Joystick8Button1, false, false, false),
		new inputData(KeyCode.Joystick8Button2, false, false, false),
		new inputData(KeyCode.Joystick8Button3, false, false, false),
		new inputData(KeyCode.Joystick8Button4, false, false, false),
		new inputData(KeyCode.Joystick8Button5, false, false, false),
		new inputData(KeyCode.Joystick8Button6, false, false, false),
		new inputData(KeyCode.Joystick8Button7, false, false, false),
		new inputData(KeyCode.Joystick8Button8, false, false, false),
		new inputData(KeyCode.Joystick8Button9, false, false, false),
		new inputData(KeyCode.Joystick8Button10, false, false, false),
		new inputData(KeyCode.Joystick8Button11, false, false, false),
		new inputData(KeyCode.Joystick8Button12, false, false, false),
		new inputData(KeyCode.Joystick8Button13, false, false, false),
		new inputData(KeyCode.Joystick8Button14, false, false, false),
		new inputData(KeyCode.Joystick8Button15, false, false, false),
		new inputData(KeyCode.Joystick8Button16, false, false, false),
		new inputData(KeyCode.Joystick8Button17, false, false, false),
		new inputData(KeyCode.Joystick8Button18, false, false, false),
		new inputData(KeyCode.Joystick8Button19, false, false, false)
	};

	public bool is_input_;

	public static InputCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public void Update()
	{
		is_input_ = true;
		for (int i = 0; i < input_key_.Count; i++)
		{
			bool key = Input.GetKey(input_key_[i].code);
			bool keyUp = Input.GetKeyUp(input_key_[i].code);
			bool keyDown = Input.GetKeyDown(input_key_[i].code);
			input_key_[i].input_key = input_key_[i].input_key || key;
			input_key_[i].input_up = input_key_[i].input_up || keyUp;
			input_key_[i].input_down = input_key_[i].input_down || keyDown;
		}
	}

	public void FixedUpdate()
	{
		if (is_input_)
		{
			is_input_ = false;
			for (int i = 0; i < input_key_.Count; i++)
			{
				input_key_[i].up = ((input_key_[i].key && !input_key_[i].input_key) ? true : false);
				input_key_[i].down = ((!input_key_[i].key && input_key_[i].input_key) ? true : false);
				input_key_[i].key = input_key_[i].input_key;
				input_key_[i].input_key = false;
				input_key_[i].input_up = false;
				input_key_[i].input_down = false;
			}
		}
		else
		{
			for (int j = 0; j < input_key_.Count; j++)
			{
				input_key_[j].up = false;
				input_key_[j].down = false;
			}
		}
	}

	public int GetCode(KeyCode in_code)
	{
		int result = 0;
		for (int i = 0; i < input_key_.Count; i++)
		{
			if (input_key_[i].code == in_code)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	public bool GetKey(KeyCode in_code)
	{
		int code = GetCode(in_code);
		return input_key_[code].key;
	}

	public bool GetKeyUp(KeyCode in_code)
	{
		int code = GetCode(in_code);
		return input_key_[code].up;
	}

	public bool GetKeyDown(KeyCode in_code)
	{
		int code = GetCode(in_code);
		return input_key_[code].down;
	}
}
