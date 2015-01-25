using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GooInput : MonoBehaviour
{
    public Image uiImage;
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
		{"LVertical:-"  , true}, //up
		{"LVertical:+"  , true},
		{"LHorizontal:-", true},  //left
		{"LHorizontal:+", true},
		{"RVertical:-"  , true},
		{"RVertical:+"  , true},
		{"RHorizontal:-", true},
		{"RHorizontal:+", true},
		{"Triggers:-", true}, //right trigger
		{"Triggers:+", true},
		{"DPadH:-", true},
		{"DPadH:+", true},
		{"DPadV:-", true},
		{"DPadV:+", true},
	};

    public Sprite[] buttonSprites;

    private Dictionary<string, int> spriteLookup;

	public bool isAxis = false;
	public string selectedInput = "";

	private GooBall ballComponent;
	private bool isAxisDown = false;

	// Use this for initialization
	void Start ()
	{
		ballComponent = GetComponent<GooBall> ();
        spriteLookup = new Dictionary<string, int>();
        for (int i = 0; i < buttonSprites.Length; ++i)
        {
            spriteLookup.Add(buttonSprites[i].name, i);
        }
	}

	void OnDisable ()
	{
		if (selectedInput == "") {
			return;
		}
		if (isAxis) {
			axisAvailable[selectedInput] = true;
		}
		else {
			buttonAvailable[selectedInput] = true;
		}
		isAxis = false;
		selectedInput = "";
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.P))
        {
            ballComponent.Phase();
        }

		if (selectedInput == "") {
			foreach (string buttonName in buttonAvailable.Keys) {
				if (buttonAvailable [buttonName] && Input.GetButtonDown (buttonName)) {
					selectedInput = buttonName;
                    uiImage.sprite = buttonSprites[spriteLookup[buttonName]];
					isAxis = false;
					buttonAvailable [buttonName] = false;
					return;
				}
			}
			foreach (string axisDirName in axisAvailable.Keys) {
				if (axisAvailable [axisDirName]) {
					var axisSplit = axisDirName.Split (':');
					string axisName = axisSplit [0];
					bool axisPositive = axisSplit [1] == "+";
					float axisInput = Input.GetAxis (axisName);
					if (((axisInput > 0.5f && axisPositive) || (axisInput < -0.5f && !axisPositive))) {
						isAxisDown = true;
						selectedInput = axisDirName;
						isAxis = true;
						axisAvailable [axisDirName] = false;
                        uiImage.sprite = buttonSprites[spriteLookup[axisDirName]];
						return;
					}
					else {

					}
				}
			}

		} else {
			// trigger jump here.
			if (isAxis) {
				var axisSplit = selectedInput.Split (':');
				string axisName = axisSplit [0];
				bool axisPositive = axisSplit [1] == "+";
				float axisInput = Input.GetAxis (axisName);
				if (((axisInput > 0.5f && axisPositive) || (axisInput < -0.5f && !axisPositive)) && !isAxisDown) {
					isAxisDown = true;
					ballComponent.Phase();
				}
				else if (((axisInput <= 0.5f && axisPositive) || (axisInput >= -0.5f && !axisPositive)) && isAxisDown) {
					isAxisDown = false;
				}

			}
			else {
				if (Input.GetButtonDown(selectedInput)) {
					ballComponent.Phase();
				}
			}
		}

	}
}
