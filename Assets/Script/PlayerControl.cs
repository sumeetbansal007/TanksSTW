using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class PlayerControl : NetworkBehaviour {

	public float moveSpeed;
    private int totalPlayersCount = 0;
    //List<NetworkPlayer> players;
    // Use this for initialization
    void Start () {
        Debug.Log("Inside Start");
    }
    
    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
          
            return;
        }
        
       
        //Moves Forward and back along z axis                           //Up/Down
        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical")* moveSpeed);
		//Moves Left and right along x Axis                               //Left/Right
		transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal")* moveSpeed);      
	}
    public void AlterTurns() {
        Debug.Log("Alter");
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Debug.Log("Start");
     
       
    }
    public override void OnStartClient()
    {
        GameObject.Find("Canvas").GetComponent<Timer>().UpdateTimeDisplay();
        
    }
    }
