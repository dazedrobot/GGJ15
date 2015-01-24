using UnityEngine;
using System.Collections;

public class GooballRender : MonoBehaviour 
{
    public float scrollSpeed = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        //transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), -40.0f * (10.0f / transform.localScale.x) * Time.deltaTime);
        float offset = Time.time * scrollSpeed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(0, -offset));
        renderer.material.SetTextureOffset("_BumpMap", new Vector2(0, -offset));

        transform.localScale = new Vector3(0.9f + (Mathf.Cos(Time.time+0.2f) * 0.1f), 1.0f + (Mathf.Sin(Time.time) * 0.2f), 0.9f + (Mathf.Cos(Time.time) * 0.1f));
    }
}
