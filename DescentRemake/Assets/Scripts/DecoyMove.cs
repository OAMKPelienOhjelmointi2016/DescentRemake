using UnityEngine;
using System.Collections;

public class DecoyMove : MonoBehaviour
{
    private Vector3 direction;
    private float speed;

    void Start()
    {
        direction = this.transform.up;
        speed = 200f;
        GameObject.Destroy(this.gameObject, 10f);
        this.GetComponent<Rigidbody>().AddForce(direction * speed);
    }

    void Update()
    {

    }
}
