using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    public GameObject door;
    int health = 3;

	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "BulletPrefab(Clone)")
        {
            health--;
            if (health == 0)
            {
                Destroy(this.gameObject);
                door.GetComponent<Animator>().Play("DoorMove");
            }
        }else if (col.gameObject.name == "MissilePrefab(Clone)")
        {
            health = 0;
            Destroy(this.gameObject);
            door.GetComponent<Animator>().Play("DoorMove");
        }
    }
}
