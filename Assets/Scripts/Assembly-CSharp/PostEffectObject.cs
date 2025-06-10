using UnityEngine;

public class PostEffectObject : MonoBehaviour
{
	[SerializeField]
public Material grayscale_;

	[SerializeField]
public Material sepia_;

	[SerializeField]
public Material black_;

	[SerializeField]
public Material red_;

	private Material ctrl_material_;

	public void ResetAll()
	{
		SetGrayscale(0f);
		SetSepia(0f);
		SetBlack(0f);
		SetRed(0f);
	}

	public void SetGrayscale(float volume)
	{
		SetVolume(grayscale_, volume);
	}

	public void SetSepia(float volume)
	{
		SetVolume(sepia_, volume);
	}

	public void SetBlack(float volume)
	{
		SetVolume(black_, volume);
	}

	public void SetRed(float volume)
	{
		SetVolume(red_, volume);
	}

	private void SetVolume(Material mat, float volume)
	{
		if (ctrl_material_ != mat)
		{
			ctrl_material_ = mat;
		}
		if ((double)volume == 0.0)
		{
			ctrl_material_ = null;
		}
		mat.SetFloat("_Volume", volume);
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if (ctrl_material_ != null)
		{
			dest.Release();
			Graphics.Blit(src, dest, ctrl_material_);
		}
		else
		{
			Graphics.Blit(src, dest);
		}
	}
}
