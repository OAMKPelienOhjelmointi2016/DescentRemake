using UnityEngine;
using System.Collections;

public class MissileExplosionFunc : MonoBehaviour {

    public float volume;
    public AudioClip explosionSoundeffect;

	void Start () {
        AudioSource.PlayClipAtPoint(explosionSoundeffect, transform.position, volume);
        GameObject.Destroy(this.gameObject, .18f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
