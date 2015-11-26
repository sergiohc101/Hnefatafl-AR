using UnityEngine;
using System.Collections;

public abstract class Selectable : MonoBehaviour {

	public Vector2 coord;						// Coordinate of the Selectable object within the board array
	protected virtual void Awake(){ }			// virtual function Awake, used to initialize members
	protected IEnumerator coroutine;

	public abstract void rollOver( bool bValue );

	protected static Color[] rollOverColors;

	static Selectable()
	{
		rollOverColors = new Color[10];
		float g=1f, b=0.5f; 
		for ( int i=0; i<10; i++ )
		{
			rollOverColors[i] = new Color ( 1f, g, b );
			g -= 0.05f;
			b -= 0.05f;
		}
	}

	protected IEnumerator rollOverCoroutine ()	// rollOver coroutine
	{
		while (true) {
			renderer.material.color = rollOverColors[  (int)Mathf.PingPong (Time.time*12, 9.9f ) ];
			yield return null;
		}
	}
}
