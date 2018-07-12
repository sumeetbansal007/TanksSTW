using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
public class Timer : NetworkBehaviour {


    public TextMeshProUGUI Timerr;
    private float timeLeft;
  
    public List<GameObject> Players;
    private int Counter;
    private bool room;
    private bool search;
    private bool switchTurn;

    int numberOfPlayerJoined;
    // Use this for initialization
    void Start () {
       
        Counter = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (search) {
            foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {
                if (gameObj.name == "Tank(Clone)")
                {
                    numberOfPlayerJoined++;
                    gameObj.name = "Player_"+ numberOfPlayerJoined;
                    Debug.Log("Tanks");
                    Players.Add(gameObj);
                    
                   
                }
                print(Players.Count);
            }
            StartRound();
        }
        if (Players.Count == 2) {
            Debug.Log("Player count is >>" + Players.Count);
            search = false;
            room = true;
        }
      
        if (Players.Count ==2) {
          
            
        }
        if (room) {
            timeLeft = timeLeft - Time.deltaTime;
            Timerr.text = "" + timeLeft.ToString();
            if (timeLeft < 1)
            {
                Debug.Log("Time over");

            }

        }
    }
    void StartRound() {
        Debug.Log(Players[0].name);
        Debug.Log(Players[1].name);
        InvokeRepeating("AlterTurn",0,10.0f );
    }
    void AlterTurn() {
        switchTurn =! switchTurn;
        if (switchTurn)
        {
            Players[0].GetComponent<TanksMovement>().enabled = true;
            Players[1].GetComponent<TanksMovement>().enabled = false;
            timeLeft = 10.0f;

        }
        else {
            Players[0].GetComponent<TanksMovement>().enabled = false;
            Players[1].GetComponent<TanksMovement>().enabled = true;
            timeLeft = 10.0f;

        }


    }
    public void UpdateTimeDisplay()
    {
        Counter++;
        if(Counter == 2)
        {
            room = true;
            search = true;
        }
 
    }

    }
