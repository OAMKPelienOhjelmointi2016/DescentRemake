using UnityEngine;
using System.Collections;

public class NetworkCharacterMovement : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
    Vector3 realVelocity = Vector3.zero;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
		} 
		else {
            transform.position = Vector3.Lerp(transform.position, realPosition, 5 * Time.deltaTime);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 5 * Time.deltaTime);
            transform.GetComponent<Rigidbody>().velocity = realVelocity;
		}

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
	
}
