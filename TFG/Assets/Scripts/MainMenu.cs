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
    //[SerializeField] ToggleGroup dificultyToogle;
    // Start is called before the first frame update
    void Start()
    {
        field.text = PlayerPrefs.GetString("User", "default");
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
}
