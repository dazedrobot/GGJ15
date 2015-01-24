using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlimeBar : MonoBehaviour 
{
    public Slider slimeSlider;

	void Start () 
    {
        slimeSlider.value = 1;
	}
	
	void Update ()
    {
        slimeSlider.value += 10 * Time.smoothDeltaTime;
	}
}
