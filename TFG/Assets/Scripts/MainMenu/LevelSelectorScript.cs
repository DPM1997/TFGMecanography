using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// Manages all the world system and levels from the Level sub-menu.
/// </summary>
public class LevelSelectorScript : MonoBehaviour
{
    /// <summary>
    /// Matrix where all the worlds are Saved.
    /// </summary>
    private Dictionary<string, List<string>> worlds;
    /// <summary>
    /// List or levels in the world.
    /// </summary>
    private List<string> worldsList;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text[] worldsText;
    [SerializeField] private LeaderboardScript leaderboard;
    [SerializeField] private MainMenuScript menu;
    [SerializeField] private TMP_Text durationValue, scoreValue;
    /// <summary>
    /// Load all the worlds dictionary and then load the metadata of the level
    /// </summary>
    private void Start()
    {
        LoadData();
        worldsList = new List<string>(worlds.Keys);
        //Load text in the submenu 
        worldsText[0].text = worldsList[worldsList.Count - 1];
        worldsText[1].text = worldsList[0];
        if (worldsList.Count != 1)
            worldsText[2].text = worldsList[1];
        else
            worldsText[2].text = worldsList[0];
        levelText.text = worlds[worldsText[1].text][0];
        loadLevelMetadata();
    }
    /// <summary>
    /// Load all the worlds dictionary.
    /// </summary>
    private void LoadData()
    {
        string folderPath = "./Assets/Levels";
        string[] levels = Directory.GetFiles(folderPath, "*.csv");
        worlds = new Dictionary<string, List<string>>();
        foreach (var level in levels)
        {
            // Divide the string by '-'.
            string fileName = Path.GetFileName(level);
            string[] parts = fileName.Split('-');
            string key = parts[0];
            string[] levelpath = parts[1].Split('.');
            string levelName = levelpath[0];

            // If the dictionary does not contain the key, add it with an empty list.
            if (!worlds.ContainsKey(key))
            {
                worlds[key] = new List<string>();
            }
            // Add the string to the corresponding list in the dictionary. 
            worlds[key].Add(levelName);
        }
    }

    /// <summary>
    /// Change the current world forward or backwards. Also load the first level of the world.
    /// </summary>
    /// <param name="forward">True for going fordward or right. False for going backwards of left.</param>
    public void changeWorld(bool forward)
    {
        if (forward)
        {
            int actualIndex = worldsList.IndexOf(worldsText[2].text);
            worldsText[0].text = worldsText[1].text;
            worldsText[1].text = worldsText[2].text;
            if (actualIndex == worldsList.Count - 1)
                worldsText[2].text = worldsList[0];
            else
                worldsText[2].text = worldsList[actualIndex + 1];
            levelText.text = worlds[worldsText[1].text][0];
        }
        else
        {
            int actualIndex = worldsList.IndexOf(worldsText[0].text);
            worldsText[2].text = worldsText[1].text;
            worldsText[1].text = worldsText[0].text;
            if (actualIndex == 0)
                worldsText[0].text = worldsList[worldsList.Count - 1];
            else
                worldsText[0].text = worldsList[actualIndex - 1];
            levelText.text = worlds[worldsText[1].text][0];
        }
        loadLevelMetadata();
    }

    /// <summary>
    /// Change the current level forward or backwards.
    /// </summary>
    /// <param name="forward">True for going fordward or right. False for going backwards of left.</param>
    public void changeLevel(bool forward)
    {
        List<string> levels = worlds[worldsText[1].text];
        int actualIndex = levels.IndexOf(levelText.text);
        if (forward)
            if (actualIndex == levels.Count - 1)
                levelText.text = levels[0];
            else
                levelText.text = levels[actualIndex + 1];
        else
            if (actualIndex == 0)
            levelText.text = levels[levels.Count - 1];
        else
            levelText.text = levels[actualIndex - 1];
        loadLevelMetadata();
    }

    /// <summary>
    /// Read the first line of the actual level and reeds the metadata from it. The level is in .csv format.
    /// </summary>
    private void loadLevelMetadata()
    {
        List<PlayerInfo> playerInfos = leaderboard.ImportLeatherBoardLevel(worldsText[1].text + '-' + levelText.text);
        string line = File.ReadLines("./Assets/Levels/" + worldsText[1].text + '-' + levelText.text + ".csv", Encoding.UTF8).First();
        string[] words = line.Split(';');
        durationValue.text = words[2];
        string user = PlayerPrefs.GetString("User", "default");
        foreach (PlayerInfo player in playerInfos)
            if (player.name == user)
            {
                scoreValue.text = player.score + "";
                break;
            }
    }

    /// <summary>
    /// Load the game with Level mode. Calls the MainMenuScript.
    /// </summary>
    public void loadLevel()
    {
        menu.levelMode("./Assets/Levels/" + worldsText[1].text + '-' + levelText.text + ".csv");
    }
}
