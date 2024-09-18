using System.Text;

public class MessageText
{
	private const string COLOR_TAG_START = "<color=#";

	private const string COLOR_END_TAG = "</color>";

	private const string TAG_END = ">";

	private const int GRADATION_COLOR_START_INDEX = 4;

	private StringBuilder text_;

	private string string_;

	private int character_count_;

	private int color_;

	private bool gradation_flag_;

	private bool is_usa_;

	private static readonly string[] text_color_table;

	private static readonly int[] gradation_character_threshold;

	public int character_count
	{
		get
		{
			return character_count_;
		}
	}

	public int color
	{
		get
		{
			return color_;
		}
	}

	public MessageText()
	{
		text_ = new StringBuilder();
		string_ = string.Empty;
		character_count_ = 0;
		color_ = 0;
	}

	static MessageText()
	{
		text_color_table = new string[8] { "ffffff", "ff0000", "68c0f0", "00f000", "828282", "707070", "5c5c5c", "424242" };
		gradation_character_threshold = new int[4] { 4, 8, 11, 14 };
	}

	public override string ToString()
	{
		if (string_ == null)
		{
			string_ = text_.ToString();
			if (color_ != 0)
			{
				string_ += "</color>";
			}
		}
		return string_;
	}

	public void Clear()
	{
		text_.Length = 0;
		string_ = string.Empty;
		character_count_ = 0;
		if (gradation_flag_)
		{
			gradation_flag_ = false;
			SetColor(0);
		}
	}

	public void Append(char character)
	{
		string_ = null;
		if (gradation_flag_)
		{
			for (int i = 0; i < gradation_character_threshold.Length; i++)
			{
				if (gradation_character_threshold[i] * ((!is_usa_) ? 1 : 2) == character_count_)
				{
					SetColor(4 + i);
					break;
				}
			}
		}
		if (text_.Length == 0 && color_ != 0)
		{
			AppendColorTag(color_);
		}
		text_.Append(character);
		character_count_++;
	}

	public void SetColor(int color)
	{
		if (color_ != color)
		{
			string_ = null;
			if (color_ != 0 && text_.Length > 0)
			{
				text_.Append("</color>");
			}
			color_ = color;
			if (color_ != 0)
			{
				AppendColorTag(color);
			}
		}
	}

	public void SetGradationColor(bool flag)
	{
		gradation_flag_ = flag;
	}

	public void SetUSAFlag(bool flag)
	{
		is_usa_ = flag;
	}

	private void AppendColorTag(int color)
	{
		text_.Append("<color=#");
		text_.Append(text_color_table[color]);
		text_.Append(">");
	}
}
