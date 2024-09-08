using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using System.Text;

/// <summary>
/// Class with all the user score. Has a Name and a Score.
/// </summary>
public class PlayerInfo : IComparable<PlayerInfo>
{
    /// <summary>
    /// Username
    /// </summary>
    public string name;
    /// <summary>
    /// Score of the games.
    /// </summary>
    public int score;
    /// <summary>
    /// Parameterized constructors.
    /// </summary>
    /// <param name="name">Username</param>
    /// <param name="score">Score of the game</param>
    public PlayerInfo(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
    /// <summary>
    /// Descending order by score.
    /// </summary>
    public int CompareTo(PlayerInfo other)
    {
        // Descending order by score, if equals, will go with alphanumerical order.
        if (score == other.score)
        {
            return name.CompareTo(other.name);
        }
        return other.score.CompareTo(score);
    }
    /// <summary>
    /// To string override.
    /// </summary>
    /// <returns>name;score</returns>
    public override string ToString()
    {
        return name + ';' + score;
    }
}
/// <summary>
/// Script that manages all the data of the leaderboard.
/// </summary>
public class LeaderboardScript : MonoBehaviour
{
    /// <summary>
    /// Ordered score list.
    /// </summary>
    private SortedSet<PlayerInfo> collectedStats =  new SortedSet<PlayerInfo>();
    /// <summary>
    /// The coordinates of the box with the leaderboard.
    /// </summary>
    [SerializeField] private Transform _entryDisplayParent;
    /// <summary>
    /// The script that manages all the visualization of the leaderboard.
    /// </summary>
    [SerializeField] private LeaderBoardDisplayScript _entryDisplayPrefab;

    /// <summary>
    /// Function called at the end of a game to add a score.
    /// </summary>
    /// <param name="score">Integer value of the last game score.</param>
    public void SubmitUser(int score)
    {
        ImportLeatherBoard();
        PlayerInfo stats = new PlayerInfo(PlayerPrefs.GetString("User", "default"), score);
        collectedStats.Add(stats);
        ExportLeaderBoard();
        OnLeaderboardLoaded(collectedStats.ToList());
    }

    /// <summary>
    /// Load the leaderboard in the scene.
    /// </summary>
    /// <param name="collectedStats">Ordered score list with the level.</param>
    private void OnLeaderboardLoaded(List<PlayerInfo> collectedStats)
    {
        foreach (Transform t in _entryDisplayParent) 
            Destroy(t.gameObject);
        int rank = 1;
        foreach (var t in collectedStats)
        {
            CreateEntryDisplay(t, rank);
            rank++;
        }
    }
    /// <summary>
    /// Creates a leaderboard prefab with the actual data and rank.
    /// </summary>
    /// <param name="entry">Name and score of the player.</param>
    /// <param name="rank">Rank assigned to the player.</param>
    private void CreateEntryDisplay(PlayerInfo entry, int rank)
    {
        var entryDisplay = Instantiate(_entryDisplayPrefab.gameObject, _entryDisplayParent);
        entryDisplay.GetComponent<LeaderBoardDisplayScript>().SetEntry(entry, rank, false);
    }

    /// <summary>
    /// Loads the data from the saved leaderboards. Read the actual gamemode from the <see cref="LetterManagerScript">LetterManagerScript</see>.
    /// and then loads the correct data.
    /// </summary>
    private void ImportLeatherBoard()
    {
        collectedStats = new SortedSet<PlayerInfo>();
        string route;
        if (LetterManagerScript.randomMode == true)
        {
            route = "./Assets/LeaderBoard/random-" + LetterManagerScript.dificulty.ToString() + ".csv";
        }
        else
        {
            string levelPath = LetterManagerScript.levelPath;
            string level = levelPath.Split('/').Last().Split('.').First();
            route = "./Assets/LeaderBoard/level-" + level + ".csv";
        }
        try
        {
            foreach (string line in File.ReadLines(route, Encoding.UTF8))
            {
                string[] words = line.Split(';');
                PlayerInfo player = new PlayerInfo(words[0], Int32.Parse(words[1]));
                collectedStats.Add(player);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("No existe el documento");
        }

    }

    /// <summary>
    /// Only used in the MainMenu scene to load the data to the Random submenu, also load the leaderboard in the scene. 
    /// Has to indicate the difficulty.
    /// </summary>
    /// <param name="difficulty">Value from 0 to 2. According to <see cref="Difficulty"/>Difficulty</param>
    public void ImportLeatherBoardRandom(int difficulty)
    {
        collectedStats = new SortedSet<PlayerInfo>();
        string route;
        Difficulty aux = (Difficulty)difficulty;
        route = "./Assets/LeaderBoard/random-" + aux.ToString() + ".csv";
        try
        {
            foreach (string line in File.ReadLines(route, Encoding.UTF8))
            {
                string[] words = line.Split(';');
                PlayerInfo player = new PlayerInfo(words[0], Int32.Parse(words[1]));
                collectedStats.Add(player);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("No existe el documento");
        }
        OnLeaderboardLoaded(collectedStats.ToList());
    }
    /// <summary>
    /// Only used in the MainMenu scene to load the data to the Level submenu, also load the leaderboard in the scene. 
    /// Has to indicate the level.
    /// </summary>
    /// <param name="level">Level name</param>
    public List<PlayerInfo> ImportLeatherBoardLevel(string level)
    {
        collectedStats = new SortedSet<PlayerInfo>();
        string route;
        route = "./Assets/LeaderBoard/level-" + level + ".csv";
        try
        {
            foreach (string line in File.ReadLines(route, Encoding.UTF8))
            {
                string[] words = line.Split(';');
                PlayerInfo player = new PlayerInfo(words[0], Int32.Parse(words[1]));
                collectedStats.Add(player);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("No existe el documento");
        }
        List<PlayerInfo> returnList = collectedStats.ToList();
        OnLeaderboardLoaded(returnList);
        return returnList;
    }

    /// <summary>
    /// Export the data from the actual leaderboards updated. Read the actual gamemode from the 
    /// <see cref="LetterManagerScript">LetterManagerScript</see>.
    /// </summary>
    private void ExportLeaderBoard()
    {
        string route;
        if (LetterManagerScript.randomMode == true)
        {
            route = "./Assets/LeaderBoard/random-" + LetterManagerScript.dificulty.ToString() + ".csv";
        }
        else
        {
            String levelPath = LetterManagerScript.levelPath;
            string level = levelPath.Split('/').Last().Split('.').First();
            route = "./Assets/LeaderBoard/level-" + level + ".csv";
        }
        using (StreamWriter writer = new StreamWriter(route))
        {
            foreach (PlayerInfo player in collectedStats)
                writer.WriteLine(player.ToString());
        }
    }

}
