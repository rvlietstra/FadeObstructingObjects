using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        //FadeObstructingObjects.Instance.RegisterShouldBeVisible(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
        gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * 4.0f, Input.GetAxis("Vertical") * Time.deltaTime * 4.0f, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = (GameObject)Instantiate(Resources.Load("Bullet"), gameObject.transform.position + new Vector3(0, 0, -2), Quaternion.identity);
        }

	}
}
