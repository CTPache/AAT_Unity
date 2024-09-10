using System;

namespace DebugMenu
{
	public class Func : Item
	{
		private Action action_;

		public Func(string name, Action action)
			: base(name)
		{
			action_ = action;
		}

		public void OnDecide()
		{
			if (action_ != null)
			{
				action_();
			}
		}

		public override void Update(Menu menu)
		{
			if ((menu.input_down & Input.Decide) != 0)
			{
				OnDecide();
			}
		}
	}
}
