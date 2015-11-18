using UnityEngine;
using System.Collections;

public abstract class Player {
	public Piece selectedPiece;
	public bool isAttackerPlayer;
	
	public abstract GameAction act();
}
