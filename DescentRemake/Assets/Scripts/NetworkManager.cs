using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NetworkManager : MonoBehaviour
{

    // Use this for initialization
    public Camera standbyCamera;
    public float respawnTimer = 0;
    bool connecting = false;
    SpawnSpot[] spawnSpots;
    Camera[] cameras;

    List<string> chatMessages;
    int maxChatMessages = 10;

    private Text NameText;
    private InputField NameInput;
    private Button ConnectMP;



    public void ConnectMPButton()
    {
        Connect();

    }
    void Start()
    {
        spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
        PhotonNetwork.player.name = PlayerPrefs.GetString("Username", "Awesome player");
        chatMessages = new List<string>();

        NameText = GameObject.Find("NameText").GetComponent<Text>();
        NameInput = GameObject.Find("NameInput").GetComponent<InputField>();
        ConnectMP = GameObject.Find("ConnectMP").GetComponent<Button>();
 



    }

    void OnDestroy()
    {
        PlayerPrefs.SetString("Username", PhotonNetwork.player.name);

    }
    public void AddChatMessage(string m)
    {

      // GetComponent<PhotonView>().RPC("AddChatMessage_PunRPC", PhotonTargets.AllBuffered, m);


    }

    [PunRPC]
    void AddChatMessage_PunRPC(string m)
    {
        if (chatMessages.Count >= maxChatMessages)
        {
            chatMessages.RemoveAt(0);
        }
        chatMessages.Add(m);
    }

    void Connect()
    {
        //PhotonNetwork.offlineMode = true;
        PhotonNetwork.ConnectUsingSettings("DescentRemake");

        NameInput.enabled = false;
        NameInput.GetComponent<CanvasGroup>().alpha = 0;

        NameText.enabled = false;
        NameText.GetComponent<CanvasGroup>().alpha = 0;

        ConnectMP.enabled = false;
        ConnectMP.GetComponent<CanvasGroup>().alpha = 0;



    }
    void Update()
    {
        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;

            if (respawnTimer <= 0)
            {
                SpawnMyPlayer();
            }
        }
    }

    void OnGUI()
    {
        //GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.connected == false && connecting == false)
        {

            PhotonNetwork.player.name = NameInput.text;
            if (NameInput.text.Length > 0)
            {
                ConnectMP.enabled = true;


                if (Input.GetKeyDown(KeyCode.Return))
                {
                    ConnectMPButton();
                }

                 

            }
            else
                ConnectMP.enabled = false;


        }

        if (NameInput.enabled)
        {
            NameInput.Select();
            NameInput.ActivateInputField();
        }



    }
    void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        connecting = false;
        SpawnMyPlayer();
    }

    void SpawnMyPlayer()
    {



        if (spawnSpots == null)
        {
            Debug.LogError("Broken");
            return;
        }

        
        SpawnSpot mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
        GameObject myPlayerGO = (GameObject)
        PhotonNetwork.Instantiate("Player", mySpawnSpot.transform.position, transform.rotation, 0);
        myPlayerGO.GetComponent<NetworkCharacterMovement>().enabled = true;
        myPlayerGO.GetComponent<MouseMovement>().enabled = true;
        myPlayerGO.GetComponent<ChatManager>().enabled = true;

        myPlayerGO.GetComponent<PlayerMovement>().enabled = true;

        cameras = myPlayerGO.GetComponentsInChildren<Camera>();
        foreach (Camera child in cameras)
        {
            child.enabled = true;
        }
        myPlayerGO.GetComponent<PlayerShoot>().enabled = true;
        myPlayerGO.GetComponent<FiringWeapons>().enabled = true;
        myPlayerGO.GetComponent<TrackFiringScript>().enabled = true;
        myPlayerGO.GetComponentInChildren<AudioListener>().enabled = true;
        myPlayerGO.GetComponent<HealthShield>().enabled = true;
       // myPlayerGO.GetComponent<UIController>().enabled = true;


        //myPlayerGO.GetComponentInChildren<> ().enabled = true;



    }






}