using UnityEngine;
using System.Collections;

public class Square : Selectable {

	public SquareState state; 	// Current state of the Square object
	public Piece piece;			// Piece object this Square has currently on top of it
	private Color traceColor;

	protected override void Awake()
	{
		state = SquareState.DEFAULT;
		traceColor = new Color (0.44314f, 0.52157f, 0.89804f);
		//gameObject.SetActive(false);
	}

    public void changeState(SquareState squareState)
    {
        switch (squareState)
        {
            case SquareState.VALID:
				if ( state == SquareState.TRACED )
				{
					state = SquareState.VALID_TRACED;
				}else 
				{
					state = SquareState.VALID;
				}
				GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case SquareState.DEFAULT:
				if ( state == SquareState.VALID_TRACED )
				{
					state = SquareState.TRACED;
					GetComponent<Renderer>().material.color = traceColor;
				}
				else
				{
					GetComponent<Renderer>().material.color = Color.white;
					state = SquareState.DEFAULT;
				}
                break;
            case SquareState.POINTED:
				state = squareState;
                break;
            case SquareState.TRACED:
				state = squareState;
				GetComponent<Renderer>().material.color = traceColor;
                break;
		}
    }
	public bool hasAnAttacker()
	{
		return (piece && piece.transform.tag == "Attacker") || (coord.x == 5f && coord.y == 5f );
	}
	public bool hasTheKing()
	{
		return (piece && piece.transform.tag == "King");
	}
	
	public override void rollOver( bool bValue )
	{
		if (bValue)
		{
			coroutine = rollOverCoroutine ();
			StartCoroutine ( coroutine );
		}else
		{
			StopCoroutine ( coroutine );
			GetComponent<Renderer>().material.color = Color.yellow;
		}
	}
}
