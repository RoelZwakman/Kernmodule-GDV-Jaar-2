using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooler : MonoBehaviour {

    public static ParticlePooler instance;

    public int hitFXPoolSize;
    public List<GameObject> hitFXPool = new List<GameObject>(5);
    public GameObject hitFXPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            InitializeHitFX();
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void InitializeHitFX() ////generates the pool of HitFX GameObjects
    {
        Debug.Log("initializing pool");
        hitFXPool = new List<GameObject>(hitFXPoolSize);
        Debug.Log(hitFXPool.Count + " was hitFXPool.Count. hitFXPoolSize is " + hitFXPoolSize);
        for(int i = 0; i < hitFXPoolSize; i++)
        {
            GameObject poolItem = Instantiate(hitFXPrefab, transform.position, Quaternion.identity);
            AddToHitFXPoolInit(poolItem);
        }
    }

    public void AddToHitFXPoolInit(GameObject hitFXGO) ////Adds a GO to the pool. This is only used during initialization. Had some issues with instancing, this fixed it.
    {
        hitFXGO.SetActive(true);
        ParticleSystem pSystem = hitFXGO.GetComponentInChildren<ParticleSystem>();
        pSystem.Stop();
        pSystem.Clear();
        hitFXPool.Add(hitFXGO);
        hitFXGO.SetActive(false);
    }

    public void AddToHitFXPool(GameObject hitFXGO) ////readies for and adds hitFXGO to the hitFXPool
    {
        ParticleSystem pSystem = hitFXGO.GetComponentInChildren<ParticleSystem>();
        pSystem.Stop();
        pSystem.Clear();
        instance.hitFXPool.Add(hitFXGO);
        hitFXGO.SetActive(false);
    }

    public void SpawnFromPoolHitFX(Transform positionTransform) /////Spawns a hitFX GameObject at positionTransform and removes it from the pool.
    {
        instance.hitFXPool[0].transform.position = positionTransform.position;
        instance.hitFXPool[0].GetComponentInChildren<ParticleSystem>().Play();
        instance.hitFXPool[0].SetActive(true);
        instance.hitFXPool[0].GetComponent<PaddleHitExplosion>().StartDespawnSequence();
        instance.hitFXPool.RemoveAt(0);
    }

}
