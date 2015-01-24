using UnityEngine;
using System.Collections;

public class Merger : MonoBehaviour 
{
    public GameObject g_largeGooBall;

	void Start ()
    {
	    
	}
	
	void Update () 
    {
	    
	}

    void OnTriggerEnter(Collider smallGooBall)
    {
        if (collider.gameObject.tag.ToString() == "GooBall")
        {
            FindObjectOfType<TheManager>().MergeGooBall(smallGooBall.gameObject, g_largeGooBall);
            Debug.Log("MARGE");
        }
    }
}
