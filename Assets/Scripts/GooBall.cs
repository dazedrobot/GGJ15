using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GooBall : MonoBehaviour
{
    //Draggy Drop
    public GameObject BallRenderer;
    public GameObject LaneLine;
    //
    public List<GameObject> Knives;
    public List<GameObject> Mergers;
    public Vector3 TargetPosition;
    public Vector3 TargetScale;
    public bool g_phase = false;
    public float LerpTime = 1.0f;
    //
    private float m_duration = 0.0f;
    private float m_cooldown = 0.0f;
    private LineRenderer laneLineRenderer;
    private float lerpStartTime;
    private Vector3 lerpStartPos;
    private float lerpSizeStartTime;
    private Vector3 lerpStartSize;
    
	void Awake(){
		TargetScale = transform.localScale;
		TargetPosition = transform.position;
	}
    
	void Start () 
    {
        Knives = new List<GameObject>();
        Mergers = new List<GameObject>();
        laneLineRenderer = LaneLine.GetComponent<LineRenderer>();
        //
        UpdateLanes();
    }
    
	void Update () 
	{
        //lerp
        if (transform.position != TargetPosition)
        {
            float lerpPercentDone = (Time.time - lerpStartTime) / (LerpTime / TheManager.GAMESPEED);
            //avoid rounding errors
            if (lerpPercentDone > 1.0f) 
            {
                transform.position = TargetPosition;
            }
            else
            {
                transform.position = Vector3.Lerp(lerpStartPos, TargetPosition, lerpPercentDone);
            }
            UpdateLanes();
        }
        if (transform.localScale != TargetScale)
        {
            float lerpPercentDone = (Time.time - lerpSizeStartTime) / (LerpTime / TheManager.GAMESPEED);
            //avoid rounding errors
            if (lerpPercentDone > 1.0f)
            {
                transform.localScale = TargetScale;
            }
            else
            {
                transform.localScale = Vector3.Lerp(lerpStartSize, TargetScale, lerpPercentDone);
            }
            UpdateLanes();
        }


        if(g_phase == true)
            {
                m_duration += Time.deltaTime;
                if(m_duration >= 1.5f)
                {
                    g_phase = false;
                    m_cooldown = 0.3f;
                    BallRenderer.renderer.material.color = new Color(0.5f, 0.0f, 0.0f, 1.0f);
                }
            }
        if (m_cooldown > 0)
        {
            m_cooldown -= Time.deltaTime;
            if (m_cooldown < 0)
            {
                BallRenderer.renderer.material.color = new Color(0.0f, 0.5f, 0.0f, 1.0f);
                m_duration = 0.0f;
            }
        }
	}

    public void Move(Vector3 Position)
    {
        TargetPosition = Position;
        lerpStartTime = Time.time;
        lerpStartPos = transform.position;
    }
    public void MoveNow(Vector3 Position)
    {
        TargetPosition = Position;
        transform.position = Position;
    }

    public void Resize(Vector3 Scale)
    {
        TargetScale = Scale;
        lerpSizeStartTime = Time.time;
        lerpStartSize = transform.localScale;
    }
    public void ResizeNow(Vector3 Scale)
    {
        TargetScale = Scale;
        transform.localScale = Scale;
    }

	public void Phase()
    {
		g_phase = true;
		BallRenderer.renderer.material.color = new Color(0.5f, 0.0f, 0.5f, 0.8f);
	}

    private void UpdateLanes()
    {
		laneLineRenderer.SetPosition(0, new Vector3(transform.position.x + transform.localScale.x * 0.5f, 1, TheManager.GOOSTARTZ - 30.0f));
		laneLineRenderer.SetPosition(1, new Vector3(transform.position.x + transform.localScale.x * 0.5f, 1, TheManager.Road.transform.position.z+TheManager.Road.transform.localScale.z*5.0f));
    }
}