using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlimeBar : MonoBehaviour 
{
    public Slider g_slimeSlider;

	void Start () 
    {
       g_slimeSlider.value = 100.0f;
	}

    public void DisplayRemainingLife(float life)
    {
        g_slimeSlider.value = life;
    }
}
