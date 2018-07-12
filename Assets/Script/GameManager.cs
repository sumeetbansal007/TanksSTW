using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public ParticleSystem[] bombEffects;
    public Dropdown bombDropDown;
    public string currentBombName;
    public int currentIndexOfBomb;
    public Sprite[] bombSprites;
    List<string> bombNamesList = new List<string>();

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        GetBombTypes();
        AddBombToDropDown();
	}

    void AddBombToDropDown()
    {
        bombDropDown.ClearOptions();
        //Add the options created in the List above
        bombDropDown.AddOptions(bombNamesList);
    }

    void GetBombTypes()
    {
        string[] bombNamesArr = System.Enum.GetNames(typeof(BombTypes));
        for (int i = 0; i < bombNamesArr.Length; i++)
        {
           bombNamesList.Add(bombNamesArr[i]);
        }
    }

    public void OnDropDownValueChanged()
    {
        currentBombName = bombDropDown.options[bombDropDown.value].text;
        Debug.Log("Current Bomb = "+ currentBombName);
        currentIndexOfBomb = bombDropDown.value;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
