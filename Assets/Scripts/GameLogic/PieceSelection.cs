using UnityEngine;
using System.Collections;

public class PieceSelection : GameAction
{
	int pieceIndex;
	public bool belongsToAttacker;

	public PieceSelection( int iPieceIndex, bool bta )
	{
		pieceIndex = iPieceIndex;
		belongsToAttacker = bta;
	}

	public override bool validate ()
	{
		ValidateAction validation = new PieceSelectionValidation ();
		return validation.validate ( this );
	}

	public override void execute ()
	{
		Game.audio.playSelect ();

		// Set Marker on selected piece
		float h = Game.pieces[ pieceIndex ].transform.tag == "King" ? 24f : 13f ;
		Game.marker.gameObject.SetActive ( true );
		Game.marker.activate ( Game.pieces[ pieceIndex ].transform.position +
		                        new Vector3( 0f,  h, 0f ) );
		
		
		// Reset valid squares for the previous selection (if any)
		if ( Game.currentPlayer.selectedPiece ) 
			setCrossOfSquares ( SquareState.DEFAULT, Game.currentPlayer.selectedPiece.coord );
		
		// Set new selection
		Game.currentPlayer.selectedPiece = Game.pieces [pieceIndex];
		
		// Show valid squares for the new selection
		setCrossOfSquares ( SquareState.VALID, Game.pieces [pieceIndex].coord );
		CheckHostileZones ( Game.pieces [pieceIndex].transform.tag );
		
		// Assign TurnState
		Game.turnState = TurnState.PIECE_SELECTED;
	}
	
	private void CheckHostileZones( string selectedPieceTag )
	{
		if ( selectedPieceTag != "King" )
		{
			Game.board [ 0, 0].changeState ( SquareState.DEFAULT );
			Game.board [ 0,10].changeState ( SquareState.DEFAULT );
			Game.board [10, 0].changeState ( SquareState.DEFAULT );
			Game.board [10,10].changeState ( SquareState.DEFAULT );
			Game.board [ 5, 5].changeState ( SquareState.DEFAULT );
		}
	}
}
