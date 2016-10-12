using UnityEngine;
using System.Collections;

public abstract class GameAction {
	
	public abstract bool validate();
	public abstract void execute();
	public abstract GameMessage getMessage();

	protected void setCrossOfSquares( SquareState stateValue, Vector2 pieceCoords )
	{ // Checar casillas hostiles
		int coord_x = (int)pieceCoords.x;
		int coord_y = (int)pieceCoords.y;
        for ( int x = coord_x + 1; x < 11; x++ )
            if (!Game.board[coord_y, x].piece)
                Game.board[coord_y, x].changeState(stateValue);
            else
                break;
		for ( int x = coord_x - 1; x >= 0; x-- )
			if ( !Game.board [coord_y, x].piece )
                Game.board[coord_y, x].changeState(stateValue);
			else 
				break;
		for ( int y = coord_y + 1; y < 11; y++ )
			if ( !Game.board [y, coord_x].piece )
                Game.board[y, coord_x].changeState(stateValue);
			else 
				break;
		for ( int y = coord_y - 1; y >= 0; y-- )
			if ( !Game.board [y, coord_x].piece )
                Game.board[y, coord_x].changeState(stateValue);
			else 
				break;
	}
}
