using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextEffect
{
	[RequireComponent(typeof(Text))]
	public abstract class BaseMessageTextEffect : BaseMeshEffect
	{
		protected const int CHARACTER_VERTEX_MAX = 6;

		protected Text text_;

		public abstract void MessageModifyMesh(ref List<UIVertex> stream);

		public new virtual void ModifyMesh(Mesh mesh)
		{
		}

		public override void ModifyMesh(VertexHelper vh)
		{
			if (IsActive() && GSStatic.global_work_.language != "KOREA")
			{
				List<UIVertex> stream = ListPool<UIVertex>.Get();
				vh.GetUIVertexStream(stream);
				MessageModifyMesh(ref stream);
				vh.Clear();
				vh.AddUIVertexTriangleStream(stream);
				ListPool<UIVertex>.Release(stream);
			}
		}

		protected Vector2 GetCenterPosition(int index, List<UIVertex> ui_vertext_list)
		{
			return Vector2.Lerp(ui_vertext_list[index].position, ui_vertext_list[index + 3].position, 0.5f);
		}

		protected Vector2 GetUpperPosition(int index, List<UIVertex> ui_vertext_list)
		{
			return Vector2.Lerp(ui_vertext_list[index].position, ui_vertext_list[index + 1].position, 0.5f);
		}

		protected override void Start()
		{
			text_ = GetComponent<Text>();
		}
	}
}
