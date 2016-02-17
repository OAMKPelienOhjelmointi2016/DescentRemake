using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseMovement : MonoBehaviour {

	public float horizontalSpeed = 4.0f;
	public float verticalSpeed = 4.0f;
	float h;
	float v;

	void FixedUpdate ()
	{
		h = horizontalSpeed * Input.GetAxis ("Mouse X");
		v = verticalSpeed * Input.GetAxis ("Mouse Y");
		transform.Rotate (-v, h, 0);
	}
}