using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	Quaternion initialRot;
	
	public int forwardForce = 2;
	public int backwardForce = 2;
	public int leftForce = 2;
	public int rightForce = 2;
	public int upForce = 2;
	public int downForce = 2;
	
	public int forwardSpeed = 5;
	public int turnSpeed = 80;
	public static Vector3 velocity;
	public int rotateSpeed = 80;
	public int maxSpeed = 6;
	public int maxSpeedThrust = 10;
	
	//	Vector3 oldRotation;
	//	public Transform from;
	//	public Transform to;
	//	public float rotationSpeed = 0.1F;
	
	
	void Start() {
		
		initialRot = transform.rotation;
		GetComponent<Rigidbody> ().freezeRotation = true;
		
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
			GetComponent<Rigidbody> ().AddRelativeForce (Vector3.forward * forwardForce);
			yield return null;
		}
	}
	
	void Update() {
		
	}
	void FixedUpdate() {
		
		if (Input.GetKey (KeyCode.W)) {
			
			if(GetComponent<Rigidbody> ().velocity.magnitude > maxSpeed)
			{
				GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.normalized * maxSpeed;
			}
			GetComponent<Rigidbody> ().AddRelativeForce (Vector3.forward * forwardForce);
		}
		
		if (Input.GetKey(KeyCode.S)) {
			if(GetComponent<Rigidbody> ().velocity.magnitude > maxSpeed)
			{
				GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.normalized * maxSpeed;
			}
			this.GetComponent<Rigidbody> ().AddRelativeForce(Vector3.back * backwardForce);	
		}
		
		if (Input.GetKey(KeyCode.Q)) {
			transform.Rotate(Vector3.down, turnSpeed * Time.deltaTime);
		}
		
		if (Input.GetKey(KeyCode.E)) {
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
		}
		
		if (Input.GetKey(KeyCode.A)) {
			if(GetComponent<Rigidbody> ().velocity.magnitude > maxSpeed)
			{
				GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.normalized * maxSpeed;
			}
			GetComponent<Rigidbody> ().AddRelativeForce(Vector3.left * leftForce);
		}
		
		if (Input.GetKey(KeyCode.D)) {
			if(GetComponent<Rigidbody> ().velocity.magnitude > maxSpeed)
			{
				GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.normalized * maxSpeed;
			}
			GetComponent<Rigidbody> ().AddRelativeForce(Vector3.right * rightForce);
		}
		
		if (Input.GetKey (KeyCode.Z)) {
			transform.Rotate(Vector3.right, turnSpeed * Time.deltaTime);
			//transform.Rotate(0, 0, turnSpeed * Time.deltaTime, Space.Self);
		}
		
		if (Input.GetKey (KeyCode.X)) {
			transform.Rotate(0, 0, turnSpeed * Time.deltaTime, Space.Self);
		}
		
		if (Input.GetKey (KeyCode.C)) {
			transform.Rotate(0, 0, -turnSpeed * Time.deltaTime, Space.Self);
		}
		
		if (Input.GetKey (KeyCode.V)) {		
			transform.Rotate(Vector3.left, turnSpeed * Time.deltaTime);
			//transform.Rotate(0, 0, -turnSpeed * Time.deltaTime, Space.Self);
		}
		
		if (Input.GetKey(KeyCode.R)) {
			if(GetComponent<Rigidbody> ().velocity.magnitude > maxSpeed)
			{
				GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.normalized * maxSpeed;
			}
			GetComponent<Rigidbody> ().AddRelativeForce(Vector3.up * upForce);
		}
		
		if (Input.GetKey(KeyCode.F)) {
			if(GetComponent<Rigidbody> ().velocity.magnitude > maxSpeed)
			{
				GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.normalized * maxSpeed;
			}
			GetComponent<Rigidbody> ().AddRelativeForce(Vector3.down * downForce);
		}
		if (Input.GetKey (KeyCode.Space)) {
			
			StartCoroutine(turboBoostTimer());	
			
		}
		
		if (Input.anyKey == false) {
			GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity * 0.95f;
			
			GetComponent<Rigidbody> ().angularVelocity = Vector3.zero * 0.95f;
			
			//			GetComponent<Rigidbody> ().transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * rotationSpeed);
			//	rb.transform.rotation = Vector3.RotateTowards(0, 0, 0);
			
		}
		
	}	
	//	}
}