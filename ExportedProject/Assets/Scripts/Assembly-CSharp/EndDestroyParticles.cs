using System.Collections;
using UnityEngine;

public class EndDestroyParticles : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem parical_system_;

	private void OnEnable()
	{
		StartCoroutine(ParticleWorking());
	}

	private IEnumerator ParticleWorking()
	{
		yield return new WaitWhile(() => parical_system_.IsAlive(true));
		Object.Destroy(base.gameObject);
	}
}
