using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        Destroy(gameObject);
        Destroy(collider.gameObject);
    }
}
