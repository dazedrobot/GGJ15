using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour 
{
	void Start ()
    {
	
	}

	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.ToString() == "GooBall")
        {
            GameObject.FindWithTag("TheManager").GetComponent<TheManager>().SplitGooBall(collider.gameObject);
            Debug.Log("SPLIT");
        }
    }
}
