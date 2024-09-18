using System.IO;
using UnityEngine;

public class IndicateRevision : MonoBehaviour
{
	private TextAsset textAsset_;

	private int revision_no_;

	public int RevisionNum
	{
		get
		{
			if ((bool)textAsset_)
			{
				return revision_no_;
			}
			return revision_no_;
		}
	}

	public void DoNothing()
	{
	}

	private void SetUp()
	{
		textAsset_ = new TextAsset();
		try
		{
			textAsset_ = Resources.Load("Debug/SVNRevision", typeof(TextAsset)) as TextAsset;
		}
		catch (FileNotFoundException)
		{
			Debug.LogWarning("textファイルが見つかりませんでした。");
			return;
		}
		if ((bool)textAsset_)
		{
			string text = textAsset_.text;
			revision_no_ = int.Parse(text);
		}
	}
}
