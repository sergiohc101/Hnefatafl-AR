/*============================================================================== 
 * Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GazeRay : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES
    public ViewTrigger[] viewTriggers;
	public Transform parent;
	public GameObject Marker;
    #endregion // PUBLIC_MEMBER_VARIABLES


	private Transform[] elements;

	private Ray m_Ray;
	private RaycastHit m_RayCastHit;
	//private Piece SelectedPiece;
	private Transform SelectedPiece;

    #region MONOBEHAVIOUR_METHODS
	// Use this for initialization
	void Start () {
		Debug.Log ("iniciando script GazeRay");
		elements = parent.GetComponentsInChildren<Transform>();
		Debug.Log ("# elements= " + elements.GetLength(0));

		Marker.SetActive ( false );
		SelectedPiece = null;
	}

    void Update()
    {
        // Check if the Head gaze direction is intersecting any of the ViewTriggers
        RaycastHit hit;
        Ray cameraGaze = new Ray(this.transform.position, this.transform.forward);
        Physics.Raycast(cameraGaze, out hit, Mathf.Infinity);
        foreach (var trigger in viewTriggers)
        {
            trigger.Focused = hit.collider && (hit.collider.gameObject == trigger.gameObject);
        }
		foreach (var e in elements) {
			if(e.GetComponent<ViewTrigger>()){
				if(hit.collider && (hit.collider.gameObject == e.gameObject)){
					Debug.Log("Piramide apuntada");
					//Detectar touch
					if ( Input.touches.Length == 1 )
					{
						Touch touchedFinger = Input.touches[0];
						if ( touchedFinger.phase == TouchPhase.Ended )
						{
							m_Ray = Camera.main.ScreenPointToRay( touchedFinger.position );

							if ( SelectedPiece == null )
							{
								Marker.SetActive ( true );
								float h = e.transform.name == "Piece King" ? 52f : 15f ;
								Marker.transform.position = e.transform.position + new Vector3( 0f,  h, 0f );
								SelectedPiece = e;
							}
							//Piece touchedPiece = m_RayCastHit.collider.gameObject.GetComponent<Piece>();
							/*if ( Physics.Raycast(m_Ray.origin, m_Ray.direction, out m_RayCastHit, Mathf.Infinity) )
							{
								switch ( m_RayCastHit.transform.name )
								{
								case "Piece Cilinder":
									//case "PieceDefense":
								case "Piece Diamond":
								case "Piece King":
									Piece touchedPiece = m_RayCastHit.collider.gameObject.GetComponent<Piece>();
									if ( touchedPiece )
									{
										Marker.SetActive ( true );
										float h = m_RayCastHit.transform.name == "Piece King" ? 52f : 15f ;
										Marker.transform.position = touchedPiece.transform.position + new Vector3( 0f,  h, 0f );
										SelectedPiece = touchedPiece;
									}
									break;
								case "SquareA":
								case "SquareB":
									if ( SelectedPiece != null )
									{
										SquareBehavior touchedSquare = m_RayCastHit.collider.gameObject.GetComponent<SquareBehavior>();
										if( touchedSquare )
										{
											SelectedPiece.transform.position = touchedSquare.transform.position;
											Marker.transform.position = touchedSquare.transform.position +
												new Vector3( 0f, Marker.transform.position.y, 0f );
										}
									}
									break;
								}

							}*/
						}
					}
				}
			}
		}
    }
    #endregion // MONOBEHAVIOUR_METHODS
}

