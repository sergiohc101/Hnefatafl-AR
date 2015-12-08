using UnityEngine;
using System.Collections;

public class LocalGame : Game {
	
	LocalPlayerPDC player1;
	LocalPlayerPDC player2;

	public LocalGame()
	{
		player1 = new LocalPlayerPDC (true);
		player2 = new LocalPlayerPDC (false);
        currentPlayer = player1;
	}
	public override void performAction( GameAction gameAction )
	{
		if ( gameAction.validate () )
			gameAction.execute ();
		else
			Game.audio.playError ();
	}
	public override void endTurn()
	{
		// Set TurnState
		turnState = TurnState.PIECE_SELECTION;

		// Change player on turn and show turn change animation
		if (currentPlayer.Equals(player2)) {
			currentPlayer = player1;
			attackerTurnIndicator.show();
		}
		else {
			currentPlayer = player2;
			defenderTurnIndicator.show();
		}
	}
}
