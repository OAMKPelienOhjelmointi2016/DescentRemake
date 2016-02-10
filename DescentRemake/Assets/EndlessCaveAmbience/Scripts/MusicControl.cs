using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicControl : MonoBehaviour {

	public bool startImmediately=false;
	public GameObject musicToPlay;
	public AudioMixerSnapshot volumeUp;
	public AudioMixerSnapshot volumeDown;
	public float fadeInTime=2;
	public float fadeOutTime=3;

	RandomisedMusic randomisedMusic;
	
	void Awake () {
		randomisedMusic = musicToPlay.GetComponent<RandomisedMusic>();
	}

	void Start ()
	{
		if (startImmediately ==true)
		{
			StartButton();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartButton()
	{
		randomisedMusic.StartMusic();
		StopCoroutine ("WaitToFade");
		volumeUp.TransitionTo(fadeInTime);
	}

	public void StopButton()
	{
		if(randomisedMusic.musicPlaying == true)
		{
		StartCoroutine ("WaitToFade");
		}
	}

	IEnumerator WaitToFade()
	{
		volumeDown.TransitionTo(fadeOutTime);
		yield return new WaitForSeconds (fadeOutTime);
		randomisedMusic.StopMusic();
	}
		
}
