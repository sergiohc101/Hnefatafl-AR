using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour 
{
	public GameObject Marker;

 	private Ray m_Ray;
 	private RaycastHit m_RayCastHit;
	private Piece SelectedPiece;

	void Start ()
	{
		Marker.SetActive ( false );
		SelectedPiece = null;
	}

	void Update () 
	{
		if ( Input.touches.Length == 1 ) 
		{
			Touch touchedFinger = Input.touches[0];
			if ( touchedFinger.phase == TouchPhase.Ended )
			{
				m_Ray = Camera.main.ScreenPointToRay( touchedFinger.position );
				//Piece touchedPiece = m_RayCastHit.collider.gameObject.GetComponent<Piece>();
				if ( Physics.Raycast(m_Ray.origin, m_Ray.direction,
			                     	 out m_RayCastHit, Mathf.Infinity) )
				{
					switch ( m_RayCastHit.transform.name )
					{
					case "PieceAttack":
					//case "PieceDefense":
					case "Piece Diamond":
					case "PieceKing":
						Piece touchedPiece = m_RayCastHit.collider.gameObject.GetComponent<Piece>();
						if ( touchedPiece )
						{
							Marker.SetActive ( true );
							float h = m_RayCastHit.transform.name == "PieceKing" ?
											52f :
											15f ;
							Marker.transform.position = touchedPiece.transform.position +
								new Vector3( 0f,  h, 0f );
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

				}
			}
		}
	}
}