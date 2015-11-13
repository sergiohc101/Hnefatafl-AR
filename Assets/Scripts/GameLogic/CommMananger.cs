using UnityEngine;
using System.Collections;

public abstract class CommManager {

	// static Queue<Action> actionsQueue;

	public abstract void sendAction ( Action a );
	protected abstract void onActionReceived ();
}
