using UnityEngine;
using System.Collections;

public class PieceMove : Action 
{
	Vector2 squareIndex;
	
	public PieceMove( Vector2 v2SquareIndex )
	{
		squareIndex = v2SquareIndex;
	}
	
	public override bool validate ()
	{
		ValidateAction validation = new PieceMoveValidation();
		return validation.validate ( this );
	}
	
	public override void execute ()
	{
		// Use static members of Game
		// Reset squares valids for move
		// Call piece's coroutine "translate"
		// Reset traced squares (if any)
		// Show the new trace of squares
		// Assing TurnState
	}
}
