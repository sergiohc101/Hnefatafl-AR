using UnityEngine;
using System.Collections;

public class Square : Selectable {

	public SquareState state; 	// Current state of the Square object
	public Piece piece;			// Piece object this Square has currently on top of it

	protected override void Awake()
	{
		state = SquareState.DEFAULT;
	}

	// rollOver corroutine
	public override void rollOver ()
	{

	}
}
