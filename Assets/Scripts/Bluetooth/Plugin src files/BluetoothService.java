package com.BonzoLabs.HnefataflAR;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.UUID;
import com.unity3d.player.UnityPlayer;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothServerSocket;
import android.bluetooth.BluetoothSocket;
import android.content.Context;

public class BluetoothService {
	
	private final BluetoothAdapter BTAdapter;
	private ServerThread serverThread;
    private ClientThread clientThread;
    private ConnectedThread connectedThread;
    private int sState;
    // Constants that indicate the current connection state
    public static final int STATE_NONE = 0;       // we're doing nothing
    public static final int STATE_LISTEN = 1;     // now listening for incoming connections
    public static final int STATE_CONNECTING = 2; // now initiating an outgoing connection
    public static final int STATE_CONNECTED = 3;  // now connected to a remote device
    
    public String rMessage;
    public byte[] rPacket;
    public byte[] getrPacket()
    {
    	return rPacket;
    }
    
    private static String orgName; //Filtrar servers
    
    private static final UUID MY_UUID = UUID.fromString("989e9fe7-5652-417d-8a32-b72cbaa5ac2e");
    
    public BluetoothService(Context context) {
    	BTAdapter = BluetoothAdapter.getDefaultAdapter();
        sState = STATE_NONE;
    }
    
    private synchronized void setState(int state) {
        sState = state;
    }
    
    public synchronized int getState() {
        return sState;
    }
    
    /**
     * Start the chat service. Specifically start ServerThread to begin a
     * session in listening (server) mode. Called by the Activity onResume()
     */
    public synchronized void start() {
		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "starting Server");
        // Cancel any thread attempting to make a connection
        if (clientThread != null) {
            clientThread.cancel();
            clientThread = null;
        }
        // Cancel any thread currently running a connection
        if (connectedThread != null) {
            connectedThread.cancel();
            connectedThread = null;
        }
        setState(STATE_LISTEN);
        
        // Start the thread to listen on a BluetoothServerSocket
        if (serverThread == null) {
            serverThread = new ServerThread();
            serverThread.start();
        }
        
