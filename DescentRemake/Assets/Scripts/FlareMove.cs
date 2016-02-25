using UnityEngine;
using System.Collections;

public class FlareMove : MonoBehaviour {
    private Vector3 direction;
    private float speed;
    // Use this for initialization
    void Start () {
        direction = this.transform.forward;
        speed = 350f;
        GameObject.Destroy(this.gameObject, 60f);
        this.GetComponent<Rigidbody>().AddForce(direction * speed);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
