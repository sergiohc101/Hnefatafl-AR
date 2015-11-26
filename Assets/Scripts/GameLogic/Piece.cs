﻿using UnityEngine;
using System.Collections;

public class Piece : Selectable {

	public int index;	// Index of the piece within the pieces array


	// translate coroutine
	public void translate (Vector2 pieceDest)
	{
		Game.audio.playMove ();
        StartCoroutine(translateCoroutine(pieceDest));
	}

    private IEnumerator translateCoroutine(Vector2 pieceDest)
    {
        Transform targetSquare = Game.board[(int)pieceDest.y, (int)pieceDest.x].transform;
        float progress = 0;
		float duration = 1f;
        float startTime = Time.time;

        while (progress < 1)
        {
            transform.position = Vector3.Lerp(transform.position, targetSquare.position, progress);
            progress = (Time.time - startTime) / duration;
            yield return null;
        }

		//Debug.Log("Pieza trasladada!");
        transform.position = targetSquare.position;
        Game.turnState = TurnState.APPLYNG_RULES;
		//Game.board[(int)pieceDest.y, (int)pieceDest.x].gameObject.SetActive(false);
    }

	public void die ()
	{
		Game.audio.playDie ();
		StartCoroutine( dieCoroutine() );
	}

	// die coroutine
	public IEnumerator dieCoroutine ()
	{
		Vector3 dest = transform.localPosition;
		dest.y = -0.15f;
		while (Vector3.Distance(transform.localPosition, dest) > 0.05f) {
			transform.localPosition = Vector3.Lerp(transform.localPosition, dest, 0.5f * Time.deltaTime);
			yield return null;
		}
		//Debug.Log("Pieza capturada!");
		gameObject.SetActive (false);
		Game.turnState = TurnState.END;
	}

	public override void rollOver( bool bValue )
	{
		if (bValue)
		{
			coroutine = rollOverCoroutine ();
			StartCoroutine ( coroutine );
		}else
		{
			StopCoroutine ( coroutine );
			renderer.material.color = Color.white;
		}
	}
}
