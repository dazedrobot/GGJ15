using UnityEngine;
using System.Collections;

public class ChangeMusic : MonoBehaviour {

    public AudioClip music;
    private AudioSource source;
	
	void Awake () 
    {
        source = GetComponent<AudioSource>();
	}
    void OnLevelWasLoaded(int level)
    {
        if (level > 0)
        {
            source.clip = music;
            source.Play();
        }
    }
}
