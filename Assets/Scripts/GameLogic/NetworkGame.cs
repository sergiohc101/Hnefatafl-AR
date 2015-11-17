using UnityEngine;
using System.Collections;

public class NetworkGame : Game {
	
	LocalPlayerPI player1;
	RemotePlayer player2;

	public NetworkGame()
	{
		player1 = new LocalPlayerPI ();
		player2 = new RemotePlayer ();
	}
	public override void performAction ( GameAction gameAction )
	{
		if (currentPlayer is RemotePlayer) //  currentPlayer == player2
			gameAction.execute ();
		else if (gameAction.validate ()) 
		{
			// Send Action
			gameAction.execute ();
		}
	}
	public override void endTurn()
	{
		// Execute couroutine of change of turn
		// Assign TurnState
	}
}
