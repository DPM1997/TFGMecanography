using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField field;
    [SerializeField] private SoundScript script;
    [SerializeField] GameObject randomMenu,levelMenu;

    private LevelSelectorScript levelSelector;
    //[SerializeField] ToggleGroup dificultyToogle;
    // Start is called before the first frame update
    void Start()
    {
        field.text = PlayerPrefs.GetString("User", "default");
        levelSelector = GetComponent<LevelSelectorScript>();
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
    public void enableLevelMenu(){
        levelMenu.SetActive(true);
    }

    public void randomMode()
    {
        LetterManagerScript.randomMode = true;
        SceneManager.LoadScene("GameZone");
    }

    public void levelMode(string level)
    {
        LetterManagerScript.randomMode = false;
        LetterManagerScript.levelPath = level;
        SceneManager.LoadScene("GameZone");
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
                LetterManagerScript.dificulty = Difficulty.Easy;
                break;
            case 1:
                LetterManagerScript.dificulty = Difficulty.Medium;
                break;
            case 2:
                LetterManagerScript.dificulty = Difficulty.Hard;
                break;
            default:
                LetterManagerScript.dificulty = Difficulty.Easy;
                break;

        }
    }

    public void setLanguage(int language)
    {
        switch (language)
        {
            case 0:
                LetterManagerScript.language = true;
                LetterManagerScript.english = true;
                break;
            case 1:
                LetterManagerScript.language = true;
                LetterManagerScript.english = false;
                break;
            case 2:
                LetterManagerScript.language = false;
                break;
            default:
                LetterManagerScript.language = false;
                break;

        }
    }
}
