using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GooBall : MonoBehaviour
{
    public List<GameObject> Knives;
    public List<GameObject> Mergers;
    // Use this for initialization
    void Start () 
    {
        Knives = new List<GameObject>();
        Mergers = new List<GameObject>(); 
    }
    
    // Update is called once per frame
    void Update () 
    {
        if (Input.GetMouseButtonDown (0)) 
        {
            //rigidbody.AddForce(new Vector3(0.0f, 100.0f, 0.0f));
            //Debug.Log("Click");
        }
    }
}