using UnityEngine;
using System.Collections;

public class PieceSelection : GameAction
{
	int pieceIndex;
	bool belongsToAttacker;

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
		// Use the static members of Game
		// Position "Marker" on selected piece
		// Reset valid squares for the previous selection (if any)
		// Show valid squares for the new selection
		// Assign TurnState
	}
}
