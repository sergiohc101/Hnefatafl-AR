using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PanelScript : MonoBehaviour {

	[Tooltip("TabPanel 1")]
	public Transform tabPanel1;
	[Tooltip("TabPanel 2")]
	public Transform tabPanel2;
	[Tooltip("TabPanel 3")]
	public Transform tabPanel3;
	[Tooltip("TabPanel 4")]
	public Transform tabPanel4;

	private short activePanel;
	private Transform[] panels;

	void Start () {
		if(!tabPanel1 || !tabPanel2 || !tabPanel3 || !tabPanel4)
			Debug.LogWarning("Select all four Tab Panels! ");
		else{
			activePanel = 1;
			panels = new Transform[4];

			panels[0] = tabPanel1;
			panels[1] = tabPanel2;
			panels[2] = tabPanel3;
			panels[3] = tabPanel4;
		}
	}

	void ShowTab(int tabIndex)
	{
		Debug.Log ("TAB = " + tabIndex);

		//Transform ScrollRect = new Transform;
		//Transform PageSelector = new Transform;
		for(int i = 0; i < 4; i++) {
			if(panels[i] != panels[tabIndex-1]){								//DISABLING...
				panels[i].GetComponent<Image>().enabled = false; 				//Panel Image
				panels[i].GetChild(0).GetChild(0).gameObject.SetActive(false); 	//ScrollRectContainer
				panels[i].GetChild(1).gameObject.SetActive(false);				//PageSelector
				//ScrollRect.GetComponent<Image>().enabled = false;
				//PageSelector.gameObject.SetActive(false);
				//panels[i].gameObject.SetActive(false);
			}
		}
		panels[tabIndex-1].GetComponent<Image>().enabled = true; 				//Panel Image
		panels[tabIndex-1].GetChild(0).GetChild(0).gameObject.SetActive(true);	//ScrollRectContainer
		panels[tabIndex-1].GetChild(1).gameObject.SetActive(true);				//PageSelector
	}
	


}


