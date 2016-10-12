using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class BTManager : MonoBehaviour {

	//public BTAdapter adapter;
	public bool DEBUG;
	public int algo;
	private Stack<string> messagee = new Stack<string>();
	private Queue<GameMessage> GameMessages = new Queue<GameMessage>();
	//public string messagee;
	public string count;
	private string connectedTo;

	private string[] devices = {"hola","mundo","hnefatafl"};
	public enum STATE {IDLE, DISCOVERING, CONNECTED};
	public STATE BTState;
	public static bool isServer = false;
	//Singleton
		//private static BTManager _instance;
		//private BTManager(){}
		//public static BTManager Instance { get { return _instance;}}


	void Awake(){
		/*
		if(_instance != null && _instance != this)
			Destroy(this.gameObject);
		else
			_instance = this;
		*/
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
		messagee.Push("BT On"); // = "BT On";
		//setDevil();
	}

	public void turnBTON()
	{
		algo = 333;
		BTAdapter.turnOnBT();
		algo = 777;
		DEBUG = !DEBUG;
	}

	public bool isBTEnabled()
	{
		return BTAdapter.isBTEnabled();
	}

	public void StartServer()
	{
		BTAdapter.startServer();
		isServer = true;
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
		messagee.Push("Connected to <"+name+">");
		this.connectedTo = name;
	}

	public void connectToDevice(string name)
	{
		this.connectedTo = name;
		messagee.Push("*"+ name +" >_<");
		algo = 555;
		BTAdapter.searchAndConnectDevice(name);
		BTAdapter.sendMessage("Connected to <" + name + ">");		
		BTState = STATE.CONNECTED;
	}


	public void sendBTMessage(string message)
	{
		messagee.Push(" >>" +  message);
		BTAdapter.sendMessage(message);
	}

	public void sendGameMessage(GameMessage msg)
	{
		BTAdapter.sendBytes(ObjectToByteArray(msg));
	}

	public void getMessageFromAPI(string msg)
	{
		messagee.Push(msg);
	}

	public void getBytesFromAPI(string msg)
	{	
		GameMessages.Enqueue(ByteArrayToObject(BTAdapter.getBytesfromAPI()));

		//byte[] b = BTAdapter.getBytesfromAPI();
		//messagee.Push("*"+ ByteArrayToString(b) +"*");

		messagee.Push(msg);
	}

	public GameMessage getGameMessage()
	{
		if(GameMessages.Count > 0) return GameMessages.Dequeue();
		else return null;
	}


	void OnGUI(){
		if(DEBUG){
			GUI.color = Color.white;
			GUILayout.BeginArea (new Rect (25, 100, 200, 400), "Android Bluetooth Debug", "Window");
			GUILayout.BeginVertical ();
			GUILayout.TextArea (algo.ToString(), GUILayout.Height (30));
			string mstr = "";
			foreach (var msg in messagee) {
				mstr += msg + "\n";
			}
			GUILayout.TextArea (mstr, GUILayout.Height (250));
			GUILayout.TextArea (count, GUILayout.Height (30));
			if(GUILayout.Button("Send Message", GUILayout.Height (55))) {
				sendBTMessage("Message from <EARTH" + ">");
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}
	}

	public byte[] ObjectToByteArray(GameMessage obj)
	{
		if (obj == null)
			return null;
		BinaryFormatter bf = new BinaryFormatter();
		MemoryStream ms = new MemoryStream();
		bf.Serialize(ms, obj);
		return ms.ToArray();
	}

	public GameMessage ByteArrayToObject(byte[] Barry)
	{
		BinaryFormatter binForm = new BinaryFormatter();
		MemoryStream memStream = new MemoryStream();
		memStream.Write(Barry, 0, Barry.Length);
		memStream.Seek(0, SeekOrigin.Begin);
		GameMessage obj = (GameMessage) binForm.Deserialize(memStream);
		return obj;
	}

	public string ByteArrayToString(byte[] Barry)
	{
		return System.Text.Encoding.ASCII.GetString(Barry);
	}

}
