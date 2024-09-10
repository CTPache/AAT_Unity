public class piceData
{
	private ushort name_id_j_ = 255;

	private ushort name_id_u_ = 255;

	private ushort comment_id_ = 255;

	public string path = string.Empty;

	public ushort file_id = 255;

	public int type;

	public int no;

	public int detail_id;

	public int obj_id;

	public ushort file_language_id = 255;

	public string name
	{
		get
		{
			return advCtrl.instance.nolb_data_.GetText(name_id_);
		}
	}

	public string comment00
	{
		get
		{
			return advCtrl.instance.note_data_.GetText(comment_id_, 0);
		}
	}

	public string comment01
	{
		get
		{
			return advCtrl.instance.note_data_.GetText(comment_id_, 1);
		}
	}

	public string comment02
	{
		get
		{
			return advCtrl.instance.note_data_.GetText(comment_id_, 2);
		}
	}

	private ushort name_id_
	{
		get
		{
			return (GSStatic.global_work_.language != 0) ? name_id_u_ : name_id_j_;
		}
	}

	public piceData(ushort in_name_id_j, ushort in_name_id_u, ushort in_comment_id, string in_path, ushort in_file_id, int in_type, int in_no, int in_detail_id, int in_obj_id, ushort in_file_language_id)
	{
		name_id_j_ = in_name_id_j;
		name_id_u_ = in_name_id_u;
		comment_id_ = in_comment_id;
		path = in_path;
		file_id = in_file_id;
		type = in_type;
		no = in_no;
		detail_id = in_detail_id;
		obj_id = in_obj_id;
		file_language_id = in_file_language_id;
	}
}
