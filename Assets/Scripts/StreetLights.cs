using UnityEngine;
using System.Collections;

public class StreetLights : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position -= new Vector3(0.0f, 0.0f, 10.0f) * Time.deltaTime;
        if (transform.position.z < -40.0f)
            gameObject.transform.position = new Vector3(0.0f, 0.0f, 30.0f);
	}
}
