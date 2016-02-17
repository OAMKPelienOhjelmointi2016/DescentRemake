using UnityEngine;
using System.Collections;

public class BulletHitEffect : MonoBehaviour
{

    void Start()
    {
        GameObject.Destroy(this.gameObject, .05f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
