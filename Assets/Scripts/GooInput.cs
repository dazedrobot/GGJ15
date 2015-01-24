using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GooInput : MonoBehaviour
{

	public static Dictionary<string, bool> buttonAvailable = new Dictionary<string, bool>{
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

	public static Dictionary<string, bool> axisAvailable = new Dictionary<string, bool>{
		{"LVertical:-"  , true},
		{"LVertical:+"  , true},
		{"LHorizontal:-", true},
		{"LHorizontal:+", true},
		{"RVertical:-"  , true},
		{"RVertical:+"  , true},
		{"RHorizontal:-", true},
		{"RHorizontal:+", true},
	};

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
			foreach (string axisDirName in axisAvailable.Keys) {
				var axisSplit = axisDirName.Split(':');
				string axisName = axisDirName[0];
				string axisDir =  axisSplit[1];
				if (Input.GetAxis(axisName)){
				}
			}

		} else {
			// trigger jump here.
		}

	}
}
