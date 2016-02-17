using UnityEngine;
using System.Collections;

public class FlareMove : MonoBehaviour {
    private Vector3 direction;
    //Projectile's speed
    [SerializeField]
    private float speed = 350f;
    private GameObject player;
    // Use this for initialization
    void Start () {
        direction = this.transform.forward;
        player = GameObject.FindGameObjectWithTag("Player");
        this.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity;
        GameObject.Destroy(this.gameObject, 60f);
        this.GetComponent<Rigidbody>().AddForce(direction * speed);
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>())
        {
            Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
