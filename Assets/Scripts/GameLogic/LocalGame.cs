using UnityEngine;
using System.Collections;

public class LocalGame : Game {
	
	LocalPlayerPDC player1;
	LocalPlayerPDC player2;

	public LocalGame()
	{
		player1 = new LocalPlayerPDC ();
		player2 = new LocalPlayerPDC ();
	}
	public override void performAction( GameAction gameAction )
	{
		if ( gameAction.validate () )
			gameAction.execute ();
	}
	public override void endTurn()
	{
		// Execute couroutine of change of turn
		// Assign TurnState
	}
}
