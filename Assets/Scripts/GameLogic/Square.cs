using UnityEngine;
using System.Collections;

public class Square : Selectable {

	public SquareState state; 	// Current state of the Square object
	public Piece piece;			// Piece object this Square has currently on top of it

	protected override void Awake()
	{
		state = SquareState.DEFAULT;
        gameObject.SetActive(false);
	}

	// rollOver coroutine
	public override IEnumerator rollOver ()
	{
		yield return null;
	}

    public void changeState(SquareState squareState)
    {
        switch (squareState)
        {
            case SquareState.VALID:
                gameObject.SetActive(true);
				state = state == SquareState.TRACED ? SquareState.VALID_TRACED : SquareState.VALID;
                break;
            case SquareState.DEFAULT:
                gameObject.SetActive(false);
				state = state == SquareState.VALID_TRACED ? SquareState.TRACED : SquareState.DEFAULT;
				state = squareState;
                break;
            case SquareState.POINTED:
                gameObject.SetActive(true);
				state = squareState;
                break;
            case SquareState.TRACED:
                gameObject.SetActive(true);
				state = squareState;
                break;
		}
    }
}
