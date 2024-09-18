using UnityEngine;

namespace DebugMenu.uGUI
{
	public class Item
	{
		public virtual DebugMenu.Item item
		{
			get
			{
				return null;
			}
		}

		public virtual GameObject CreateObject(Menu menu, Transform parent)
		{
			return null;
		}

		public virtual void SetSelected(Menu menu, bool selected)
		{
		}

		public virtual void Process(Menu menu)
		{
		}
	}
}
