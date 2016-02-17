using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Credits : MonoBehaviour {
    public Transform target;
    public Image fadeOut;
    public float speed;

    void Start()
    {
        fadeOut.canvasRenderer.SetAlpha(0f);
        StartCoroutine(endCredits());
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if(Input.GetKeyDown("space"))
        {
            StartCoroutine(skipCredits());   
        }
    }

    IEnumerator skipCredits()
    {
        fadeOut.CrossFadeAlpha(1f, 0.3f, true);
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel("Menu");
    }

    IEnumerator endCredits()
    {
        yield return new WaitForSeconds(20f);
        fadeOut.CrossFadeAlpha(1f, 3f, false);
        yield return new WaitForSeconds(4f);
        Application.LoadLevel("Menu");
    }
}
