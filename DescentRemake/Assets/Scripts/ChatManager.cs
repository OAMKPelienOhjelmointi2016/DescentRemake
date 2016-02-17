using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.Chat;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using LitJson;

public class ChatManager : MonoBehaviour, IChatClientListener
{


    private const string GeneralChannel = "general";
    private const string AppId = "ecdfc811-a1df-4dab-9e5d-e54fdb0276e3";
    private const string AppVersion = "1.0";

    public string url = "http://oamkpo2016.esy.es/login?";
    public int id;

    private ChatClient _chat;
    public string username = "";
    private string _chatText = "";
    //private string _privateText = "";
    private string _input = "";

    private bool _connected;
    private bool _escpressed;

    private Text ChatText;
    private InputField ChatInput;
    private Button DisconnectButton;
  
    ExitGames.Client.Photon.Chat.AuthenticationValues authValues = new ExitGames.Client.Photon.Chat.AuthenticationValues();

    public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
    {
        Debug.Log(message);
    }

    // Use this for initialization
    void Start()
    {
        ChatText = GameObject.Find("ChatText").GetComponent<Text>();
        ChatInput = GameObject.Find("ChatInput").GetComponent<InputField>();
        DisconnectButton = GameObject.Find("DisconnectButton").GetComponent<Button>();
        DontDestroyOnLoad(gameObject);
        Application.runInBackground = true;

        _chat = new ChatClient(this);
    }

    // Update is called once per frame
    void Update()
    {

        if (_chat != null)
            _chat.Service();

        _chat.Service();

        if (!_connected)
        {
            Connect();

        }
        else
        {

            ChatText.text = _chatText;

            _input = ChatInput.text;


            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (ChatInput.enabled)
                {

                    if (!string.IsNullOrEmpty(_input) && _input.Length > 0)
                    {
                        SendMessage(_input);
                    }

                    ChatInput.text = "";
                    ChatInput.enabled = false;
                    ChatInput.GetComponent<CanvasGroup>().alpha = 0;

                }
                else
                {
                    ChatInput.GetComponent<CanvasGroup>().alpha = 1;
                    ChatInput.enabled = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _escpressed = !_escpressed;
                
                
                if (_escpressed == true)
                {
                    DisconnectButton.GetComponent<CanvasGroup>().alpha = 1;
                    DisconnectButton.enabled = true;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        disconnectbutton();
                    }
                }
                else
                DisconnectButton.GetComponent<CanvasGroup>().alpha = 0;
                DisconnectButton.enabled = false;
            }         
        }
        
    }

    void OnGUI()
    {
        if (ChatInput.enabled)
        {
            ChatInput.Select();
            ChatInput.ActivateInputField();
        }
    }

    void OnApplicationQuit()
    {
        if (_chat != null)
            _chat.Disconnect();
    }

    private void Connect()
    {
        username = PhotonNetwork.player.name;
        authValues.UserId = username;
        authValues.AuthType = ExitGames.Client.Photon.Chat.CustomAuthenticationType.None;
        StartCoroutine(GetID(username));


        if (!_chat.Connect(AppId, AppVersion, this.authValues))
        {
            Debug.Log("Error connecting server");
            _chat.DebugOut = ExitGames.Client.Photon.DebugLevel.ALL;
        }
        else
        {
            _connected = true;
        }
        Debug.Log("Connected");
    }

    private void SendMessage(string message)
    {
        if (message.StartsWith("/"))
        {
            ParseCommand(message);
            return;
        }

        var  mas = message.Split(new[] { ':' });
        if (mas.Length == 2)
        {
            _chat.SendPrivateMessage(mas[0], mas[1]);
            return;
        }

        _chat.PublishMessage(GeneralChannel, message);
    }

    private void ParseCommand(string command)
    {
        switch (command.Remove(0, 1))
        {
            case "clear":
                {
                    _chatText = "";
                    _chat.PublicChannels[GeneralChannel].ClearMessages();
                    break;
                }
        }
    }

    public void OnDisconnected()
    {

        _connected = false;
    }

    public void OnConnected()
    {
        _connected = true;

        _chat.Subscribe(new[] { GeneralChannel }, -1);

       // _chat.SetFriendList(new[] { "admin" });
        _chat.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void OnChatStateChange(ChatState state)
    {

    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (channelName == GeneralChannel)
        {
            for (int i = 0; i < senders.Length; i++)
            {
                _chatText = senders[i] + ": " + messages[i] + "\r\n" + _chatText;
            }
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log("Private message! " + sender + ":" + message);
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach (var channel in channels)
        {
            _chat.PublishMessage(channel, "Connected.");
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        foreach (var channel in channels)
        {
            _chat.PublishMessage(channel, "Disconnected.");
        }
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log(string.Format("Friend {0} set status to {1}", user, status));
    }

    public void disconnectbutton()
    {

        SendMessage("Disconnected.");
        
        _connected = false;

    }

    IEnumerator GetID(string nName)
    {
        //yield return new WaitWhile(() => uName != "");
        string Get_url = new System.Net.WebClient().DownloadString(url + "nname=" + WWW.EscapeURL(nName));
        JsonData data = JsonMapper.ToObject(Get_url);
        id = int.Parse((string)data["Reply"][0]["ID"]);

      //  Debug.Log("ID: " + nName + " " + id);

        yield return Get_url; // Wait until the download is done
    }

    //*** Message all for kills
    public void killStreak(string killer, int killCount)
    {
        SendMessage(killer + " got a kill!");
        if (killCount % 5 == 0)
        {
            SendMessage(killer + " " + killCount + " kills in a row!!!");     
        }
    }

}


