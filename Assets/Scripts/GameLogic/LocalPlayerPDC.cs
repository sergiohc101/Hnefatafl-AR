using UnityEngine;
using System.Collections;

public class LocalPlayerPDC : Player {
	
	private Ray m_Ray;
	private RaycastHit m_RayCastHit;

	public LocalPlayerPDC(bool isAttacker)
	{
		isAttackerPlayer = isAttacker;
	}

	public override GameAction act()
	{

		//if ( Input.GetMouseButtonDown(0) )
		if ( Input.touches.Length == 1 ) 
		{
		  Touch touchedFinger = Input.touches[0]; // Get input of touches
		  if ( touchedFinger.phase == TouchPhase.Ended )
		  //if ( true )
		  {
			m_Ray = Camera.main.ScreenPointToRay( touchedFinger.position );
			//m_Ray = Camera.main.ScreenPointToRay ( Input.mousePosition );

            if (Physics.Raycast(m_Ray.origin, m_Ray.direction,
                                    out m_RayCastHit, Mathf.Infinity))
            {
                GameAction incoming = null;
                switch (m_RayCastHit.transform.tag)
                {
                    case "Attacker":
                        Piece touchedAttacker = m_RayCastHit.collider.gameObject.GetComponent<Piece>();
                        incoming = new PieceSelection(touchedAttacker.index, true);
                        break;

                    case "Defender":
                    case "King":
                        Piece touchedDefender = m_RayCastHit.collider.gameObject.GetComponent<Piece>();
                        incoming = new PieceSelection(touchedDefender.index, false);
                        break;

                    case "Square":
                        if (Game.turnState == TurnState.PIECE_SELECTED)
                        {
                            Square touchedSquare = m_RayCastHit.collider.gameObject.GetComponent<Square>();
                            incoming = new PieceMove(touchedSquare.coord);
                        }else
							Game.audio.playError ();
                        break;
                }
                return incoming;
            }else
				Game.audio.playError ();
          }
		}
		return null;
	}
}
