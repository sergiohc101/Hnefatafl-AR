using UnityEngine;
using System.Collections;

public abstract class CommManager {

	// static Queue<Action> actionsQueue;

	public abstract void sendAction ( GameAction gameAction );
	protected abstract void onActionReceived ();
}
