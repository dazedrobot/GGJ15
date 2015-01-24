using UnityEngine;
using System.Collections;

public class DontDestoy : MonoBehaviour 
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
