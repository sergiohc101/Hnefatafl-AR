using UnityEngine;
using System.Collections;

public abstract class RemotePlayer : Player {
	
	public override GameAction act() {
		// Check received Actions using BTManager
		return new PieceMove(new Vector2());
	}
}
