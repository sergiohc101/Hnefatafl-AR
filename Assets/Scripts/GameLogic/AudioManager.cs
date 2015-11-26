using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
	public AudioClip sndSelection;
	public AudioClip sndError;
	public AudioClip sndMove;
	public AudioClip sndDie;
	public AudioClip sndEndOfTurn;

	AudioSource audio;

	void Awake()
	{
		audio = GetComponent<AudioSource>();
	}

	public void playSelect()
	{
		audio.PlayOneShot ( sndSelection );
	}
	public void playError()
	{
		audio.PlayOneShot ( sndError );
	}
	public void playMove()
	{
		audio.PlayOneShot ( sndMove );
	}
	public void playDie()
	{
		audio.PlayOneShot ( sndDie );
	}
	public void playEnd()
	{
		audio.PlayOneShot ( sndEndOfTurn );
	}

}
