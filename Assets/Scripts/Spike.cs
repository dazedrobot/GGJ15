using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour 
{
	public GameObject targetGoo;
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
            FindObjectOfType<TheManager>().SplitGooBall(collider.gameObject);
            Debug.Log("Spiked");
            Destroy(gameObject);
        }
    }
}
