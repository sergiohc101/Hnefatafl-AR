using UnityEngine;
using System.Collections;

public class TurnIndicator : MonoBehaviour {

	protected Animator animator;

	public void show()
	{
		animator.SetTrigger("Show");
	}

	public void Awake()
	{
		animator = GetComponent<Animator> ();
	}
}
