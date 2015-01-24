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
    public GameObject MergerPrefab;
    private LaneRenderer LR;
    
    void Start () 
    {
        g_gooBallContainer = new GameObject("GooBallContainer");
        g_gooBallsPool = new Stack<GameObject>();
        for (int i = 0; i < g_numGoo; ++i)
        {
            GameObject go = Instantiate(g_gooBallPrefab, new Vector3((float)i, 0.0f, 0.0f), g_gooBallPrefab.transform.localRotation) as GameObject;
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
        if (Input.GetKeyDown (KeyCode.M)) 
        {
            MakeMerger();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            MakeSpike();
        }
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

    public void MergeGooBall(GameObject small, GameObject large)
    {
        Debug.Log("Merging ..");
        large.transform.localScale += small.transform.localScale;
        if (large.transform.position.x > small.transform.position.x)
                large.transform.position -= new Vector3 (small.transform.localScale.x * 0.5f, 0.0f, 0.0f);
        else
                large.transform.position += new Vector3 (small.transform.localScale.x * 0.5f, 0.0f, 0.0f);
        small.SetActive (false);
        g_gooBallsPool.Push (small);
        g_gooBalls.RemoveAt (g_gooBalls.IndexOf (small));
        LR.Process();
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
        int r = Random.Range(0, g_gooBalls.Count-1);
        GameObject newSpike =  (GameObject)Instantiate(SpikePrefab, new Vector3(g_gooBalls [r].transform.position.x, 0, 15), Quaternion.identity);
        newSpike.transform.localScale = new Vector3(g_gooBalls[r].transform.localScale.x * 0.5f, 5, g_gooBalls[r].transform.localScale.z * 0.25f);
        g_gooBalls[r].GetComponent<GooBall>().Knives.Add(newSpike);
    }

    public void MakeMerger()
    {
        List<intTouple> things = new List<intTouple>();
        for (int i = 0; i < g_gooBalls.Count; ++i)
        {
            //check above
            if(i>0){
                if(g_gooBalls[i-1].transform.localScale.x >= g_gooBalls[i].transform.localScale.x){
                    things.Add(new intTouple(i, i-1));
                }
            }
            //check below
            if(i<g_gooBalls.Count-1){
                if(g_gooBalls[i+1].transform.localScale.x >= g_gooBalls[i].transform.localScale.x){
                    things.Add(new intTouple(i, i+1));
                }
            }
        }
        if (things.Count < 1) {
            print("No availaible space for merger");
            return;		
        }
        int r = Random.Range(0, things.Count-1);
        GameObject newMerger =  (GameObject)Instantiate(MergerPrefab, new Vector3(g_gooBalls [things [r].x].transform.position.x, 0, 15), Quaternion.identity);
        newMerger.GetComponent<Merger>().g_largeGooBall = g_gooBalls[things[r].y];
        newMerger.transform.localScale = new Vector3(g_gooBalls[things[r].x].transform.localScale.x * 0.5f, 5, g_gooBalls[things[r].x].transform.localScale.z * 0.25f);
        g_gooBalls[things[r].x].GetComponent<GooBall>().Mergers.Add(newMerger);
        g_gooBalls[things[r].y].GetComponent<GooBall>().Mergers.Add(newMerger);
    }
}
