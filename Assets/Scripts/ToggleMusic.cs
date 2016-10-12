using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ToggleMusic : MonoBehaviour {
	
	
	public Toggle musicToggle; //Drag and drop your toggle game object here or you can even get a reference to this in Start()
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void SetMusic()
	{
		if(musicToggle.isOn) {
			// Set music On
			gameObject.GetComponent<AudioSource>().Play();

		} else {
			// Set music Off
			gameObject.GetComponent<AudioSource>().Stop();
		}
	}
}
