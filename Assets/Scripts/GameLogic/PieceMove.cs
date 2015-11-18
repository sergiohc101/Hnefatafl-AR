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
		Game.marker.gameObject.SetActive(false);		
		// Reset squares valids for move
		setCrossOfSquares ( SquareState.DEFAULT, Game.currentPlayer.selectedPiece.coord );
		
		// Call piece's coroutine "translate"
        Game.board[(int)squareIndex.y, (int)squareIndex.x].gameObject.SetActive(true);
		Game.currentPlayer.selectedPiece.translate (squareIndex);
		
		// Set piece references and coord
		Game.board [ (int)Game.currentPlayer.selectedPiece.coord.y,
		 			 (int)Game.currentPlayer.selectedPiece.coord.x ].piece = null;
		Game.board [ (int)squareIndex.y,
		             (int)squareIndex.x ].piece = Game.currentPlayer.selectedPiece;
		Game.currentPlayer.selectedPiece.coord = squareIndex;
		
		
		// Reset traced squares (if any)
		if ( Game.moveTrace.index != -1 )
			setLineOfSquares ( SquareState.DEFAULT );
		
		// Set trace of the new move
		Game.moveTrace.setTrace ( Game.currentPlayer.selectedPiece.coord, squareIndex );
		
		// Show the new trace of squares
		setLineOfSquares ( SquareState.TRACED );
		
		// Assing TurnState
		Game.turnState = TurnState.ANIMATION;
	}
	
	private void setLineOfSquares ( SquareState stateValue )
	{
		if ( Game.moveTrace.vertical )
			for ( int i = Game.moveTrace.start; i <= Game.moveTrace.end; i++ )
				Game.board [ Game.moveTrace.start,
							 Game.moveTrace.index ].state = stateValue; 
		else
			for ( int i = Game.moveTrace.start; i <= Game.moveTrace.end; i++ )
				Game.board [ Game.moveTrace.index,
							 Game.moveTrace.start ].state = stateValue;
	}
}
