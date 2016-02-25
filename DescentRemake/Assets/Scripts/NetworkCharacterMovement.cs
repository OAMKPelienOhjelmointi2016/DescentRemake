using UnityEngine;
using System.Collections;

public class NetworkCharacterMovement : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
    Vector3 realVelocity = Vector3.zero;
	private string url = "http://oamkpo2016.esy.es/kills";
    private string url2 = "http://oamkpo2016.esy.es/shots";
    private bool respawned = true;

	// Use this for initialization
	void Start () {
		//respawned = false;
	}
	// Update is called once per frame
	void Update () {
		if (GetComponent<HealthShield> ().health <= 0) {
			if (!respawned) {
				GetComponent<PhotonView> ().RPC ("respawn", PhotonTargets.AllBuffered);
			}
			respawned = false;
		}
		if (photonView.isMine) {
		} 
		else {
            transform.position = Vector3.Lerp(transform.position, realPosition, 5 * Time.deltaTime);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 5 * Time.deltaTime);
            transform.GetComponent<Rigidbody>().velocity = realVelocity;
		}

	}
	[PunRPC]
	private void respawn() {

		StartCoroutine (PostDeath ());
		Destroy (GetComponent<HealthShield> ());
		gameObject.AddComponent <HealthShield> ();
		Vector3 respawnSpot = GameObject.Find ("_Scripts").GetComponent<NetworkManager> ().Respawn ();
		this.transform.position = respawnSpot;
		GetComponent<FiringWeapons> ().killCount = 0;
		respawned = true;
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.isWriting){
			//our player

			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
            stream.SendNext(transform.GetComponent<Rigidbody>().velocity);

		}
		else{
			//someones player
			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();
            realVelocity = (Vector3)stream.ReceiveNext();
		}
	}

	IEnumerator PostDeath() {
        Debug.Log("Osumia: " + GetComponent<FiringWeapons>().hitCount);
        WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("ID", GetComponent<ChatManager> ().id.ToString ());
		wwwForm.AddField ("killed", "1");
        wwwForm.AddField("hits", GetComponent<FiringWeapons>().hitCount);
        GetComponent<FiringWeapons>().hitCount = 0;
		WWW hs_post = new WWW (url, wwwForm);
		yield return hs_post;
		if (hs_post.error != null) {
			Debug.Log ("Error posting data to database: " + hs_post.error);
		}

        WWWForm wwwForm2 = new WWWForm();
        wwwForm2.AddField("ID", GetComponent<ChatManager>().id.ToString());
        wwwForm2.AddField("hits", GetComponent<FiringWeapons>().fireCount);
        GetComponent<FiringWeapons>().fireCount = 0;
        WWW hs_post2 = new WWW(url2, wwwForm);
        yield return hs_post2;
        if (hs_post2.error != null)
        {
            Debug.Log("Error posting data to database: " + hs_post2.error);
        }
    }


	
}
