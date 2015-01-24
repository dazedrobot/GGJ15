using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        if (transform.position.z < -20) { Destroy(gameObject); }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.ToString() == "GooBall")
        {
            if (collider.GetComponent<GooBall>().g_phase == true)
            {
                FindObjectOfType<TheManager>().MergeGooBall(collider.gameObject, g_largeGooBall);
                Debug.Log("Merged");
                //Invalidate any other knived heading for this ball.
                List<GameObject> mergers = collider.gameObject.GetComponent<GooBall>().Mergers;
                for (int i = 0; i < mergers.Count; ++i)
                {
                    Destroy(mergers[i]);
                }
                mergers = g_largeGooBall.GetComponent<GooBall>().Mergers;
                for (int i = 0; i < mergers.Count; ++i)
                {
                    Destroy(mergers[i]);
                }
                Destroy(gameObject);
            }
        }
    }
}
