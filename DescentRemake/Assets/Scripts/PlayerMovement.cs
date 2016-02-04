using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	Quaternion initialRot;
	
	
	public int forwardSpeed = 5;
	public int turnSpeed = 80;
	public static Vector3 velocity;
	public int rotateSpeed = 80;
	public int maxSpeed = 6;
	public int maxSpeedThrust = 10;

	
	void Start() {
		
		initialRot = transform.rotation;
		GetComponent<Rigidbody>().freezeRotation = true;

		//StartCoroutine(turboBoostTimer());
		
	}

	IEnumerator turboBoostTimer(){ 

		StartCoroutine ("speedTurbo", 2.0F);
		yield return new WaitForSeconds(2);
		StopCoroutine ("speedTurbo");

	}
		
	IEnumerator speedTurbo()
	{
		while (true) {
			if (GetComponent<Rigidbody> ().velocity.magnitude > maxSpeedThrust) {
				GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.normalized * maxSpeedThrust;
			}
			GetComponent<Rigidbody> ().AddRelativeForce (Vector3.forward * 2);
			yield return null;
		}
	}

	void FixedUpdate() {
		
		if (Input.GetKey (KeyCode.W)) {
			
			if(GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
			{
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
			}
			GetComponent<Rigidbody>().AddRelativeForce (Vector3.forward * 2);
		}
		
		if (Input.GetKey(KeyCode.S)) {
			if(GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
			{
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
			}
			this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 2);	
		}
		
		if (Input.GetKey(KeyCode.Q)) {
			transform.Rotate(Vector3.down, turnSpeed * Time.deltaTime);
		}
		
		if (Input.GetKey(KeyCode.E)) {
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
		}
		
		if (Input.GetKey(KeyCode.A)) {
			if(GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
			{
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
			}
			GetComponent<Rigidbody> ().AddRelativeForce(Vector3.left * 3);
		}
		
		if (Input.GetKey(KeyCode.D)) {
			if(GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
			{
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
			}
			GetComponent<Rigidbody> ().AddRelativeForce(Vector3.right * 3);
		}
		
		if (Input.GetKey (KeyCode.Z)) {
			transform.Rotate(0, 0, turnSpeed * Time.deltaTime, Space.Self);
		}
		
		if (Input.GetKey (KeyCode.C)) {		
			transform.Rotate(0, 0, -turnSpeed * Time.deltaTime, Space.Self);
		}

		if (Input.GetKey (KeyCode.X)) {
						
			StartCoroutine(turboBoostTimer());
	/*		if (GetComponent<Rigidbody> ().velocity.magnitude > maxSpeedThrust) {
				GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.normalized * maxSpeedThrust;
									//turboBoost -= turboBoostDrain * Time.deltaTime;
			}
			GetComponent<Rigidbody> ().AddRelativeForce (Vector3.forward * 5);
			*/
		}

		if (Input.anyKey == false) {
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 0.96f;
			
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero * 0.95f;

		}
		
		}	
//	}
}