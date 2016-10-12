using UnityEngine;
using System.Collections;

public class NetworkGame : Game {

	private BTManager BManager;

	LocalPlayerPI player1;
	RemotePlayer player2;
	//LocalPlayerPI player2;

	public NetworkGame()
	{
		BManager = GameObject.Find("PersistanceManager").GetComponent<BTManager>();

		player1 = new LocalPlayerPI ();
		player2 = new RemotePlayer ();
		//player2 = new LocalPlayerPI ();

		if(BTManager.isServer){
			player1.isAttackerPlayer = false;
			player2.isAttackerPlayer = true;
			currentPlayer = player2;
		}
		else{
			player1.isAttackerPlayer = true;
			player2.isAttackerPlayer = false;
			currentPlayer = player1;
		}
	}
		
	public override void performAction ( GameAction gameAction )
	{
		if( currentPlayer.Equals(player2) ) //  currentPlayer == player2 turn
			gameAction.execute ();
		else if (gameAction.validate ()) 
		{
			BManager.sendGameMessage(gameAction.getMessage());
			gameAction.execute ();
		}
		else audio.playError ();
	}
	public override void endTurn()
	{
			// Execute couroutine of change of turn

			// Change player on turn
		currentPlayer = currentPlayer.Equals(player1) ? (Player)player2 : (Player)player1;
		
			// Set TurnState
		turnState = TurnState.PIECE_SELECTION;
	}
}
