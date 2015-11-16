using UnityEngine;
using System.Collections;

public class NetworkGame : Game {
	
	LocalPlayerPI player1;
	RemotePlayer player2;

	protected override void Awake()
	{
		
	}
	public override void performAction ( GameAction a )
	{
		if (currentPlayer is RemotePlayer) //  currentPlayer == player2
			a.execute ();
		else if (a.validate ()) 
		{
			// Send Action
			a.execute ();
		}
	}
	public override void endTurn()
	{
		// Execute couroutine of change of turn
		// Assign TurnState
	}
}
