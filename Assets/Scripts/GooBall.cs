using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GooBall : MonoBehaviour
{
    public List<GameObject> Knives;
    public List<GameObject> Mergers;
    private bool m_jump = true;
    // Use this for initialization
    void Start () 
    {
        Knives = new List<GameObject>();
        Mergers = new List<GameObject>(); 
    }
    
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
            if(m_jump == true)
            {
                m_jump = false;
                rigidbody.AddForce(new Vector3(0.0f, 500.0f, 0.0f));
            }
		}
	}

    void OnCollisionEnter(Collision col)
    {
        m_jump = true;
    }
}