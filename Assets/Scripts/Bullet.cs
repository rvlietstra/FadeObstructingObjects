using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position += new Vector3(0, 0, -5.0f * Time.deltaTime);

        if (transform.position.z < -20)
            Destroy(gameObject);
	}
}
