using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleHitExplosion : MonoBehaviour {

    public float delay;

	private void Start()
    {
        StartDespawnSequence();
    }

    public void StartDespawnSequence()
    {
        StartCoroutine(DespawnAfterDelay());
    }

    private IEnumerator DespawnAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        ParticlePooler.instance.AddToHitFXPool(gameObject);
    }


}
