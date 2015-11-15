using UnityEngine;
using System.Collections;

public abstract class Selectable : MonoBehaviour {

	protected Vector2 coord;			// Coordinate of the Selectable object within the board array
	public abstract void rollOver ();	// rollOver corroutine
}
