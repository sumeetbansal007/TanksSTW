using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UIManager : MonoBehaviour {
    
    public void LoadScene() {
        SceneManager.LoadScene("GamePlay");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadPauseScene()
    {
        SceneManager.LoadScene("PauseScreen");
    }

    public void QuitGame() {
        EditorApplication.isPlaying = false;
            
    }
	void Update () {
        
    }
}
