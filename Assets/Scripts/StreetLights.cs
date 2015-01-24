using UnityEngine;
using System.Collections;

public class StreetLights : MonoBehaviour 
{
    public float posX;
    public float speed = 10.0f;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position -= new Vector3(0.0f, 0.0f, speed) * Time.deltaTime;
        if (transform.position.z < -40.0f)
            gameObject.transform.position = new Vector3(posX, 20.0f, 60.0f);
	}
}
