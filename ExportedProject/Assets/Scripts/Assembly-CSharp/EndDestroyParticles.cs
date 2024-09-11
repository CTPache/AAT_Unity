using System.Collections;
using UnityEngine;

public class EndDestroyParticles : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem parical_system_;

	private void OnEnable()
	{
		coroutineCtrl.instance.Play(ParticleWorking());
	}

	private IEnumerator ParticleWorking()
	{
		while (parical_system_.IsAlive(true))
		{
			yield return null;
		}
		Object.Destroy(base.gameObject);
	}
}
