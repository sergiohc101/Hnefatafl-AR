using UnityEngine;
using System.Collections;

public abstract class Player {
	protected Piece selectedPiece;
	public abstract Action act();
}
