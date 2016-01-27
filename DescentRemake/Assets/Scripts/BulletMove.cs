using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour {

    private Vector3 direction;
    private float speed;
    private float radius = 0.35f;
    private float power = 50.0f;

    // Use this for initialization
    void Start () {
        direction = this.transform.forward;
        speed = 75f;
        GameObject.Destroy(this.gameObject, 5f);
    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Rigidbody>().AddForce(direction * speed);
        this.transform.Rotate(0f, 0f, 10f,Space.Self);
	}

    void OnTriggerEnter(Collider col)
    {
        Vector3 explosionPos = this.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0f, ForceMode.Force);
        }
        Destroy(this.gameObject);
    }
}
