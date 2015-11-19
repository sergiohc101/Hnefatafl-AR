using UnityEngine;
using System.Collections;

public abstract class Game {

	public static TurnState turnState;
	public static Piece[] pieces;
	public static Square[,] board;
	public static Marker marker;
	
	public static Trace moveTrace;

	public static Player currentPlayer;
	protected int p1Score, p2Score;
	
	public abstract void performAction( GameAction gameAction );
	public abstract void endTurn();

	public Game()
	{
		turnState = TurnState.PIECE_SELECTION;
		pieces = new Piece[37];
		board = new Square[11, 11];
		p1Score = p2Score = 0;
	}

	public void initialize () {
		moveTrace = new Trace ();
		loadBoard ();
	}

	private void loadBoard()
	{
		// Game class get references of scene gameObjects

        // Inactivate marker:
        marker = GameObject.Find("Marker").GetComponent<Marker>();
        marker.gameObject.SetActive(false);

		// Load squares inside the board array:
		Transform squares = GameObject.Find ("Squares").transform;
		Transform row;
		for (int j = 0; j < 11; j++) {
			row = squares.GetChild(j);
			for (int i = 0; i < 11; i++) {
				board[j, i] = row.GetChild(i).GetComponent<Square>();
				board[j, i].coord = new Vector2(i, j);
			}
		}

		// Load pieces inside the pieces array:
		Transform piecesSets = GameObject.Find ("Pieces").transform;
		int index = 0;
		foreach (Transform ps in piecesSets) {
			foreach (Transform piece in ps) {
				pieces[index] = piece.GetComponent<Piece>();
				pieces[index].index = index;
				index++;
			}
		}

		// Assign each piece to its initial square and assign each piece its coord:
		Transform pieceSet = GameObject.Find ("PieceSetNorth").transform;
		board [0, 3].piece = pieceSet.GetChild (0).GetComponent<Piece>();
		board [0, 4].piece = pieceSet.GetChild (1).GetComponent<Piece>();
		board [0, 5].piece = pieceSet.GetChild (2).GetComponent<Piece>();
		board [0, 6].piece = pieceSet.GetChild (3).GetComponent<Piece>();
		board [0, 7].piece = pieceSet.GetChild (4).GetComponent<Piece>();
		board [1, 5].piece = pieceSet.GetChild (5).GetComponent<Piece>();
		board [0, 3].piece.coord = new Vector2 (3, 0);
		board [0, 4].piece.coord = new Vector2 (4, 0);
		board [0, 5].piece.coord = new Vector2 (5, 0);
		board [0, 6].piece.coord = new Vector2 (6, 0);
		board [0, 7].piece.coord = new Vector2 (7, 0);
		board [1, 5].piece.coord = new Vector2 (5, 1);

		pieceSet = GameObject.Find ("PieceSetWest").transform;
		board [3,0].piece = pieceSet.GetChild (0).GetComponent<Piece>();
		board [4,0].piece = pieceSet.GetChild (1).GetComponent<Piece>();
		board [5,0].piece = pieceSet.GetChild (2).GetComponent<Piece>();
		board [5,1].piece = pieceSet.GetChild (3).GetComponent<Piece>();
		board [6,0].piece = pieceSet.GetChild (4).GetComponent<Piece>();
		board [7,0].piece = pieceSet.GetChild (5).GetComponent<Piece>();
		board [3,0].piece.coord = new Vector2 (0, 3);
		board [4,0].piece.coord = new Vector2 (0, 4);
		board [5,0].piece.coord = new Vector2 (0, 5);
		board [5,1].piece.coord = new Vector2 (1, 5);
		board [6,0].piece.coord = new Vector2 (0, 6);
		board [7,0].piece.coord = new Vector2 (0, 7);

		pieceSet = GameObject.Find ("PieceSetCenter").transform;
		board [3,5].piece = pieceSet.GetChild (0).GetComponent<Piece>();
		board [4,4].piece = pieceSet.GetChild (1).GetComponent<Piece>();
		board [4,5].piece = pieceSet.GetChild (2).GetComponent<Piece>();
		board [4,6].piece = pieceSet.GetChild (3).GetComponent<Piece>();
		board [5,3].piece = pieceSet.GetChild (4).GetComponent<Piece>();
		board [5,4].piece = pieceSet.GetChild (5).GetComponent<Piece>();
		board [5,5].piece = pieceSet.GetChild (6).GetComponent<Piece>();
		board [5,6].piece = pieceSet.GetChild (7).GetComponent<Piece>();
		board [5,7].piece = pieceSet.GetChild (8).GetComponent<Piece>();
		board [6,4].piece = pieceSet.GetChild (9).GetComponent<Piece>();
		board [6,5].piece = pieceSet.GetChild (10).GetComponent<Piece>();
		board [6,6].piece = pieceSet.GetChild (11).GetComponent<Piece>();
		board [7,5].piece = pieceSet.GetChild (12).GetComponent<Piece>();
		board [3,5].piece.coord = new Vector2 (5, 3);
		board [4,4].piece.coord = new Vector2 (4, 4);
		board [4,5].piece.coord = new Vector2 (5, 4);
		board [4,6].piece.coord = new Vector2 (6, 4);
		board [5,3].piece.coord = new Vector2 (3, 5);
		board [5,4].piece.coord = new Vector2 (4, 5);
		board [5,5].piece.coord = new Vector2 (5, 5);
		board [5,6].piece.coord = new Vector2 (6, 5);
		board [5,7].piece.coord = new Vector2 (7, 5);
		board [6,4].piece.coord = new Vector2 (4, 6);
		board [6,5].piece.coord = new Vector2 (5, 6);
		board [6,6].piece.coord = new Vector2 (6, 6);
		board [7,5].piece.coord = new Vector2 (5, 7);

		pieceSet = GameObject.Find ("PieceSetEast").transform;
		board [3,10].piece = pieceSet.GetChild (0).GetComponent<Piece>();
		board [4,10].piece = pieceSet.GetChild (1).GetComponent<Piece>();
		board [5, 9].piece = pieceSet.GetChild (2).GetComponent<Piece>();
		board [5,10].piece = pieceSet.GetChild (3).GetComponent<Piece>();
		board [6,10].piece = pieceSet.GetChild (4).GetComponent<Piece>();
		board [7,10].piece = pieceSet.GetChild (5).GetComponent<Piece>();
		board [3,10].piece.coord = new Vector2 (10, 3);
		board [4,10].piece.coord = new Vector2 (10, 4);
		board [5, 9].piece.coord = new Vector2 ( 9, 5);
		board [5,10].piece.coord = new Vector2 (10, 5);
		board [6,10].piece.coord = new Vector2 (10, 6);
		board [7,10].piece.coord = new Vector2 (10, 7);

		pieceSet = GameObject.Find ("PieceSetSouth").transform;
		board [9, 5].piece = pieceSet.GetChild (0).GetComponent<Piece>();
		board [10,3].piece = pieceSet.GetChild (1).GetComponent<Piece>();
		board [10,4].piece = pieceSet.GetChild (2).GetComponent<Piece>();
		board [10,5].piece = pieceSet.GetChild (3).GetComponent<Piece>();
		board [10,6].piece = pieceSet.GetChild (4).GetComponent<Piece>();
		board [10,7].piece = pieceSet.GetChild (5).GetComponent<Piece>();
		board [9, 5].piece.coord = new Vector2 (5, 9);
		board [10,3].piece.coord = new Vector2 (3, 10);
		board [10,4].piece.coord = new Vector2 (4, 10);
		board [10,5].piece.coord = new Vector2 (5, 10);
		board [10,6].piece.coord = new Vector2 (6, 10);
		board [10,7].piece.coord = new Vector2 (7, 10);
	}

