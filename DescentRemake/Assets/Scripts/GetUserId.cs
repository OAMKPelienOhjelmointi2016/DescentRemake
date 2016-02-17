using UnityEngine;
using System.Collections;
using LitJson;


public class GetUserId : MonoBehaviour {

    public string url = "http://oamkpo2016.esy.es/login?";
    private ChatManager chatManager;
    private string uName;
    public int id;
    private bool userNameFound = true;

	void Start () {

    }

    public void FindPlayer()
    {
        chatManager = GetComponent<ChatManager>();
        StartCoroutine(GetUserName());
        StartCoroutine(GetID());
    }

    void Update() {
	}

    IEnumerator GetID()
    {
        yield return new WaitWhile(() => uName != "");
        string Get_url = new System.Net.WebClient().DownloadString(url + "nname=" + WWW.EscapeURL(uName));
        JsonData data = JsonMapper.ToObject(Get_url);
        id = int.Parse((string)data["Reply"][0]["ID"]);
       



        yield return Get_url; // Wait until the download is done
    }

    IEnumerator GetUserName() {
        yield return new WaitUntil(() => (uName = chatManager.username) != "");
    }
}
