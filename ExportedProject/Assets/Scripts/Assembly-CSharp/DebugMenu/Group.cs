using System;

namespace DebugMenu
{
	public class Group : Item
	{
		private int item_count_;

		private Item first_;

		private Item last_;

		private bool update_item_;

		private Item selected_;

		private Action<Group, bool> on_set_current;

		public int item_count
		{
			get
			{
				return item_count_;
			}
		}

		public Item first
		{
			get
			{
				return first_;
			}
		}

		public Item last
		{
			get
			{
				return last_;
			}
		}

		public bool update_item
		{
			get
			{
				return update_item_;
			}
			set
			{
				update_item_ = value;
			}
		}

		public Item selected
		{
			get
			{
				return selected_;
			}
			set
			{
				if (selected_ != value && HasItem(value))
				{
					selected_ = value;
				}
			}
		}

		public Group(string name)
			: base(name)
		{
		}

		public Group AddItem(Item item)
		{
			if (item.parent != this)
			{
				if (item.parent != null)
				{
					item.parent.RemoveItem(item);
				}
				item.EnterGroup(this, last_);
				if (first_ == null)
				{
					first_ = item;
					selected_ = item;
				}
				last_ = item;
				item_count_++;
				update_item_ = true;
			}
			return this;
		}

		public void RemoveItem(Item item)
		{
			if (item.parent != this)
			{
				return;
			}
			if (first_ == item)
			{
				first_ = item.next;
			}
			if (last_ == item)
			{
				last_ = item.prev;
			}
			if (selected_ == item)
			{
				if (item.next != null)
				{
					selected_ = item.next;
				}
				else
				{
					selected_ = item.prev;
				}
			}
			item.LeaveGroup();
			item_count_--;
			update_item_ = true;
		}

		public void RemoveAllItem()
		{
			while (first_ != null)
			{
				RemoveItem(first_);
			}
		}

		public Item FindItem(string name)
		{
			Item item = first_;
			while (item != null && string.Compare(item.name, name) != 0)
			{
				item = item.next;
			}
			return item;
		}

		public Group OnSetCurrentAction(Action<Group, bool> action)
		{
			on_set_current = action;
			return this;
		}

		public void OnSetCurrent(bool current)
		{
			if (on_set_current != null)
			{
				on_set_current(this, current);
			}
		}

		public void OnUp(bool repeat)
		{
			if (selected_ != null)
			{
				if (selected_.prev != null)
				{
					selected_ = selected_.prev;
				}
				else if (!repeat)
				{
					selected_ = last_;
				}
			}
			else
			{
				selected_ = last_;
			}
		}

		public void OnDown(bool repeat)
		{
			if (selected_ != null)
			{
				if (selected_.next != null)
				{
					selected_ = selected_.next;
				}
				else if (!repeat)
				{
					selected_ = first_;
				}
			}
			else
			{
				selected_ = first_;
			}
		}

		public void OnDecide(Menu menu)
		{
			menu.current = this;
		}

		public void OnCancel(Menu menu)
		{
			menu.current = base.parent;
		}

		public override void Update(Menu menu)
		{
			if (menu.current == this)
			{
				if (((menu.input_down | menu.input_repeat) & Input.Up) != 0)
				{
					OnUp((menu.input_repeat & Input.Up) != 0);
				}
				else if (((menu.input_down | menu.input_repeat) & Input.Down) != 0)
				{
					OnDown((menu.input_repeat & Input.Down) != 0);
				}
				else if ((menu.input_down & Input.Cancel) != 0)
				{
					OnCancel(menu);
				}
				else if (selected_ != null)
				{
					selected_.Update(menu);
				}
			}
			else if ((menu.input_down & Input.Decide) != 0)
			{
				OnDecide(menu);
			}
		}

		private bool HasItem(Item item)
		{
			return item != null && item.parent == this;
		}
	}
}
