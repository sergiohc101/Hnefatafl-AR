using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour {

	//private Vector3 screenPoint;
	public float horizontallimit = 2.5f;
	public float verticallimit = -2.5f;
	public float dragSpeed = 0.1f;

	Transform cachedTransform;
	Vector3 startingPos;

	// Use this for initialization
	void Start () {
		//referencia transform 
		cachedTransform = transform; 
		//salva a posicao inicial 
		startingPos = cachedTransform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			Vector2 deltaPosition = Input.GetTouch(0).deltaPosition; 
			switch(Input.GetTouch(0).phase) {
				case TouchPhase.Began: 
				break; 
				case TouchPhase.Moved: 
				DragObj(deltaPosition); 
				break; 
				case TouchPhase.Ended: 
				break; 
			}
		}
	}

	void DragObj (Vector2 deltaPos) {
		cachedTransform.position = new Vector3(Mathf.Clamp((deltaPos.x * dragSpeed) + cachedTransform.position.x, 
			startingPos.x - horizontallimit, startingPos.x + horizontallimit),
			Mathf.Clamp((deltaPos.y * dragSpeed) + cachedTransform.position.y,
			startingPos.y - verticallimit, startingPos.y + verticallimit), cachedTransform.position.z);
	}

	/*void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
	}

	void OnMouseDrag(){
		Vector3 currentScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPos = Camera.main.ScreenToWorldPoint (currentScreenPoint);
		transform.position = currentPos;
	}*/
}
