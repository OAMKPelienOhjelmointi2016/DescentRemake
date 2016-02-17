using UnityEngine;
using System.Collections;

public class MissileExplosionFunc : MonoBehaviour {

	void Start () {
        GameObject.Destroy(this.gameObject, .18f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
