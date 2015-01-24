using UnityEngine;
using System.Collections;

public class LaneRenderer : MonoBehaviour {
	public GameObject road;
	public GameObject Manager;
	public GameObject[] Lines;
	public GameObject LinePrefab;
	private TheManager mgr;
	// Use this for initialization
	void Start () {
		mgr = Manager.GetComponent<TheManager> ();

	}

	// Update is called once per frame
	void Update () {
		Process ();
	}

	void Process(){
		//Get rid of all the lines, we could keep some of them but I aint coded that right now
		foreach(Transform child in transform) {
			Destroy(child.gameObject);
		}
		if (mgr.g_gooBalls.Count < 1) {
			return;		
		}
		//make the lines
		Lines = new GameObject[mgr.g_gooBalls.Count+1];
		Vector3 pos = road.transform.position;
		float scale = road.transform.localScale.x * 10;

		for(int i =0; i<Lines.Length; i++){
			Lines [i] =  (GameObject)Instantiate(LinePrefab, new Vector3(0, 0, 0), Quaternion.identity);
			Lines [i].transform.parent = transform;
		}

		Lines [0].transform.position = new Vector3(pos.x - (scale /2.0f),0,0);

		float prevX = pos.x - (scale / 2.0f);

		for(int i =1; i<Lines.Length; i++){
			prevX += mgr.g_gooBalls[i-1].transform.localScale.x;
			Lines [i].transform.position = new Vector3(prevX,0,0);
		}

		//Lines [2].transform.position = new Vector3(2,0,0);
	}
}
