using UnityEngine;


public class BTAdapter {

	#if UNITY_ANDROID
	AndroidJavaClass androidClass;

	
	public BTAdapter() {
		AndroidJNI.AttachCurrentThread();
		androidClass = new AndroidJavaClass("com.BonzoLabs.HnefataflAR.BTPlugin");
	}

	public bool initBT(){
		return androidClass.CallStatic<bool>("initBT");
	}

	public void turnOnBT(){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			activity.Call("setBTOn");
		}
	}

	public void sendMessage(string message){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			object[] args = new object[] {message};
			activity.Call("sendMessage", args);
		}
	}

	public void searchAndConnectDevice(string device){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			object[] args = new object[] {device};
			activity.Call("searchAndConnectDevice", args);
		}
	}

	public void startServer(){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			activity.Call("startServer");
		}
	}

	public void stopConnection(){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			activity.Call("stopConnection");
		}
	}

	public void getPairedDevices(){
		androidClass.CallStatic("getPairedDev");
	}

	public void discoverBTDev(){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			activity.Call("discoverDevices");
		}
	}

	public void enableDiscoverability(){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			activity.Call("enableBeDiscovered");
		}
	}

	public void enableDiscoverability(int seconds){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			object[] args = new object[] {seconds};
			activity.Call("enableBeDiscovered",args);
		}
	}

	public string[] returnPairedDevices(){
		return androidClass.CallStatic<string[]>("returnPairedDev");
	}

	public string[] getDiscoveredDevices(){
		return androidClass.CallStatic<string[]>("returnPairedDev");
	}
		
	#endif
}
