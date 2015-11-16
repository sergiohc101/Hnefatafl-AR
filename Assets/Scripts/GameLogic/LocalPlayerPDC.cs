using UnityEngine;
using System.Collections;

public abstract class LocalPlayerPDC : Player {

	public override GameAction act(){
		// Check input of touches
		// Do RayCast
		// 		Assign "selectedPiece"
		// 		Create and return Action according to "Hit" and "TurnState"
		return new PieceMove(new Vector2());
	}
}
