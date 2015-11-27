using UnityEngine;
using System.Collections;

public class NetworkGame : Game {
	
	LocalPlayerPI player1;
	//RemotePlayer player2;
	LocalPlayerPI player2;

	public NetworkGame()
	{
		player1 = new LocalPlayerPI ();
		//player2 = new RemotePlayer ();
		player2 = new LocalPlayerPI ();
		player1.isAttackerPlayer = true;
		player2.isAttackerPlayer = false;
		currentPlayer = player1;
	}
	public override void performAction ( GameAction gameAction )
	{
		/*if ( currentPlayer.Equals(player2) ) //  currentPlayer == player2
			gameAction.execute ();
		else if (gameAction.validate ()) 
		{
			// Send Action
			gameAction.execute ();
		}*/
		if (gameAction.validate ()) 
			gameAction.execute ();
		else
			audio.playError ();
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
