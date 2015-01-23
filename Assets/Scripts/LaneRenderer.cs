using UnityEngine;
using System.Collections;

public class LaneRenderer : MonoBehaviour {
	public GameObject road;
	public GameObject[] Goos;
	public LineRenderer[] LRS;
	private int cool = 1;
	// Use this for initialization
	void Start () {
		Process ();
		Goos = new GameObject[2];
	}
	
	// Update is called once per frame
	void Update () {
		//Goos.Length
	}

	void Process(){
		//Get rid of all the lines, we could keep some of them but I aint coded that right now
		LineRenderer[] lrs = GetComponents<LineRenderer> ();
		foreach(LineRenderer lr in lrs){
			Destroy(lr);
		}
		//make the lines
		LRS = new LineRenderer[cool];
		Vector3 pos = road.transform.position;
		float scale = road.transform.localScale.x;
		for(int i =0; i<LRS.Length; i++){
			LRS [i] = gameObject.AddComponent<LineRenderer>();
		}
		LRS [0].SetPosition (0, new Vector3 (0, 1, -10));
		LRS [0].SetPosition (0, new Vector3 (0, 1, 10));



	}
}
