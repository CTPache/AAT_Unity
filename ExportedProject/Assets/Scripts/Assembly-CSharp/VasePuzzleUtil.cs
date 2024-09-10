using UnityEngine;

public class VasePuzzleUtil : MonoBehaviour
{
	public enum SortOrder
	{
		Background = -10,
		IconWindow = -9,
		IconBase = -8,
		Icon = -7,
		model = -3,
		canvas = 0
	}

	private class puzzleCorrectData
	{
		public int pieces_index;

		public int angle_id;

		public int vase_angle;

		public puzzleCorrectData(int in_pieces_index, int in_angle_id, int in_vase_angle)
		{
			pieces_index = in_pieces_index;
			angle_id = in_angle_id;
			vase_angle = in_vase_angle;
		}
	}

	private puzzleCorrectData[] puzzle_correct_data = new puzzleCorrectData[9]
	{
		new puzzleCorrectData(4, 0, 60),
		new puzzleCorrectData(3, 0, 150),
		new puzzleCorrectData(5, 0, 230),
		new puzzleCorrectData(0, 0, 340),
		new puzzleCorrectData(7, 0, 360),
		new puzzleCorrectData(2, 0, 420),
		new puzzleCorrectData(1, 0, 505),
		new puzzleCorrectData(6, 0, 540),
		new puzzleCorrectData(0, 0, 250)
	};

	public static VasePuzzleUtil instance { get; private set; }

	private void Awake()
	{
		instance = this;
	}

	public GameObject createAssetBundle(string in_path, string in_name, SortOrder in_sort_order)
	{
		debugLogger.instance.Log("Puzzle", "load AssetBundle > path[" + in_path + "] name[" + in_name + "]");
		GameObject gameObject = new GameObject();
		gameObject.transform.parent = base.transform;
		gameObject.name = in_name;
		gameObject.layer = base.gameObject.layer;
		gameObject.transform.localScale = Vector3.one;
		SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sortingOrder = (int)in_sort_order;
		AssetBundleSprite assetBundleSprite = new AssetBundleSprite();
		assetBundleSprite.sprite_renderer_ = spriteRenderer;
		assetBundleSprite.load(in_path, in_name);
		VasePuzzleSprite vasePuzzleSprite = gameObject.AddComponent<VasePuzzleSprite>();
		vasePuzzleSprite.sprite = assetBundleSprite;
		return gameObject;
	}

	public bool checkPuzzle(int in_step, int in_index, int in_angle_id)
	{
		if (puzzle_correct_data.Length <= in_step)
		{
			Debug.LogWarning("puzzle_correct_data.Length(" + puzzle_correct_data.Length + ") <= in_step(" + in_step + ")");
			return false;
		}
		if (puzzle_correct_data[in_step].pieces_index != in_index)
		{
			debugLogger.instance.Log("Puzzle", "index:ng.");
			return false;
		}
		if (puzzle_correct_data[in_step].angle_id != in_angle_id)
		{
			debugLogger.instance.Log("Puzzle", "angle:ng.");
			return false;
		}
		debugLogger.instance.Log("Puzzle", "checkPuzzle:ok.");
		return true;
	}

	public int GetVaseRotateY(int in_next_step)
	{
		if (in_next_step <= 0)
		{
			return 0;
		}
		if (puzzle_correct_data.Length <= in_next_step)
		{
			Debug.LogWarning("puzzle_correct_data.Length(" + puzzle_correct_data.Length + ") <= in_step(" + in_next_step + ")");
			return 0;
		}
		return puzzle_correct_data[in_next_step].vase_angle - puzzle_correct_data[in_next_step - 1].vase_angle;
	}
}
