using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	//Some UI texts are text meshes. They will later all be converted into Ui-texts
	//Some icons are sprites. They will later be converted into UI-images.

	public TextMesh shieldText;
	public TextMesh hullText;

	public SpriteRenderer missileIcon;
	public TextMesh missileCountText;

	public GameObject shieldBar;
	public GameObject hullBar;

	public Image weaponIcon;
	public Text weaponText;

	//These are for debug purposes. These will let you test within the Unity editor whether the UI-indicators work. Delete these later.
	public int debugSetShields = 100; 
	public int debugSetHull = 100;
	public int debugSetMissiles = 5;

	//These enumerators will probably find a use later
	//enum MissileTypes { CONCUSSION, HOMING, SPLIT };
	//enum Meters { SHIELD, HEALTH };

	void Start() {
		SetShields(100);
		SetHull(100);
	}

	public void SetShields(float _percentage) {
		float percent = nonNegativeFloat(_percentage);

		if (shieldText != null) {
			shieldText.text = "SHIELD:" + percent + "%";
		}
		if (shieldBar != null) {
			float scale = percent / 100;
			shieldBar.transform.localScale = new Vector3(1, scale, 1);
		}
	}

	public void SetHull(float _percentage) {
		float percent = nonNegativeFloat(_percentage);

		if (hullText != null) {
			hullText.text = "HULL:" + percent + "%";
		}
		if (hullBar != null) {
			float scale = percent / 100;
			hullBar.transform.localScale = new Vector3(1, scale, 1);
		}
	}

	public void SetMissileCount(int _count) {
		int count = nonNegativeInt(_count);

		if (missileCountText != null) {
			missileCountText.text = "x"+ count;
		}
	}
		
	float nonNegativeFloat(float _floatNumber) {
		float number = _floatNumber;
		if (_floatNumber < 0) {
			number = 0;
		}
		return number;
	}

	int nonNegativeInt(int _intNumber) {
		int number = _intNumber;
		if (_intNumber < 0) {
			number = 0;
		}
		return number;
	}

	void Update () {
		//These let you test within Unity editor whether the indicators work. Delete these later.
		SetShields(debugSetShields);
		SetHull(debugSetHull);
		SetMissileCount(debugSetMissiles);
	}

	/*
	void Awake() {
		//Transform UIMain = monitorMain.transform.Find("UIMain");

		shieldText = UIMain.Find("ShieldText").GetComponent<TextMesh>();
		hullText = UIMain.Find("HullText").GetComponent<TextMesh>();

		shieldBar = UIMain.Find("ShieldBar").gameObject;
		hullBar = UIMain.Find("HullBar").gameObject;

		//Transform UIR1 = monitorMain.transform.Find("MonitorR1").transform.Find("UIR1");
		//missileCountText = UIR1.transform.Find("MissileCountText").GetComponent<TextMesh>();
	}
	*/
}
