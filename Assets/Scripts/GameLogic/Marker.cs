using UnityEngine;
using System.Collections;

public class Marker : MonoBehaviour {

	public void activate (Vector3 markerDest)
	{
		gameObject.SetActive(true);
		transform.position = markerDest;
		StartCoroutine(rotateCoroutine(markerDest));
	}

	public void inactivate ()
	{
		StopCoroutine("rotateCoroutine");
		gameObject.SetActive(false);
	}
	
	// translate corroutine
	private IEnumerator rotateCoroutine(Vector3 markerDest)
	{
		float newY;
		Vector3 newPosition = new Vector3();
		while(true)
		{
			transform.GetChild(0).Rotate(0, Time.deltaTime * 180, 0);
			newY = markerDest.y + Mathf.PingPong(Time.time * 10, 5);
			newPosition.Set(transform.position.x, newY, transform.position.z);
			transform.position = newPosition;
			yield return null;
		}
	}
}
