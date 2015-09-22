using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	public GameObject menu;
	private RectTransform granpa;
	private bool isShowing;

	void Awake(){
		Debug.Log ("Awakeed!");
	}

	// Use this for initialization
	void Start () {



	}

	// Update is called once per frame
	void FixedUpdate () {
		
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Space))
		{
			isShowing = !isShowing;
			menu.SetActive(isShowing);
		}
		else if(Input.GetKeyUp(KeyCode.Escape))
		{
			Debug.Log ("Escape!! ");	
			menu.SetActive(true);
		}
	}
}
