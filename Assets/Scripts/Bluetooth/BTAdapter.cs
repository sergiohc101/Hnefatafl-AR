using UnityEngine;


public static class BTAdapter {

	static AndroidJavaClass androidClass; //= new AndroidJavaClass("com.BonzoLabs.HnefataflAR.BTPlugin");
	static AndroidJavaObject androidObj;

	/*
	public BTAdapter() {
		AndroidJNI.AttachCurrentThread();
		androidClass = new AndroidJavaClass("com.BonzoLabs.HnefataflAR.BTPlugin");
	}
	*/

	#if UNITY_ANDROID

	public static void initAdapter(){
		AndroidJNI.AttachCurrentThread();
		androidClass = new AndroidJavaClass("com.BonzoLabs.HnefataflAR.BTPlugin");
		//androidObj = androidClass.GetStatic<AndroidJavaObject>("mContext");
	}

	public static bool initBT(){
		return androidClass.CallStatic<bool>("initBT");
	}

	public static bool isBTEnabled(){
		return androidClass.CallStatic<bool>("isBTOn");
	}

	public static void turnOnBT(){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			activity.Call("setBTOn");
		}
	}

	public static void turnOnBT2(){
		AndroidJNI.AttachCurrentThread();
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			activity.Call("setBTOn");
		}
	}

	public static void turnOnBT3(){
		AndroidJNI.AttachCurrentThread();
		androidObj.Call("setBTOn");
	}

	public static void sendMessage(string message){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			object[] args = new object[] {message};
			activity.Call("sendMessage", args);
		}
	}

	public static void searchAndConnectDevice(string device){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			object[] args = new object[] {device};
			activity.Call("searchAndConnectDevice", args);
		}
	}

	public  static void startServer(){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			activity.Call("startServer");
		}
	}

	public static void stopConnection(){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			activity.Call("stopConnection");
		}
	}

	public static void getPairedDevices(){
		androidClass.CallStatic("getPairedDev");
	}

	public static void discoverBTDev(){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			activity.Call("discoverDevices");
		}
	}

	public static void enableDiscoverability(){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			activity.Call("enableBeDiscovered");
		}
	}

	public static void enableDiscoverability(int seconds){
		using(AndroidJavaObject activity = androidClass.GetStatic<AndroidJavaObject>("mContext")){
			object[] args = new object[] {seconds};
			activity.Call("enableBeDiscovered",args);
		}
	}

	public static string[] returnPairedDevices(){
		return androidClass.CallStatic<string[]>("returnPairedDev");
	}

	public static string[] getDiscoveredDevices(){
		return androidClass.CallStatic<string[]>("returnDiscoveredDev");
	}
		
	#endif
}
