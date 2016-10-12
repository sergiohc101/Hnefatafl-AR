using UnityEngine;
using System.Collections;

public class App : MonoBehaviour {

	public Game game;
	public bool LocalMatch = true;

	void Awake () {
		// Decide which type of game will be played:
		/*
		if (LocalMatch)
			game = new LocalGame ();
		else 
			game = new NetworkGame ();
		*/
	}

	void Start () {
		// Decide which type of game will be played:
		if (LocalMatch)
			game = new LocalGame ();
		else 
			game = new NetworkGame ();
		
		game.initialize ();
	}

	void Update () {
		game.update ();
	}
}
