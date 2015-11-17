using UnityEngine;
using System.Collections;

public class Marker : MonoBehaviour {

	// translate corroutine
	public void translate (Vector3 markerDest)
	{
		transform.position = markerDest;
	}
}
