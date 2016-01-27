using UnityEngine;
using System.Collections.Generic;
//[RequireComponent(typeof(AudioSource))]

public class PlayerMovement : MonoBehaviour {
	Quaternion initialRot;
	
	
	public int forwardSpeed = 1;
	public int turnSpeed = 80;
	public static Vector3 velocity;
	public int rotateSpeed = 80;
	public int maxSpeed = 6;
	public int maxSpeedThrust = 10;
//	public AudioClip Afterburner;
	
	void Start() {
		initialRot = transform.rotation;
		GetComponent<Rigidbody>().freezeRotation = true;
	}
	
	void FixedUpdate() {
	/*	velocity = GetComponent<Rigidbody>().velocity;
		//Debug.Log(Input.GetAxis("Mouse X") * -5);
		if (Input.GetAxis("Mouse X") != 0) {
			transform.Rotate(0, 0, Input.GetAxis("Mouse X") * -5, Space.Self);
		}
		
		//Debug.Log(Input.GetAxis("Mouse Y") * -5);
		if (Input.GetAxis("Mouse Y") != 0) {
			transform.Rotate(Input.GetAxis("Mouse Y") * -5, 0, 0);
		}
*/		
		if (Input.GetKey (KeyCode.W)) {
			if(GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
			{
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
			}
			GetComponent<Rigidbody>().AddRelativeForce (Vector3.forward * 2);
		}
		
		if (Input.GetKey(KeyCode.S)) {
			this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 2);	
		}
		
		if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(Vector3.down, turnSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.D)) {
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
		}
		
		if (Input.GetKey(KeyCode.Q)) {
			GetComponent<Rigidbody> ().AddRelativeForce(Vector3.left * 3);
		}
		
		if (Input.GetKey(KeyCode.E)) {
			GetComponent<Rigidbody> ().AddRelativeForce(Vector3.right * 3);
		}
		if (Input.GetKey (KeyCode.Z)) {
			transform.Rotate(0, 0, turnSpeed*Time.deltaTime, Space.Self);
		}
		
		if (Input.GetKey (KeyCode.C)) {
			transform.Rotate(0, 0, -turnSpeed*Time.deltaTime, Space.Self);
		}
		
		if (Input.GetKey (KeyCode.Space)) {
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 0.98f;
			
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero * 0.95f;
		}

		if (Input.GetKey (KeyCode.X)) {

		//	AudioSource audio = GetComponent<AudioSource>();
		//	audio.Play();

			if(GetComponent<Rigidbody>().velocity.magnitude > maxSpeedThrust)
			{
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeedThrust;
			}
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 1.15f;
		}
//		if (Input.GetKeyUp(KeyCode.X)) {
//			AudioSource audio = GetComponent<AudioSource>();
//			audio.Stop();
//		}
	
	}	
}

