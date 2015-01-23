using UnityEngine;
using System.Collections;

public class LaneRenderer : MonoBehaviour {
	public GameObject road;
	public GameObject[] Goos;
	public GameObject[] Lines;
	public GameObject LinePrefab;
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
		foreach(Transform child in transform) {
			Destroy(child);
		}
		//make the lines
		Lines = new GameObject[55];
		Vector3 pos = road.transform.position;
		float scale = road.transform.localScale.x;

		for(int i =0; i<Lines.Length; i++){
			Lines [i] = (GameObject)Instantiate(LinePrefab, new Vector3(0, 0, 0), Quaternion.identity);
		}
		//Lines [0].transform.position.Set(new Vector3 (0, 1, -10));
		//Lines [1].transform.position.Set(new Vector3 (3, 1, -10));
	}
}
