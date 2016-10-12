using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleManager : MonoBehaviour {

	private BTManager BManager;
	public GameObject PersistantManager;

	public GameObject BTDevices;
	public GameObject BTButton;

	private string battle;
	private int prevCount = 0;


	private string [] devices = {};

	// Use this for initialization
	void Start () {
		BManager =  PersistantManager.GetComponent<BTManager>();

		Button[] btns = BTDevices.GetComponentsInChildren<Button>();
		foreach (Button item in btns) {
			GameObject.Destroy(item.gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {
		int devCount = 0;
		if(BManager.BTState == BTManager.STATE.DISCOVERING){
			devices = BManager.returnDiscoveredDevices();
			for (int i = 0; i< devices.Length ; i++) {
				if (devices[i] != null) devCount++;
			}
		
			BManager.count = devCount.ToString();

			if(devCount != prevCount){
				Button[] btns = BTDevices.GetComponentsInChildren<Button>();
				foreach (Button item in btns) {
					GameObject.Destroy(item.gameObject);
				}

				for (int i = 0; i < devCount; i++)
				{
					//if (devices[i] != null){
					GameObject button = Instantiate(BTButton) as GameObject;
					button.transform.SetParent(BTDevices.transform);
					button.gameObject.GetComponentInChildren<Text>().text = devices[i];
					Button btn = button.GetComponent<Button>();
					btn.onClick.AddListener(() => BManager.connectToDevice(button.gameObject.GetComponentInChildren<Text>().text));
					//}
				}
				prevCount = devCount;
			}
		}
	
		if(BManager.BTState == BTManager.STATE.CONNECTED)
		{
			Application.LoadLevel("PI");
		}
	}

	public void onClickSetBattleBB(){
		battle = "bb";
	}

	public void onClickSetBattleFB(){
		battle = "fb";
	}

	public void onClickLoadBattle(){
		if (battle == "bb") {
			Application.LoadLevel("PDC");
		} else if (battle == "fb") {
			if(BManager.isBTEnabled()){
				BManager.StartServer();
				BManager.enableDiscoverability();
				Application.LoadLevel("PI");
			}
			else BManager.turnBTON();
		}
	}
}
