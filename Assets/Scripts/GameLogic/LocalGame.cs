using UnityEngine;
using System.Collections;

public abstract class LocalGame : Game {
	
	LocalPlayerPDC player1;
	LocalPlayerPDC player2;


	protected override void Awake()
	{
		
	}
	public override void performAction( GameAction a )
	{
		if ( a.validate () )
			a.execute ();
	}
	public override void endTurn()
	{
		// Execute couroutine of change of turn
		// Assign TurnState
	}
}
