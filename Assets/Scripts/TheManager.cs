using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheManager : MonoBehaviour 
{
	public static int g_numGoo = 128;
	public GameObject g_gooBallPrefab;
    public GameObject g_gooBallContainer;
	public Stack<GameObject> g_gooBallsPool;
    public List<GameObject> g_gooBalls;
	public GameObject LaneRendererObj;
	public GameObject SpikePrefab;
	private LaneRenderer LR;
	
	void Start () 
	{
        g_gooBallContainer = new GameObject("GooBallContainer");
        g_gooBallsPool = new Stack<GameObject>();
        for (int i = 0; i < g_numGoo; ++i)
        {
            GameObject go = Instantiate(g_gooBallPrefab, new Vector3((float)i, 0.0f, 0.0f), Quaternion.identity) as GameObject;
            go.transform.SetParent(g_gooBallContainer.transform);
            go.SetActive(false);
            g_gooBallsPool.Push(go);
        }
        g_gooBalls.Add(g_gooBallsPool.Pop());
        g_gooBalls[0].SetActive(true);
        g_gooBalls[0].transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		LR = LaneRendererObj.GetComponent<LaneRenderer>();
		LR.Process ();
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
        g_gooBalls[index].transform.position = go.transform.position - new Vector3(go.transform.localScale.x / 2.0f, 0.0f, 0.0f);
        go.transform.position += new Vector3(go.transform.localScale.x / 2.0f, 0.0f, 0.0f);
		LR.Process ();
	}

	private struct intTouple{
		public int x;
		public int y;
		public intTouple(int x, int y) 
		{
			this.x = x;
			this.y = y;
		}
	};

	public void MakeSpike()
	{
		List<intTouple> things = new List<intTouple>();
		for (int i = 0; i < g_gooBalls.Count; ++i)
		{
			//check above
			if(i>0){
				if(g_gooBalls[i-1].transform.localScale.x == 0){
					things.Add(new intTouple(i, i-1));
				}
			}
			//check below
			if(i<g_gooBalls.Count){
				if(g_gooBalls[i+1].transform.localScale.x == 0){
					things.Add(new intTouple(i, i+1));
				}
			}
		}
		int r = Random.Range(0, things.Count);
		GameObject newSpike =  (GameObject)Instantiate(SpikePrefab, new Vector3(g_gooBalls [things [r].x].transform.position.x, 10, 0), Quaternion.identity);
		newSpike.GetComponent<Spike>().targetGoo = g_gooBalls[things[r].y];
		new Spike ();

	}
}
