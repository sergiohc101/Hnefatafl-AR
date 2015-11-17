using UnityEngine;
using System.Collections;

public abstract class GameAction {
	
	public abstract bool validate();
	public abstract void execute();

	protected void setCrossOfSquares( SquareState stateValue, Vector2 pieceCoords )
	{ // Checar casillas hostiles
		int coord_x = (int)pieceCoords.x;
		int coord_y = (int)pieceCoords.y;
		for ( int x = coord_x + 1; x < 10; x++ )
			if ( Game.board [x,coord_y].piece )
				Game.board [x,coord_y].state = stateValue;
			else 
				break;
		for ( int x = coord_x - 1; x > 0; x-- )
			if ( Game.board [x,coord_y].piece )
				Game.board [x,coord_y].state = stateValue;
			else 
				break;
		for ( int y = coord_y + 1; y < 11; y++ )
			if ( Game.board [coord_x,y].piece )
				Game.board [coord_x,y].state = stateValue;
			else 
				break;
		for ( int y = coord_y - 1; y >= 0; y-- )
			if ( Game.board [coord_x,y].piece )
				Game.board [coord_x,y].state = stateValue;
			else 
				break;
	}
}
