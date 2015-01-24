using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GooBall : MonoBehaviour
{
    public List<GameObject> Knives;
    public List<GameObject> Mergers;
    public bool g_phase = false;
    private float m_duration = 0.0f;
    private float m_cooldown = 0.0f;
    
    
    void Start () 
    {
        Knives = new List<GameObject>();
        Mergers = new List<GameObject>(); 
    }
    
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
            g_phase = true;
            renderer.material.color = new Color(0.8f, 0.0f, 0.8f, 0.5f);
		}
        if(g_phase == true)
            {
                m_duration += Time.deltaTime;
                if(m_duration >= 2.0f)
                {
                    g_phase = false;
                    m_duration = 0.0f;
                    renderer.material.color = new Color(0.0f, 0.8f, 0.0f, 1.0f);
                }
            }
        if (m_cooldown > 0)
        {
            m_cooldown -= Time.deltaTime;
        }
	}
}