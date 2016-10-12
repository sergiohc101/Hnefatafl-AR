using UnityEngine;
using System.Collections;

public class RemotePlayer : Player {

	private BTManager BManager;

	public RemotePlayer()
	{
		BManager = GameObject.Find("PersistanceManager").GetComponent<BTManager>();
		//if(BManager == null)
		//BManager.sendBTMessage("BTManager found by RemotePlayer");
	}

	public override GameAction act() {

		if(BManager == null){
			BManager = GameObject.Find("PersistanceManager").GetComponent<BTManager>();
			BManager.sendBTMessage("BTManager found by RemotePlayer");
			return null;
		}
		else{
			// Check received Actions using BTManager
			GameMessage gm = BManager.getGameMessage();
			if(gm != null)
			{
				if(gm.bPieceSelectionMessage)
					return new PieceSelection(gm.iIndex1,gm.belongsToAttacker);
				else
					return new PieceMove(new Vector2(gm.iIndex1, gm.iIndex2));
			}
			return null;
		}
	}

}
