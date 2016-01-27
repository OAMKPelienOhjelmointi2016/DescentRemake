using UnityEngine;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization
	public Camera standbyCamera;
	public float respawnTimer = 0;
    bool connecting = false;
	SpawnSpot[] spawnSpots;

    List<string> chatMessages;
    int maxChatMessages = 10;

	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot> ();
        PhotonNetwork.player.name = PlayerPrefs.GetString("Username", "Awesome player");
        chatMessages = new List<string>();

	}

    void OnDestroy()
    {
        PlayerPrefs.SetString("Username", PhotonNetwork.player.name);
       /* Hashtable props = new Hashtable();
        props["asddsaddsa"] = 2;
        PhotonNetwork.player.SetCustomProperties(props);*/
    }

    public void AddChatMessage(string m)
    {
        if (chatMessages.Count >= maxChatMessages)
        {
            chatMessages.RemoveAt(0);
        }
        chatMessages.Add(m);
    }
	
	void Connect(){
		//PhotonNetwork.offlineMode = true;
		PhotonNetwork.ConnectUsingSettings ("DescentRemake");
        

	}
	void Update(){
		if (respawnTimer > 0) {
			respawnTimer -= Time.deltaTime;
			
			if (respawnTimer <= 0) {
				SpawnMyPlayer ();
			}
		}
	}

	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());

        if (PhotonNetwork.connected == false && connecting == false )
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Username: ");
            PhotonNetwork.player.name = GUILayout.TextField(PhotonNetwork.player.name);
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Multiplayer"))
            {
                Connect();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        if (PhotonNetwork.connected == true && connecting == false)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            foreach(string msg in chatMessages){
                GUILayout.Label(msg);
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
	}
	void OnJoinedLobby(){
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
	}

	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
        connecting = false;
		SpawnMyPlayer ();
	}

	void SpawnMyPlayer(){
        AddChatMessage("Spawning player: " + PhotonNetwork.player.name);
        
		if (spawnSpots == null) {
			Debug.LogError ("Broken");
			return;
		}
		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];
		GameObject myPlayerGO = (GameObject)
		PhotonNetwork.Instantiate ("Player", mySpawnSpot.transform.position, transform.rotation, 0);
		//came.enabled = false;
		myPlayerGO.GetComponent<NetworkCharacterMovement> ().enabled = true;
        myPlayerGO.GetComponent<PlayerMovement> ().enabled = true;
        myPlayerGO.AddComponent<Camera>();
        //Instantiate(gameObject.AddComponent<Camera>(), transform.position, Quaternion.identity) as Camera;

        //myPlayerGO.GetComponent<PlayerControl> ().enabled = true;
        //myPlayerGO.GetComponentInChildren<PlayerShooting> ().enabled = true;
        //myPlayerGO.GetComponentInChildren<PlayerHealth> ().enabled = true;

        //GameObject.GetComponent<CameraFollow> ().enabled = true;
        //myPlayerGO.GetComponentInChildren<CameraFollow>().enabled = true;


    }

}
