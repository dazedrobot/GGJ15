using UnityEngine;
using System.Collections;

public class GooBall : MonoBehaviour
{
    private bool m_jump = true;
	void Start () 
	{
		
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