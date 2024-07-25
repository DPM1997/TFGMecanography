using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using System.Text;


public class PlayerInfo : IComparable<PlayerInfo>
{
    public string name;
    public int score;

    public static string levelPath;

    public PlayerInfo(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public int CompareTo(PlayerInfo other)
    {
        // Comparar por puntuación, y si son iguales, comparar por nombre
        if (score == other.score)
        {
            return name.CompareTo(other.name);
        }
        return other.score.CompareTo(score); // Orden descendente por puntuación
    }

    public override string ToString()
    {
        return name + ';' + score;
    }
}
public class LeaderboardScript : MonoBehaviour
{
    SortedSet<PlayerInfo> collectedStats =  new SortedSet<PlayerInfo>();
    [SerializeField] private Transform _entryDisplayParent;
    [SerializeField] private LeaderBoardDisplayScript _entryDisplayPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SubmitUser(int score)
    {
        ImportLeatherBoard();
        PlayerInfo stats = new PlayerInfo(PlayerPrefs.GetString("User", "default"), score);
        collectedStats.Add(stats);
        ExportLeaderBoard();
        OnLeaderboardLoaded(collectedStats.ToList());
    }

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

    private void CreateEntryDisplay(PlayerInfo entry, int rank)
    {
        var entryDisplay = Instantiate(_entryDisplayPrefab.gameObject, _entryDisplayParent);
        entryDisplay.GetComponent<LeaderBoardDisplayScript>().SetEntry(entry, rank, false);
    }

    void ImportLeatherBoard()
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

    void ExportLeaderBoard()
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
