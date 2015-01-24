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

    void FixedUpdate()
    {
        this.transform.Translate(new Vector3(0, 0, -10.0f) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.ToString() == "GooBall")
        {
            FindObjectOfType<TheManager>().MergeGooBall(collider.gameObject, g_largeGooBall);
            Debug.Log("Merged");
            Destroy(gameObject);
        }
    }
}
