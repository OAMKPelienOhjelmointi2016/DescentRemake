using UnityEngine;
using System.Collections;

public class RandomisedMusicOriginal : MonoBehaviour {

	public bool playOnStart=true;
	public int bpm=95;
	public int beatsPerBar=4;
	public int barsBetweenTriggers=4;
	public AudioSource bed;
	public AudioSource melody;
	public int melodyChanceToPlay=5;
	public AudioSource perc;
	public int percChanceToPlay=8;
	public AudioSource fx;
	public int fxChanceToPlay=2;
	public int chanceRange=10;
	public AudioClip[] melodyArray;
	public AudioClip[] percArray;
	public AudioClip[] fxArray;

	int melodyIndex;
	int percIndex;
	int fxIndex;
	int triggerIndex;

	float timer;
	float timeBetweenTriggers;
	public bool musicPlaying;

	void Awake ()
	{
		timeBetweenTriggers = ((60f / bpm) * beatsPerBar)* barsBetweenTriggers;
		melodyIndex = Random.Range(0,melodyArray.Length);
		percIndex = Random.Range(0,percArray.Length);
		fxIndex = Random.Range(0,fxArray.Length);
	}
	
	void Start () {
	
	if(playOnStart == true)
		{
		StartMusic();
		}
	}

	void Update () {

		if (musicPlaying == true)
		{
			timer += Time.deltaTime;
			if(timer > timeBetweenTriggers)
			{
				TriggerPoint();
			} 
		}

	}

	public void StartMusic()
	{	
		if (musicPlaying == false)
		{
		
		musicPlaying = true;
		timer = 0f;
		//Debug.Log("Music Started");
		bed.Play();
		}
	}

	void PlayMelody()
	{
		if (!melody.isPlaying)
		{
			melody.clip = melodyArray[melodyIndex];
			melody.Play();
			melodyIndex = Random.Range(0,melodyArray.Length);
			//Debug.Log("Melody Triggered");
		}
	}

	void PlayPerc()
	{
		if (!perc.isPlaying)
		{
			perc.clip = percArray[percIndex];
			perc.Play();
			percIndex = Random.Range(0,percArray.Length);
			//Debug.Log("Percussion Triggered");
		}

	}

	void PlayFX()
	{
		if (!fx.isPlaying)
		{
			fx.clip = fxArray[fxIndex];
			fx.Play();
			fxIndex = Random.Range(0,fxArray.Length);
			//Debug.Log("FX Triggered");
		}
	}

	void TriggerPoint()
	{	
		timer = 0f;
		triggerIndex = Random.Range(1,chanceRange);

		if (triggerIndex <= melodyChanceToPlay){
			PlayMelody();
		}

		if (triggerIndex <= percChanceToPlay){
			PlayPerc();
		}

		if (triggerIndex <= fxChanceToPlay){
			PlayFX();
		}

	}

	public void StopMusic()
	{
		bed.Stop();
		melody.Stop();
		perc.Stop();
		fx.Stop();
		musicPlaying = false;
		//Debug.Log("Music Stopped");
	}

}