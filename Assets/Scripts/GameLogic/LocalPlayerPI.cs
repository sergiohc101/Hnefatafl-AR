using UnityEngine;
using System.Collections;

public abstract class LocalPlayerPI : Player {

	//Selectable selectablePointed;

	public override GameAction act(){
		// Do RayCast
		// Check change of pointed object
		// 		Reset previous Selectable (if any)
		// 		Call RollOvers according to "Hit" and "TurnState"
		// 		Assign selectablePointed

		// Revisar la entrada del boton de cardboard
		// 		Crear y devolver Action segun "Hit" y "TurnState"
		// 		Asignar valor de "selectedPiece"
		return new PieceMove(new Vector2());
	}
}
