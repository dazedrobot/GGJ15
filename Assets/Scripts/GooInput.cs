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
		{"Triggers:-", true},
		{"Triggers:+", true},
		{"DPadH:-", true},
		{"DPadH:+", true},
		{"DPadV:-", true},
		{"DPadV:+", true},
	};
	public bool isAxis = true;
	public string selectedInput = "";

	// Use this for initialization
	void Start ()
	{
	
	}

	void OnEnable ()
	{
		isAxis = true;
		selectedInput = "";
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (selectedInput == "") {
		foreach (string buttonName in buttonAvailable.Keys) {
			if (buttonAvailable [buttonName] && Input.GetButtonDown (buttonName)) {
				selectedInput = buttonName;
				isAxis = false;
				buttonAvailable[buttonName] = false;
				return;
			}
		}
		foreach (string axisDirName in axisAvailable.Keys) {
			if (axisAvailable[axisDirName]) {
				var axisSplit = axisDirName.Split (':');
				string axisName = axisSplit [0];
				bool axisPositive = axisSplit [1] == "+";
				float axisInput = Input.GetAxis (axisName);
				if (axisInput > 0.5f && axisPositive) {
					selectedInput = axisDirName;
					isAxis = true;
					axisAvailable [axisDirName] = false;
					return;

				} else if (axisInput < -0.5 && !axisPositive) {
					selectedInput = axisDirName;
					isAxis = true;
					axisAvailable [axisDirName] = false;
					return;

				}
			}
		}

//		} else {
//			// trigger jump here.
//			if (Input.GetButtonDown("Cancel")) {
//				selectedInput = "";
//			}
//		}

	}
}
