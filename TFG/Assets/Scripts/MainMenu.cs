using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void randomMode(){
        ScriptBajada.randomMode = true;
        SceneManager.LoadScene("TestingArea");
    }

    public void levelMode(string level){
        ScriptBajada.randomMode = false;
        ScriptBajada.levelPath = level;
        SceneManager.LoadScene("TestingArea");
    }
}
