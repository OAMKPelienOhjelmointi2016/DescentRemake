using UnityEngine;
using System.Collections;

public class KamikazeEnemy : MonoBehaviour {

	public float spottingDistance = 50;
	public int damageAmount = 50;
	public float speed = 5;
	private GameObject nearestPlayer;
	private GameObject[] players;

	private float nearestPlayerDistance = 99999;

	private GameObject instantedObj;
	[SerializeField]
	private GameObject missileExplosion;
	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<HealthShield> ().health == 0) {
			instantedObj = (GameObject)Instantiate (missileExplosion, this.transform.position, this.transform.rotation);
			Destroy (instantedObj, 1.0f);
			Destroy (this.gameObject);
		}
		foreach (GameObject player in players) {
			if(Vector3.Distance(this.transform.position, player.transform.position) < nearestPlayerDistance) {
				nearestPlayerDistance = Vector3.Distance (this.transform.position, player.transform.position);
				nearestPlayer = player;
			}
		}
	}

	void FixedUpdate() {
		if (nearestPlayerDistance < spottingDistance) {
			transform.LookAt (nearestPlayer.transform);
			GetComponent<Rigidbody> ().AddForce (transform.forward * speed);
		}
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.GetComponent<HealthShield> () != null) {
			other.gameObject.GetComponent<HealthShield> ().takeDmg (damageAmount);
			missileExplosion.transform.localScale = new Vector3 (2f, 2f, 2f);
			instantedObj = (GameObject)Instantiate (missileExplosion, this.transform.position, this.transform.rotation);
			Destroy (instantedObj, 1.0f);
			Destroy (this.gameObject);
		} else {
			missileExplosion.transform.localScale = new Vector3 (2f, 2f, 2f);
			instantedObj = (GameObject)Instantiate (missileExplosion, this.transform.position, this.transform.rotation);
			Destroy (instantedObj, 1.0f);
			Destroy (this.gameObject);
		}
	}
}
