using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public ParticleSystem[] bombEffects;
   // public Dropdown bombDropDown;
   // public string currentBombName;
    public int currentIndexOfBomb;
    public bool playerTurn;
    public Sprite[] bombSprites;
    public List<GameObject> player = new List<GameObject>();
    float maxTurnTime = 10;
    public float timeLeft;
    int currentPlayer = 0;
    int nextPlayer = 0;
    public bool hasShoot;
    public Image timerImage;
    public Text turnTimerText;
    public bool isGameOver;
   // List<string> bombNamesList = new List<string>();

    private void Awake()
    {
        instance = this;
        player[currentPlayer].GetComponent<TanksMovement>().isMyTurn = true;
        timeLeft = maxTurnTime;
    }
    // Use this for initialization
    //void Start () {
    //GetBombTypes();
    //AddBombToDropDown();
    //
    //}
    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("GamePlay");
            }
            return;
        }

        timeLeft -= Time.deltaTime;



        if (timeLeft < 0 && !hasShoot)
        {
            SwitchTurn();
        }
        turnTimerText.text =  ((int)(timeLeft+1f)).ToString();
        timerImage.fillAmount = timeLeft / maxTurnTime;
        //Debug.Log("TimeLeft" + timeLeft);


      


    }

   public void SwitchTurn()
    {
        
        timeLeft = maxTurnTime;
        hasShoot = false;
        player[currentPlayer].GetComponent<TanksMovement>().isMyTurn = false;

        nextPlayer = ((currentPlayer + 1 )> (player.Count - 1)) ? 0 : currentPlayer + 1;
        player[nextPlayer].GetComponent<TanksMovement>().isMyTurn = true;

        currentPlayer = nextPlayer;
        
        timerImage.color = ((currentPlayer + 1) > (player.Count - 1)) ? Color.red : Color.green;
    }
    //void AddBombToDropDown()
    //{
    //    bombDropDown.ClearOptions();
    //    //Add the options created in the List above
    //    bombDropDown.AddOptions(bombNamesList);
    //}

    //void GetBombTypes()
    //{
    //    string[] bombNamesArr = System.Enum.GetNames(typeof(BombTypes));
    //    for (int i = 0; i < bombNamesArr.Length; i++)
    //    {
    //       bombNamesList.Add(bombNamesArr[i]);
    //    }
    //}



    public void OnBombSelection(int index)
    {
        //currentBombName = obj.name;
        // Debug.Log("Current Bomb = " + currentBombName);
        currentIndexOfBomb = index;//bombDropDown.value;
    }
	
	
}
