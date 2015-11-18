using UnityEngine;
using System.Collections;

public class PieceMoveValidation : ValidateAction 
{
	
	public override bool validate ( GameAction gameAction )
	{
        Debug.Log("Entra validar");
		PieceMove moveAction = (PieceMove)gameAction;
		SquareState destinationState = Game.board [(int)moveAction.squareIndex.y, 
		                                           (int)moveAction.squareIndex.x].state;
        Debug.Log(destinationState + "    " + moveAction.squareIndex);
		// Verify that the selected square is a valid destination for the selected piece
		return  destinationState == SquareState.VALID ||
				destinationState == SquareState.VALID_TRACED;
	}
}