using UnityEngine;
using System.Collections;

public class EMPFieldFunc : MonoBehaviour {

    private Vector3 objectscalevector;
    private Vector2 texturescalevector;
    private float radius = 8.0f;
    private float power = 5.0f;

    // Use this for initialization
    void Start () {
        objectscalevector = new Vector3(0.4f, 0.4f, 0.4f);
        texturescalevector = new Vector2(0.25f, 0.25f);
        GameObject.Destroy(this.gameObject, 0.3f);
    }
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.localScale += objectscalevector;
        this.gameObject.GetComponent<Renderer>().material.mainTextureScale += texturescalevector;
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Bullet")
        {
            Vector3 explosionPos = this.transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0f, ForceMode.Force);
            }
        }
    }
}
