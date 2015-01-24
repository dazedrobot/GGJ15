using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spike : MonoBehaviour
{
    public void Start() { }
    void Update() { }
    void FixedUpdate()
    {
        this.transform.Translate(new Vector3(0, 0, -10.0f) * Time.deltaTime);
        if (transform.position.z < -20)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.tag.ToString() == "GooBall")
    {
        if (collider.GetComponent<GooBall>().g_phase == false)
        {
            FindObjectOfType<TheManager>().SplitGooBall(collider.gameObject);
            //Invalidate any other knived heading for this ball.
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
