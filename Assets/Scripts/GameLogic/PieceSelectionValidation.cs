using UnityEngine;
using System.Collections;

public class PieceSelectionValidation : ValidateAction 
{
	
	public override bool validate ( GameAction gameAction )
	{
		// Use static members of Game
		// Verify that the selected piece belongs to player on turn  
		return true;
	}
}
