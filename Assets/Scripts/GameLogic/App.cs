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
	}
}
