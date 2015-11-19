using UnityEngine;
using System.Collections;

public class PieceMove : GameAction 
{
	public Vector2 squareIndex;
	
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
		Game.marker.inactivate ();		

		// Reset squares valids for move
		setCrossOfSquares ( SquareState.DEFAULT, Game.currentPlayer.selectedPiece.coord );

		// Reset traced squares (if any)
		if ( Game.moveTrace.index != -1 )
			setLineOfSquares ( SquareState.DEFAULT );

		// Save and Show the new trace of squares
		Game.moveTrace.setTrace ( Game.currentPlayer.selectedPiece.coord, squareIndex );
		setLineOfSquares ( SquareState.TRACED );

		// Set piece references and coord
		Game.board [ (int)Game.currentPlayer.selectedPiece.coord.y,
		             (int)Game.currentPlayer.selectedPiece.coord.x ].piece = null;
		Game.board [ (int)squareIndex.y,
		             (int)squareIndex.x ].piece = Game.currentPlayer.selectedPiece;
		Game.currentPlayer.selectedPiece.coord = squareIndex;


		// Assing TurnState
		Game.turnState = TurnState.ANIMATION;

		// Call piece's coroutine "translate"
		Game.board[(int)squareIndex.y, (int)squareIndex.x].gameObject.SetActive(true);
		Game.currentPlayer.selectedPiece.translate (squareIndex);
	}
	
	private void setLineOfSquares ( SquareState stateValue )
	{
		if ( Game.moveTrace.vertical )
			for ( int x = Game.moveTrace.start; x <= Game.moveTrace.end; x++ )
				Game.board [ Game.moveTrace.index, x ].changeState ( stateValue ); 
		else
			for ( int y = Game.moveTrace.start; y <= Game.moveTrace.end; y++ )
				Game.board [ y, Game.moveTrace.index ].changeState ( stateValue );
	}
}
