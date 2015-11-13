using UnityEngine;
using System.Collections;

public abstract class Game : MonoBehaviour {

	public static TurnState turnState;
	public static Piece[] pieces;
	public static SquareBehavior[][] board;

	protected Player currentPlayer;
	protected int p1Score, p2Score;

	
	protected abstract void Awake(); // Initialize class members
	public abstract void performAction( Action a );
	public abstract void endTurn();

	void Start()
	{
		loadBoard ();
	}
	void loadBoard()
	{
		// Game class get references of scene gameObjects
	}
	void Update()
	{
		// Game loop
		// switch ( turnState )
	}
	void applyRules()
	{
		// Calculate and execute victorie or captures
	}
}