        if(!BTAdapter.getName().endsWith("-HAR")){
	        orgName = BTAdapter.getName();	//Obtener nombre original
	        BTAdapter.setName(orgName + "-HAR");	//Agregar un subfijo para identificar server
        }
    }
    
    /**
     * Start the ClientThread to initiate a connection to a remote device.
     * @param device The BluetoothDevice to connect
     * @param secure Socket Security type - Secure (true) , Insecure (false)
     */
    public synchronized void connect(BluetoothDevice device) {
        // Cancel any thread attempting to make a connection
        if (sState == STATE_CONNECTING) {
            if (clientThread != null) {
                clientThread.cancel();
                clientThread = null;
            }
        }
        // Cancel any thread currently running a connection
        if (connectedThread != null) {
            connectedThread.cancel();
            connectedThread = null;
        }
        
		UnityPlayer.UnitySendMessage("PersistanceManager", "connectingFromDevice",device.getName());

        // Start the thread to connect with the given device
        clientThread = new ClientThread(device);
        clientThread.start();
        setState(STATE_CONNECTING);
    }
    
    /**
     * Start the ConnectedThread to begin managing a Bluetooth connection
     * @param socket The BluetoothSocket on which the connection was made
     * @param device The BluetoothDevice that has been connected
     */
    public synchronized void connected(BluetoothSocket socket, BluetoothDevice device) {
        // Cancel the thread that completed the connection
        if (clientThread != null) {
            clientThread.cancel();
            clientThread = null;
        }

        // Cancel any thread currently running a connection
        if (connectedThread != null) {
            connectedThread.cancel();
            connectedThread = null;
        }

        // Cancel the accept thread because we only want to connect to one device
        if (serverThread != null) {
            serverThread.cancel();
            serverThread = null;
        }

        // Start the thread to manage the connection and perform transmissions
        connectedThread = new ConnectedThread(socket);
        connectedThread.start();
		UnityPlayer.UnitySendMessage("PersistanceManager", "connectedFromDevice",device.getName());
        setState(STATE_CONNECTED);
    }
    
    /**
     * Stop all threads
     */
    public synchronized void stop() {
		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "stopping Server");
        if (clientThread != null) {
            clientThread.cancel();
            clientThread = null;
        }

        if (connectedThread != null) {
            connectedThread.cancel();
            connectedThread = null;
        }

        if (serverThread != null) {
            serverThread.cancel();
            serverThread = null;
        }
        setState(STATE_NONE);
        
        BTAdapter.setName(orgName); //Restaurar el nombre original del server
		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "Server Stopped!");
    }
    
    /**
     * Write to the ConnectedThread in an unsynchronized manner
     * @param out The bytes to write
     * @see ConnectedThread#write(byte[])
     */
    public void write(byte[] out) {
        // Create temporary object
        ConnectedThread r;
        // Synchronize a copy of the ConnectedThread
        synchronized (this) {
            if (sState != STATE_CONNECTED) return;
            r = connectedThread;
        }
        // Perform the write unsynchronized
        r.write(out);
    }
    
    /**
     * Indicate that the connection attempt failed and notify the UI Activity.
     */
    private void connectionFailed() {
        // Start the service over to restart listening mode
		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "Connection Failed");
        BluetoothService.this.start();
    }
    
    private void connectionLost() {
        // Start the service over to restart listening mode
		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI", "Connection Lost");
        BluetoothService.this.start();
    }
    
    
	
	public class ServerThread extends Thread{
	
		private final BluetoothServerSocket serverSocket;
		
		
		public ServerThread() {
	        // Use a temporary object that is later assigned to mmServerSocket,
	        // because mmServerSocket is final
	        BluetoothServerSocket tmp = null;
	        try {
	            // MY_UUID is the app's UUID string, also used by the client code
	            //tmp = BTAdapter.listenUsingRfcommWithServiceRecord("HnefataflAR", MY_UUID);
	            tmp = BTAdapter.listenUsingInsecureRfcommWithServiceRecord("HnefataflAR", MY_UUID);
	        } catch (IOException e) { }
	        serverSocket = tmp;
	    }
	 
	    public void run() {
	        BluetoothSocket socket = null;
	        // Keep listening until exception occurs or a socket is returned
	     // Listen to the server socket if we're not connected
            while (sState != STATE_CONNECTED) {
                try {
                    // This is a blocking call and will only return on a
                    // successful connection or an exception
                    socket = serverSocket.accept();
                } catch (IOException e) {
                    break;
                }
                // If a connection was accepted
                if (socket != null) {
                    synchronized (BluetoothService.this) {
                        switch (sState) {
                            case STATE_LISTEN:
                            case STATE_CONNECTING:
                                // Situation normal. Start the connected thread.
                                connected(socket, socket.getRemoteDevice());
                                break;
                            case STATE_NONE:
                            case STATE_CONNECTED:
                                // Either not ready or already connected. Terminate new socket.
                                try {
                                    socket.close();
                                } catch (IOException e) {
                                    //Log.e(TAG, "Could not close unwanted socket", e);
                                }
                                break;
                        }
                    }
                }
            }
	    }
	 
	    /** Will cancel the listening socket, and cause the thread to finish */
	    public void cancel() {
	        try {
	        	serverSocket.close();
	        } catch (IOException e) { }
	    }
	}
	
	public class ClientThread extends Thread{
		private final BluetoothSocket socket;
        private final BluetoothDevice device;
        
        
		public ClientThread(BluetoothDevice btdevice) {
            device = btdevice;
            BluetoothSocket tmp = null;
            // Get a BluetoothSocket for a connection with the given BluetoothDevice
            try {
                    //tmp = device.createRfcommSocketToServiceRecord(MY_UUID);
                    tmp = device.createInsecureRfcommSocketToServiceRecord(MY_UUID);
               
            } catch (IOException e) {
            }
            socket = tmp;
        }

        public void run() {
            // Always cancel discovery because it will slow down a connection
            BTAdapter.cancelDiscovery();
            // Make a connection to the BluetoothSocket
            try {
                // This is a blocking call and will only return on a  successful connection or an exception
                socket.connect();
            } catch (IOException e) {
                // Close the socket
                try {
                    socket.close();
                } catch (IOException e2) {
                }
                connectionFailed();
                return;
            }
            // Reset the ClientThread because we're done
            synchronized (BluetoothService.this) {
                clientThread = null;
            }

            // Start the connected thread
            connected(socket, device);
        }

        public void cancel() {
            try {
                socket.close();
            } catch (IOException e) {
            }
        }
	}
	
	public class ConnectedThread extends Thread{
		private final BluetoothSocket socket;
        private final InputStream inStream;
        private final OutputStream outStream;

        public ConnectedThread(BluetoothSocket btsocket) {
            socket = btsocket;
            InputStream tmpIn = null;
            OutputStream tmpOut = null;

            // Get the BluetoothSocket input and output streams
            try {
                tmpIn = socket.getInputStream();
                tmpOut = socket.getOutputStream();
            } catch (IOException e) {
            }

            inStream = tmpIn;
            outStream = tmpOut;
        }

        public void run() {
            byte[] buffer = new byte[1024];
            int bytes;
            // Keep listening to the InputStream while connected
            while (true) {
                try {
                    // Read from the InputStream
                    bytes = inStream.read(buffer);

                    // Send the obtained bytes to the UI Activity
                    //mHandler.obtainMessage(Constants.MESSAGE_READ, bytes, -1, buffer).sendToTarget();
                    //rMessage = String.valueOf(buffer);
                    if(bytes > 0){
                        rPacket = buffer;
	                    String s = new String(buffer);
	                    //rMessage = s;
	                    //UnityPlayer.UnitySendMessage("Main Camera", "receivedMessage", rMessage);
	                    s = "";
	                    String cad[] = new String[bytes];
	                    for(int i = 0; i < bytes;i++){
	                    	 cad[i]= new String(String.valueOf((char)buffer[i]));
	                    	 s += cad[i];
	                    }
	                    rMessage = s;
	            		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI","Received "+ s);
	            		UnityPlayer.UnitySendMessage("PersistanceManager", "getBytesFromAPI",">>");	            		
                    }
                } catch (IOException e) {
                    connectionLost();
                    // Start the service over to restart listening mode
                    BluetoothService.this.start();
                    break;
                }
            }
        }

        /**
         * Write to the connected OutStream.
         *
         * @param buffer The bytes to write
         */
        public void write(byte[] buffer) {
            try {
            	String m = new String(buffer);
        		UnityPlayer.UnitySendMessage("PersistanceManager", "getMessageFromAPI","Message send to Device[API]");
                outStream.write(buffer);

                // Share the sent message back to the UI Activity
                //mHandler.obtainMessage(Constants.MESSAGE_WRITE, -1, -1, buffer).sendToTarget();
            } catch (IOException e) {
            }
        }

        public void cancel() {
            try {
                socket.close();
            } catch (IOException e) {
            }
        }
	}

}

