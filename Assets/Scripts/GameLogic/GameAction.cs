using UnityEngine;
using System.Collections;

public abstract class GameAction {
	
	public abstract bool validate();
	public abstract void execute();
}
