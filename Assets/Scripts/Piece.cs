using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

	private Vector3 pos;
	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public Vector3 getPosition()
	{
		return pos;
	}
}