	public void update()
	{
		// Game loop
		switch ( turnState )
		{
		case TurnState.PIECE_SELECTION:
		case TurnState.PIECE_SELECTED:
			GameAction incomingAction = currentPlayer.act ();
			if( incomingAction != null )
				performAction ( incomingAction );
			break;
			
		case TurnState.APPLYNG_RULES:
			applyRules ();
			break;
			
		case TurnState.END:
			endTurn ();
			break;
		}
	}

	private void applyRules()
	{
		// Calculate and execute victorie or captures
		if ( victorie() )
		{
			pieces[18].die ();
			turnState = TurnState.END; // ANIMATION
			return;
		}

		// Calculate captures and execute piece coroutine die()
		int r = (int)currentPlayer.selectedPiece.coord.y;
		int c = (int)currentPlayer.selectedPiece.coord.x;
		if( c < 9 )		checkCapture ( board [r,c+1], board [r,c+2] );
		if( c > 1 )		checkCapture ( board [r,c-1], board [r,c-2] );
		if( r < 9 )		checkCapture ( board [r+1,c], board [r+2,c] );
		if( r > 1 )		checkCapture ( board [r-1,c], board [r-2,c] );
		
		
		currentPlayer.selectedPiece = null;
		turnState = TurnState.END; // ANIMATION
	}

