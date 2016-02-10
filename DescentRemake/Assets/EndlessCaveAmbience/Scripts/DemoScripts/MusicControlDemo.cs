using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicControlDemo : MonoBehaviour {
	
	public bool startImmediately=false;
	public GameObject musicToPlay;
	public AudioMixerSnapshot volumeUp;
	public AudioMixerSnapshot volumeDown;
	public float fadeInTime=2;
	public float fadeOutTime=3;
	
	RandomisedMusicDemo RandomisedMusicDemo;
	
	void Awake () {
		RandomisedMusicDemo = musicToPlay.GetComponent<RandomisedMusicDemo>();
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
		RandomisedMusicDemo.StartMusic();
		StopCoroutine ("WaitToFade");
		volumeUp.TransitionTo(fadeInTime);
	}
	
	public void StopButton()
	{
		if(RandomisedMusicDemo.musicPlaying == true)
		{
			StartCoroutine ("WaitToFade");
		}
	}
	
	IEnumerator WaitToFade()
	{
		volumeDown.TransitionTo(fadeOutTime);
		yield return new WaitForSeconds (fadeOutTime);
		RandomisedMusicDemo.StopMusic();
	}
	
}
