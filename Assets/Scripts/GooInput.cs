using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GooInput : MonoBehaviour
{

	public static Dictionary<string, bool> buttonAvailable = {
		{"ButtonA"  , true},
		{"ButtonB"  , true},
		{"ButtonX"  , true},
		{"ButtonY"  , true}, 
		{"DPadUp"   , true},
		{"DPadLeft" , true},
		{"DPadRight", true},
		{"DPadDown" , true},
		{"ShoulderL", true},
		{"ShoulderR", true},
		{"ThumbL"   , true},
		{"ThumbR"   , true},
		{"Back"     , true},
		{"Start"    , true},
	
	};

	public static

	bool isAxis = true;
	string selectedInput = "";

	// Use this for initialization
	void Start ()
	{
	
	}

	void OnEnable() {
		isAxis = true;
		selectedInput = "";
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (selectedInput == "") {
			foreach (string buttonName in buttonAvailable.Keys) {
				if (buttonAvailable[buttonName] && Input.GetButtonDown(buttonName)) {
					selectedInput = buttonName;
					isAxis = false;
					buttonAvailable[buttonName] = false;
					return;
				}
			}

		} else {
			// trigger jump here.
		}

	}
}
