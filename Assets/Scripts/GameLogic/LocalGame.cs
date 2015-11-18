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
	}
	public override void endTurn()
	{
		Debug.Log("endTurn");
		// Execute couroutine of change of turn
		// Assign TurnState
		currentPlayer = currentPlayer.Equals(player2) ? player1 : player2;
		turnState = TurnState.PIECE_SELECTION;
		Debug.Log(currentPlayer);
		Debug.Log(player1);
		Debug.Log(player2);
	}
}
