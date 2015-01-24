using UnityEngine;
using System.Collections;

public class RoadScroll : MonoBehaviour 
{
	public float scrollSpeed = 0.5F;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
		float offset = Time.time * scrollSpeed;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        renderer.material.SetTextureOffset("_BumpMap", new Vector2(0, offset));
	}
}