	bool victorie()
	{
		if ( currentPlayer.isAttackerPlayer )
		{		// Attack team 
			return checkAttackVictorie ();
		}
		else
		{		// Defense team
			if ( currentPlayer.selectedPiece.transform.tag == "King" )
			{
				Vector2 kingCoord = currentPlayer.selectedPiece.coord;
				if ( (kingCoord.x == 0 || kingCoord.x == 10) &&
				     (kingCoord.y == 0 || kingCoord.y == 10) )
					return true;  // King is in corner
			}
			
		}
		return false;
	}
	
	bool checkAttackVictorie()
	{
		int r = (int)currentPlayer.selectedPiece.coord.y;
		int c = (int)currentPlayer.selectedPiece.coord.x;
		if     ( c<10 && board[r,c+1].hasTheKing() )
		{
			if ( (c==9  || board[r  ,c+2].hasAnAttacker() ) && 
			     (r==0  || board[r-1,c+1].hasAnAttacker() ) &&
			     (r==10 || board[r+1,c+1].hasAnAttacker() ) )
				return true;
		}
		else if( c>0  && board[r,c-1].hasTheKing() )
		{
			if ( (c==1  || board[r,c-2  ].hasAnAttacker() ) && 
			     (r==0  || board[r-1,c+1].hasAnAttacker() ) &&
			     (r==10 || board[r+1,c+1].hasAnAttacker() ) )
				return true;
		}
		else if( r<10 && board[r+1,c].hasTheKing() )
		{
			if ( (r==9  || board[r+2,c  ].hasAnAttacker() ) && 
			     (c==0  || board[r+1,c-1].hasAnAttacker() ) &&
			     (c==10 || board[r+1,c+1].hasAnAttacker() ) )
				return true;
		}
		else if( r>0  && board[r-1,c].hasTheKing() )
		{
			if ( (r==1  || board[r-2,c  ].hasAnAttacker() ) && 
			     (c==0  || board[r-1,c-1].hasAnAttacker() ) &&
			     (c==10 || board[r-1,c+1].hasAnAttacker() ) )
				return true;
		}
		return false;
	}
	
	void checkCapture ( Square sqrFirst, Square sqrSecond )
	{
		if (sqrFirst.piece)
		{
			if (sqrSecond.piece)
			{
				bool firstIsAttacker = sqrFirst.piece.transform.tag == "Attacker";
				bool secondIsAttacker = sqrSecond.piece.transform.tag == "Attacker";

				if (firstIsAttacker != currentPlayer.isAttackerPlayer  && // first is Enemy
				    secondIsAttacker == currentPlayer.isAttackerPlayer && // second is Teammate
				    sqrFirst.piece.transform.tag != "King")
				{
					sqrFirst.piece.die ();
					sqrFirst.piece = null;
				}

			} else if ((sqrSecond.coord.x == 0 || sqrSecond.coord.x == 10)  &&
				       (sqrSecond.coord.y == 0 || sqrSecond.coord.y == 10)  ||
				       (sqrSecond.coord.x == 5 && sqrSecond.coord.y == 5 )) 
			{ 
				// sqrSecond is Hostil Zone
				bool firstIsAttacker = sqrFirst.piece.transform.tag == "Attacker";
				if (firstIsAttacker != currentPlayer.isAttackerPlayer && // first is Enemy
				    sqrFirst.piece.transform.tag != "King")
				{
					sqrFirst.piece.die ();
					sqrFirst.piece = null;
				}
			}
		}
	}
}
