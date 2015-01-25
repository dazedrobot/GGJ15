using UnityEngine;
using System.Collections;

public class ChangeMusic : MonoBehaviour 
{

    public AudioClip titleMusic;
	public AudioClip levelMusic;
	private AudioSource source;

	void Awake () 
    { 
       source = GetComponent<AudioSource>();
	}

    void OnLevelWasLoaded(int level)
    {
        if (level == 0 || level == 2)
        {
            source.clip = titleMusic;
            source.Play();
        }

		else if (level == 1)
		{
			source.clip = levelMusic;
			source.Play ();
	    }
	}
}
