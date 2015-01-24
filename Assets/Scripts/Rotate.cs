using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{
	void Update ()
    {
        transform.Rotate(new Vector3(1.0f, 1.0f, 1.0f), Mathf.Sin(Time.deltaTime * 10.0f));
	}
}
