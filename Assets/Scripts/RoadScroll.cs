using UnityEngine;
using System.Collections;

public class RoadScroll : MonoBehaviour 
{
	public float scrollSpeed = 0.5F;
    public bool useGamespeed;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        float s = scrollSpeed;
        if (useGamespeed)
        {
            s *= TheManager.GAMESPEED;
        }
        float offset = Time.time * s;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        renderer.material.SetTextureOffset("_BumpMap", new Vector2(0, offset));
	}
}
