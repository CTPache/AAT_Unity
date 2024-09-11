namespace DebugMenu
{
	public class Item
	{
		private string name_;

		private Group parent_;

		private Item prev_;

		private Item next_;

		public string name
		{
			get
			{
				return name_;
			}
		}

		public Group parent
		{
			get
			{
				return parent_;
			}
			protected set
			{
				parent_ = value;
			}
		}

		public Item prev
		{
			get
			{
				return prev_;
			}
			protected set
			{
				prev_ = value;
			}
		}

		public Item next
		{
			get
			{
				return next_;
			}
			protected set
			{
				next_ = value;
			}
		}

		protected Item(string name)
		{
			name_ = name;
		}

		public void EnterGroup(Group parent, Item prev)
		{
			parent_ = parent;
			prev_ = prev;
			if (prev_ != null)
			{
				prev_.next_ = this;
			}
		}

		public void LeaveGroup()
		{
			parent_ = null;
			if (prev_ != null)
			{
				prev_.next_ = next_;
			}
			if (next_ != null)
			{
				next_.prev_ = prev_;
			}
			prev_ = null;
			next_ = null;
		}

		public virtual void Process(Menu menu)
		{
		}
	}
}
