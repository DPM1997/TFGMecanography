using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Script that manages all the functionality in the Main menu.
/// </summary>
public class MainMenuScript : MonoBehaviour
{
    /// <summary>
    /// Field of the usermane.
    /// </summary>
    [SerializeField] private TMP_InputField usernameField;
    /// <summary>
    /// See <see cref="SoundScript"/>
    /// </summary>
    [SerializeField] private SoundScript soundScript;
    /// <summary>
    /// Other submenus.
    /// </summary>
    [SerializeField] GameObject randomMenu, levelMenu;
    /// <summary>
    /// See <see cref="LevelSelectorScript"/> 
    /// </summary>
    private LevelSelectorScript levelSelector;
    /// <summary>
    /// Write the username from the last username used. 
    /// </summary>
    private void Start()
    {
        usernameField.text = PlayerPrefs.GetString("User", "default");
        levelSelector = GetComponent<LevelSelectorScript>();
    }

    /// <summary>
    /// To move the worlds in the level submenu
    /// </summary>
    private void Update()
    {
        if (levelMenu.activeInHierarchy == true)
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
    }

    /// <summary>
    /// Show RandomMenu.
    /// </summary>
    public void enableRandomMenu()
    {
        randomMenu.SetActive(true);
    }
    /// <summary>
    /// Show LevelMenu.
    /// </summary>
    public void enableLevelMenu()
    {
        levelMenu.SetActive(true);
    }

    /// <summary>
    /// Load the game with Random mode.
    /// </summary>
    public void randomMode()
    {
        LetterManagerScript.randomMode = true;
        SceneManager.LoadScene("GameZone");
    }
    /// <summary>
    /// Load the game with Level mode.
    /// </summary>
    /// <param name="level">Path of the level to load.</param>
    public void levelMode(string level)
    {
        LetterManagerScript.randomMode = false;
        LetterManagerScript.levelPath = level;
        SceneManager.LoadScene("GameZone");
    }

    /// <summary>
    /// Saves the user form the InputField.
    /// </summary>
    public void submitUser()
    {
        PlayerPrefs.SetString("User", usernameField.text);
        levelSelector.loadLevelMetadata();
    }

    /// <summary>
    /// Changes between 3 difficulties from Easy to Hard. Called from a Toogle in Random Menu.
    /// </summary>
    /// <param name="difficulty">Value from 0 to 2. According to <see cref="Difficulty"/>Difficulty</see>.</param>
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

    /// <summary>
    /// Changes between English, Spanish o None dictionary in Random mode. Called from a Toogle in Random Menu.
    /// </summary>
    /// <param name="language">Values from 0 to 2. 0 to English, 1 to Spanish and 2 to None.</param>
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

    /// <summary>
    /// Function that exit the game
    /// </summary>
    public void exit(){
        Application.Quit();
    }
}
