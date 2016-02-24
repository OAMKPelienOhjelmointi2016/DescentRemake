using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	public Text shieldText;
	public Text hullText;

	public Image missileIcon;
	public Text missileCountText;
	public Text missileTypeText;

	public GameObject shieldBar;
	public GameObject hullBar;

	public Image weaponIcon;
	public Text weaponText;

	public Text flareCountText;

	public Text decoyText;

	//These are for debug purposes. These will let you test within the Unity editor whether the UI-indicators work. Delete these later.
	//public int debugSetShields = 100; 
	//public int debugSetHull = 100;
	//public int debugSetMissiles = 5;

	void Start() {
		SetShields(100);
		SetHull(100);
	}

	public void SetFlares(int _amount) {
		int count = nonNegativeInt(_amount);
		if (flareCountText != null) {
			flareCountText.text = "x" + count;
		}
	}

	public void SetDecoyState(string _state) {
		//Whenever the decoy is ready for use, the text should say "READY".
		//When the decoy has been deployed, the text should say "DEPLOYED"
		//After the decoy has expired, the text should go back to "READY"

		if (decoyText != null) {
			decoyText.text = "DECOY:" + _state;
		}
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
			missileCountText.text = "x" + count;
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

	void Update() {
		//These let you test within Unity editor whether the indicators work. Delete these later.
		//SetShields(debugSetShields);
		//SetHull(debugSetHull);
		//SetMissileCount(debugSetMissiles);
	}
}
