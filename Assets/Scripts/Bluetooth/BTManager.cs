using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class BTManager : MonoBehaviour {

	//public BTAdapter adapter;
	public int algo;
	public string messagee;
	public string count;
	public string connectedTo;

	private string[] devices = {"hola","mundo","hnefatafl"};
	public enum STATE {IDLE, DISCOVERING, CONNECTED};
	public STATE BTState;

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
		//adapter = new BTAdapter();
		algo = 666;
	}

	void Start () {

	}

	void Update() {
		if(BTState == STATE.DISCOVERING){
			//message = devices[0];
			devices = BTAdapter.getDiscoveredDevices();
		}
	}

	public void setDevil()
	{
		algo = 999;
	}

	public void initBT()
	{
		algo = 222;
		BTAdapter.initAdapter();
		BTAdapter.initBT();
		BTAdapter.turnOnBT();
		messagee = "BT On";
		setDevil();
	}

	public void turnBTON()
	{
		algo = 333;
		BTAdapter.turnOnBT();
		algo = 777;
	}

	public bool isBTEnabled()
	{
		return BTAdapter.isBTEnabled();
	}

	public void StartServer()
	{
		BTAdapter.startServer();
	}

	public void enableDiscoverability()
	{
		BTAdapter.enableDiscoverability(300);
	}

	public void discoverBTDevices()
	{
		BTState = STATE.DISCOVERING;
		BTAdapter.discoverBTDev();
	}

	public string[] returnDiscoveredDevices()
	{
		return devices;
	}

	public int returnDiscoveredDevicesCount()
	{
		return devices.Length;
	}

	public void connectedFromDevice(string name)
	{
		messagee = "Connected to <"+name+">";
		this.connectedTo = name;
	}

	public void connectToDevice(string name)
	{
		this.connectedTo = name;
		messagee = "*"+ name +" >_<";
		algo = 555;
		BTAdapter.searchAndConnectDevice(name);
		BTAdapter.sendMessage("Connected to <" + name + ">");		
		BTState = STATE.CONNECTED;
	}

	public void sendBTMessage(string message)
	{
		messagee = "Message send to Device";
		BTAdapter.sendMessage(message);
	}

	void getMessageFromAPI(string msg)
	{
		messagee = msg;
	}


	void OnGUI(){
		GUI.color = Color.white;
		GUILayout.BeginArea (new Rect (25, 100, 200, 200), "Android Bluetooth Debug", "Window");
		GUILayout.BeginVertical ();
		GUILayout.TextArea (algo.ToString(), GUILayout.Height (30));
		GUILayout.TextArea (messagee, GUILayout.Height (50));
		GUILayout.TextArea (count, GUILayout.Height (30));
		if(GUILayout.Button("Send Message", GUILayout.Height (55))) {
			sendBTMessage("Message from <" + this.connectedTo + ">");
		}
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}

	private byte[] ObjectToByteArray(GameAction obj)
	{
		if (obj == null)
			return null;
		BinaryFormatter bf = new BinaryFormatter();
		MemoryStream ms = new MemoryStream();
		bf.Serialize(ms, obj);
		return ms.ToArray();
	}

	private GameAction ByteArrayToObject(byte[] Barry)
	{
		MemoryStream memStream = new MemoryStream();
		BinaryFormatter binForm = new BinaryFormatter();
		memStream.Write(Barry, 0, Barry.Length);
		memStream.Seek(0, SeekOrigin.Begin);
		GameAction obj = (GameAction) binForm.Deserialize(memStream);
		return obj;
	}

}
