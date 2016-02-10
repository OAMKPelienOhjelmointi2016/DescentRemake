using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour {

	public AudioMixer masterMixer;

	public void SetVolume(float masterLevel)
	{
		masterMixer.SetFloat ("masterVol", masterLevel);
	}

}
