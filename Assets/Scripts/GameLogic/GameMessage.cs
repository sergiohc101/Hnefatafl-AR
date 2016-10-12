using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class GameMessage
{
	public bool bPieceSelectionMessage;	// Flag to identify type of GameAction
	public int iIndex1; // PieceIndex or SquareIndex.coord.x
	public int iIndex2; // SquareIndex.coord.y
	public bool belongsToAttacker; 
}


