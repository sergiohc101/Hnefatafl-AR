using UnityEngine;
using System.Collections;
//using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TabScript : MonoBehaviour, IPointerClickHandler{

	[Tooltip("MainPanel")]
	public Transform panel;
	[Tooltip("Tab Index")]
	public int index;

	public void OnPointerClick(PointerEventData eventData)
	{
		panel.SendMessage("ResetActiveTab",index);
	}
}
