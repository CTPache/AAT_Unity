using System.Linq;
using UnityEngine;

public class TestimonyRoot : MonoBehaviour
{
	[SerializeField]
	private Evaluator testimonyIconEvaluator;

	[SerializeField]
	private SpriteRenderer testimonyIcon;

	public static TestimonyRoot instance { get; private set; }

	public bool TestimonyIconEnabled
	{
		get
		{
			return testimonyIconEvaluator.enabled;
		}
		set
		{
			if (TestimonyIconEnabled == value)
			{
				return;
			}
			if (value)
			{
				testimonyIconEvaluator.Initialize();
				if (testimonyIcon.sprite == null)
				{
					string in_name = "testimony" + GSUtility.GetResourceNameLanguage(GSStatic.global_work_.language);
					AssetBundle assetBundle = AssetBundleCtrl.instance.load("/menu/common/", in_name, true);
					testimonyIcon.sprite = assetBundle.LoadAllAssets<Sprite>().First();
				}
			}
			testimonyIconEvaluator.enabled = value;
			testimonyIcon.enabled = value;
		}
	}

	private void Awake()
	{
		instance = this;
	}
}
