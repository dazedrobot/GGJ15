﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Merger : MonoBehaviour 
{
    public GameObject g_largeGooBall;
    public GooBall g_smallGooBall;
    public float direction = 1;
	void Start ()
    {
	}
	
	void Update () 
    {
        transform.Translate(new Vector3(0, 0, -5.0f * TheManager.GAMESPEED) * Time.deltaTime);
        if (transform.position.z < -20)
        {
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(direction * g_smallGooBall.TargetScale.x * 0.5f, g_smallGooBall.TargetScale.y * 0.5f, g_smallGooBall.TargetScale.z * 0.25f);
        transform.position = new Vector3(g_smallGooBall.transform.position.x, transform.position.y, transform.position.z);
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.ToString() == "GooBall")
        {
            GooBall GB = collider.GetComponent<GooBall>();
            if (GB != g_smallGooBall)
            {
                //not our target
                return;
            }
            if (GB.g_phase == true)
            {
                FindObjectOfType<TheManager>().MergeGooBall(collider.gameObject, g_largeGooBall);
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
