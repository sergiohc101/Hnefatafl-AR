using UnityEngine;
using System.Collections;

public class App : MonoBehaviour {

	public Game game;

	void Awake () {
		// Decide which type of game will be played:
		game = new LocalGame ();
	}

	void Start () {
		game.initialize ();
	}

	void Update () {
		game.update ();

		//Codigo para probar las corrutinas:
//        if (Input.GetKeyDown("f"))
//        {
//            StartCoroutine(Game.pieces[12].translate(new Vector2(9, 3)));
//            //StartCoroutine(Game.pieces[12].die());
//        }
	}
}
