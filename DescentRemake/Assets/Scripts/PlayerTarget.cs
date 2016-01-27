using UnityEngine;
using System.Collections.Generic;

public class PlayerTarget : MonoBehaviour {
    private static List<PlayerTarget> playerList = new List<PlayerTarget>();

    static public IEnumerable<PlayerTarget> Players
    {
        get { return playerList; }
    }

	// Use this for initialization
	void Start ()
    {
        playerList.Add(this);
	}

    void OnDestroy()
    {
        playerList.Remove(this);
    }
}
