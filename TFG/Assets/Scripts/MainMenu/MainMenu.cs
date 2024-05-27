using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField field;
    [SerializeField] private SoundScript script;
    [SerializeField] GameObject randomMenu;

    private LevelSelector levelSelector;
    //[SerializeField] ToggleGroup dificultyToogle;
    // Start is called before the first frame update
    void Start()
    {
        field.text = PlayerPrefs.GetString("User", "default");
        levelSelector = GetComponent<LevelSelector>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            levelSelector.changeLevel(false);
        if (Input.GetKeyDown(KeyCode.D))
            levelSelector.changeLevel(true);
        if (Input.GetKeyDown(KeyCode.Q))
            levelSelector.changeWorld(false);
        if (Input.GetKeyDown(KeyCode.E))
            levelSelector.changeWorld(true);

    }

    public void enableRandomMenu()
    {
        randomMenu.SetActive(true);
        //this.GetComponents<Leaderboard>
    }

    public void randomMode()
    {
        ScriptBajada.randomMode = true;
        SceneManager.LoadScene("TestingArea");
    }

    public void levelMode(string level)
    {
        ScriptBajada.randomMode = false;
        ScriptBajada.levelPath = level;
        SceneManager.LoadScene("TestingArea");
    }

    public void submitUser()
    {
        PlayerPrefs.SetString("User", field.text);
    }

    public void setDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                ScriptBajada.dificulty = Difficulty.Easy;
                break;
            case 1:
                ScriptBajada.dificulty = Difficulty.Medium;
                break;
            case 2:
                ScriptBajada.dificulty = Difficulty.Hard;
                break;
            default:
                ScriptBajada.dificulty = Difficulty.Easy;
                break;

        }
    }

    public void setLanguage(int language)
    {
        switch (language)
        {
            case 0:
                ScriptBajada.language = true;
                ScriptBajada.english = true;
                break;
            case 1:
                ScriptBajada.language = true;
                ScriptBajada.english = false;
                break;
            case 2:
                ScriptBajada.language = false;
                break;
            default:
                ScriptBajada.language = false;
                break;

        }
    }
}
