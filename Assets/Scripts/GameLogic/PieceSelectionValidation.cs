using UnityEngine;
using System.Collections;

public class PieceSelectionValidation : ValidateAction 
{
	
	public override bool validate ( GameAction gameAction )
	{
		PieceSelection selectionAction = (PieceSelection)gameAction;
		
		// Verify that the selected piece belongs to player on turn
		return selectionAction.belongsToAttacker == Game.currentPlayer.isAttackerPlayer ;
	}
}
