using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class BTManager : MonoBehaviour {

	public BTAdapter adapter;
	public int algo;

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
		adapter = new BTAdapter();
		algo = 666;
	}

	void Start () {
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
