using UnityEngine;
using System.Collections;

public class GooBall : MonoBehaviour
{
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			rigidbody.AddForce(new Vector3(0.0f, 10.0f, 10000.0f));
			Debug.Log("Click");
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag.ToString() == "Spike") 
		{
			gameObject.transform.localScale = gameObject.transform.localScale / 2;
			GameObject.FindWithTag("TheManager").GetComponent<TheManager>().SplitGooBall(this.gameObject);

			Debug.Log("SPLIT");
		}
	}
}