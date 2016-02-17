using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {

    GameObject tutorial;
    GUIText tutorialText;
    bool pressedPrimaryFire = false;
    bool pressedSecondaryFire = false;
    bool pressedForwards = false;
    bool pressedBackwards = false;
    bool pressedLeft = false;
    bool pressedRight = false;
    string text1, text2, text3, text4, text5, text6;

    void Start() {
        tutorial = GameObject.Find("Tutorial");
        tutorialText = tutorial.GetComponent<GUIText>();
        text1 = "<color=red>Mouse 1 - Primary Fire</color>\n";
        text2 = "<color=red>Mouse 2 - Secondary Fire</color>\n";
        text3 = "<color=red>W - Forwards</color>\n";
        text4 = "<color=red>S - Backwards</color>\n";
        text5 = "<color=red>A - Left</color>\n";
        text6 = "<color=red>D - Right</color>\n";
        tutorialText.text = text1 + text2 + text3 + text4 + text5 + text6;
    }

    void OnEnable()
    {
        StartCoroutine(tutorialCompletion());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !pressedPrimaryFire)
        {
            pressedPrimaryFire = true;
            text1 = "<color=green>Mouse 1 - Primary Fire</color>\n";
            tutorialText.text = text1 + text2 + text3 + text4 + text5 + text6;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && !pressedSecondaryFire)
        {
            pressedSecondaryFire = true;
            text2 = "<color=green>Mouse 2 - Secondary Fire</color>\n";
            tutorialText.text = text1 + text2 + text3 + text4 + text5 + text6;
        }
        else if (Input.GetKeyDown(KeyCode.W) && !pressedForwards) {
            pressedForwards = true;
            text3 = "<color=green>W - Forwards</color>\n";
            tutorialText.text = text1 + text2 + text3 + text4 + text5 + text6;
        }
        else if (Input.GetKeyDown(KeyCode.S) && !pressedBackwards)
        {
            pressedBackwards = true;
            text4 = "<color=green>S - Backwards</color>\n";
            tutorialText.text = text1 + text2 + text3 + text4 + text5 + text6;
        }
        else if (Input.GetKeyDown(KeyCode.A) && !pressedLeft)
        {
            pressedLeft = true;
            text5 = "<color=green>A - Left</color>\n";
            tutorialText.text = text1 + text2 + text3 + text4 + text5 + text6;
        }
        else if (Input.GetKeyDown(KeyCode.D) && !pressedRight)
        {
            pressedRight = true;
            text6 = "<color=green>D - Right</color>\n";
            tutorialText.text = text1 + text2 + text3 + text4 + text5 + text6;
        }
    }

    IEnumerator tutorialCompletion() {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (pressedPrimaryFire == true && pressedSecondaryFire == true && pressedForwards == true && pressedBackwards == true && pressedLeft == true && pressedRight == true)
            {
                yield return new WaitForSeconds(2f);
                tutorial.SetActive(false);
                break;
            }
        }
    }
}
