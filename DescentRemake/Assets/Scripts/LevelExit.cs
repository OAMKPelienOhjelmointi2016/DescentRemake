using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
