using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spike : MonoBehaviour
{
    public GooBall targetGoo;
    
    public void Start() {    }

    void Update() 
    {
        transform.Translate(new Vector3(0, 0, -5.0f * TheManager.GAMESPEED) * Time.deltaTime);
        if (transform.position.z < -20.0f)
        {
            Destroy(gameObject);
        }

        transform.localScale = new Vector3(targetGoo.TargetScale.x * 0.5f, targetGoo.TargetScale.y*0.5f, targetGoo.TargetScale.z * 0.25f);
        transform.position = new Vector3(targetGoo.transform.position.x, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.ToString() == "GooBall")
        {

            GooBall GB = collider.GetComponent<GooBall>();
            if (GB != targetGoo)
            {
                // Not our target
                return;
            }
            if (GB.g_phase == false)
            {
                FindObjectOfType<TheManager>().SplitGooBall(collider.gameObject);
                // Invalidate any other knived heading for this ball.
                List<GameObject> knives = collider.gameObject.GetComponent<GooBall>().Knives;
                for (int i = 0; i < knives.Count; ++i)
                {
                    Destroy(knives[i]);
                }
                Destroy(gameObject);
            }
        }
    }
}
