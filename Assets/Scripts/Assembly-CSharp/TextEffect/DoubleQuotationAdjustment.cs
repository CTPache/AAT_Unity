using System.Collections.Generic;
using UnityEngine;

namespace TextEffect
{
	public class DoubleQuotationAdjustment : BaseMessageTextEffect
	{
		[SerializeField]
public float alignment_upperleft_quotation_diff_ = 34f;

		[SerializeField]
public float alignment_upperleft_quotation_diff_china_s_ = 32f;

		[SerializeField]
public float alignment_uppercenter_quotation_diff_ = 25f;

		[SerializeField]
public float alignment_uppercenter_quotation_diff_china_s_ = 24f;

		[SerializeField]
public float diaeresis_i_diff_ = 2.8f;

		private const char BEGIN_QUOTATION = '“';

		private const char END_QUOTATION = '”';

		private const char DIAERESIS_I = 'ï';

		public override void MessageModifyMesh(ref List<UIVertex> stream)
		{
			if (text_ == null)
			{
				return;
			}
			int num = CountChar(text_.text, '“');
			num += CountChar(text_.text, '”');
			TextAnchor alignment = text_.alignment;
			float num2 = 0f;
			if (alignment == TextAnchor.UpperCenter)
			{
				num2 = (float)num * (alignment_uppercenter_quotation_diff_ / 2f);
			}
			float num3 = 0f;
			int num4 = 0;
			int count = stream.Count;
			int num5 = 0;
			while (num4 < count)
			{
				Vector2 centerPosition = GetCenterPosition(num4, stream);
				if (text_.text[num5] == '“')
				{
					num3 = ((GSStatic.global_work_.language != "CHINA_S") ? (num3 + ((alignment != 0) ? alignment_uppercenter_quotation_diff_ : alignment_upperleft_quotation_diff_)) : (num3 + ((alignment != 0) ? alignment_uppercenter_quotation_diff_china_s_ : alignment_upperleft_quotation_diff_china_s_)));
				}
				else if (num5 > 0 && text_.text[num5 - 1] == '”')
				{
					num3 = ((GSStatic.global_work_.language != "CHINA_S") ? (num3 + ((alignment != 0) ? alignment_uppercenter_quotation_diff_ : alignment_upperleft_quotation_diff_)) : (num3 + ((alignment != 0) ? alignment_uppercenter_quotation_diff_china_s_ : alignment_upperleft_quotation_diff_china_s_)));
				}
				else if (GSStatic.global_work_.language != "JAPAN" && GSStatic.global_work_.language != "USA")
				{
					if (text_.text[num5] == 'ï')
					{
						num3 += diaeresis_i_diff_;
					}
					if (num5 > 0 && text_.text[num5 - 1] == 'ï')
					{
						num3 += diaeresis_i_diff_;
					}
				}
				for (int i = 0; i < 6; i++)
				{
					UIVertex value = stream[num4 + i];
					Vector2 vector = value.position - (Vector3)centerPosition;
					vector = new Vector2(vector.x + num3 - num2, vector.y);
					value.position = vector + centerPosition;
					stream[num4 + i] = value;
				}
				num4 += 6;
				num5++;
			}
			text_.GraphicUpdateComplete();
		}

		private int CountChar(string s, char c)
		{
			return s.Length - s.Replace(c.ToString(), string.Empty).Length;
		}
	}
}
