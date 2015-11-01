using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour {

	private string battle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClickSetBattleBB(){
		battle = "bb";
	}

	public void onClickSetBattleFB(){
		battle = "fb";
	}

	public void onClickLoadBattle(){
		if (battle == "bb") {
			Application.LoadLevel("PDI");
		} else if (battle == "fb") {
			Application.LoadLevel("PI");
		}
	}
}
