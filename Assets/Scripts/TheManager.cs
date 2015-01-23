using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheManager : MonoBehaviour 
{
	public static int g_numGoo = 128;
	public GameObject g_gooBallPrefab;
	public List<GameObject> g_gooBalls;

	void Start () 
	{
		g_gooBalls = new List<GameObject>(); 
		for (int i = 0; i < g_numGoo; ++i) 
		{
			GameObject go = Instantiate(g_gooBallPrefab, new Vector3((float)i, 0.0f, 0.0f), Quaternion.identity) as GameObject;
			g_gooBalls.Add(go);
			g_gooBalls[i].gameObject.SetActive(false);
		}
		g_gooBalls[0].gameObject.SetActive(true);
		g_gooBalls[0].gameObject.transform.position = new Vector3 (2.0f, 0.0f, 0.0f);
	}

	void Update () 
	{
	
	}

	public void SplitGooBall(GameObject go)
	{
		Debug.Log("SplitManager");
		int index = g_gooBalls.IndexOf(go) + 1;
		g_gooBalls[index].SetActive(true);
		g_gooBalls[index].transform.localScale = go.transform.localScale;
		g_gooBalls[index].transform.position = go.transform.position + new Vector3(0.0f, 0.0f, 0.0f);
	}
}
