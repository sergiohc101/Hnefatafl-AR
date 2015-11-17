using UnityEngine;
using System.Collections;

public abstract class Game {

	public static TurnState turnState;
	public static Piece[] pieces;
	public static Square[,] board;

	protected Player currentPlayer;
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
		loadBoard ();
	}

	private void loadBoard()
	{
		// Game class get references of scene gameObjects

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
		board [0, 3].piece.coord = new Vector2 (0, 3);
		board [0, 4].piece.coord = new Vector2 (0, 4);
		board [0, 5].piece.coord = new Vector2 (0, 5);
		board [0, 6].piece.coord = new Vector2 (0, 6);
		board [0, 7].piece.coord = new Vector2 (0, 7);
		board [1, 5].piece.coord = new Vector2 (1, 5);

		pieceSet = GameObject.Find ("PieceSetWest").transform;
		board [3,0].piece = pieceSet.GetChild (0).GetComponent<Piece>();
		board [4,0].piece = pieceSet.GetChild (1).GetComponent<Piece>();
		board [5,0].piece = pieceSet.GetChild (2).GetComponent<Piece>();
		board [5,1].piece = pieceSet.GetChild (3).GetComponent<Piece>();
		board [6,0].piece = pieceSet.GetChild (4).GetComponent<Piece>();
		board [7,0].piece = pieceSet.GetChild (5).GetComponent<Piece>();
		board [3,0].piece.coord = new Vector2 (3, 0);
		board [4,0].piece.coord = new Vector2 (4, 0);
		board [5,0].piece.coord = new Vector2 (5, 0);
		board [5,1].piece.coord = new Vector2 (5, 1);
		board [6,0].piece.coord = new Vector2 (6, 0);
		board [7,0].piece.coord = new Vector2 (7, 0);

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
		board [3,5].piece.coord = new Vector2 (3, 5);
		board [4,4].piece.coord = new Vector2 (4, 4);
		board [4,5].piece.coord = new Vector2 (4, 5);
		board [4,6].piece.coord = new Vector2 (4, 6);
		board [5,3].piece.coord = new Vector2 (5, 3);
		board [5,4].piece.coord = new Vector2 (5, 4);
		board [5,5].piece.coord = new Vector2 (5, 5);
		board [5,6].piece.coord = new Vector2 (5, 6);
		board [5,7].piece.coord = new Vector2 (5, 7);
		board [6,4].piece.coord = new Vector2 (6, 4);
		board [6,5].piece.coord = new Vector2 (6, 5);
		board [6,6].piece.coord = new Vector2 (6, 6);
		board [7,5].piece.coord = new Vector2 (7, 5);

		pieceSet = GameObject.Find ("PieceSetEast").transform;
		board [3,10].piece = pieceSet.GetChild (0).GetComponent<Piece>();
		board [4,10].piece = pieceSet.GetChild (1).GetComponent<Piece>();
		board [5, 9].piece = pieceSet.GetChild (2).GetComponent<Piece>();
		board [5,10].piece = pieceSet.GetChild (3).GetComponent<Piece>();
		board [6,10].piece = pieceSet.GetChild (4).GetComponent<Piece>();
		board [7,10].piece = pieceSet.GetChild (5).GetComponent<Piece>();
		board [3,10].piece.coord = new Vector2 (3,10);
		board [4,10].piece.coord = new Vector2 (4,10);
		board [5, 9].piece.coord = new Vector2 (5, 9);
		board [5,10].piece.coord = new Vector2 (5,10);
		board [6,10].piece.coord = new Vector2 (6,10);
		board [7,10].piece.coord = new Vector2 (7,10);

		pieceSet = GameObject.Find ("PieceSetSouth").transform;
		board [9, 5].piece = pieceSet.GetChild (0).GetComponent<Piece>();
		board [10,3].piece = pieceSet.GetChild (1).GetComponent<Piece>();
		board [10,4].piece = pieceSet.GetChild (2).GetComponent<Piece>();
		board [10,5].piece = pieceSet.GetChild (3).GetComponent<Piece>();
		board [10,6].piece = pieceSet.GetChild (4).GetComponent<Piece>();
		board [10,7].piece = pieceSet.GetChild (5).GetComponent<Piece>();
		board [9, 5].piece.coord = new Vector2 (9, 5);
		board [10,3].piece.coord = new Vector2 (10,3);
		board [10,4].piece.coord = new Vector2 (10,4);
		board [10,5].piece.coord = new Vector2 (10,5);
		board [10,6].piece.coord = new Vector2 (10,6);
		board [10,7].piece.coord = new Vector2 (10,7);
	}

	public void update()
	{
		// Game loop
		// switch ( turnState )
	}

	private void applyRules()
	{
		// Calculate and execute victorie or captures
	}
}
