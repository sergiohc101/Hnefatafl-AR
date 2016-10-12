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

	private int activePanel;
	private Transform[] panels;

	void Start () {
		if(!tabPanel1 || !tabPanel2 || !tabPanel3 || !tabPanel4)
			Debug.LogWarning("Select all four Tab Panels! ");
		else{
			activePanel = -1;
			panels = new Transform[4];

			panels[0] = tabPanel1;
			panels[1] = tabPanel2;
			panels[2] = tabPanel3;
			panels[3] = tabPanel4;
		}
		ShowTab(1);
	}

	void ResetActiveTab(int tabIndex)
	{
		if(tabIndex == activePanel){							//ACTIVE TAB was clicked
			panels[tabIndex-1].GetChild(0).SendMessage("ResetPage");	
			//Debug.Log ("SAME PANEL SELECTED: " + tabIndex);
			return;
		}
		else ShowTab(tabIndex);
	}

	void ShowTab(int tabIndex)
	{
		Transform ScrollRectContainer;

		if(tabIndex == -1){	 										//LAST TAB								
			for(int j = 0; j < 4; j++)
				panels[j].GetChild(0).SendMessage("ResetPage");		//RESET ALL SCROLLRECTS
			tabIndex = 1;
		}

		for(int i = 0; i < 4; i++) {
			if(panels[i] != panels[tabIndex-1]){								//DISABLING...
				panels[i].GetComponent<Image>().enabled = false; 				//Panel Image
				ScrollRectContainer = panels[i].GetChild(0).GetChild(0);		//ScrollRectContainer
				for(int j= 0; j < ScrollRectContainer.childCount; j++)
					ScrollRectContainer.GetChild(j).GetComponent<Image>().enabled = false; 	//Images
				panels[i].GetChild(1).gameObject.SetActive(false);				//PageSelector
			}
		}
		//ENABLE SELECTED PANEL
		panels[tabIndex-1].parent.SetAsLastSibling();							//TabPanel
		panels[tabIndex-1].GetComponent<Image>().enabled = true; 				//Panel Image
		ScrollRectContainer = panels[tabIndex-1].GetChild(0).GetChild(0);		//ScrollRectContainer
		for (int j= 0; j < ScrollRectContainer.childCount; j++)
			ScrollRectContainer.GetChild(j).GetComponent<Image>().enabled = true;//Images
		panels[tabIndex-1].GetChild(1).gameObject.SetActive(true);				//PageSelector

		activePanel = tabIndex;
		//Debug.Log ("Active Panel = " + activePanel);
	}
	


}


