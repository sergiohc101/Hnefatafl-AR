using UnityEngine;
using System.Collections;

public class ERASE_ASAP : MonoBehaviour {

	public Transform target;

	// Update is called once per frame
	void Update () {
		Vector3 relativePos = (target.position + new Vector3 (0, 0, 0)) - transform.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos);
		Quaternion current = transform.localRotation;
		transform.localRotation = Quaternion.Slerp (current, rotation, Time.deltaTime);
		transform.Translate (0, 0, 1000 * Time.deltaTime);
	}
}
