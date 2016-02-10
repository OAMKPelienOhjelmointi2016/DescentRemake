using UnityEngine;
using System.Collections;

public class WeaponSoundEffects : MonoBehaviour {

    public float volume;
    public AudioClip[] laserAudioClips;
    public AudioClip missileAudioClip;

    public void PlayLaserSoundEffect()
    {
        if (laserAudioClips.Length <= 0)
            return;
        int randomizedClip = Random.Range(0, laserAudioClips.Length);
        AudioSource.PlayClipAtPoint(laserAudioClips[randomizedClip], Camera.main.transform.position, volume);
    }

    public void PlayMissileSoundEffect()
    {
        AudioSource.PlayClipAtPoint(missileAudioClip, Camera.main.transform.position, volume);
    }
}
