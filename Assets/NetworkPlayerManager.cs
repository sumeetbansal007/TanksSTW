using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetworkPlayerManager : NetworkBehaviour {

    public bool playerIsLocal = false;
   // public static NetworkPlayerManager instance;
    // Update is called once per frame

   
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        playerIsLocal = true;
        Debug.Log("Start" + isLocalPlayer);
    }
    public override void OnStartClient()
    {
        //GameObject.Find("Canvas").GetComponent<Timer>().UpdateTimeDisplay();
        Debug.Log("Client Activated");
    }
}
