using UnityEngine;
using System.Collections;

public class Piece : Selectable {

	public int index;	// Index of the piece within the pieces array

	// rollOver coroutine
	public override IEnumerator rollOver ()
	{
		yield return null;
	}

	// translate coroutine
	public IEnumerator translate (Vector2 pieceDest)
	{
		Transform targetSquare = Game.board[(int)pieceDest.y, (int)pieceDest.x].transform;
		while (Vector3.Distance(transform.position, targetSquare.position) > 0.05f) {
			transform.position = Vector3.Lerp(transform.position, targetSquare.position, 2f * Time.deltaTime);
			yield return null;
		}
		//Debug.Log("Pieza trasladada!");
	}

	// die coroutine
	public IEnumerator die ()
	{
		Vector3 dest = transform.localPosition;
		dest.y = -0.15f;
		while (Vector3.Distance(transform.localPosition, dest) > 0.05f) {
			transform.localPosition = Vector3.Lerp(transform.localPosition, dest, 0.5f * Time.deltaTime);
			yield return null;
		}
		//Debug.Log("Pieza capturada!");
	}
}
