using UnityEngine;
using System.Collections;

public abstract class Selectable : MonoBehaviour {

	public Vector2 coord;						// Coordinate of the Selectable object within the board array
	public abstract IEnumerator rollOver ();	// rollOver coroutine
	protected virtual void Awake(){ }			// virtual function Awake, used to initialize members
}
