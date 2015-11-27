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
			// Execute couroutine of change of turn

			// Change player on turn
		currentPlayer = currentPlayer.Equals(player2) ? player1 : player2;

			// Set TurnState
		turnState = TurnState.PIECE_SELECTION;
	}
}
