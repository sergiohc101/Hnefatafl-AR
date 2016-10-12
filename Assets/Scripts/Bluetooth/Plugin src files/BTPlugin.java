package com.BonzoLabs.HnefataflAR;


import java.util.Set;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class BTPlugin extends UnityPlayerActivity{
	
	//good stuff
	static public Context mContext;
	static private BluetoothAdapter BTAdapter;
	static private String[] pDevices = new String[25];
	static private String[] aDevices = new String[25];
	static private IntentFilter filter;
	static private int devIndex;
	static private BluetoothDevice[] cDevices = new BluetoothDevice[25];
	
	private BluetoothService btService = null;
	

	final BroadcastReceiver mReceiver = new BroadcastReceiver(){
		public void onReceive(Context context, Intent intent){
			String action = intent.getAction();
			
			if(BluetoothDevice.ACTION_FOUND.equals(action)){
				BluetoothDevice device = (BluetoothDevice)intent.getParcelableExtra(BluetoothDevice.EXTRA_DEVICE);
				UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "Device found <"+device.getName()+">");
				
				if(device.getName().endsWith("-HAR")){
					String s[]  = device.getName().split("-HAR");
					cDevices[devIndex] = device;
					aDevices[devIndex++] = s[0];
				}
			}
			
			else if(BluetoothAdapter.ACTION_DISCOVERY_STARTED.equals(action)){
				UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "BT Discovery Started...");
            }
            else if(BluetoothAdapter.ACTION_DISCOVERY_FINISHED.equals(action)){
				UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "BT Discovery Finished...");
            }
            else if(BluetoothAdapter.ACTION_STATE_CHANGED.equals(action)){
                if(BTAdapter.getState() == BluetoothAdapter.STATE_OFF){
    				UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "BT STATE OFF!");
                }
            }

			
		}
	};
	
	
	
	@Override
	protected void onCreate(Bundle bundle){
		super.onCreate(bundle);
		mContext = this;
		//inicializaciones
		devIndex = 0;
		
		filter = new IntentFilter();
        filter.addAction(BluetoothDevice.ACTION_FOUND);
        filter.addAction(BluetoothAdapter.ACTION_DISCOVERY_STARTED);
        filter.addAction(BluetoothAdapter.ACTION_DISCOVERY_FINISHED);
        filter.addAction(BluetoothAdapter.ACTION_STATE_CHANGED);
        // Register the BroadcastReceiver
        this.registerReceiver(mReceiver, filter);
	}
	
	@Override
	public void onDestroy(){
		super.onDestroy();

        // Make sure we're not doing discovery anymore
        if (BTAdapter != null) {
            BTAdapter.cancelDiscovery();
        }

        // Unregister broadcast listeners
        this.unregisterReceiver(mReceiver);
	}
	
	@Override
	public void onStart(){
		super.onStart();
		btService = new BluetoothService(mContext);
	}
	
	static public boolean initBT(){
		BTAdapter = BluetoothAdapter.getDefaultAdapter();
		if(BTAdapter == null){
			return false;
		}
		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "InitBT!");
		return true;
	}
	
	static public boolean isBTOn()
	{
		return BTAdapter.isEnabled();
	}
	
	public void setBTOn(){
		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "Function<setBTON>Called");
		if(BTAdapter == null) 	UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "!BTNULL");
		else	UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "!BTGOOD");

		if(!BTAdapter.isEnabled()){
			UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "EnablingBT");

			Intent enableBtIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
			int REQUEST_ENABLE_BT = 1;
			startActivityForResult(enableBtIntent, REQUEST_ENABLE_BT);
			
		}
	}
	
	static public void getPairedDev(){
		Set<BluetoothDevice> pairedDevices = BTAdapter.getBondedDevices();
		if (pairedDevices.size() > 0) {
			int i = 0;
		    for (BluetoothDevice device : pairedDevices) {
		        pDevices[i++] = device.getName();
		    }
		}
	}
	
	//***
	public void startDiscovery(){
		//filter = new IntentFilter();
		//filter.addAction(BluetoothDevice.ACTION_FOUND);
		//registerReceiver(mReceiver, filter);
		BTAdapter.startDiscovery();
		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "Discovery Started!");
	}
	
	static public String[] returnDiscoveredDev(){
		return aDevices;
	}
	
	static public String[] returnPairedDev(){
		return pDevices;
	}
	
	public void enableBeDiscovered(){
		Intent discoverableIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_DISCOVERABLE);
		discoverableIntent.putExtra(BluetoothAdapter.EXTRA_DISCOVERABLE_DURATION,120);
		startActivity(discoverableIntent);
	}

	public void enableBeDiscovered(int seconds){
		Intent discoverableIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_DISCOVERABLE);
		discoverableIntent.putExtra(BluetoothAdapter.EXTRA_DISCOVERABLE_DURATION,seconds);
		startActivity(discoverableIntent);
	}
	
	public void searchAndConnectDevice(String devName){
		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "Connecting to: "+ devName);
		int i =0;
		for(; i < devIndex; i++)
			if(aDevices[i].equals(devName)) break;
			
		BluetoothDevice device = cDevices[i];
		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "Found "+device.getName()+"//"+device.getAddress());
    	connectDevice(cDevices[i].getAddress());
	}
	
	public void searchAndConnectDevice2(String devName){
		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "Connecting to: "+ devName);
		for(BluetoothDevice device: cDevices){
			 if(device.getName().equals(devName)){
					UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "Found "+device.getName()+"//"+device.getAddress());
		        	connectDevice(device.getAddress());
		        }
		}
	}
	
	public void connectDevice(String address){
		BluetoothDevice device = BTAdapter.getRemoteDevice(address);
		btService.connect(device);
	}
	
	
	public void sendBytes(byte[] packet){
		btService.write(packet);
	}
	
	
	public void sendMessage(String message){
		byte[] send = message.getBytes();
		String m = new String(send);
		m = m.concat(String.valueOf(send.length));
		btService.write(send);
	}
	
	static public String returnReceivedMessage(){
		return null; //btService.rMessage;
	}
	
	public byte[] returnReceivedPacket()
	{
		return btService.getrPacket();
	}
	
	public void stopConnection(){
		btService.stop();
	}
	
	public void startServer(){
		btService.start();
	}
}
