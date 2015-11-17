using UnityEngine;
using System.Collections;

public class LocalPlayerPDC : Player {
	
	private Ray m_Ray;
	private RaycastHit m_RayCastHit;

	public override GameAction act(){

		if ( Input.touches.Length == 1 ) 
		{
			Touch touchedFinger = Input.touches[0]; // Get input of touches
			if ( touchedFinger.phase == TouchPhase.Ended )
			{
				m_Ray = Camera.main.ScreenPointToRay( touchedFinger.position );
				
				if ( Physics.Raycast(m_Ray.origin, m_Ray.direction,
				                     out m_RayCastHit, Mathf.Infinity) )
				{
					GameAction incoming = null;
					switch ( m_RayCastHit.transform.name )
					{
					case "PieceAttack":
						Piece touchedAttacker = m_RayCastHit.collider.gameObject.GetComponent<Piece>();
						incoming = new PieceSelection( touchedAttacker.index, true );
						break;
						
					case "PieceDefense":
					case "PieceKing":
						Piece touchedDefender = m_RayCastHit.collider.gameObject.GetComponent<Piece>();
						incoming =  new PieceSelection( touchedDefender.index, false );
						break;
						
					case "Square":
						if( Game.turnState == TurnState.PIECE_SELECTION )
						{
							Square touchedSquare = m_RayCastHit.collider.gameObject.GetComponent<Square>();
							incoming = new PieceMove( touchedSquare.coord );
						}
						break;
					}
					return incoming;
				}
			}
		}
		return null;

		//return new PieceMove(new Vector2());
	}
}
