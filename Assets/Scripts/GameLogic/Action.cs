using UnityEngine;
using System.Collections;

public abstract class Action {
	
	public abstract bool validate();
	public abstract void execute();
}
