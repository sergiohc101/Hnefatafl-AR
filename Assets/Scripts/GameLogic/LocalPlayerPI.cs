using UnityEngine;
using System.Collections;

public class LocalPlayerPI : Player {

	private Selectable selectablePointed;
	private Ray m_Ray;
	private RaycastHit m_RayCastHit;
	private Transform m_Camera;
	private bool validHit;

	public LocalPlayerPI()
	{
		selectablePointed = null;
		m_Camera = GameObject.Find ("ARCamera").transform;
		validHit = false;
	}

	public override GameAction act()
	{
			// Do RayCast
		m_Ray = new Ray (m_Camera.position, m_Camera.forward);
		//m_Ray = Camera.main.ScreenPointToRay ( Input.mousePosition );

		Selectable newSelectable = null;
		if ( Physics.Raycast ( m_Ray.origin, m_Ray.direction, out m_RayCastHit, Mathf.Infinity ) )
			newSelectable = m_RayCastHit.collider.gameObject.GetComponent<Selectable>();

		
			// Check change of pointed object
		if ( (selectablePointed && ! selectablePointed.Equals ( newSelectable )) ||
		     (! selectablePointed && newSelectable ) 							 )
		{
			if ( validHit )
				selectablePointed.rollOver ( false ); // Reset previous Selectable (if any)

			validHit = false;
			if ( newSelectable )
			{
				if ( newSelectable.tag != "Square" )
					validHit = true;
				else if ( Game.turnState == TurnState.PIECE_SELECTED )
				{
					Square sqrPointed = (Square)newSelectable;
					if ( sqrPointed.state == SquareState.VALID || sqrPointed.state == SquareState.VALID_TRACED )
						validHit = true;
				}
				if ( validHit )
					newSelectable.rollOver ( true ); // Call RollOver according to "Hit" and "TurnState"
			}
			selectablePointed = newSelectable; // Assign selectablePointed
		}

		if ( Input.touches.Length == 1 ) // Check cardboard's button input (for v2.0)
		//if ( true )
		{
			Touch touchedFinger = Input.touches[0]; // Get input of touches
			GameAction incoming = null;
			if ( touchedFinger.phase == TouchPhase.Ended )
			//if ( Input.GetMouseButtonDown(0) )
			{
				if  ( validHit )
				{
					switch ( selectablePointed.tag )
					{
					case "Attacker":
						incoming = new PieceSelection ( ((Piece)selectablePointed).index, true );
						break;
						
					case "Defender":
					case "King":
						incoming = new PieceSelection ( ((Piece)selectablePointed).index, false );
						break;
						
					case "Square":
						incoming = new PieceMove ( ((Square)selectablePointed).coord );
						break;
					}
					selectablePointed.rollOver (false);
					selectablePointed = null;
					validHit = false;
					return incoming; // Create and return GameAction according to "Hit" & "TurnState"
				}
				else
					Game.audio.playError ();
			}
		}
		return null;
	}
}
