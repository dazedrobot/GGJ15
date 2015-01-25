using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheManager : MonoBehaviour 
{
    //things that need to be draggy dropped:
    public GameObject g_gooBallPrefab;
    public GameObject SpikePrefab;
    public GameObject MergerPrefab;
    public static GameObject Road;
	public GameObject Road2;
    //
    public static int g_numGoo = 128;
    public GameObject g_gooBallContainer;
    public Stack<GameObject> g_gooBallsPool;
    public List<GameObject> g_gooBalls;
    public GameObject g_slimeBar;

    // Sounds
    private AudioSource source;
    public AudioClip splitSound;
    public AudioClip deathSound;
    public AudioClip mergeSound;

	public static float GOOSTARTZ = -20.0f;
    public static float GAMESPEED = 1.0f;
	
	public float spawnChance = 0.01f;
	public float spawnSplit = 0.7f;
    
    void Start () 
    {
		Road = Road2;

        //create stack of unused goo
        g_gooBallContainer = new GameObject("GooBallContainer");
        g_gooBallsPool = new Stack<GameObject>();
        for (int i = 0; i < g_numGoo; ++i)
        {
            GameObject go = Instantiate(g_gooBallPrefab, new Vector3((float)i, 0.0f, 0.0f), g_gooBallPrefab.transform.localRotation) as GameObject;
            go.transform.SetParent(g_gooBallContainer.transform);
            go.SetActive(false);
            g_gooBallsPool.Push(go);
        }

        //Create the initial Goo
        g_gooBalls.Add(g_gooBallsPool.Pop());
        g_gooBalls[0].SetActive(true);
		g_gooBalls[0].transform.position = new Vector3(0.0f, g_gooBalls[0].transform.localScale.x * 0.5f, GOOSTARTZ);
        UpdatePositions();

        source = GetComponent<AudioSource>();
    }

    void Update () 
    {
        if (Random.Range(0f, 1f) < spawnChance * (GAMESPEED))
        {
			if (Random.Range(0f, 1f) < spawnSplit) {
				MakeSpike();
			}
			else {
				MakeMerger();
			}
		}

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
        source.PlayOneShot(splitSound);
        if (go.transform.localScale.x * 0.5f > 0.5f)
        {
            int index = g_gooBalls.IndexOf(go);
            g_gooBalls.Insert(index, g_gooBallsPool.Pop());
            GameObject newGoo = g_gooBalls[index];
            newGoo.SetActive(true);

            // Half the scale of the original goo
            GooBall GB1 = go.GetComponent<GooBall>();
			Vector3 targetScale = GB1.TargetScale / 2.0f;

			GB1.ResizeNow(targetScale);

            // Set the new goo to also be that size
            GooBall GB2 = newGoo.GetComponent<GooBall>();
			GB2.ResizeNow(targetScale);
            
            // Reposition new goo to be in the same position as the original
            newGoo.transform.position = go.transform.position;
        }
        else // Not enough goo!
        {
            go.SetActive(false);
            g_gooBallsPool.Push(go);
            g_gooBalls.Remove(go);

            float temp = 0.0f;
            for (int i = 0; i < g_gooBalls.Count; ++i )
            {
                temp += g_gooBalls[i].transform.localScale.x;
            }
            Debug.Log("Display Re");
            g_slimeBar.GetComponent<SlimeBar>().DisplayRemainingLife(temp);
        }
        UpdatePositions();
    }

    public void MergeGooBall(GameObject small, GameObject large)
    {
        GooBall GBlarge = large.GetComponent<GooBall>();
        GooBall GBsmall = small.GetComponent<GooBall>();

        //Increase the size of the larger goo
        GBlarge.Resize(GBlarge.TargetScale + GBsmall.TargetScale);

        //Delete any spikes or mergers heading for the soon to be deleted goo
        for (int i = 0; i < GBsmall.Knives.Count; ++i)
        {
            Destroy(GBsmall.Knives[i]);
        }
        for (int i = 0; i < GBsmall.Mergers.Count; ++i)
        {
            Destroy(GBsmall.Mergers[i]);
        }

        //delete small goo.
		small.GetComponent<GooInput>().releaseInput();
        small.SetActive (false);
        g_gooBallsPool.Push (small);
        g_gooBalls.Remove (small);
        UpdatePositions();
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
        //Find a random target
        int r = Random.Range(0, g_gooBalls.Count);
        //Make a new spike
		GameObject newSpike =  (GameObject)Instantiate(SpikePrefab, new Vector3(g_gooBalls [r].transform.position.x, g_gooBalls [r].transform.position.y, (Road.transform.position.z+Road.transform.localScale.z*5.0f)), Quaternion.identity);
        GooBall GB = g_gooBalls[r].GetComponent<GooBall>();
        //make it the spike the same size of the goo
        newSpike.transform.localScale = new Vector3(GB.TargetScale.x * 0.5f, 5, GB.TargetScale.z * 0.25f);
        //Tell the Goo about the incomming knive
        GB.Knives.Add(newSpike);
        //Tell the knive who to target.
        newSpike.GetComponent<Spike>().targetGoo = GB;
    }

    public void MakeMerger()
    {
        //Get a list of comaptacble merge pairs.
        List<intTouple> things = new List<intTouple>();
        for (int i = 0; i < g_gooBalls.Count; ++i)
        {
            GooBall GB1 = g_gooBalls[i].GetComponent<GooBall>();
            //check above
            if(i>0){
                if (g_gooBalls[i - 1].GetComponent<GooBall>().TargetScale.x >= GB1.TargetScale.x)
                {
                    things.Add(new intTouple(i, i-1));
                }
            }
            //check below
            if(i<g_gooBalls.Count-1){
                if (g_gooBalls[i + 1].GetComponent<GooBall>().TargetScale.x >= GB1.TargetScale.x)
                {
                    things.Add(new intTouple(i, i+1));
                }
            }
        }
        if (things.Count < 1) {
            return;		
        }
        //pick a random pair
        int r = Random.Range(0, things.Count);
        //make a merger
		GameObject newMerger =  (GameObject)Instantiate(MergerPrefab, new Vector3(g_gooBalls [things [r].x].transform.position.x, g_gooBalls [things [r].x].transform.position.y, (Road.transform.position.z+Road.transform.localScale.z*5.0f)), Quaternion.identity);
        //make the merger the size of the small goo
        newMerger.transform.localScale = new Vector3(g_gooBalls[things[r].x].transform.localScale.x * 0.5f, 5, g_gooBalls[things[r].x].transform.localScale.z * 0.25f);
        //point the merger target to the big goo.
        newMerger.GetComponent<Merger>().g_largeGooBall = g_gooBalls[things[r].y];
        //tell the merger about which goo to head for
        newMerger.GetComponent<Merger>().g_smallGooBall = g_gooBalls[things[r].x].GetComponent<GooBall>();
        //tell the small goo about the incomming merger.
        g_gooBalls[things[r].x].GetComponent<GooBall>().Mergers.Add(newMerger);
    }

    public void UpdatePositions()
    {
        //get the top edge of the road as the starting point
        float currentPos = Road.transform.position.x - ((Road.transform.localScale.x * 10.0f) / 2.0f);
        for (int i = 0; i < g_gooBalls.Count; ++i)
        {
            GooBall GB = g_gooBalls[i].GetComponent<GooBall>();
            float scale = GB.TargetScale.x * 0.5f;
			GB.Move(new Vector3(currentPos + scale, scale, GOOSTARTZ));
            currentPos += scale * 2.0f;
        }
    }
}
