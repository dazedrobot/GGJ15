using UnityEngine;
using System.Collections;

public class Merger : MonoBehaviour 
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
            FindObjectOfType<TheManager>().MergeGooBall(collider.gameObject);
            Debug.Log("SPLIT");
        }
    }
}
