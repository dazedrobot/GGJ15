using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ControllerUI : MonoBehaviour {

	public GameObject unavailableContainer;
	Dictionary<string, Image> unavailableSprites;

	// Use this for initialization
	void Start () {
		unavailableSprites = new Dictionary<string, Image> ();
		foreach (Image i in unavailableContainer.GetComponentsInChildren<Image>()) {
			unavailableSprites.Add(i.name, i);
		}

	}
	
	// Update is called once per frame
	void Update () {
		UpdateAvailableButtons ();
	}

	void UpdateAvailableButtons () {
		foreach (string k in unavailableSprites.Keys) {
			bool available = (k.Split (':').Length > 1) ? GooInput.axisAvailable[k] : GooInput.buttonAvailable[k];
			unavailableSprites[k].enabled = !available;
		}
	}
}
