using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleHitExplosion : MonoBehaviour {

    public float delay;

	void Start()
    {
        StartCoroutine(DespawnAfterDelay());
    }

    IEnumerator DespawnAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }


}
