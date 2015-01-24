using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheManager : MonoBehaviour 
{
	public static int g_numGoo = 128;
	public GameObject g_gooBallPrefab;
	public Stack<GameObject> g_gooBallsPool;
    public List<GameObject> g_gooBalls;

	void Start () 
	{
        g_gooBallsPool = new Stack<GameObject>();
        for (int i = 0; i < g_numGoo; ++i)
        {
            GameObject go = Instantiate(g_gooBallPrefab, new Vector3((float)i, 0.0f, 0.0f), Quaternion.identity) as GameObject;
            go.SetActive(false);
            g_gooBallsPool.Push(go);
        }
        g_gooBalls.Add(g_gooBallsPool.Pop());
        g_gooBalls[0].SetActive(true);
        g_gooBalls[0].transform.position = new Vector3(0.0f, 0.0f, 0.0f);
	}

	void Update () 
	{
	
	}

	public void SplitGooBall(GameObject go)
	{
		Debug.Log("SplitManager");
		int index = g_gooBalls.IndexOf(go);
        g_gooBalls.Insert(index, g_gooBallsPool.Pop());
        g_gooBalls[index].SetActive(true);
        // Scale
        go.transform.localScale /= 2.0f;
		g_gooBalls[index].transform.localScale = go.transform.localScale;
        // Reposition
        g_gooBalls[index].transform.position = go.transform.position + new Vector3(go.transform.localScale.x / 2.0f, 0.0f, 0.0f);
        go.transform.position -= new Vector3(go.transform.localScale.x / 2.0f, 0.0f, 0.0f);
        
	}
}
