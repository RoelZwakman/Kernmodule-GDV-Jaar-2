using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBoundsCollider : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<Collider>().tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
