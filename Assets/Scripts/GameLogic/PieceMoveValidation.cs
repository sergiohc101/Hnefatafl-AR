using UnityEngine;
using System.Collections;

public class PieceMoveValidation : ValidateAction 
{
	
	public override bool validate ( GameAction gameAction )
	{
		// Use the static members of Game
		// Verify that the selected square is a valid destination for the selected piece
		return true;
	}
}